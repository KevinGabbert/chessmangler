using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

using WinUIParts;

using System.Configuration;

namespace SKChess
{
    public partial class ChessGrid : Form
    {
        public ChessGrid()
        {
            InitializeComponent();
        }

        private void ChessGrid_Load(object sender, EventArgs e)
        {
            int rows = 8;
            int columns = 8;
            int squareSize = 72;

            //ConfigurationManager.OpenExeConfiguration(System.Environment.CurrentDirectory

            //int rows = Convert.ToInt16(ConfigurationManager.AppSettings["DefaultBoard_Rows"]);
            //int columns = Convert.ToInt16(ConfigurationManager.AppSettings["DefaultBoard_Columns"]);
            //int squareSize = Convert.ToInt16(ConfigurationManager.AppSettings["DefaultBoard_SquareSize"]);

            UIBoard newBoard = new UIBoard();
            newBoard.CreateBoard(this, rows, columns, squareSize); //get these from XML file  
        }
    }
}
