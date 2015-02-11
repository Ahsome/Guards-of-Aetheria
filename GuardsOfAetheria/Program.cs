using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardsOfAetheria
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            //Change input to use UniversalMethod (PLEASE)
            //Set the proper attribute values for different clases
            //Add method to change Vit/End/Mana when Str/Dex/Wis changes.
            //Player.Instance.Vit = Player.Instance.BaseVit + Player.Instance.VitMod
            //Player.Instance.BaseVit = Player.Instance.Strength
            //Repeat the top with End and Mana

            //Think about these:
            //Damage = MainAtt * random.Next(-lvl,lvl+1) + DamageModifier;
            //Fake Class: Spaghetti Monster ;)
            //Try saying the character name less often, bitch/asshole/The Doctor

            Console.CursorVisible = false;
            var guiMenu = new GUIMainMenu();
            guiMenu.DisplayMainMenu();

            Player.Instance.Location = "TutorialArea";

            Movement movement = new Movement();
            while (true)
            {
                movement.ShowLocation();
                Console.SetCursorPosition(0, 0);
                Console.Clear();
            }
            //Took way to long to make this ;)
        }

    }
}
/* Save:
 *  XmlDocument doc = new XmlDocument();
 *  doc.AppendChild(doc.CreateElement("<name>","<value>")); 
 *  doc.Save("<destination>.GoA"); 
 *  to read, use XmlDocument.Load*/
