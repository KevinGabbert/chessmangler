using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Linq;
using System.Text;
using System.Windows.Forms;

using WinUIParts;

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
            UIBoard newBoard = new UIBoard();
            newBoard.CreateBoard(this);  
        }
    }
}
