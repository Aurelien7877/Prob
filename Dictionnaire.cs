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
        private string langue;
        private string[] ensembleMot = null;

        //constructeur
        public Dictionnaire(string[] ensembleMot, string langue)
        {
            this.ensembleMot = ensembleMot;
            this.langue = langue;
        }

        public string[] EnsembleMot
        {
            get { return this.ensembleMot; }
            set { this.ensembleMot = value; }
        }
        public string Langue
        {
            get { return this.langue; }
            set { this.langue = value; }
        }        
        

        //vérifie que la chaine est un chiffre/nombre
        public bool VerifChiffre(string chaine)
        {
            bool valeurNum = false;
            int compteur = chaine.Length;

            foreach (char c in chaine)
            {
                if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                {
                    compteur--;    // le compteur == 0 => c'est un chiffre/nombre
                }
            }

            if (compteur == 0)
            {
                valeurNum = true;
            }

            return valeurNum;   
        }        
        

        //renvoie le nombre de mots ayant le nombre de lettres entré en paramètre
        public int CompteurDeMots(int nombreDeLettres)
        {
            int compteur = 0;
            foreach (string mot in EnsembleMot)
            {
                if (mot.Length == nombreDeLettres)
                {
                    compteur++;
                }
            }
            return compteur;
        }


        public string toString()
        {
            string chaine = "Langue : " + Langue + "\nLe dictionnaire contient : \n";

            for (int i = 2; i < 16; i++)
            {
                chaine += CompteurDeMots(i) + " mots avec" + i + "lettres\n";
            }
            return chaine;
        }

        //tri test 1 (non fonctionnel)
        public string[] TriAlphabetique()
        {
            //on copie le ensembleMots dans un tableau
            string[] tab = new string[EnsembleMot.Length];
            for(int i = 0; i < tab.Length; i++)
            {
                tab[i] = EnsembleMot[i];
            }

            //tri
            string memoire = " ";
            for (int i = 0; i < tab.Length; i++)
            {
                string chaine = tab[i];
                int position = i;

                for (int j = 1 + i; j < tab.Length; j++)
                {
                    if (String.Compare(tab[j], chaine) < 0)
                    {
                        chaine = tab[j];
                        position = j;
                    }
                }
                memoire = tab[i];
                tab[i] = tab[position];
                tab[position] = memoire;
            }
            return tab;
        }

        //test tri fonctionnel
        public string[] triArray()
        {
            string[] tab = new string[EnsembleMot.Length];
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = EnsembleMot[i];
            }
            Array.Sort(tab);
            return tab;
        }


        //recherche en récursif; Fonctionnel
        public bool RechDichoRecursif(int debut, int fin, string mot)
        {

            int milieu = (debut + fin) / 2;
           

            int comparaison = String.Compare(mot, triArray()[milieu]); //on compare le mot avec le mot situé au milieu du tableau contenant l'ensemble des mots et trié par ordre alphabetique

            if (comparaison != 0 && debut==fin)
            {
                return false;
            }
            else if (comparaison < 0) //=> mot est avant TriAlphabetique()[milieu] => fin = milieu-1;
            {                
                return RechDichoRecursif(debut, milieu -1, mot);
            }
            else if(comparaison > 0)//=> mot est apres TriAlphabetique()[milieu] => debut = milieu+1;
            {
                return RechDichoRecursif(milieu+1, fin, mot);
            }
            else if(comparaison == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
        

    }
}
