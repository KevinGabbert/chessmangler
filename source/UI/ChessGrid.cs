using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

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
            ushort rows = 8;
            ushort columns = 8;
            int squareSize = 72;

            //ConfigurationManager.OpenExeConfiguration(System.Environment.CurrentDirectory

            //int rows = Convert.ToInt16(ConfigurationManager.AppSettings["DefaultBoard_Rows"]);
            //int columns = Convert.ToInt16(ConfigurationManager.AppSettings["DefaultBoard_Columns"]);
            //int squareSize = Convert.ToInt16(ConfigurationManager.AppSettings["DefaultBoard_SquareSize"]);

            XmlDocument defaultSetup = new XmlDocument();

            UIBoard newBoard = new UIBoard();
            newBoard.CreateBoard(this, rows, columns, squareSize, defaultSetup); //get these from XML file  
        }
    }
}
