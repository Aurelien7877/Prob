using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prob
{
    //1
    class Dictionnaire
    {
        private string[][] ensembleMot = new string [15][]; //longueur déterminée : 15 tableaux 
        private int longeur; //longeur
        private string langue;

        //constructeur
         
        public Dictionnaire (string [][] ensembleMot, string langue)
        {
            this.ensembleMot = ensembleMot;
            this.langue = langue;
        }

        public string[][] EnsembleMot
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
            for (int i =0; i<ensembleMot.Length; i++)
            {
                //for (int j= 0; j<ensembleMot.GetLength(1); j++)
                //{
                    res += ensembleMot[i];
                //}
                Console.WriteLine(i + 1 +" :");
            }

            return "langue : "+ this.langue +", "+ res;

        }
        //Pour lire fichier ne mode flux
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

        //vérifie que on a la chaine est un chiffre
        static bool VerifChiffre(string chaine)
        {
            foreach (char c in chaine)
            {
                if (c >= '0' && c <= '9')
                {
                    return true;    //Si tout les caractères sont compris entre 0 ET 9 -> OK
                }
            }
            return false;   //SINON FAUX
        }

        //methode lecture dictionnaire
        public void ReadFile(StreamReader sReader)
        {
            
            string line;
            char[] separateur = { ' ' };
            int i = 0;
            
            //enregistre different tableaux en fonction de la longeur -> tableau de tableaux
            try
            {
                while ((line = sReader.ReadLine()) != null)
                {
                    string[] tab = line.Split(separateur);

                    for (int j =0; j<tab.Length; j++)
                    {
                        if (VerifChiffre(tab[0]) == true) { longeur = int.Parse(tab[0]); }
                        ensembleMot[longeur][i] = tab[j];
                        i++;
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
