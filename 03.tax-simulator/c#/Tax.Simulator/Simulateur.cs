namespace Tax.Simulator;

/// <summary>
/// Simulateur d'impôts
/// </summary>
public static class Simulateur
{
    private static readonly decimal[] TranchesImposition = {10225m, 26070m, 74545m, 160336m}; // Plafonds des tranches
    private static readonly decimal[] TauxImposition = {0.0m, 0.11m, 0.30m, 0.41m, 0.45m}; // Taux correspondants

    /// <summary>
    /// Calcule l'impôt annuel en fonction de la situation familiale,
    /// du salaire mensuel,
    /// du salaire mensuel du conjoint
    /// et du nombre d'enfants
    /// </summary>
    /// <param name="situationFamiliale">situation familiale : Célibataire ou Marié/Pacsé</param>
    /// <param name="salaireMensuel">salaire mensuel >= 0</param>
    /// <param name="salaireMensuelConjoint">salaire mensuel du conjoint >= 0 si Marié/Pacsé</param>
    /// <param name="nombreEnfants">nombre d'enfants >= 0 </param>
    /// <returns>l'Impôt annuel</returns>
    public static decimal CalculerImpotsAnnuel(
        string situationFamiliale,
        decimal salaireMensuel,
        decimal salaireMensuelConjoint,
        int nombreEnfants)
    {
        Situation situation = new Situation(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants); 

        decimal impotParPart = CalculerImpotParPart(situation);

        return Math.Round(impotParPart * situation.PartsFiscales, 2);
    }

    private static decimal CalculerImpotParPart(Situation situation)
    {
        decimal revenuImposableParPart = situation.RevenuAnnuel / situation.PartsFiscales;
        decimal trancheSuperieure = TranchesImposition.FirstOrDefault(t => t > revenuImposableParPart,revenuImposableParPart);

        decimal impot = TranchesImposition
            // Pour chaque tranche, création d'un objet anonyme avec
            // la tranche actuelle, la précédente et le taux correspondant
            .Select((tranche, index) => new
            {
                Tranche = tranche,
                TranchePrecedente = index > 0 ? TranchesImposition[index - 1] : 0,
                Taux = TauxImposition[index]
            })
            // Tant que le revenu imposable par part est supérieur ou égal
            // à la tranche supérieure au revenu imposable par part
            .TakeWhile(x => x.Tranche <= trancheSuperieure)
            .Sum(x => (Math.Min(x.Tranche, revenuImposableParPart) - x.TranchePrecedente) * x.Taux);


        //Si le revenu imposable par part est supérieur à la dernière tranche
        if (TranchesImposition[^1] < revenuImposableParPart)
        {
            impot += (revenuImposableParPart - TranchesImposition[^1]) * TauxImposition[^1];
        }

        return impot;
    }

}