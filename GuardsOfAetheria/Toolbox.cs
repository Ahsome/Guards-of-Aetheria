namespace GuardsOfAetheria{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    public class Toolbox{
        public static Lazy<Toolbox> Lazy=new Lazy<Toolbox>(()=>new Toolbox());
        public static Toolbox Bag=Lazy.Value;
        private Player player=new Player();
        protected Toolbox() {Initialise();}
        protected void Initialise(){
            // Your code here
        }
        public Player Player() {return player;}
        public void LoadPlayer() {Json.Load(out player,"bad file path");}
    }
    public static class Json{
        public static void Save<T>(this T t,string path) {File.WriteAllText(path,JsonConvert.SerializeObject(t));}
        public static void Load<T>(out T t,string path) {t=JsonConvert.DeserializeObject<T>(File.ReadAllText(path));}
    }
}
