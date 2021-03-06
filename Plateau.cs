﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prob
{
    public class Plateau
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
                    char lettretemp = ' ';
                    De deTempo = new De(tab,lettretemp);    //on cré un dé temporaire
                    char lettretiree = deTempo.Lance(r);    //on lance ce dé
                    //char lettretiree = tab[r.Next(0, 6)]; //obtient une lettre parmi les 6
                    De deVisible = new De(tab,lettretiree); //on crée une instance Dé avec le bon dé visible
                    
                    des[i] = deVisible;                     //on l'enregistre dans notre tableau de dé
                    lettresTirees[k, j] = lettretiree;      //on enregistre aussi la face visivle dans le plateau 
                    i++;
                    j++;
                    if (j>=4) 
                    {
                        j = 0; //on remet les colonnes a 0 et on augmente la ligne de 1
                        k++; 
                    } //pour remplir la matrice de lettre tirées, modulo 4 car on veut du 4*4

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

      



        //      listePositionsGlobal[i][j][k]
        // i = index pour parcourir la liste référençant les lettres du mot //ici incrémenteurLettre, première liste
        // j = index pour parcourir la liste des positions référencées      //ici incrementeurPosition, 2e liste, parcourt les positions de la lettre associée
        // k = index pour parcourir les 2 valeurs d'une position            //Position selon la colonne ou la ligne

        //methode de verification 
        public string PositionToString(int[] tab)
        {
            return "(" + tab[0] + "," + tab[1] + ")";
        }


        //return true si la lettre entrée existe dans le plateau
        public bool ExistenceLettre(char lettre)
        {
            for (int i = 0; i < LettresTirees.GetLength(0); i++)
            {
                for (int j = 0; j < LettresTirees.GetLength(1); j++)
                {
                    if (LettresTirees[i, j] == lettre)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //permet de parcourir une liste de tableau d'entier à la recherche d'un tableau précis      return true si on trouve l'objet
        public bool Contient(List<int[]> liste, int[] position)
        {
            foreach (int[] tab in liste)
            {
                if (tab[0] == position[0] && tab[1] == position[1])
                {
                    return true;
                }
            }
            return false;
        }

        //ajoute une position (definie par 2 index) dans la liste entrée
        public void AjouterPosition(List<int[]> listeIndex, int i, int j)
        {
            int[] tab = new int[2];
            tab[0] = i;
            tab[1] = j;
            listeIndex.Add(tab);
        }


        //Liste contenant l'ensemble des positions d'une lettre dans la matrice
        public List<int[]> ListePositions(char lettre)
        {
            List<int[]> listePositions = new List<int[]>();

            for (int i = 0; i < LettresTirees.GetLength(0); i++)
            {
                for (int j = 0; j < LettresTirees.GetLength(1); j++)
                {
                    if (LettresTirees[i, j] == lettre)
                    {
                        AjouterPosition(listePositions, i, j);
                    }
                }
            }
            return listePositions;
        }

        //Liste contenant l'ensemble des listes où l'on trouve les positions de chaque lettre du mot dans la matrice
        // à la n-ieme position de la liste, on trouve les infos relatives à la n-ième lettre du mot
        public List<List<int[]>> ListePositionsGlobal(string mot)
        {
            List<List<int[]>> listeGlobale = new List<List<int[]>>();

            foreach (char lettre in mot)
            {
                listeGlobale.Add(ListePositions(lettre));
            }
            return listeGlobale;
        }

        //test les conditions d'adjacence pour 2 positions (représentées par un tableau de 2 entiers)
        public bool Adjacence(int[] positionA, int[] positionB)
        {
            bool adjacenceValide = false;
            //Position A pour lettre n, Position B pour n+1
            //[0] correspond a i (lignes) , et [1] correspond a j (colonnes)


            ///Pour les 4 coins
            //si on est en 00
            if (positionA[0] == 0 && positionA[1] == 0)
            {
                //a droite
                if (positionB[1] == positionA[1] + 1)
                {
                    adjacenceValide = true;
                }
                //en dessous
                else if (positionB[0] == positionA[0] + 1)
                {
                    adjacenceValide = true;
                }
                //diag inf droite
                else if ((positionB[1] == positionA[1] + 1) && ((positionB[0] == positionA[0] + 1)))
                {
                    adjacenceValide = true;
                }
            }
            //si on est en 0, j max
            else if (positionA[0] == 0 && positionA[1] == LettresTirees.GetLength(1) - 1)
            {
                //a gauche
                if (positionB[1] == positionA[1] - 1)
                {
                    adjacenceValide = true;
                }
                //en dessous                
                else if (positionB[0] == positionA[0] + 1)
                {
                    adjacenceValide = true;
                }
                //diag inf gauche
                else if ((positionB[0] == positionA[0] + 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
            }
            //si on est en imax, 0
            else if (positionA[0] == LettresTirees.GetLength(0) - 1 && positionA[1] == 0)
            {
                //a droite
                if (positionB[1] == positionA[1] + 1)
                {
                    adjacenceValide = true;
                }
                //au dessus
                else if (positionB[0] == positionA[0] - 1)
                {
                    adjacenceValide = true;
                }
                //diag supp droite               
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] + 1))
                {
                    adjacenceValide = true;
                }
            }
            // si on est en i max, j max
            else if (positionA[0] == LettresTirees.GetLength(0) - 1 && positionA[1] == LettresTirees.GetLength(1) - 1)
            {
                //a gauche
                if (positionB[1] == positionA[1] - 1)
                {
                    adjacenceValide = true;
                }
                //au dessus                
                else if (positionB[0] == positionA[0] - 1)
                {
                    adjacenceValide = true;
                }
                //diag supp gauche      
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
            }

            ///Pour les bords
            // si on est en i, jmax : Bord droit
            else if (positionA[1] == LettresTirees.GetLength(1) - 1)
            {
                //a gauche
                if (positionB[1] == positionA[1] - 1)
                {
                    adjacenceValide = true;
                }
                //au dessus
                else if (positionB[0] == positionA[0] - 1)
                {
                    adjacenceValide = true;
                }
                //en dessous                
                else if (positionB[0] == positionA[0] + 1)
                {
                    adjacenceValide = true;
                }
                //diag supp gauche
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
                //diag inf gauche
                else if ((positionB[0] == positionA[0] + 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
            }
            //Bord bas
            // si on est en i max, j
            else if (positionA[0] == LettresTirees.GetLength(0) - 1)
            {
                //a droite
                if (positionB[1] == positionA[1] + 1)
                {
                    adjacenceValide = true;
                }
                //a gauche
                else if (positionB[1] == positionA[1] - 1)
                {
                    adjacenceValide = true;
                }
                //au dessus
                else if (positionB[0] == positionA[0] - 1)
                {
                    adjacenceValide = true;
                }
                //diag supp droite
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] + 1))
                {
                    adjacenceValide = true;
                }
                //diag supp gauche
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
            }

            ///Bord gauche
            //si on est en i,0
            else if (positionA[1] == LettresTirees.GetLength(1) - 1)
            {
                //a droite
                if (positionB[1] == positionA[1] + 1)
                {
                    adjacenceValide = true;
                }
                //au dessus
                else if (positionB[0] == positionA[0] - 1)
                {
                    adjacenceValide = true;
                }
                //en dessous
                else if (positionB[0] == positionA[0] + 1)
                {
                    adjacenceValide = true;
                }
                //diag supp droite
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] + 1))
                {
                    adjacenceValide = true;
                }
                //diag inf droite
                else if ((positionB[1] == positionA[1] + 1) && ((positionB[0] == positionA[0] + 1)))
                {
                    adjacenceValide = true;
                }
            }

            //Bord haut
            // si on est en 0 , j
            else if (positionA[0] == LettresTirees.GetLength(0) - 1)
            {
                //a droite
                if (positionB[1] == positionA[1] + 1)
                {
                    adjacenceValide = true;
                }
                //a gauche
                else if (positionB[1] == positionA[1] - 1)
                {
                    adjacenceValide = true;
                }
                //en dessous
                else if (positionB[0] == positionA[0] + 1)
                {
                    adjacenceValide = true;
                }
                //diag inf droite
                else if ((positionB[1] == positionA[1] + 1) && ((positionB[0] == positionA[0] + 1)))
                {
                    adjacenceValide = true;
                }
                //diag inf gauche
                else if ((positionB[0] == positionA[0] + 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
            }
            //Milieu
            //si on n'est pas sur un bord
            else
            {
                //a droite
                if (positionB[1] == positionA[1] + 1)
                {
                    adjacenceValide = true;
                }
                //a gauche
                else if (positionB[1] == positionA[1] - 1)
                {
                    adjacenceValide = true;
                }
                //au dessus
                else if (positionB[0] == positionA[0] - 1)
                {
                    adjacenceValide = true;
                }
                //en dessous
                else if (positionB[0] == positionA[0] + 1)
                {
                    adjacenceValide = true;
                }
                //diag supp droite
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] + 1))
                {
                    adjacenceValide = true;
                }
                //diag inf droite
                else if ((positionB[1] == positionA[1] + 1) && ((positionB[0] == positionA[0] + 1)))
                {
                    adjacenceValide = true;
                }
                //diag supp gauche
                else if ((positionB[0] == positionA[0] - 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
                //diag inf gauche
                else if ((positionB[0] == positionA[0] + 1) && (positionB[1] == positionA[1] - 1))
                {
                    adjacenceValide = true;
                }
            }
            return adjacenceValide;
        }




        //en récursif
        public bool Test_Plateau(string mot, List<List<int[]>> listePositionsGlobal, List<int[]> listePositionsUtilisees, int incrementeurLettre = 0, int incrementeurPosition = 0)
        {
            //On verifie tout d'abord que l'ensemble des lettres du mot se trouve dans la matrice
            int compteurExistence = 0;
            foreach (char lettre in mot)
            {
                if (ExistenceLettre(lettre) == true)
                {
                    compteurExistence++;
                }
            }

            if (compteurExistence != mot.Length) //si ce n'est pas le cas on return false
            {
                return false;
            }
            else //si toutes les lettres existent : 
            {
                // on verifie que pour la n-ieme lettre du mot, il existe la lettre n+1 respectant les conditions d'adjacence.

                if (incrementeurLettre < mot.Length-2) // on va effectuer cette opération pour chaque lettre du mot, sauf la dernière puisqu'elle n'a pas de lettre après elle
                {
                    if ((Adjacence(listePositionsGlobal[incrementeurLettre][incrementeurPosition], listePositionsGlobal[incrementeurLettre + 1][incrementeurPosition]) == true)) // si les positions sont adjacentes 
                    {
                        if (Contient(listePositionsUtilisees, listePositionsGlobal[incrementeurLettre][incrementeurPosition]) == false) //si le dé n'a pas été utilisé
                        {
                            listePositionsUtilisees.Add(listePositionsGlobal[incrementeurLettre][incrementeurPosition]); // on ajoute la position utilisée dans la liste des positions utilisées                                
                            return Test_Plateau(mot, listePositionsGlobal, listePositionsUtilisees, incrementeurLettre + 1, 0); // on return la méthode pour faire les tests sur la lettre suivante
                        }
                        else // si le dé a déjà été utilisé : on return false
                        {
                            //Console.WriteLine("dé déjà utilisé");
                            return false;
                        }

                    }
                    else // si les 2 positions testées ne sont pas adjacentes : 
                    {
                        if (incrementeurPosition == listePositionsGlobal[incrementeurLettre].Count - 1) //si on a testé toutes les valeurs de j (<=> on a parcouru l'ensemble des positions de la lettre donnée)
                        {
                            //Console.WriteLine("Les dés ne sont pas adjacents"); //les lettres ne sont pas adjacentes donc on return false;

                            //Console.WriteLine("Positions en cause  : " + PositionToString(listePositionsGlobal[incrementeurLettre][incrementeurPosition]) + "et" + PositionToString(listePositionsGlobal[incrementeurLettre + 1][incrementeurPosition]));

                            return false;
                        }
                        //si incrémenteurPosition n'est pas à sa valeur max, c'est qu'il reste des occurences de la lettre dans le plateau : on refait un tour de boucle, en incrementant j
                        else //on doit vérifier les potentielles autres positions de la lettre dans le plateau : on incremente le compteur associé pour verifier les autres positions
                        {
                            return Test_Plateau(mot, listePositionsGlobal, listePositionsUtilisees, incrementeurLettre, incrementeurPosition + 1); // on return la méthode pour faire les tests sur la lettre suivante
                        }
                    }
                }
                // dans le cas où on arrive à la derniere lettre : on est arrivé au bout du mot. Sachant que celle-ci existe : on return true.
                else
                {
                    //Console.WriteLine("Mot valide");
                    return true;
                }
            }
        }
    }
}
