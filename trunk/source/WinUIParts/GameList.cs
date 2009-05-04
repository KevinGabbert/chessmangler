using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ChessMangler.Engine.Types;

namespace ChessMangler.WinUIParts
{
    public partial class GameList : Form
    {
        public GameList()
        {
            InitializeComponent();
        }

        private void openGrid_Click(object sender, EventArgs e)
        {
            if ((udGridX.Value > 0) && (udGridY.Value > 0) && (udSquareSize.Value > 0))
            {
                BoardDef board = new BoardDef((short)udGridX.Value, (short)udGridY.Value);
                ChessGrid open = new ChessGrid(board, this.txtImages.Text, (short)udSquareSize.Value);
                open.Show();
            }
            else
            {
                ChessGrid open = new ChessGrid();
                open.Show();
            }
        }
    }
}
