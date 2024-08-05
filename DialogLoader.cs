using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistsOfThelema
{
    /*
    internal class Program
    {
        static void Main(string[] args)
        {
            DialogLoader diLo = new DialogLoader();

            Console.WriteLine(diLo.LoadDialog("..\\..\\..\\dialog\\intro.txt"));
        }
    }*/

    internal class DialogLoader
    {
        public DialogLoader() { }

        public string LoadSingleDialog(string filePath)
        {
            //resets the dialogContent so one DialogLoader per scene is enough
            string dialogContent = "";

            // Get the absolute path to the file
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string endPath = Path.Combine(basePath, filePath);

            try
            {
                dialogContent = File.ReadAllText(endPath);
                return dialogContent;
            }
            catch //(Exception ex)
            {
                dialogContent = filePath + " XXXXX " + basePath + " XXXXX " + endPath + " XXXXX " + "Sorry, there seems to be a problem loading this dialog, please contact the dev team.";
                return dialogContent;
            }
        }
    }
}
