using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml;

using WinUIParts;

using System.Configuration;

namespace SKChess
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid : Form
    {
        public ChessGrid()
        {
            InitializeComponent();
        }

        private void ChessGrid_Load(object sender, EventArgs e)
        {
            //A test file so we can have something to develop with..
            XmlDocument testSetup = new XmlDocument();

            //The App should start up with a blank form and a combobox showing what boards are available to load up.
            //If there are none, then the combobox should not be there.  Instead there will be a "Browse" button so
            //the user can point to a directory with config files.

            string configPath = System.Environment.CurrentDirectory + "\\Board2D.config"; //have it copy local

            bool exists = false;

            exists = File.Exists(configPath);

            if (exists)
            {
                testSetup = Config.LoadXML(configPath);

                UIBoard newBoard = new UIBoard();
                newBoard.CreateBoard(this, testSetup, System.Environment.CurrentDirectory); //get these from XML file 
 
                //Steve, see those screwed up cell colors?? that's you..
            }
            else
            {
                //Whine pitifully..
                MessageBox.Show("Default Board Setup file not found");
            }
        }
    }
}
