using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prob
{
    public class Dictionnaire
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
        public bool RechDichoRecursif(int debut, int fin, string mot,string []tab)
        {

            if (debut > fin) //condition d'arret si erreur de borne
            {
                return false;
            }
            if ((tab==null)||(tab.Length==0)) //si tab null
            {
                return false;
            }
            int milieu = (debut + fin) / 2;
            int comparaison = String.Compare(mot, tab[milieu]); //on compare le mot avec le mot situé au milieu du tableau contenant l'ensemble des mots et trié par ordre alphabetique
            if (comparaison == 0) //=> mot est avant triArray()[milieu] => fin = milieu-1;
            {
                return true;
            }
            else if (comparaison > 0)//=> mot est apres triArray()[milieu] => debut = milieu+1;
            {
                return RechDichoRecursif(milieu + 1, fin, mot,tab);
            }
            else 
            {
                return RechDichoRecursif(debut, milieu - 1, mot, tab);
            }
            
        }


    }
}
