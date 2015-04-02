using System;
using System.Xml;

namespace GuardsOfAetheria
{
    public class Options
    {
        public enum Settings { Pages, Scroll }

        private static readonly Options OrigInstance = new Options();

        public string[] Names =
        {
            "Menu scrolling"
        };

        public string[][] Strings =
        {
            new[]
            {
                "Pages",
                "Scroll"
            }
        };

        public Settings[][] List =
        {
            new[]
            {
                Settings.Pages,
                Settings.Scroll
            }
        };

        private Options() {}
        static Options() {}

        public Settings[] Current { get; set; }

        public static Options Instance { get { return OrigInstance; } }

        public void InitialiseOptions()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var doc = new XmlDocument();
            var root = doc.CreateElement("settings");
            doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", null), doc.DocumentElement);
            doc.AppendChild(root);
            Instance.Current = new Settings[List.Length];
            for (var i = 0; i < List.Length; i++)
            {
                var setting = (XmlElement) root.AppendChild(doc.CreateElement("setting"));
                setting.SetAttribute("number", i.ToString());
                setting.SetAttribute("value", "0");
                Instance.Current[i] = Instance.List[i][0];
                //TODO: rename
            }
            doc.Save(appdata + @"\Guards of Aetheria\Options.option");
        }
    }
}