using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GuardsOfAetheria
{
    class Movement
    {
        readonly Utility _utility = new Utility();
        public void ShowLocation()
        {
            XElement xelement = XElement.Load("..\\..\\LocationDatabase.xml");
            var xmlData = xelement.Elements("world")
                .Elements("region")
                .Where(r => (string)r.Attribute("name") == Player.Instance.LocationRegion)
                .Elements("area")
                .Where(a => (string)a.Attribute("name") == Player.Instance.LocationArea)
                .Elements("building")
                .Where(b => (string)b.Attribute("name") == Player.Instance.LocationBuilding)
                .Elements("room")
                .Where(ro => (string)ro.Attribute("name") == Player.Instance.LocationRoom);

            Console.Clear();
            DisplayLocation(xmlData);

            int possibleOptions = DisplayOption(xmlData);
            int optionSelected = _utility.SelectOption(5, possibleOptions);

            SetLocation(optionSelected, xmlData);
        }

        private void DisplayLocation(IEnumerable<XElement> locationXmlData)
        {
            var textToDisplay = (string)locationXmlData.Elements("textToDisplay").FirstOrDefault();

            string[] tempVariable = ((string)locationXmlData.Elements("textVariables").FirstOrDefault()).Split(',');

            var variableDictionary = LocationDictionary();

            object[] textVariable = new object[tempVariable.Length];

            for (int i = 0; i < tempVariable.Length; i++ )
            {
                if (variableDictionary.ContainsKey(tempVariable[i]))
                {
                    textVariable[i] = variableDictionary[tempVariable[i]];
                }
            }
            Console.WriteLine(textToDisplay.Replace(@"\n", Environment.NewLine), textVariable);
        }

        private int DisplayOption(IEnumerable<XElement> locationXmlData)
        {
            var possibleOptions = locationXmlData.Elements("options");
            string[] options = ((string)possibleOptions.FirstOrDefault()).Split(',');
            Console.SetCursorPosition(0, 6);
            foreach (var element in options)
            {
                Console.WriteLine("  {0}", element);
            }
            return options.Length;
        }

        private Dictionary<string, object> LocationDictionary()
        {
            var variableDictionary = new Dictionary<string, object> {{"Player.Instance.Name", Player.Instance.Name}};
            return variableDictionary;
        }

        private void SetLocation(int option, IEnumerable<XElement> xmlData)
        {
            var newRegion = ((string)xmlData.Elements("optionRegion").FirstOrDefault()).Split(',');

            var newArea = ((string)xmlData.Elements("optionArea").FirstOrDefault()).Split(',');

            var newBuilding = ((string)xmlData.Elements("optionBuilding").FirstOrDefault()).Split(',');

            var newRoom = ((string)xmlData.Elements("optionRoom").FirstOrDefault()).Split(',');

            Player.Instance.LocationRegion = newRegion[option-1];
            Player.Instance.LocationArea = newArea[option - 1];
            Player.Instance.LocationBuilding = newBuilding[option - 1];
            Player.Instance.LocationRoom = newRoom[option - 1];
        }
    }
}
