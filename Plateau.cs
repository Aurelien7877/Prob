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
        /// Affichge de l'état du plateau  <=> affiche les 16 lettres tirées sous forme d'une matrice 4*4
        /// </summary>
        /// <returns> return un string affiché sous forme de matricestring</returns>
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



        //signature modifiée par rapport à l'enoncé  /!\ (pour utiliser la recursivité)
        //compteur permet de recuperer la lettre recherchée dans le mot entré en parametre
        // la lliste de positions sert a referencer les cases deja "utilisée"
        //public bool Test_Plateau(string mot, bool eligible = true, int compteur =0, List<int[]> positions)    
        //{
        //    char lettreRecherchee = mot[compteur];

        //    for (int i = 0; i < this.lettresTirees.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < this.lettresTirees.GetLength(1); j++)
        //        {
        //            if (this.lettresTirees[i, j] == lettreRecherchee)
        //            {

        //            }
        //        }                
        //    }


        //    return eligible;
        //}

    }
}
