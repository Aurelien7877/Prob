using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prob
{
    class De
    {
        private char[] ensembleLettre;  //pour l'ensemble de lettre
        private char lettreTriee;       //la lettre triée

        //constructeur
        public De (char [] ensembleLettre, char lettreTriee)
        {
            this.ensembleLettre = ensembleLettre;
            this.lettreTriee = lettreTriee;
        }

        //propriétées
        public char[] EnsembleLettre
        {
            get { return this.ensembleLettre; }
            set { this.ensembleLettre = value; }
        }

        public char LettreTriee
        {
            get { return this.lettreTriee; }
            set { this.lettreTriee = value; }
        }

        //Méthode imposée : cette méthode permet de tirer au hasard une lettre parmi les 6
        public void Lance(Random r)
        {
            int tri = r.Next(0, 7);
            this.lettreTriee = ensembleLettre[tri];
        }
    }
}
