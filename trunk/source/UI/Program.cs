using System;
using System.Collections.Generic;

using System.Windows.Forms;

using System.IO;

namespace ChessMangler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //---------  This pulls from ProgramSettings DB

            string sourceDir = sourceDir = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString();          
            string configFile =  sourceDir + "\\Config\\Board2D.config"; //This needs to come from ProgramSettings
            //_sourceDir = _gridOptions.Get("_configFile");

            if(!File.Exists(configFile))
            {
                throw new System.Exception("Unable to find Config file at: " + configFile);
            }

            //---------  This pulls from ProgramSettings DB


            Application.Run(new ChessMangler.WinUIParts.ChessGrid());
        }
    }
}
