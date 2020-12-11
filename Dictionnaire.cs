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
            char[] separateur = { ' ' };
            int nbLettres = -1;
            List<string> listeTempo = new List<string>();

            try
            {
                while ((line = sReader.ReadLine()) != null)
                {


                    if (VerifChiffre(line) == true) // si on detecte un nombre : on met listeTempo dans notre SortedList et on transforme le string detecté en entier pour ensuite l'assigner à un indexeur de notre sortedList 
                    {
                        if (nbLettres >= 1) //pour eviter le 1er tour de boucle
                        {
                            this.ensembleMot.Add(nbLettres, listeTempo);
                        }

                        nbLettres = int.Parse(line);
                    }
                    else // si ce n'est pas un chiffre : on sépare dans un tableau chaque mot lu et on copie les données de ce tableau dans une liste, que l'on ajoute ensuite à la sorted liste avec l'indexeur associé
                    {
                        string[] tab = line.Split(separateur);

                        foreach (string mot in tab)
                        {
                            listeTempo.Add(mot);
                        }
                    }
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
