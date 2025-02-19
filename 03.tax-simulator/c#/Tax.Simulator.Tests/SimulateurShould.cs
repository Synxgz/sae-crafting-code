using FluentAssertions;
using Xunit;

namespace Tax.Simulator.Tests;

public class SimulateurShould
{
    [Fact(DisplayName = "Calcul de l'imp�t pour un c�libataire")]
    public void CalculerImpotsAnnuel_Celibataire()
    {
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

        Assert.Equal((decimal)1515.25, resultatValide);

    }
}