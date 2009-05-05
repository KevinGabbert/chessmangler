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

namespace ChessMangler.WinUIParts
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid2D_Form : Form
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

        #endregion

        UIBoard _freeFormBoard = null;

        DebugForm _debugForm = new DebugForm();

        ChessGrid2D_MenuBarHandlers _menuBarHandlers = new ChessGrid2D_MenuBarHandlers();
        ChessGrid2D_SquareHandlers _squareHandlers = new ChessGrid2D_SquareHandlers();
        ChessGrid2D_Settings _gridOptions = new ChessGrid2D_Settings();

        public ChessGrid2D_Form()
        {
            InitializeComponent();
        }

        public ChessGrid2D_Form(BoardDef board, string imagesDirectory, short squareSize)
        {
            InitializeComponent();

            this.UIBoard = new UIBoard(0, this.chessMenu.Height + this.statusBar.Height); //adjust for Menu Bar
            this.UIBoard.CreateBoard(this, board, squareSize);

            this.UIBoard.EngineBoard.RulesEnabled = false;

            //imagesDirectory is for a "tools window (like photoshop) that has chess piece images mapped as buttons
        }

        private void ChessGrid2D_Load(object sender, EventArgs e)
        {
            //A test file so we can have something to develop with..
            XmlDocument testSetup = new XmlDocument();

            string uiDirectory = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString();
            string imagesDirectory = uiDirectory + "\\images";

            bool uiDirectoryExists = Directory.Exists(uiDirectory);
            bool imagesDirectoryExists = Directory.Exists(imagesDirectory);

            bool configFileExists = File.Exists(this.RulesFilePath);

            if (configFileExists)
            {
                testSetup = Config.LoadXML(this.RulesFilePath);

                if (_freeFormBoard == null)
                {
                    this.UIBoard = new UIBoard(0, this.chessMenu.Height + this.statusBar.Height); //adjust for Menu Bar
                    this.UIBoard.CreateBoard(this, testSetup, uiDirectory); //get these from XML file
                }
                else
                {
                    this.UIBoard = _freeFormBoard;
                }
            }

            _squareHandlers.DebugForm = _debugForm;
            _squareHandlers.Add_Required_Square_Handlers(this);

            //Menu Handlers
            this.toggleDebugModeToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);

            //Adjust Form size to account for menu bar
            ClientSize = new Size(ClientSize.Width, ClientSize.Height + this.chessMenu.Height); //+ this.StatusBar.Height
        }



        #region Form Event Handlers

        private void ChessGrid2D_Resize(object sender, EventArgs e)
        {
            //ChessGrid2D thisForm = (ChessGrid2D)sender;

            //for the moment, we will pull this from config.  (we'll use a pre-loaded prop later)
            //Int16 defaultSquareSize  = this.UIBoard.get(Config.LoadXML(_configFile));

            this.Redraw_UIBoard();
        }

        public void Redraw_UIBoard()
        {
            if (this.ConstrainProportions)
            {
                this.KeepSquare();
            }

            //Use our good friend SquareLogic to help us find all the squares on the board, and reset their locations

            if (this.UIBoard != null)
            {
                int newRow = 0;
                int columnCount = 0;

                BoardDef board = this.UIBoard.EngineBoard.Definition;
                foreach (Square2D currentSquare in this.UIBoard.EngineBoard.SquareLogic(board))
                {
                    UISquare currentUISquare = this.UIBoard.GetByBoardLocation(currentSquare.Column, currentSquare.Row);

                    if (currentUISquare != null)
                    {
                        int heightAdjustment = 6;
                        int x = currentSquare.Column * ClientSize.Width / board.Columns;
                        int y = (newRow * (ClientSize.Height - this.chessMenu.Height - this.statusBar.Height - heightAdjustment) / board.Rows);

                        y = y + this.chessMenu.Height;

                        currentUISquare.Location = new Point(x, y);
                        currentUISquare.CurrentPiece = currentSquare.CurrentPiece;
                        currentUISquare.Height = (ClientSize.Height / board.Columns);
                        currentUISquare.Width = (ClientSize.Width) / board.Rows;

                        if (this.UIBoard.DebugMode)
                        {
                            if (currentUISquare.CurrentPiece == null)
                            {
                                currentUISquare.Image = UISquare.CreateBitmapImage(currentSquare.BoardLocation, "Arial", 25);
                            }
                        }
                    }

                    //This is what we use to impose a new order (different than the Square2D list)
                    if (++columnCount > board.Columns - 1)
                    {
                        columnCount = 0;
                        newRow++;
                    }
                }
            }
        }

        private void KeepSquare()
        {
            //Ensure that the client area is always square
            //TODO: this needs to account for fullscreen
            int iSize = Math.Min(ClientSize.Height, ClientSize.Width);
            ClientSize = new Size(iSize, iSize);
        }

        #endregion

        private void ChessGrid2D_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();

            Application.Exit();
        }
    }
}

