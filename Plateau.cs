using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prob
{
    class Plateau
    {
        private De[] des = new De[16];
        private char[,] lettresTirees = new char[4,4];

        public Plateau(De[] des, char[,] lettresTirees)
        {
            this.des = des;
            this.lettresTirees = lettresTirees;
        }

        public De[] Des
        {
            get { return this.des; }
            set { this.des = value; }
        }
        public char[,] LettresTirees
        {
            get { return this.lettresTirees; }
            set { this.lettresTirees = value; }
        }

        

        /// <summary>
        /// Affichage de l'état du plateau  <=> affiche les 16 lettres tirées sous forme d'une matrice 4*4
        /// </summary>
        /// <returns> return un string affiché sous forme de matrice string</returns>
        public override string ToString()
        {
            string chaine = "";

            for(int i=0; i<this.des.Length; i++)
            {
                for (int j = 0; j < this.des.Length; j++)
                {
                    chaine += this.LettresTirees[i, j] + " ";
                }
                chaine += "\n";
            }
            return chaine;
        }



        
        //public bool Test_Plateau(string mot, )
        //{

        //}
            
      
    }
}
