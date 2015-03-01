using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GuardsOfAetheria
{
    class Movement
    {
        readonly Utility utility = new Utility();
        public void ShowLocation()
        {
            var xelement = XElement.Load("..\\..\\LocationDatabase.xml");
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
            var locationXmlData = xmlData as XElement[] ?? xmlData.ToArray();
            DisplayLocation(locationXmlData);

            string[] options = DisplayOption(locationXmlData);
            Console.SetCursorPosition(0, 5);
            var optionSelected = utility.SelectOption(options);

            SetLocation(optionSelected, locationXmlData);
        }

        private void DisplayLocation(IEnumerable<XElement> locationXmlData)
        {
            var xElements = locationXmlData as XElement[] ?? locationXmlData.ToArray();
            var textToDisplay = (string)xElements.Elements("textToDisplay").FirstOrDefault();

            string[] tempVariable = ((string)xElements.Elements("textVariables").FirstOrDefault()).Split(',');

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

        private string[] DisplayOption(IEnumerable<XElement> locationXmlData)
        {
            var possibleOptions = locationXmlData.Elements("options");
            string[] options = ((string)possibleOptions.FirstOrDefault()).Split(',');
            return options;
        }

        private Dictionary<string, object> LocationDictionary()
        {
            var variableDictionary = new Dictionary<string, object> {{"Player.Instance.Name", Player.Instance.Name}};
            return variableDictionary;
        }

        private void SetLocation(int option, IEnumerable<XElement> xmlData)
        {
            var xElements = xmlData as XElement[] ?? xmlData.ToArray();
            var newRegion = ((string)xElements.Elements("optionRegion").FirstOrDefault()).Split(',');

            var newArea = ((string)xElements.Elements("optionArea").FirstOrDefault()).Split(',');

            var newBuilding = ((string)xElements.Elements("optionBuilding").FirstOrDefault()).Split(',');

            var newRoom = ((string)xElements.Elements("optionRoom").FirstOrDefault()).Split(',');

            Player.Instance.LocationRegion = newRegion[option-1];
            Player.Instance.LocationArea = newArea[option - 1];
            Player.Instance.LocationBuilding = newBuilding[option - 1];
            Player.Instance.LocationRoom = newRoom[option - 1];
        }
    }
}
