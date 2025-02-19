using Xunit;

namespace Tax.Simulator.Tests;

public class SimulateurShould
{
    [Fact(DisplayName = "Calcul de l'impôt pour un célibataire")]
    public void CalculerImpotsAnnuel_Celibataire()
    {
        decimal valeurAttendue = 1515.25M;
        string situationFamiliale = "Célibataire";
        int salaireMensuelInvalide = -2000;
        int salaireMensuelValide = 2000;
        int salaireMensuelZero = 0;
        int salaireMensuelConjoint = 0;
        int nombreEnfants = 0;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjoint, nombreEnfants);
        
        var exception = Assert.Throws<ArgumentException>(() => 
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelInvalide, salaireMensuelConjoint, nombreEnfants));

        var exceptionSalaireZero = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelZero, salaireMensuelConjoint, nombreEnfants));

        Assert.Equal("Les salaires doivent être positifs.", exception.Message);
        Assert.IsType<ArgumentException>(exception);

        Assert.Equal("Les salaires doivent être positifs.", exceptionSalaireZero.Message);
        Assert.IsType<ArgumentException>(exceptionSalaireZero);

        Assert.Equal(valeurAttendue, resultatValide);
    }

    [Fact(DisplayName = "Calcul de l'impôt pour un couple marié ou pacsé")]
    public void CalculerImpotsAnnuel_CoupleMariePacse()
    {
        decimal valeurAttendue = 4043.90M;

        string situationFamiliale = "Marié/Pacsé";
        int salaireMensuelValide = 2500;
        int salaireMensuelConjointValide = 2000;
        int salaireMensuelConjointInvalide = -2500;
        int nombreEnfants = 0;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjointValide, nombreEnfants);

        var exception = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjointInvalide, nombreEnfants));

        Assert.Equal("Les salaires doivent être positifs.", exception.Message);
        Assert.IsType<ArgumentException>(exception);

        Assert.Equal(valeurAttendue, resultatValide);

    }

    [Fact(DisplayName = "Calcul de l'impôt pour un couple marié ou pacsé avec des enfants")]
    public void CalculerImpotsAnnuel_CoupleMariePacseAvecEnfants()
    {
        decimal valeurAttendue = 3983.37M;

        string situationFamiliale = "Marié/Pacsé";
        int salaireMensuel = 3000;
        int salaireMensuelConjoint = 3000;
        int nombreEnfantsValide = 3;
        int nombreEnfantsInvalide = -1;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfantsValide);

        var exception = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfantsInvalide));

        Assert.Equal("Le nombre d'enfants ne peut pas être négatif.", exception.Message);
        Assert.IsType<ArgumentException>(exception);

        Assert.Equal(valeurAttendue, resultatValide);

    }
}