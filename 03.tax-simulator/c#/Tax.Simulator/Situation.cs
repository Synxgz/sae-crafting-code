using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax.Simulator
{
    /// <summary>
    /// Situation familiale
    /// </summary>
    public record Situation
    {
        const string CELIBATAIRE = "Célibataire";
        const string MARIE = "Marié/Pacsé";
        const decimal VALEUR_ENFANT = 0.5m;

        public bool EstCelibataire { get; }
        public decimal SalaireMensuel { get; }
        public decimal SalaireMensuelConjoint { get;  }
        public int NombreEnfants { get; }
        public decimal RevenuAnnuel => (SalaireMensuel + (EstCelibataire ? 0 : SalaireMensuelConjoint)) * 12;

        public decimal PartsFiscales 
        { 
            get
            {
                // Nombre de personnes dans le foyé
                decimal baseQuotient = EstCelibataire ? 1 : 2;

                decimal quotientEnfants = VALEUR_ENFANT * NombreEnfants;
                return baseQuotient + quotientEnfants;
            } 
        }

        /// <summary>
        /// Constructeur de la classe Situation
        /// </summary>
        /// <param name="situationFamiliale">situation familiale : Célibataire ou Marié/Pacsé</param>
        /// <param name="salaireMensuel">salaire mensuel >= 0</param>
        /// <param name="salaireMensuelConjoint">salaire mensuel du conjoint >= 0 si Marié/Pacsé</param>
        /// <param name="nombreEnfants">nombre d'enfants >= 0 </param>
        public Situation(string situationFamiliale, decimal salaireMensuel, decimal salaireMensuelConjoint, int nombreEnfants)
        {
            VerifierCondition(situationFamiliale, salaireMensuel, salaireMensuelConjoint, nombreEnfants);
            EstCelibataire = situationFamiliale == CELIBATAIRE;
            SalaireMensuel = salaireMensuel;
            SalaireMensuelConjoint = salaireMensuelConjoint;
            NombreEnfants = nombreEnfants;
        }


        private void VerifierCondition(string situationFamiliale,
            decimal salaireMensuel,
            decimal salaireMensuelConjoint,
            int nombreEnfants)
        {
            if (situationFamiliale != CELIBATAIRE && situationFamiliale != MARIE)
            {
                throw new ArgumentException("Situation familiale invalide.");
            }

            if (salaireMensuel <= 0)
            {
                throw new ArgumentException("Les salaires doivent être positifs.");
            }

            if (situationFamiliale == MARIE && salaireMensuelConjoint < 0)
            {
                throw new ArgumentException("Les salaires doivent être positifs.");
            }

            if (nombreEnfants < 0)
            {
                throw new ArgumentException("Le nombre d'enfants ne peut pas être négatif.");
            }
        }

    }
}
