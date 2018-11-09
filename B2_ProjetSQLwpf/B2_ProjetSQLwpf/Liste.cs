using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2_ProjetSQLwpf
{
    class Liste
    {
        #region Attribut et Propriété auto
        public string nom { get; set; }
        public string vendeur { get; set; }
        public string description { get; set; }
        public string categorie { get; set; }
        public string etat { get; set; }
        public float prix { get; set; }
        #endregion

        public Liste(string nom, string vendeur, string description, string categorie, string etat, float prix)
        {
            this.nom = nom;
            this.vendeur = vendeur;
            this.description = description;
            this.categorie = categorie;
            this.etat = etat;
            this.prix = prix;
        }
    }
}
