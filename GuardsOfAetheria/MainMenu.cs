namespace GuardsOfAetheria{
    using System;
    using Improved.Consoles;
    using static System.Console;
    using static Improved.Lists;
    using static Improved.Consoles.Alignment;
    internal static class MainMenu{
        //TODO: autosave plot story
        public static BoolObject Character=new BoolObject(Kvp("selected",false));
        public static void Display(){//TODO: commands, command autocomplete with choose() maybe
            while(!Character.Is("selected")){
                //new Frame(0, 0, WindowHeight, WindowWidth).ShowBorder(Style.Normal);
                @"
╔═╗┬ ┬┌─┐┬─┐┌┬┐┌─┐  ┌─┐┌─┐  ╔═╗┌─┐┌┬┐┬ ┬┌─┐┬─┐┬┌─┐
║ ╦│ │├─┤├┬┘ ││└─┐  │ │├┤   ╠═╣├┤  │ ├─┤├┤ ├┬┘│├─┤
╚═╝└─┘┴ ┴┴└──┴┘└─┘  └─┘└    ╩ ╩└─┘ ┴ ┴ ┴└─┘┴└─┴┴ ┴".WriteAt(Centre);//TODO: ascii font, default space = 2
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
Johnathan").WriteAt(Centre,clear:true);
            ReadKey();
        }
        public static void Activate(this int option){
            switch(option){
                case 0:
                    CharacterCreation.Create();
                    Character["selected"]=true;
                    break;
                case 1:
                    Toolbox.Bag.LoadPlayer();
                    Character["Selected"]=true;
                    break;
                //TODO: save - player.cs add progress, enemies defeated/locs unlocked, ach stats - kills etc
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
