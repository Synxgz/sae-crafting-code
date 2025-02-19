using Xunit;

namespace Tax.Simulator.Tests;

public class SimulateurShould
{
    [Fact(DisplayName = "Calcul de l'imp�t pour un c�libataire")]
    public void CalculerImpotsAnnuel_Celibataire()
    {
        decimal valeurAttendue = 1515.25M;
        string situationFamiliale = "C�libataire";
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

        Assert.Equal("Les salaires doivent �tre positifs.", exception.Message);
        Assert.IsType<ArgumentException>(exception);

        Assert.Equal("Les salaires doivent �tre positifs.", exceptionSalaireZero.Message);
        Assert.IsType<ArgumentException>(exceptionSalaireZero);

        Assert.Equal(valeurAttendue, resultatValide);
    }


    [Fact(DisplayName = "Calcul de l'imp�t pour un couple mari� ou pacs�")]
    public void CalculerImpotsAnnuel_CoupleMariePacse()
    {
        decimal valeurAttendue = 4043.90M;

        string situationFamiliale = "Mari�/Pacs�";
        int salaireMensuelValide = 2500;
        int salaireMensuelConjointValide = 2000;
        int salaireMensuelConjointInvalide = -2500;
        int nombreEnfants = 0;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjointValide, nombreEnfants);

        var exception = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjointInvalide, nombreEnfants));

        Assert.Equal("Les salaires doivent �tre positifs.", exception.Message);
        Assert.IsType<ArgumentException>(exception);

        Assert.Equal(valeurAttendue, resultatValide);

    }
}