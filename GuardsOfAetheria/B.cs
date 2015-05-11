using System;
using System.IO;
using Newtonsoft.Json;

namespace GuardsOfAetheria {
    public class B {
        public static Lazy<B> Lazy = new Lazy<B>(() => new B());
        public static B Ag = Lazy.Value;

        protected B() { Initialise(); }

        protected void Initialise() {
            // Your code here
        }

        private Player player = new Player();
        public Player Player() { return player; }
        public void LoadPlayer() { Json.Load(out player,"bad file path"); }
        //For adding instances at runtime:
        /*private readonly Dictionary<string,object> components = new Dictionary<string,object>();

        public object Get(string name) { return components[name]; }
        public void Register(string name,object component) { components.Add(name,component); }
        public void Deregister(string name) { components.Remove(name); }*/
    }

    public static class Json {
        public static void Save<T>(this T t,string path) {
            File.WriteAllText(path,JsonConvert.SerializeObject(t));
        }

        public static void Load<T>(out T t,string path) {
            t=JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}