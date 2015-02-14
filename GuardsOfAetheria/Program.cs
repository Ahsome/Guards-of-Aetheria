using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace GuardsOfAetheria
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            /*Think about these:
            /Damage = rounddown?(BaseDamage * random.Next(0, 10001) * .00002 + .9)
            /Damage = MainAtt * random.Next(-lvl,lvl+1) + DamageModifier;
            /Fake Class: Spaghetti Monster ;)
            */

            Console.CursorVisible = false;
            Player.Instance.LocationRegion = "TestRegion";
            Player.Instance.LocationArea = "TestArea";
            Player.Instance.LocationBuilding = "Outside";
            Player.Instance.LocationRoom = "TutorialRoom";

            var guiMenu = new GUIMainMenu();
            guiMenu.DisplayMainMenu();

            while (true)
            {
                Movement movement = new Movement();
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
 *  doc.Save("C:\Program Files\Guards of Aetheria\Character\<destination>.GoA"); or maybe
 *  doc.Save("<destination>.GoA"); 
 *  to read, use XmlDocument.Load 
 */
