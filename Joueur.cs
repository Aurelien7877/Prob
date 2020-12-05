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
        private string[] motTrouves;

        //Constructeur
        public Joueur (string nom, int score, string[] motTrouves)
        {
            this.nom = nom;
            this.score = score;
            this.motTrouves = motTrouves;
        }


    }
}
