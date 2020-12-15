using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prob
{

    public class De
    {
        private char[] ensembleLettre;  //pour l'ensemble de lettre
        private char lettreTiree;       //la lettre triée

        //constructeur
        public De (char [] ensembleLettre, char lettreTriee)
        {
            this.ensembleLettre = ensembleLettre;
            this.lettreTiree = lettreTriee;
        }

        //propriétées
        public char[] EnsembleLettre
        {
            get { return this.ensembleLettre; }
            set { this.ensembleLettre = value; }
        }

        public char LettreTiree
        {
            get { return this.lettreTiree; }
            set { this.lettreTiree = value; }
        }

        //Méthode imposée : cette méthode permet de tirer au hasard une lettre parmi les 6
        public char Lance(Random r)
        {
            int tri = r.Next(0, 6);
            this.lettreTiree = ensembleLettre[tri];
            return lettreTiree;
        }

        //Chaine qui décrit le dé
        public string toString()
        {
            string n= null;
            for (int i = 0; i<ensembleLettre.Length; i++)
            {
                n += ensembleLettre[i] + ", ";
            }
            string res = "Lettres sur ce dé : " + n + " | Lettre tirée : " + lettreTiree;
            return res;
        }
    }
}
