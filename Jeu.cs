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
            
            Joueur[] joueurs = new Joueur[nbjoueur];

            //PSeudo des n joueurs
            for (int i =1; i<=nbjoueur; i++)
            {
                WriteAt("Pseudo joueur " +i+" : ", 30, 12);
                string nom = Console.ReadLine();        //on créé n instances de joueurs
                List<string> motTrouves = new List<string>(100);
                Console.Clear();
                joueurs[i-1] = new Joueur(nom, score, motTrouves);
               
            }
            return joueurs;           
        }

        //méthode de vérification des contraintes
        static bool verifContraintes (Plateau plateau, string mot) 
        {
            //Initialisation pour les contraintes
            Dictionnaire mondico = CreationDico();
            int debut = 0;
            int fin = mondico.EnsembleMot.Length;
            int incrementeurLettre = 0;
            
            List<List<int[]>> listePositionsGlobal=new List<List<int[]>>();
            List<int[]> listePositionsUtilisees = new List<int[]>();
            
            WriteAt("Vérification ....patientez svp....",0,11);
            
            
            //pour aller au plus vite et ne pas chercher 10 secondes un mot d'une lettre, containtes dans ordre croissant de temps pris
            if (mot.Length >= 3)//si longueur ok
            {
                bool verifAdjacence = plateau.Test_Plateau(mot, incrementeurLettre, listePositionsGlobal, listePositionsUtilisees);
                if (verifAdjacence==true) //si adjacence ok
                {
                    bool verifDico = mondico.RechDichoRecursif(debut, fin, mot);
                    if (verifDico== true) //si appartient au dico
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //méthode de calcul du score en fonction de l'énoncé
        static int calculScore(string mot)
        {
            int score = 0;
            if ((mot.Length>=3)&&(mot.Length<=6)) 
            {
                score = mot.Length - 1;
            }
            if (mot.Length>=7)
            {
                score = 11;
            }
            return score;
        }

        //Déroulement du jeu
        static void tourDeJeu (Joueur [] joueur, int nbDeToursDeJeu,int nbjoueur)
        {
            //initialisation
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
                
                StreamReader sReader = plateau.OpenFile("Des.txt"); //on lit le plateau
                plateau.ReadFile(sReader);                          
                
                string affichage = plateau.ToString();
                WriteAt(affichage, 0, 3);

                while (chrono1min.ElapsedMilliseconds<60000) //Boucle 1 min
                {
                    WriteAt("                                      ", 0, 11); //pour nettoyer l'affichage
                    WriteAt("                  ", 0, 9);                        //
                    WriteAt("                                           ", 20, 3);  //
                    WriteAt("             ", 0, 10);                                //idem
                    WriteAt("Au tour de " + joueur[i].Nom + "\n", 20, 3);
                    WriteAt("Chronomètre lancé", 20, 5);
                    WriteAt("", 0, 8);
                    Console.WriteLine("Saissisez un mot");                   
                    mots = Convert.ToString(Console.ReadLine()).ToUpper(); //ToUpper car les mots du dico sont en maj

                    if ((verifContraintes(plateau, mots) == true)&&(joueur[i].Contain(mots)==false)) //si le joueur n'a pas encore dit le mot + toutes les contraintes
                    {
                        joueur[i].Score += calculScore(mots);       //appel de la méthode calcul score
                        joueur[i].Add_Mot(mots);                    //on add le mot trouvé a la liste des mots trouvés
                        WriteAt("Score de " + joueur[i].Nom + " = " + joueur[i].Score, 20, 7);
                        WriteAt("                      ", 20, 9);
                        WriteAt("Mot valide :D ", 20, 9);
                    }
                    else                                            //si mot pas valide
                    {
                        WriteAt("                      ", 20, 9);
                        WriteAt("Mot invalide :(", 20, 9);
                        
                    }
                    
                }
                WriteAt("Fin du temps imparti, au suivant !",0,13);
                Thread.Sleep(1000);
                WriteAt("                                       ", 0, 13);
                WriteAt("                            ", 20, 7);

                i++;                                //pour passer au joueur d'apres
                if (i==nbjoueur) { i = 0; } //si on a fait les n joueurs, on recommence
                chrono1min.Reset();                 //reset d'une chrono de 1 min
                
            }
            Console.Clear();
            WriteAt("Fin du temps imparti !", 45, 10);
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("Récapitulatif : ");
            int indiceGagnant = 0;
            int scoreGagnant = 0;
            for (int k =0; k<nbjoueur; k++)
            {
                           
                Console.WriteLine(joueur[k].toString());
                if (joueur[k].Score>scoreGagnant) //pour trouver le score max
                {
                    scoreGagnant = joueur[k].Score;
                    indiceGagnant = k;
                }              
            }
            Console.WriteLine("Le vainqueur est ...... " + joueur[indiceGagnant].Nom + "\nFélicitations !!");
            
            

        }

        static void Main(string[] args)
        {
            //Console.WriteLine(Console.LargestWindowHeight); //41 Mesures max de la fenetre (de mon ordi donc on met des valeures inf)
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
                    //cas jeu
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

                        Joueur [] joueurs = CreationInstances(nbjoueur); //création des instances

                        tourDeJeu(joueurs,nbToursDeJeu,nbjoueur);       //déroulement de la partie

                        
                        break;
                    //cas regle
                    case 2:
                        Console.Clear();
                        string regles = "Au début de chaque tour, 16 dés à 6 faces différentes et réprésentant des lettres sont lancés.\nUn plateau 4x4 représente alors ce lancer.\n" +
                            "Un compte à rebours défini par l'utilisateur se lance alors pour chronométrer l'entiereté de la partie. \n" +
                            "Chaque joueur joue l’un après l’autre pendant un laps de temps de 1 mn.\n\n\n" +
                            "Le but du jeu est de trouver les mots formés sur le plateau.\n" +
                            "Ceux-ci doivent respecter les règles suivantes pour être validés : \n\n" +
                            "-Un mot doit être composé de 3 lettres minimum.\n" +
                            "-Les lettres consécutives d'un mot doivent être adjacentes sur le plateau \n" +
                            "(que ce soit horizontalement, verticalement ou en diagonalement).\n" +
                            "-Chaque mot trouvé et validé rapporte des points selon sa longueur.\n" +
                            "-Les mots peuvent être au singulier ou au pluriel, conjugués ou non, \n" +
                            "mais ne doivent pas utiliser plusieurs fois le même dé pour le même mot.\n" +
                            "-Enfin, un mot ne sera accepté qu'une fois pour un même joueur.\n\n";
                        Console.WriteLine(regles);
                        break;
                    //cas quitter
                    case 3:
                        Console.Clear();
                        System.Environment.Exit(1);         //Pur tout quitter d'un coup
                        break;
                }
                Console.WriteLine("Tapez Escape pour sortir ou un numero d'action");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
            
            Console.ReadKey();
        }
    }
}
