using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prob
{
    class Joueur
    {
        //attributs
        private string nom;
        private int score;
        private List<string> motTrouves;

        //Constructeur
        public Joueur (string nom, int score, List<string> motTrouves)
        {
            this.nom = nom;
            this.score = score;
            this.motTrouves = motTrouves;
        }
        //propriétés
        public string Nom
        {
            get { return this.nom; }
        }

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
        public List<string> MotTrouves
        {
            get { return this.motTrouves; }
            set { this.motTrouves = value; }
        }

        //Méthodes imposées

        //teste si le mot passé appartient déjà aux mots trouvés par le joueur pendant la partie
        public bool Contain(string mot)
        {
            for (int i =0; i<motTrouves.Count; i++)
            {
                if (motTrouves[i] == mot) return true;
            }
            return false;
        }

        // ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie
        public void Add_Mot(string mot)
        {
            motTrouves.Add(mot);
        }

        // teste si le mot passé en paramètre a déjà été cité
        public bool Mot_Cite(string mot)
        {
            return false; //A developper
        }

        //décrit un joueur
        public string toString()
        {
            string liste=null;
            for (int i =0; i<motTrouves.Count; i++)
            {
                liste = motTrouves[i] + ", ";
            }
            string res = "Joueur : " + this.nom + " | Score : " + this.score + "\nMots trouvés par ce joueur : " + liste;
            return res;
        }
    }
}
