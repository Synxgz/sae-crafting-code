using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax.Simulator
{
    /// <summary>
    /// Classe de résultat de méthode
    /// </summary>
    /// <typeparam name="TSucces">type de succès</typeparam>
    /// <typeparam name="TEchec">type d'échectypeparam>
    public record Resultat<TSucces, TEchec>
        where TEchec : class
    {
        private readonly TSucces? _success;
        private readonly TEchec? _echec;

        private Resultat(TSucces success) => _success = success;
        private Resultat(TEchec echec) => _echec = echec;

        /// <summary>
        /// Succès de la méthode
        /// </summary>
        /// <param name="success">valeur de succès</param>
        /// <returns>objet resultat</returns>
        public static Resultat<TSucces, TEchec> Succes(TSucces success) => new(success);
        /// <summary>
        /// Echec de la méthode
        /// </summary>
        /// <param name="echec">valeur d'échec</param>
        /// <returns>pbjet d'échec</returns>
        public static Resultat<TSucces, TEchec> Echec(TEchec echec) => new(echec);

        public void Match(Action<TSucces> surSucces, Action<TEchec> surEchec)
        {
            if (EstEchec())
                surEchec(_echec!);
            else surSucces(_success!);
        }

        /// <summary>
        /// Si le résultat est un echec
        /// </summary>
        /// <returns>vrai si echec, faux sinon</returns>
        public bool EstEchec() => _echec is { };

        /// <summary>
        /// Récupère le contenu du succès (si succès)
        /// sinon valeur par défaut (null...)
        /// </summary>
        /// <returns>contenu du succès ou défaut</returns>
        public TSucces? SiSucces()
            => EstEchec()
                ? default(TSucces)
                : _success!;
        /// <summary>
        /// Récupère le contenu de l'échec (si échec)
        /// </summary>
        /// <returns>contenu de l'échec ou défaut</returns>
        public TEchec? SiEchec()
            => !EstEchec()
                ? default(TEchec)
                : _echec!;
    }
}
