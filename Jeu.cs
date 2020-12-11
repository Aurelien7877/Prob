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

        //méthode de vérification des contraintes
        static bool verifContraintes (Plateau plateau, string mot) 
        {
            if ((plateau.Test_Plateau(mot) == true) && (mot.Length >= 3))//Si adjacent
            {
                return true;
            }
            else Console.WriteLine("Mot invalide");
                return false;
        }

        static void tourDeJeu (Joueur [] joueur, int nbDeToursDeJeu)
        {
            Stopwatch chronotot = new Stopwatch(); //création du chronomètre total
            Stopwatch chrono1min = new Stopwatch(); //création du chronomètre pour 1 min
            int tempsTotal = nbDeToursDeJeu * 60000; //permet d'avoir le temps total en millisecondes 
            string mots = "";
            int i = 0;
            //Pour le plateau
            char[,] lettre = new char[4, 4];
            De[] de = new De[16];
            Plateau plateau = new Plateau(de, lettre);

            Console.Clear();

            chronotot.Start(); //démarre le chrono total
            
            while (chronotot.ElapsedMilliseconds <tempsTotal) //boucle total
            {
                chrono1min.Start(); //démarre le chrono d'une minute
                
                StreamReader sReader = plateau.OpenFile("Des.txt"); 
                plateau.ReadFile(sReader);                          
                
                string affichage = plateau.ToString();
                WriteAt(affichage, 0, 3);

                while (chrono1min.ElapsedMilliseconds<60000) //Boucle 1 min
                {
                    WriteAt("                                           ", 20, 3);//pour nettoyer l'affichage
                    WriteAt("Au tour de " + joueur[i].Nom + "\n", 20, 3);
                    WriteAt("Chronomètre lancé", 20, 5);
                    WriteAt("", 0, 8);
                    Console.WriteLine("Saissisez un mot");
                    mots = Console.ReadLine();
                    if (verifContraintes(plateau,mots)==true)
                    {
                        joueur[i].Score += mots.Length - 1;
                        Console.WriteLine("Score de " + joueur[i].Nom + " = " + joueur[i].Score);
                    }
                   
                }
                Console.WriteLine("Fin du temps imparti, au suivant !");
                i++;
                if (i==joueur.Length) { i = 0; } //si on a fait les n joueurs, on recommence
                chrono1min.Reset();
                
            }
            Console.Clear();
            Console.WriteLine("Fin du temps imparti !");
            

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
                        int nbToursDeJeu = 0; ;
                        while (tempsValide == false)
                        {
                            WriteAt("                                                                 ", 30, 12);
                            WriteAt("Veuillez entrer le nombre total de tour de jeu (1 min par tour)", 30, 12);
                            WriteAt("Exemple : une partie de 6 tours durera 6 min, 3 tours par joueurs", 30, 13);
                            
                            WriteAt("(minimum 2 tours, le nombre doit être pair (sinon inégalité) -> ", 30, 14);
                            nbToursDeJeu = Convert.ToInt32(Console.ReadLine());
                            if (nbToursDeJeu < 2 || nbToursDeJeu % 2 != 0)
                            {
                                WriteAt("Entrée invalide", 30, 15);
                                Thread.Sleep(1000);
                                WriteAt(" ", 70, 14);
                                WriteAt("               ", 30, 15);
                            }
                            else
                            {
                                tempsValide = true;
                            }
                        }

                        Joueur [] joueurs = CreationInstances(nbjoueur);

                        tourDeJeu(joueurs,nbToursDeJeu);

                        
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
