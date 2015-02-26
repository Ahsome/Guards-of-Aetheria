using System;

namespace GuardsOfAetheria
{
    public class Options
    {
        public enum Settings
        {
            Pages,
            Scroll
        }

        public Settings[][] SettingsList =
        {
            new[]
            {
                Settings.Pages, 
                Settings.Scroll
            }
        };
        public string[][] SettingNameStrings =
        {
            new[]
            {
                "Pages", 
                "Scroll"
            }
        };

        public string[] SettingNames =
        {
            "Menu scrolling"
        };
        public Settings[] CurrentSettings { get; set; }
        public void InitialiseOptions()
        {
            Instance.CurrentSettings = new[]
            {
                Settings.Pages
            };
        }
        
        private static readonly Options instance = new Options();

        static Options()
        {
        }

        private Options()
        {
        }

        public static Options Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
