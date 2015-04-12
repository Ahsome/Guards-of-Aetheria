using System.Collections.Generic;
using GuardsOfAetheria.Properties;

namespace GuardsOfAetheria
{
    public class Options
    {
        private static readonly Options OrigInstance = new Options();

        public List<string> Names = new List<string>
        {
            "Menu scrolling"
        };

        public List<List<string>> Strings = new List<List<string>>
        {
            new List<string> { "Pages", "Scroll" }
        };

        public List<List<object>> List = new List<List<object>>
        {
            new List<object> { false, true },
        };

        public List<object> InitialValues = new List<object>
        {
            true,
        };

        internal Options() {}
        static Options() {}

        public List<object> Current { get; set; }

        public static Options Instance { get { return OrigInstance; } }

        public void LoadOptions()
        {
            Instance.Current = new List<object>();
            foreach (var name in Names) Instance.Current.Add(Settings.Default[name.Replace(" ","_")]);
            Settings.Default.Save();
        }
    }
}