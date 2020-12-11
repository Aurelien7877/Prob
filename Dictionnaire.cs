using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prob
{
    class Dictionnaire
    {

        private int longueur; //longueur = nb de mots        
        private string langue;
        private SortedList<int, List<string>> ensembleMot = new SortedList<int, List<string>>();

        //constructeur

        public Dictionnaire(SortedList<int, List<string>> ensembleMot, string langue)
        {
            this.ensembleMot = ensembleMot;
            this.langue = langue;
        }

        public SortedList<int, List<string>> EnsembleMot
        {
            get { return this.ensembleMot; }
            set { this.ensembleMot = value; }
        }
        public string Langue
        {
            get { return this.langue; }
            set { this.langue = value; }

        }

        public string toString()
        {
            string chaine = "Langue : " + this.langue + "\nLe dictionnaire contient : \n";

            for (int i = 0; i < this.ensembleMot.Count; i++)
            {
                chaine += this.ensembleMot.Values.Count + " mots avec" + this.ensembleMot.Keys[i] + "lettres\n";
            }

            return chaine;
        }

        //Pour lire fichier en mode flux
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

        //vérifie que la chaine est un chiffre
        static bool VerifChiffre(string chaine)
        {
            bool valeurNum = false;
            int compteur = chaine.Length;

            foreach (char c in chaine)
            {
                if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                {
                    compteur--;    
                }
            }

            if (compteur == 0) //si le compteur vaut 0 c'est que tous les caracteres testés sont des chiffres => c'est un chiffre/nombre
            {
                valeurNum = true;
            }

            return valeurNum;   //SINON FAUX
        }

        //methode lecture dictionnaire
        public void ReadFile(StreamReader sReader)
        {
            string line;
            char separateur = ' ';
            //int nbLettres = -1;
            List<string> listeTempo = new List<string>();


            List<string> liste2 = new List<string>();
            List<string> liste3 = new List<string>();
            List<string> liste4 = new List<string>();
            List<string> liste5 = new List<string>();
            List<string> liste6 = new List<string>();
            List<string> liste7 = new List<string>();
            List<string> liste8 = new List<string>();
            List<string> liste9 = new List<string>();
            List<string> liste10 = new List<string>();
            List<string> liste11 = new List<string>();
            List<string> liste12 = new List<string>();
            List<string> liste13 = new List<string>();
            List<string> liste14 = new List<string>();
            List<string> liste15 = new List<string>();

            try
            {

                while ((line = sReader.ReadLine()) != null)
                {

                    string[] tab = line.Split(separateur);

                    foreach (string mot in tab)
                    {
                        if (VerifChiffre(line) == false)
                        {
                            //listeTempo.Add(mot); //liste avec tous les mots
                            Console.WriteLine(mot);
                            if (mot.Length == 2)
                            {
                                liste2.Add(mot);
                                Console.WriteLine(mot);
                            }
                            else if (mot.Length == 3)
                            {
                                liste3.Add(mot);
                            }
                            else if (mot.Length == 4)
                            {
                                liste4.Add(mot);
                            }
                            else if (mot.Length == 5)
                            {
                                liste5.Add(mot);
                            }
                            else if (mot.Length == 6)
                            {
                                liste6.Add(mot);
                            }
                            else if (mot.Length == 7)
                            {
                                liste7.Add(mot);
                            }
                            else if (mot.Length == 8)
                            {
                                liste8.Add(mot);
                            }
                            else if (mot.Length == 9)
                            {
                                liste9.Add(mot);
                            }
                            else if (mot.Length == 10)
                            {
                                liste10.Add(mot);
                            }
                            else if (mot.Length == 11)
                            {
                                liste11.Add(mot);
                            }
                            else if (mot.Length == 12)
                            {
                                liste12.Add(mot);
                            }
                            else if (mot.Length == 13)
                            {
                                liste13.Add(mot);
                            }
                            else if (mot.Length == 14)
                            {
                                liste14.Add(mot);
                            }
                            else if (mot.Length == 15)
                            {
                                liste15.Add(mot);
                            }
                        }

                    }
                }

                this.ensembleMot.Add(2, liste2);
                this.ensembleMot.Add(3, liste3);
                this.ensembleMot.Add(4, liste4);
                this.ensembleMot.Add(5, liste5);
                this.ensembleMot.Add(6, liste6);
                this.ensembleMot.Add(7, liste7);
                this.ensembleMot.Add(8, liste8);
                this.ensembleMot.Add(9, liste9);
                this.ensembleMot.Add(10, liste10);
                this.ensembleMot.Add(11, liste11);
                this.ensembleMot.Add(12, liste12);
                this.ensembleMot.Add(13, liste13);
                this.ensembleMot.Add(14, liste14);
                this.ensembleMot.Add(15, liste15);

                //while ((line = sReader.ReadLine()) != null)
                //{
                //    if (VerifChiffre(line) == true) // si on detecte un nombre : on met listeTempo dans notre SortedList et on transforme le string detecté en entier pour ensuite l'assigner à un indexeur de notre sortedList 
                //    {
                //        if (nbLettres >= 1) //pour eviter le 1er tour de boucle
                //        {
                //            this.ensembleMot.Add(nbLettres, listeTempo);
                //            listeTempo.Clear();
                //        }
                //        nbLettres = int.Parse(line);
                //    }                    
                //    else // si ce n'est pas un chiffre : on sépare dans un tableau chaque mot lu et on copie les données de ce tableau dans une liste, que l'on ajoute ensuite à la sorted liste avec l'indexeur associé
                //    {
                //        string[] tab = line.Split(separateur); 
                //        foreach(string mot in tab)
                //        {
                //            listeTempo.Add(mot);                          
                //        }
                //    }                    
                //}
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
        //recherche en récursif;
        public bool RechDichoRecursif(int debut, int fin, string mot)
        {

            if (fin < debut) return false; //Erreur de placement des bornes
            int milieu = (debut + fin) / 2;
            int resultat = 0;// string.Compare(mot, ensembleMot[milieu], true);
            if (resultat == 0) return true;
            if (resultat < 0) fin = milieu - 1;
            if (resultat > 0) debut = milieu + 1;
            else return RechDichoRecursif(debut, fin, mot);
            return false;

        }

    }
}
