﻿using System;
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


        static public StreamReader OpenFile(string fileName)
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

        //methode lecture dictionnaire
        static public string[] ReadFile(StreamReader sReader)
        {
            string line;
            char separateur = ' ';
            List<string> liste = new List<string>();

            try
            {
                //on met tous les mots dans une liste
                while ((line = sReader.ReadLine()) != null)
                {
                    string[] tab = line.Split(separateur);

                    foreach (string mot in tab)
                    {
                        liste.Add(mot);
                    }
                }

                //on recupere la longueur de la liste
                int longueur = liste.Count;
                string[] tableauFinal = new string[longueur];

                //on remplit le tableau de la classe
                for (int i = 0; i < longueur; i++)
                {
                    tableauFinal[i] = liste[i];
                }
                return tableauFinal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                if (sReader != null) sReader.Close();
            }
        }

        static Dictionnaire CreationDico()
        {
            string langue = "Français";
            string[] ensembleMots = ReadFile(OpenFile("MotsPossibles.txt"));
            Dictionnaire mondico = new Dictionnaire(ensembleMots, langue);
            
            return mondico;
        }



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

              
            
        }

        //méthode de vérification des contraintes
        static bool verifContraintes (Plateau plateau, string mot) 
        {
            Dictionnaire mondico = CreationDico();
            int debut = 0;
            int fin = mondico.EnsembleMot.Length;


            /*if ((plateau.Test_Plateau(mot) == true) && (mot.Length >= 3))//Si adjacent, longueur et dico a rajouter
            {
                return true;
            }*/
            if (mondico.RechDichoRecursif(debut, fin, mot) == true) { return true; }
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

            

            chronotot.Start(); //démarre le chrono total
            
            while (chronotot.ElapsedMilliseconds <tempsTotal) //boucle total
            {
                Console.Clear();
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
                    mots = Convert.ToString(Console.ReadLine()).ToUpper();
                    char[] motTemp = new char[mots.Length];
                    for (int j = 0; j < mots.Length; j++) { motTemp[j] = mots.ToCharArray()[j]; } //transforme le mot en tab de carac
                    if (verifContraintes(plateau,mots)==true)
                    {
                        joueur[i].Score += mots.Length - 1;
                        WriteAt("Score de " + joueur[i].Nom + " = " + joueur[i].Score,20,7);
                    }
                   
                }
                WriteAt("Fin du temps imparti, au suivant !",20,8);
                Thread.Sleep(1000);
                WriteAt("                                       ", 20, 8);
                WriteAt("                            ", 20, 7);

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
                        Console.Clear();
                        string regles = "Au début de chaque tour, 16 dés à 6 faces différentes et réprésentant des lettres sont lancés. Un plateau 4x4 représente alors ce lancer.\n" +
                            "Un compte à rebours défini par l'utilisateur se lance alors pour chronométrer l'entiereté de la partie. \n" +
                            "Chaque joueur joue l’un après l’autre pendant un laps de temps de 1 mn.\n\n\n" +
                            "Le but du jeu est de trouver les mots formés sur le plateau.\n" +
                            " Ceux-ci doivent respecter les règles suivantes pour être validés : \n\n" +
                            "-Un mot doit être composé de 3 lettres minimum.\n" +
                            "-Les lettres consécutives d'un mot doivent être adjacentes sur le plateau \n" +
                            "(que ce soit horizontalement, verticalement ou en diagonalement).\n" +
                            "-Chaque mot trouvé et validé rapporte des points selon sa longueur.\n" +
                            "-Les mots peuvent être au singulier ou au pluriel, conjugués ou non, \n" +
                            "mais ne doivent pas utiliser plusieurs fois le même dé pour le même mot.\n" +
                            "-Enfin, un mot ne sera accepté qu'une fois pour un même joueur.";
                        Console.WriteLine(regles);
                        break;

                    case 3:
                        Dictionnaire mondico = CreationDico();
                        string affichage = mondico.toString();
                        Console.WriteLine(affichage);
                        Console.WriteLine("Entrer un mot pour savoir si il appartient au dico");
                        string mot = Console.ReadLine();
                        int debut = 0;
                        int fin = mondico.EnsembleMot.Length;
                        Console.WriteLine("fin =" + fin);
                        //string[] tab = mondico.triArray(); TRI OK
                        
                        bool verif = mondico.RechDichoRecursif(debut, fin, mot); //OKKKK
                        if (verif == true) Console.WriteLine("trouvé");
                        else Console.WriteLine("pas trouvé");
                        break;
                }
                Console.WriteLine("Tapez Escape pour sortir ou un numero d'action");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
            
            Console.ReadKey();
        }
    }
}
