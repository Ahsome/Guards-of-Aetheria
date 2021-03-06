using System;
using Improved.Consoles;

namespace GuardsOfAetheria
{
    internal class MainMenu
    {
        //TODO: autosave plot story selected to .txt
        private MainMenu(){}

        public static bool CharacterIsSelected;
        public static void Display()
        {
            while (!CharacterIsSelected)
            {
                //var f = new Frame(0, 0, Console.WindowHeight, Console.WindowWidth);
                //f.ShowBorder(Frame.Style.Normal);
                //Console.ReadKey();
                Console.Clear();
                Console.WriteLine(@"

              ╔═╗┬ ┬┌─┐┬─┐┌┬┐┌─┐  ┌─┐┌─┐  ╔═╗┌─┐┌┬┐┬ ┬┌─┐┬─┐┬┌─┐
              ║ ╦│ │├─┤├┬┘ ││└─┐  │ │├┤   ╠═╣├┤  │ ├─┤├┤ ├┬┘│├─┤
              ╚═╝└─┘┴ ┴┴└──┴┘└─┘  └─┘└    ╩ ╩└─┘ ┴ ┴ ┴└─┘┴└─┴┴ ┴

Welcome to the Guards of Atheria
A simple game, set in the land of Aesrin
What would you like to do?");
                Console.CursorTop = Console.WindowHeight - 2;
                "- ".WriteBorder();
                Console.Write(@"                      © Black-Strike Studios, 2014 - {0}                      ", DateTime.Now.Year);
                Console.SetCursorPosition(0, 9);
                new[] { "New", "Load", "Options", "Credits", "Quit" }.Choose().Activate();
            }
        }

        public static void ShowCredits()
        {
            Console.Clear(); Console.WriteLine(@"Credits:

Coders:
Ahkam ""Ahsome"" Nihardeen
Timothy ""aytimothy"" Chew
E-Hern ""somebody1234"" Lee

Writers/Designers:
Perry ""Lafamas"" Luo
Johnathan");
            Console.ReadKey();
        }
    }

    static class MenuExtensions
    {
        public static void Activate(this int option)
        {
            switch (option)
            {
                case 0: CharacterCreation.Create(); MainMenu.CharacterIsSelected = true; break;
                case 1: MainMenu.CharacterIsSelected = true; break;
                //TODO: LoadGame();, (SaveGame(); at home) - player.cs, progress, enemies defeated/locations unlocked
                case 2: Options.Change(); break;
                case 3: MainMenu.ShowCredits(); break;
                case 4: Environment.Exit(0); break;
            }
        }
    }
}