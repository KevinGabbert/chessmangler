using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;

using System.Collections.Generic;

using ChessMangler.Engine.Interfaces;
using ChessMangler.Engine.Config;
using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;
using ChessMangler.Settings.Types.WinUI;
using ChessMangler.Engine.Enums;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid2D_Form : GridForm
    {
        DebugForm _debugForm = new DebugForm();
        ChessGrid2D_MenuBarHandlers _menuBarHandlers = new ChessGrid2D_MenuBarHandlers();
        ChessGrid2D_Settings _gridOptions = new ChessGrid2D_Settings();

        public ChessGrid2D_Form()
        {
            InitializeComponent();

            this.InitGrid();
        }
        public ChessGrid2D_Form(BoardDef board, string imagesDirectory, short squareSize)
        {
            InitializeComponent();

            this.InitGrid();
            this.Grid.SetUp_FreeFormBoard(this, board, squareSize);
        }

        #region Event Handlers

        private void ChessGrid2D_Load(object sender, EventArgs e)
        {
            this.Grid.SetUp_DefaultUIBoard(this);

            //Menu Handlers
            this.toggleDebugModeToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);
        }
        private void ChessGrid2D_Resize(object sender, EventArgs e)
        {
            this.Grid.Redraw();
        }
        private void ChessGrid2D_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            this.Grid.ToggleBoardMode();
        }

        #endregion

        public void InitGrid()
        {
            this.Grid = new Grid2D(this);
        }
    }
}

