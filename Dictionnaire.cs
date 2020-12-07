using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prob
{
    class Dictionnaire
    {
        private string[] ensembleMot; //longueur déterminée ? 
        private string langue;

        //constructeur
         
        public Dictionnaire (string [] ensembleMot, string langue)
        {
            this.ensembleMot = ensembleMot;
            this.langue = langue;
        }

        public string[] EnsembleMot
        {
            get { return this.ensembleMot; }
            set { this.ensembleMot=value; }
        }
        public string Langue
        {
            get { return this.langue; }
            
        }

        public string toString()
        {
            string res = null;
            for (int i =0; i<ensembleMot.GetLength(0); i++)
            {
                //for (int j= 0; j<ensembleMot.GetLength(1); j++)
                //{
                    res += ensembleMot[i];
                //}
                Console.WriteLine(i + 1 +" :");
            }

            return "langue : "+ this.langue +", "+ res;

        }

        //recherche en récursif;
        public bool RechDichoRecursif(int debut, int fin, string mot)
        {
            if (fin < debut) return false; //Erreur de placement des bornes
            int milieu = (debut + fin) / 2;
            int resultat = string.Compare(mot, ensembleMot[milieu], true);
            if (resultat == 0) return true;
            if (resultat < 0) fin = milieu - 1;
            if (resultat > 0) debut = milieu + 1;
            else return RechDichoRecursif(debut, fin, mot);
            return false;

        }

    }
}
