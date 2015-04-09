using GuardsOfAetheria.Properties;

namespace GuardsOfAetheria
{
    public class Options
    {
        private static readonly Options OrigInstance = new Options();

        public string[] Names =
        {
            "Menu scrolling"
        };

        public string[][] Strings =
        {
            new[] { "Pages", "Scroll" }
        };

        public object[][] List =
        {
            new object[] { false, true }
        };

        public object[] InitialValues =
        {
            true
        };

        public object[] Types = {typeof(int)};
        internal Options() {}
        static Options() {}

        public object[] Current { get; set; }

        public static Options Instance { get { return OrigInstance; } }

        public void LoadOptions()
        {
            Instance.Current = new object[Names.Length];
            for (var i = 0; i < Names.Length; i++) Instance.Current[i] = Settings.Default[Names[i].Replace(" ","_")];
            Settings.Default.Save();
        }
    }
}