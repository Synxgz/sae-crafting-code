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
        /// <param name="estCelibataire">situation de célibat si vrai, marié/pacsé sinon</param>
        /// <param name="salaireMensuel">salaire mensuel >= 0</param>
        /// <param name="salaireMensuelConjoint">salaire mensuel du conjoint >= 0 si Marié/Pacsé</param>
        /// <param name="nombreEnfants">nombre d'enfants >= 0 </param>
        public Situation(bool estCelibataire, decimal salaireMensuel, decimal salaireMensuelConjoint, int nombreEnfants)
        {
            EstCelibataire = estCelibataire;
            SalaireMensuel = salaireMensuel;
            SalaireMensuelConjoint = salaireMensuelConjoint;
            NombreEnfants = nombreEnfants;
        }


    }
}
