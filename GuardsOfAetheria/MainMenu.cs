using static System.Console;
using static GuardsOfAetheria.MainMenu;
using static GuardsOfAetheria.Toolbox;
using static Improved.Consoles.Aligned;
using static Improved.Consoles.Alignment;
namespace GuardsOfAetheria{
    using System;
    using Improved.Consoles;
    internal class MainMenu{
        //TODO: autosave plot story selected to .txt
        //TODO: using heirarchy - using{system{} } (with tabs and stuff of course)
        public static bool CharacterIsSelected;
        public static void Display(){//TODO: commands, command autocomplete with choose()
            while(!CharacterIsSelected){
                //var f = new Frame(0, 0, Console.WindowHeight, Console.WindowWidth); f.ShowBorder(Frame.Style.Normal);
                Clear();
                Write(@"
╔═╗┬ ┬┌─┐┬─┐┌┬┐┌─┐  ┌─┐┌─┐  ╔═╗┌─┐┌┬┐┬ ┬┌─┐┬─┐┬┌─┐
║ ╦│ │├─┤├┬┘ ││└─┐  │ │├┤   ╠═╣├┤  │ ├─┤├┤ ├┬┘│├─┤
╚═╝└─┘┴ ┴┴└──┴┘└─┘  └─┘└    ╩ ╩└─┘ ┴ ┴ ┴└─┘┴└─┴┴ ┴",Centre);
                Write(@"
Welcome to the Guards of Atheria
A simple game, set in the land of Aesrin
What would you like to do?",Left);
                CursorTop=WindowHeight-3;
                "- ".WriteBorder();
                Write($"© Black-Strike Studios, 2014 - {DateTime.Now.Year}",Centre);
                SetCursorPosition(0,9);
                Consoles.Choose("New","Load","Options","Credits","Quit").Activate();
            }
        }
        public static void ShowCredits(){
            (@"Credits:

Coders:
Ahkam ""Ahsome"" Nihardeen
Timothy ""aytimothy"" Chew
E-Hern ""somebody1234"" Lee

Writers/Designers:
Perry ""Lafamas"" Luo
Johnathan").CWrite(Centre);
            ReadKey();
        }
    }
    internal static class MenuExtensions{
        public static void Activate(this int option){
            switch(option){
                case 0:
                    CharacterCreation.Create();
                    CharacterIsSelected=true;
                    break;
                case 1:
                    Bag.LoadPlayer();
                    CharacterIsSelected=true;
                    break;
                //TODO: Load();, (Save(); at home) - player.cs json labels -> json export, progress, enemies defeated/locations unlocked
                case 2:
                    Options.Change();
                    break;
                case 3:
                    ShowCredits();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
