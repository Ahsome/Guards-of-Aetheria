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
            Console.CursorVisible = false;
            var guiMenu = new GUIMainMenu();
            guiMenu.DisplayMainMenu();
            //Took way to long to make this ;)
        }

    }
}
/* Save:
 *  XmlDocument doc = new XmlDocument();
 *  doc.AppendChild(doc.CreateElement("<name>","<value>")); 
 *  doc.Save("<destination>.GoA"); 
 *  to read, use XmlDocument.Load*/
