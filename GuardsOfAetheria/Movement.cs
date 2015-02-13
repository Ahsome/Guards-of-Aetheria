using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GuardsOfAetheria
{
    class Movement
    { 
        Utility utility = new Utility();
        public void ShowLocation()
        {
            Console.Clear();
           
            DisplayLocationText();
            LocationOption();
        }

        public int LocationOption()
        {
            switch (Player.Instance.LocationRoom)
            {
                case "TutorialRoom":
                    Console.SetCursorPosition(0, 6);
                    Console.WriteLine("> Corridor\n  Random House (NOT WORKING)\n  A subway (NOT WORKING)\n  Heaven (NOT DEAD ENOUGH)");
                    int option = utility.SelectOption(5, 3);
                    switch (option)
                    {
                        case 1:
                            SetLocation("Corridor");
                            break;
                        default:
                            LocationOption();
                            break;
                    }
                    break;
                case "Corridor":
                    Console.WriteLine("Sorry I haven't bothered to give any options, {0}", Player.Instance.Name);
                    Console.ReadKey();
                    break;
            }
            return 0;
        }

        private void DisplayLocationText()
        {
            XElement xelement = XElement.Load("..\\..\\LocationDatabase.xml");

            var textToDisplay = xelement.Elements("world")
                .Elements("region").Where(region => (string)region.Attribute("name") == Player.Instance.LocationRegion)
                .Elements("area").Where(area => (string)area.Attribute("name") == Player.Instance.LocationArea)
                .Elements("building").Where(building => (string)building.Attribute("name") == Player.Instance.LocationBuilding)
                .Elements("room").Where(room => (string)room.Attribute("name") == Player.Instance.LocationRoom)
                .Elements("textToDisplay");

            var textVariables = xelement.Elements("world")
                .Elements("region").Where(region => (string)region.Attribute("name") == Player.Instance.LocationRegion)
                .Elements("area").Where(area => (string)area.Attribute("name") == Player.Instance.LocationArea)
                .Elements("building").Where(building => (string)building.Attribute("name") == Player.Instance.LocationBuilding)
                .Elements("room").Where(room => (string)room.Attribute("name") == Player.Instance.LocationRoom)
                .Elements("textVariables");

            Console.WriteLine(((string)textToDisplay.First()).Replace(@"\n", Environment.NewLine), ((string)textVariables.First()).Split(','));
        }

        private void SetLocation(string newArea)
        {
            Player.Instance.LocationRoom = newArea;
        }
    }
}
