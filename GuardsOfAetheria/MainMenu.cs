using static System.Console;
using static GuardsOfAetheria.MainMenu;
using static GuardsOfAetheria.Toolbox;
using static Improved.Consoles.Alignment;
namespace GuardsOfAetheria{
    using System;
    using Improved.Consoles;
    internal class MainMenu{
        //TODO: autosave plot story selected to .txt
        //TODO: using{system{}} as c#6 suggestion system.collections=!system{collections}, as well as bool=; bool=!;
        public static bool CharacterIsSelected;
        public static void Display(){//TODO: commands, command autocomplete with choose()
            while(!CharacterIsSelected){
                //new Frame(0, 0, Console.WindowHeight, Console.WindowWidth).ShowBorder(Frame.Style.Normal);
                @"
╔═╗┬ ┬┌─┐┬─┐┌┬┐┌─┐  ┌─┐┌─┐  ╔═╗┌─┐┌┬┐┬ ┬┌─┐┬─┐┬┌─┐
║ ╦│ │├─┤├┬┘ ││└─┐  │ │├┤   ╠═╣├┤  │ ├─┤├┤ ├┬┘│├─┤
╚═╝└─┘┴ ┴┴└──┴┘└─┘  └─┘└    ╩ ╩└─┘ ┴ ┴ ┴└─┘┴└─┴┴ ┴".CWrite(Centre);//TODO: ascii font, default spacing = 2
                @"
Welcome to the Guards of Atheria
A simple game, set in the land of Aesrin
What would you like to do?".WriteAt(Left);
                CursorTop=WindowHeight-3;
                "- ".WriteBorder();
                $"© Black-Strike Studios, 2014 - {DateTime.Now.Year}".WriteAt(Centre);
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
Johnathan").WriteAt(Centre);
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
                //TODO: save - player.cs add progress, enemies defeated/locations unlocked
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
