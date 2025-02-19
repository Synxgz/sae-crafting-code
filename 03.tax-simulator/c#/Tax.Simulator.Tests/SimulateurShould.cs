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
        int salaireMensuelConjointInvalide = -2000;
        int nombreEnfantsValide = 3;
        int nombreEnfantsInvalide = -1;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfantsValide);

        var exception = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfantsInvalide));

        Assert.Equal("Le nombre d'enfants ne peut pas être négatif.", exception.Message);
        Assert.IsType<ArgumentException>(exception);

        exception = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjointInvalide, nombreEnfantsValide));

        Assert.Equal("Les salaires doivent être positifs.", exception.Message);
        Assert.IsType<ArgumentException>(exception);

        Assert.Equal(valeurAttendue, resultatValide);

    }

    [Fact(DisplayName = "Validation des entrées utilisateur")]
    public void CalculerImpotsAnnuel_ValidationEntreesUtilisateur()
    {
        // Cas 1: Situation familiale invalide
        string situationFamiliale = "Divorcé";
        int salaireMensuel = 2000;
        int salaireMensuelConjoint = 2000;
        int nombreEnfants = 1;

        var resultat = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        Assert.Equal("Situation familiale invalide.", resultat.Message);
        Assert.IsType<ArgumentException>(resultat);

        // Cas 2: Salaires mensuels négatifs
        situationFamiliale = "Célibataire";
        salaireMensuel = -3000;
        salaireMensuelConjoint = 2000;
        nombreEnfants = 1;

        resultat = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        Assert.Equal("Les salaires doivent être positifs.", resultat.Message);
        Assert.IsType<ArgumentException>(resultat);

        // Cas 3: Nombre d'enfants négatif
        situationFamiliale = "Marié/Pacsé";
        salaireMensuel = 3000;
        salaireMensuelConjoint = 2000;
        nombreEnfants = -1;

        resultat = Assert.Throws<ArgumentException>(() =>
        Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        Assert.Equal("Le nombre d'enfants ne peut pas être négatif.", resultat.Message);
        Assert.IsType<ArgumentException>(resultat);

        // Cas 4 : Cas de test 1
        decimal valeurAttendue = 1515.25M;

        situationFamiliale = "Célibataire";
        salaireMensuel = 2000;
        salaireMensuelConjoint = 0;
        nombreEnfants = 0;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        // Cas 5 : Cas de test 2
        valeurAttendue = 5843.9M;

        situationFamiliale = "Célibataire";
        salaireMensuel = 5000;
        salaireMensuelConjoint = 0;
        nombreEnfants = 2;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        // Cas 6 : Cas de test 3
        valeurAttendue = 4043.90M;

        situationFamiliale = "Marié/Pacsé";
        salaireMensuel = 2000;
        salaireMensuelConjoint = 2500;
        nombreEnfants = 0;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        // Cas 7 : Cas de test 4
        valeurAttendue = 3983.37M;

        situationFamiliale = "Marié/Pacsé";
        salaireMensuel = 3000;
        salaireMensuelConjoint = 3000;
        nombreEnfants = 3;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        // Cas 8 : Cas de test 5
        valeurAttendue = 5498.62M;

        situationFamiliale = "Marié/Pacsé";
        salaireMensuel = 4000;
        salaireMensuelConjoint = 4000;
        nombreEnfants = 5;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        // Cas 9 : Cas de test 6
        valeurAttendue = 20804.88M;

        situationFamiliale = "Marié/Pacsé";
        salaireMensuel = 8000;
        salaireMensuelConjoint = 2000;
        nombreEnfants = 1;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        // Cas 10 : Cas de test 7
        valeurAttendue = 11452679.96M;

        situationFamiliale = "Marié/Pacsé";
        salaireMensuel = 2000000;
        salaireMensuelConjoint = 10000;
        nombreEnfants = 3;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants));

        // Cas 11 : Cas de test 8
        valeurAttendue = 11358302.68M;

        situationFamiliale = "Célibataire";
        decimal salaireMensuelDecimal = 1978123.98M;
        salaireMensuelConjoint = 0;
        nombreEnfants = 0;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelDecimal, salaireMensuelConjoint, nombreEnfants));
    }


    [Fact(DisplayName = "Calcul de l'impôt pour un célibataire en dessous du seuil de 500 000")]
    public void CalculerImpotsAnnuel_CelibataireInferieurSeuil500000()
    {
        decimal valeurAttendue = 87308.56M;
        string situationFamiliale = "Célibataire";
        int salaireMensuelValide = 20000;
        int salaireMensuelConjoint = 0;
        int nombreEnfants = 0;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjoint, nombreEnfants);

        Assert.Equal(valeurAttendue, resultatValide);
    }


    [Fact(DisplayName = "Calcul de l'impôt pour un célibataire au dessus du seuil de 500 000")]
    public void CalculerImpotsAnnuel_CelibataireSuperieurSeuil500000()
    {
        decimal valeurAttendue = 223508.56M;
        string situationFamiliale = "Célibataire";
        int salaireMensuelValide = 45000;
        int salaireMensuelConjoint = 0;
        int nombreEnfants = 0;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjoint, nombreEnfants);

        Assert.Equal(valeurAttendue, resultatValide);
    }

    [Fact(DisplayName = "Calcul de l'impôt pour un couple au dessus du seuil de 500 000")]
    public void CalculerImpotsAnnuel_MarieSeuil500000()
    {
        decimal valeurAttendue = 234925.68M;
        string situationFamiliale = "Marié/Pacsé";
        int salaireMensuelValide = 30000;
        int salaireMensuelConjoint = 25000;
        int nombreEnfants = 2;

        decimal resultatValide = Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuelValide, salaireMensuelConjoint, nombreEnfants);

        Assert.Equal(valeurAttendue, resultatValide);
    }

    [Fact(DisplayName = "Calcul de l'impôt pour un celibataire avec un salaire mensuel conjoint supérieur à 0")]
    public void CalculerImpotsAnnuel_CelibataireAnomalie()
    {
        decimal valeurAttendue = 5843.9M;

        string situationFamiliale = "Célibataire";
        int salaireMensuel = 5000;
        int salaireMensuelConjointAnomalie = 1;
        int salaireMensuelConjointValide = 0;
        int nombreEnfants = 2;

        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjointAnomalie, nombreEnfants));
        Assert.Equal(valeurAttendue, Simulateur.CalculerImpotsAnnuel(situationFamiliale, salaireMensuel, salaireMensuelConjointValide, nombreEnfants));
    }

}