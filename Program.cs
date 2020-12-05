using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Prob
{
    class Program
    {
        //menu Main
        static int SaisieNombre()
        {
            int choix = 0;
            do
            {
                choix = int.Parse(Console.ReadLine());
            } while ((choix < 1) || (choix > 4));               //Blindage
            return choix;
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


        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            WriteAt("B", 10, 3);
            Thread.Sleep(200);
            WriteAt("O", 13, 3);
            Thread.Sleep(200);
            WriteAt("O", 16, 3);
            Thread.Sleep(200);
            WriteAt("G", 19, 3);
            Thread.Sleep(200);
            WriteAt("L", 22, 3);
            Thread.Sleep(200);
            WriteAt("E", 25, 3);
            Thread.Sleep(200);
            
            do
            {

                Console.Clear();
                Console.Write("Menu : BIENVENUE DANS NOTRE JEU BOOGLE\n"
                                 + "1 \n"
                                 + "2 \n"
                                 + "3 \n"
                                 + "\n"
                                 + "Sélectionnez l'exercice désiré (taper le numéro de l'exo) --> ");
                int exo = SaisieNombre();
                Console.WriteLine();
                switch (exo) 
                {
                    case 1:
                        
                        break;

                    case 2:
                        
                        break;

                    case 3:
                        
                        break;

                    case 4:
                        
                        break;
                }
                Console.WriteLine("Tapez Escape pour sortir ou un numero d'action");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
            
            Console.ReadKey();
        }
    }
}
