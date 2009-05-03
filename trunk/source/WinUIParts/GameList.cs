using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessMangler.WinUIParts
{
    public partial class GameList : Form
    {
        public GameList()
        {
            InitializeComponent();
        }

        private void configList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openGrid_Click(object sender, EventArgs e)
        {
            ChessGrid open = new ChessGrid();
            open.Show();
        }
    }
}
