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
    public partial class ChessGrid2D_Form : Form, IBoardMode
    {
        #region Properties

        UIBoard _uiBoard;
        public UIBoard UIBoard
        {
            get
            {
                return _uiBoard;
            }
            set
            {
                _uiBoard = value;
            }
        }

        GridUtility _grid;
        public GridUtility Grid
        {
            get
            {
                return _grid;
            }
            set
            {
                _grid = value;
            }
        }

        bool _constrainProportions;
        public bool ConstrainProportions
        {
            get
            {
                return _constrainProportions;
            }
            set
            {
                _constrainProportions = value;
            }
        }

        string _rulesFilePath;
        public string RulesFilePath
        {
            get
            {
                return _rulesFilePath;
            }
            set
            {
                _rulesFilePath = value;
            }
        }

        public BoardMode BoardMode
        {
            get
            {
                return this.UIBoard.EngineBoard.BoardMode;
            }
            set
            {
                this.UIBoard.EngineBoard.BoardMode = value;
            }
        }

        #endregion

        UIBoard _freeFormBoard = null;

        DebugForm _debugForm = new DebugForm();

        ChessGrid2D_MenuBarHandlers _menuBarHandlers = new ChessGrid2D_MenuBarHandlers();
        ChessGrid2D_SquareHandlers _squareHandlers = new ChessGrid2D_SquareHandlers();
        ChessGrid2D_Settings _gridOptions = new ChessGrid2D_Settings();

        //TODO: Subscribe to the Comm Recieve Event
        //ChessGrid2D_CommHandlers _commHandlers = new ChessGrid2D_CommHandlers();

        public ChessGrid2D_Form()
        {
            InitializeComponent();
        }

        public ChessGrid2D_Form(BoardDef board, string imagesDirectory, short squareSize)
        {
            InitializeComponent();

            this.UIBoard = this.InitNewBoard();
            this.UIBoard.CreateBoard(this, board, squareSize);
            this.SetMode(BoardMode.FreeForm);
        }

        public UIBoard InitNewBoard()
        {
            this.Grid = new GridUtility(this);
            return new UIBoard(0, 0, 0);
        }

        #region Form Event Handlers

        private void ChessGrid2D_Load(object sender, EventArgs e)
        {
            this.SetUp_DefaultBoard();

            _squareHandlers.Add_Required_Square_Handlers(this, _debugForm);

            //Menu Handlers
            this.toggleDebugModeToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);

            //Set initial size.  This also fires the resize event, which gives the form its final shape.
            ClientSize = new Size(ClientSize.Width, ClientSize.Height + 130);  //this value should come from 1. game config or if that is not there, then 2. Program Config
        }

        private void SetUp_DefaultBoard()
        {
            //TODO: Get these 2 paths from program or game config file.
            string uiDirectory = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString();
            string imagesDirectory = uiDirectory + "\\images";

            ChessGrid2D_Form.Check_Paths(uiDirectory, imagesDirectory);

            if (File.Exists(this.RulesFilePath))
            {
                XmlDocument rulesDocument = Config.LoadXML(this.RulesFilePath);

                if (_freeFormBoard == null)
                {
                    this.UIBoard = this.InitNewBoard();
                    this.UIBoard.CreateBoard(this, rulesDocument, uiDirectory);
                    this.SetMode(BoardMode.Standard);
                }
                else
                {
                    this.UIBoard = _freeFormBoard;
                    this.SetMode(BoardMode.FreeForm);
                }
            }
        }

        private void ChessGrid2D_Resize(object sender, EventArgs e)
        {
            //ChessGrid2D thisForm = (ChessGrid2D)sender;

            //for the moment, we will pull this from config.  (we'll use a pre-loaded prop later)
            //Int16 defaultSquareSize  = this.UIBoard.get(Config.LoadXML(_configFile));

            this.Grid.Redraw();
        }
        private void ChessGrid2D_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        #endregion

        #region Mode Button

        private void ToggleBoardMode()
        {
            if (this.BoardMode == BoardMode.Standard)
            {
                this.SetMode(BoardMode.FreeForm);
            }
            else
            {
                this.SetMode(BoardMode.Standard);
            }
        }
        private void SetMode(BoardMode boardMode)
        {
            this.BoardMode = boardMode;
            this.modeButton.Text = boardMode.ToString();
        }
        private void modeButton_Click(object sender, EventArgs e)
        {
            this.ToggleBoardMode();
        }

        #endregion

        #region Helper Functions

        private static void Check_Paths(string uiDirectory, string imagesDirectory)
        {
            //Check essential files
            bool uiDirectoryExists = Directory.Exists(uiDirectory);
            if (!uiDirectoryExists)
            {
                MessageBox.Show("Unable to find UI files directory:  " + uiDirectory);

            }


            //imagesDirectory is for a "tools window (like photoshop) that has chess piece images mapped as buttons

            bool imagesDirectoryExists = Directory.Exists(imagesDirectory);
            if (!imagesDirectoryExists)
            {
                MessageBox.Show("Unable to find Images directory:  " + imagesDirectory);

            }
        }

        #endregion
    }
}

