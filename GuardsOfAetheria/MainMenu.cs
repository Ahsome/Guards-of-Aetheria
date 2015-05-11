using System;
using Improved.Consoles;
using static GuardsOfAetheria.MainMenu;
using static Improved.Consoles.Alignment;

namespace GuardsOfAetheria {
    internal class MainMenu {
        //TODO: autosave plot story selected to .txt
        public static bool CharacterIsSelected;
        public static void Display() { //TODO: commands, command autocomplete with choose()
            while(!CharacterIsSelected) {
                //var f = new Frame(0, 0, Console.WindowHeight, Console.WindowWidth); f.ShowBorder(Frame.Style.Normal);
                Console.Clear();
                Aligned.Write(@"
╔═╗┬ ┬┌─┐┬─┐┌┬┐┌─┐  ┌─┐┌─┐  ╔═╗┌─┐┌┬┐┬ ┬┌─┐┬─┐┬┌─┐
║ ╦│ │├─┤├┬┘ ││└─┐  │ │├┤   ╠═╣├┤  │ ├─┤├┤ ├┬┘│├─┤
╚═╝└─┘┴ ┴┴└──┴┘└─┘  └─┘└    ╩ ╩└─┘ ┴ ┴ ┴└─┘┴└─┴┴ ┴",Centre);
                Aligned.Write(@"
Welcome to the Guards of Atheria
A simple game, set in the land of Aesrin
What would you like to do?",Left);
                Console.CursorTop=Console.WindowHeight-3;
                "- ".WriteBorder();
                Aligned.Write($"© Black-Strike Studios, 2014 - {DateTime.Now.Year}",Centre);
                Console.SetCursorPosition(0,9);
                Consoles.Choose("New","Load","Options","Credits","Quit").Activate();
            }
        }

        public static void ShowCredits() {
            (@"Credits:

Coders:
Ahkam ""Ahsome"" Nihardeen
Timothy ""aytimothy"" Chew
E-Hern ""somebody1234"" Lee

Writers/Designers:
Perry ""Lafamas"" Luo
Johnathan").CWrite(Centre);
            Console.ReadKey();
        }
    }

    static class MenuExtensions {
        public static void Activate(this int option) {
            switch(option) {
                case 0: CharacterCreation.Create(); CharacterIsSelected=true; break;
                case 1: B.Ag.LoadPlayer(); CharacterIsSelected=true; break;
                //TODO: Load();, (Save(); at home) - player.cs json labels -> json export, progress, enemies defeated/locations unlocked
                case 2: Options.Change(); break;
                case 3: ShowCredits(); break;
                case 4: Environment.Exit(0); break;
            }
        }
    }
}
