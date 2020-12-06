using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prob
{
    //pour gérer les positions dans la matrice correspondant au plateau
    class Position
    {
        private int ligne;
        private int colonne;

        public Position(int ligne, int colonne)
        {
            this.ligne = ligne;
            this.colonne = colonne;
        }

        public int Ligne
        {
            get { return this.ligne; }
            set { this.ligne = value; }
        }

        public int Colonne
        {
            get { return this.colonne; }
            set { this.colonne = value; }
        }
    }
}
