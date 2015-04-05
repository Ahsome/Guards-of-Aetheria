using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace GuardsOfAetheria
{
    internal class MainProgram
    {
        //TODO: improve readability
        //TODO: create character + save, Console.Title for location etc
        private static readonly string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Guards of Aetheria");
        private static void Main()
        {
            // Fake Class: Spaghetti Monster ;)
            Console.CursorVisible = false;
            Player.Instance.LocationRegion = "TestRegion";
            Player.Instance.LocationArea = "TestArea";
            Player.Instance.LocationBuilding = "Outside";
            Player.Instance.LocationRoom = "TutorialRoom";
            //TODO: change when locations are finalised
            if (!Directory.Exists(AppData)) Directory.CreateDirectory(AppData);
            if (!File.Exists(Path.Combine(AppData, "Options.options"))) Options.Instance.InitialiseOptions();
            else
            {
                Options.Instance.Current = new Options.Settings[Options.Instance.List.Length];
                var doc = new XmlDocument();
                doc.Load(Path.Combine(AppData, "Options.options"));
                var values = doc.SelectNodes("settings/setting/@value");
                var i = 0;
                foreach (var value in (from XmlNode node in values select Utility.IntParseFast(node.ToString())))
                { Options.Instance.Current[i] = Options.Instance.List[i][value]; i++; }
            }

            MainMenu.DisplayMainMenu();
            var movement = new Movement();
            while (true) movement.ShowLocation();
            //Took way too long to make this ;)
        }
    }
}
