using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prob
{
    class Plateau
    {
        private De[] des = new De[16];
        private char[,] lettresTirees = new char[10,10];

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

            for(int i=0; i<lettresTirees.GetLength(0); i++)
            {
                for (int j = 0; j < lettresTirees.GetLength(1); j++)
                {
                    chaine +=lettresTirees[i, j] + " ";
                }
                chaine += "\n";
            }
            return chaine;
        }
        //Pour ouvrir en mode flux
        public StreamReader OpenFile(string fileName)
        {

            StreamReader sReader = null;
            try
            {
                sReader = new StreamReader(fileName);

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sReader;

        }

        //methode lecture dé
        public void ReadFile(StreamReader sReader)
        {

            string line;
            //char[] separateur = { ';' };
            int i = 0;
            int j = 0;
            int k = 0;
            Random r = new Random(); //seule utilisation de random

            try
            {
                while ((line = sReader.ReadLine()) != null)
                {
                    string ligne = line.Replace(";", ""); //on crée une string sans les ;

                    char[] tab = ligne.ToCharArray(0, ligne.Length); //on convertit la string en tableau de char
                    char lettretiree = tab[r.Next(0, 6)]; //obtient une lettre parmi les 6
                    De de = new De(tab,lettretiree); //on crée une instance Dé avec ce dé
                    
                    des[i] = de;
                    lettresTirees[k, j] = lettretiree;
                    i++;
                    j++;
                    if (j>=4) 
                    {
                        j = 0; //on remet les colonnes a 0 et on augmente la ligne de 1
                        k++; 
                    } //pour remplir la matrice de lettre tirées, modulo 4 car on veut du 4*4
                    //Console.WriteLine(lettresTirees[k, j]); 
                    //Console.WriteLine(des[i]);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sReader != null) sReader.Close();
            }
        }

        //public bool Test_Plateau(string mot, )
        //{

        //}


    }
}
