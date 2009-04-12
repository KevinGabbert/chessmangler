using System;
using System.Windows.Forms;

using System.IO;
using System.Xml;

using WinUIParts;


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

            string sourceDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString();
            string uiDirectory = sourceDir + "\\UI";
            string imagesDirectory = uiDirectory + "\\images";
            string configFile = sourceDir + "\\UI\\Board2D.config";

            bool exists = false;

            exists = File.Exists(configFile);

            if (exists)
            {
                testSetup = Config.LoadXML(configFile);

                UIBoard newBoard = new UIBoard();
                newBoard.CreateBoard(this, testSetup, uiDirectory); //get these from XML file 
 
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
