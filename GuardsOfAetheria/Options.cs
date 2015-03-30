using System;
using System.Xml;

namespace GuardsOfAetheria
{
    public class Options
    {
        public enum Settings { Pages, Scroll }

        private static readonly Options OrigInstance = new Options();

        public string[] SettingNames =
        {
            "Menu scrolling"
        };

        public string[][] SettingNameStrings =
        {
            new[]
            {
                "Pages",
                "Scroll"
            }
        };

        public Settings[][] SettingsList =
        {
            new[]
            {
                Settings.Pages,
                Settings.Scroll
            }
        };

        private Options() {}
        static Options() {}

        public Settings[] CurrentSettings { get; set; }

        public static Options Instance { get { return OrigInstance; } }

        public void InitialiseOptions()
        {
            Instance.CurrentSettings = new[]
            {
                Settings.Pages
            };
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var doc = new XmlDocument();
            var root = doc.CreateElement("settings");
            doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", null), doc.DocumentElement);
            doc.AppendChild(root);
            for (var i = 0; i < Instance.CurrentSettings.Length; i++)
            {
                var setting = (XmlElement) root.AppendChild(doc.CreateElement("setting"));
                setting.SetAttribute("number", i.ToString());
                var j = Array.IndexOf(SettingsList[i], CurrentSettings[i]);
                var value = (XmlElement) setting.AppendChild(doc.CreateElement("value"));
                value.SetAttribute("number", j.ToString());
            }
            doc.Save(appdata + @"\Guards of Aetheria\Options.option");
        }
    }
}