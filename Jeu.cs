using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Prob
{
    class Jeu
    {
        //menu Main
        static int SaisieNombre()
        {
            int choix = 0;
            do
            {
                choix = int.Parse(Console.ReadLine());
            } while ((choix < 1) || (choix > 4));               //Blindage
            return choix;
        }

        protected static int origRow;           //Fonction position curseur issue de
        protected static int origCol;           //docs.microsoft.com/fr-fr/dotnet/api/system.console.setcursorposition?
                                                //view=netcore-3.1


        protected static void WriteAt(string s, int x, int y)       //Fonction de position de curseur issue de microsoft.com
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        static void CreationInstances (int nbjoueur)
        {
            Console.Clear();
            //initialisation des variables dont on a besoin
            //dico
            string[][] mot= new string [15][];
            string langue = "Francais";

            //Dé 
            char[,]lettre = new char [4,4];
            De[] de = new De[16];

            //joueur
            int score = 0;
            List<string> motTrouves = new List<string>(100);
            Joueur[] joueurs = new Joueur[nbjoueur];

            //Création plateau
            Plateau plateau = new Plateau(de, lettre);
            StreamReader sReader = plateau.OpenFile("Des.txt"); //inutile ici a modif, change a chaque tour
            plateau.ReadFile(sReader);                          //idem
            //PSeudo des n joueurs
            for (int i =0; i<nbjoueur; i++)
            {
                WriteAt("Pseudo joueur " +i+1+" : ", 8, 3);
                string nom = Console.ReadLine();        //on créé n instances de joueurs

                Console.Clear();
                joueurs[i] = new Joueur(nom, score, motTrouves);
               
            }
            


            //Dictionnaire[] mondico = new Dictionnaire(mot,langue);

            
            
            //string affichage = plateau.ToString();
            //Console.WriteLine(affichage);
        }

        

        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            WriteAt("B", 10, 3);
            Thread.Sleep(200);
            WriteAt("O", 13, 3);
            Thread.Sleep(200);
            WriteAt("O", 16, 3);
            Thread.Sleep(200);
            WriteAt("G", 19, 3);
            Thread.Sleep(200);
            WriteAt("L", 22, 3);
            Thread.Sleep(200);
            WriteAt("E", 25, 3);
            Thread.Sleep(200);
            
            do
            {

                Console.Clear();
                Console.Write("Menu : BIENVENUE DANS NOTRE JEU BOOGLE\n"
                                 + "1- Commencer à jouer ! \n"
                                 + "2- Rappel des règles de jeu \n"
                                 + "3 \n"
                                 + "\n"
                                 + "Sélectionnez l'exercice désiré (taper le numéro de l'exo) --> ");
                int exo = SaisieNombre();
                Console.WriteLine();
                switch (exo) 
                {
                    case 1:
                        
                        Console.Clear();
                        int nbjoueur = 0;
                        do
                        {
                            WriteAt("Veuillez entrer le nombre de joueur svp (de 2 à 4): ", 10, 3);
                            nbjoueur = int.Parse(Console.ReadLine());
                            if ((nbjoueur > 4) || (nbjoueur < 2)) 
                            { 
                                WriteAt("Erreur, nombre invalide", 10, 4);
                                Thread.Sleep(1000);
                                WriteAt("                           ",10, 4); //remplace les espaces
                            }
                        } while ((nbjoueur>4)||(nbjoueur<2));
                        CreationInstances(nbjoueur);
                        break;

                    case 2:
                        
                        break;

                    case 3:
                        
                        break;

                    case 4:
                        
                        break;
                }
                Console.WriteLine("Tapez Escape pour sortir ou un numero d'action");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
            
            Console.ReadKey();
        }
    }
}
