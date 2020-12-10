using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;

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
                if ((choix < 1) || (choix > 4)) 
                { 
                    WriteAt("Saisie invalide, réessayez", 42, 17);
                    Thread.Sleep(1000);
                    WriteAt("                                 ", 42, 17);
                    WriteAt(" ", 80, 16);
                }
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

        //méthode qui va crée les instances de joueurs
        static Joueur[] CreationInstances (int nbjoueur)
        {
            Console.Clear();

            //Dé 
            char[,]lettre = new char [4,4];
            De[] de = new De[16];

            //joueur
            int score = 0;
            List<string> motTrouves = new List<string>(100);
            Joueur[] joueurs = new Joueur[nbjoueur];

            //PSeudo des n joueurs
            for (int i =1; i<=nbjoueur; i++)
            {
                WriteAt("Pseudo joueur " +i+" : ", 30, 12);
                string nom = Console.ReadLine();        //on créé n instances de joueurs

                Console.Clear();
                joueurs[i-1] = new Joueur(nom, score, motTrouves);
               
            }
            return joueurs;
            //Dictionnaire[] mondico = new Dictionnaire(mot,langue);  
            
        }

        static void tourDeJeu (Joueur [] joueur)
        {
            Stopwatch chrono = new Stopwatch(); //création du chronomètre

            string mots;
            Console.Clear();
            for (int i = 0; i < joueur.Length; i++)
            {
                //Création plateau qui change a chaque tour 
                //Dé 
                //ConsoleKeyInfo cki=Console.ReadKey();

                
                    chrono.Start(); //démarre le chrono
                    char[,] lettre = new char[4, 4];
                    De[] de = new De[16];
                    Plateau plateau = new Plateau(de, lettre);
                    StreamReader sReader = plateau.OpenFile("Des.txt"); //inutile ici a modif, change a chaque tour
                    plateau.ReadFile(sReader);                          //idem
                    WriteAt("Au tour de " + joueur[i].Nom + "\n", 20, 3);

                    string affichage = plateau.ToString();
                    WriteAt(affichage, 0, 3);
                    //Console.WriteLine(affichage);
                    WriteAt("Chronomètre lancé", 20, 5);
                //cki = Console.ReadKey();
                while (chrono.ElapsedMilliseconds < 60000) //tant que le chrono est inferieur a 1 min
                {
                    WriteAt("Saisissez le mot :\n",0, 8);
                    mots = Console.ReadLine();
                }
                WriteAt("Fin du temps imparti, au suivant !", 20, 6);
                Thread.Sleep(1000);
                chrono.Reset(); //on reset le chrono pour chaque tour
                //if (i == 1) { i--; }


            }

        }

        static void Main(string[] args)
        {
            //Console.WriteLine(Console.LargestWindowHeight); //41 Mesires max de la fenetre (de mon ordi donc on met des valeures inf)
            //Console.WriteLine(Console.LargestWindowWidth); //171
            Console.WindowHeight = 28; //y
            Console.WindowWidth = 120;//x
            ConsoleKeyInfo cki;
            WriteAt("B", 54, 12);
            Thread.Sleep(200);
            WriteAt("O", 57, 12);
            Thread.Sleep(200);
            WriteAt("O", 60, 12);
            Thread.Sleep(200);
            WriteAt("G", 63, 12);
            Thread.Sleep(200);
            WriteAt("L", 66, 12);
            Thread.Sleep(200);
            WriteAt("E", 69, 12);
            Thread.Sleep(200);
            
            do
            {

                Console.Clear();
                WriteAt("BIENVENUE DANS NOTRE JEU BOOGLE", 45, 10);
                WriteAt("1- Commencer à jouer ", 49, 12);
                WriteAt("2- Lire les règles ", 49, 13);
                WriteAt("3- Quitter ", 49, 14);
                WriteAt("Sélectionnez le numéro correpondant -> ", 42, 16);
                
                int exo = SaisieNombre();
                Console.WriteLine();
                switch (exo) 
                {
                    case 1:
                        
                        Console.Clear();

                        //Saisie Nb joueurs
                        int nbjoueur = 0;
                        do
                        {
                            WriteAt("Veuillez entrer le nombre de joueur svp (de 2 à 4): ", 30, 12);
                            nbjoueur = int.Parse(Console.ReadLine());
                            if ((nbjoueur > 4) || (nbjoueur < 2)) 
                            { 
                                WriteAt("Erreur, nombre invalide", 30, 13);
                                Thread.Sleep(1000);
                                WriteAt("                           ",30, 13); //remplace les espaces
                            }
                        } while ((nbjoueur>4)||(nbjoueur<2));

                        //Saisie du temps de jeu 
                        bool tempsValide = false;
                        while (tempsValide == false)
                        {
                            WriteAt("Veuiller entrer le nombre total de tour de jeu: (minimum 2 tours, le nombre doit être pair)", 30, 12);
                            int nbToursDeJeu = Convert.ToInt32(Console.ReadLine());
                            if (nbToursDeJeu < 2 || nbToursDeJeu % 2 != 0)
                            {
                                WriteAt("Entrée invalide", 30, 13);
                            }
                            else
                            {
                                tempsValide = true;
                            }
                        }

                        Joueur [] joueurs = CreationInstances(nbjoueur);
                        tourDeJeu(joueurs);

                        
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
