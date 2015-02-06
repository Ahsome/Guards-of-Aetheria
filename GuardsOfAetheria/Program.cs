using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Guards of Atheria");
            Console.WriteLine("A simple game, set in the land of Aesrin");
            Console.WriteLine("What would you like to do?\n");

            Console.WriteLine("(1) New Game");
            Console.WriteLine("(2) Load Game");
            Console.WriteLine("(3) Options Game");
            Console.WriteLine("(4) Credits Game");
            Console.WriteLine("(5) Quit Game");
            char input = Console.ReadKey().KeyChar;
            if (!Char.IsNumber(input))
            {
                //ThrowException();
            }
            else if ((int)(input - '0') == 1)
            {
                return;
            }
            else if ((int)(input - '0') == 2)
            {
                //LoadGame()
            }
            else if ((int)(input - '0') == 3)
            {
                //DisplayOptionsMenu()
            }
            else if ((int)(input - '0') == 4)
            {
                //DisplayCreditsMeny()
            }
            else if ((int)(input-'0')== 5)
            {
                Environment.Exit(0);
            }
            DisplayMainMenu();
        }
    }
}
