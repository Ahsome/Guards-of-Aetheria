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

        public int menu;
        public string playerClass;
        public int strength;
        public int intellect;
        public int dexterity;
        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Guards of Atheria\nA simple game, set in the land of Aesrin\nWhat would you like to do?\n\n> New Game\n  Load Game\n  Options\n  Credits\n  Quit Game");
            SelectOption(4, 8);

            switch (menu)
            {
                case 1:
                    Console.Clear();
                    CharCreation();
                    break;
                case 2:
                //LoadGame
                case 3:
                    Console.Clear();
                    DisplayOptions();
                    break;
                case 4:
                    Console.Clear();
                    DisplayCredits();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
            DisplayMainMenu();
        }
        public void DisplayOptions()
        {
            Console.Clear();
            //Options
            DisplayMainMenu();
        }
        public void DisplayCredits()
        {
            Console.Clear();
            //Credits
            DisplayMainMenu();
        }
        public void CharCreation()
        {
            Console.Clear();
            Console.WriteLine("You wake up and find yourself on a surprisingly comfortable hay mattress.\nYou look around and see things that you thought belonged in an era long gone.\nYou take a moment to realise that you feel... different.\nYou climb out of bed, flex your muscles and do some mental warm-ups and find\nthat you are:\n> a warrior (berserker?), strong and ... to concentrate but slow and clumsy\n  a mage, excellent at concentrating and more dexterous than average, but weak\n  an archer, quick and agile and strong enough but bad at concentrating");
            SelectOption(5,7);
            switch (menu)
            {
                case 1:
                    playerClass = "Warrior";
                    strength = 10;
                    return;
                case 2:
                    playerClass = "Mage";
                    intellect = 10;
                    return;
                case 3:
                    playerClass = "Archer";
                    dexterity = 10;
                    return;
            }
            DisplayMainMenu();
        }
        public void SelectOption(int startLine, int endLine)
        {
            int numberOfOptions = endLine - startLine + 1;
            menu = 1;
            ConsoleKey input;
            int enter = 0;
            while (enter == 0)
            {
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Enter) { enter = 1; } else { enter = 0; }
                Console.SetCursorPosition(0, menu + startLine - 1);
                Console.Write(' ');
                if (input == ConsoleKey.UpArrow) { menu--; }
                if (input == ConsoleKey.DownArrow) { menu++; }
                if (menu < 1) { menu = numberOfOptions; }
                if (menu > numberOfOptions) { menu = 1; }
                Console.SetCursorPosition(0, menu + startLine - 1);
                Console.Write('>');
            }
            return;
        }
    }
}
/* Save:
 *  XmlDocument doc = new XmlDocument();
 *  doc.AppendChild(doc.CreateElement("<name>","<value>")); 
 *  doc.Save("<destination>.GoA"); 
 *  to read, use XmlDocument.Load*/
