﻿using System;
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

namespace ChessMangler.WinUIParts
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

        //Subscribe to the Comm Recieve Event

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
                    this.UIBoard = this.InitNewBoard();
                    this.UIBoard.CreateBoard(this, testSetup, uiDirectory); //get these from XML file
                    this.SetMode(BoardMode.Standard);
                }
                else
                {
                    this.UIBoard = _freeFormBoard;
                    this.SetMode(BoardMode.FreeForm);
                }
            }

            _squareHandlers.DebugForm = _debugForm;
            _squareHandlers.Add_Required_Square_Handlers(this);

            //Menu Handlers
            this.toggleDebugModeToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);

            //Fire the resize event..
            ClientSize = new Size(ClientSize.Width, ClientSize.Height + 130);  //this value should come from 1. game config or if that is not there, then 2. Program Config
        }

        public UIBoard InitNewBoard()
        {
            return new UIBoard(0, 0, 0);
        }

        #region Form Event Handlers

        private void ChessGrid2D_Resize(object sender, EventArgs e)
        {
            //ChessGrid2D thisForm = (ChessGrid2D)sender;

            //for the moment, we will pull this from config.  (we'll use a pre-loaded prop later)
            //Int16 defaultSquareSize  = this.UIBoard.get(Config.LoadXML(_configFile));

            this.Redraw_UIBoard();
        }

        int adjust2 = 20;
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
                        //Adjusts "Board Width" (Board being all the squares)
                        int x = currentSquare.Column * ClientSize.Width / board.Columns;
                        int y = AdjustBoardHeight(newRow, board);

                        currentUISquare.Location = new Point(x, y);
                        currentUISquare.CurrentPiece = currentSquare.CurrentPiece;

                        currentUISquare.Height = (ClientSize.Height / board.Columns) - adjust2;
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

        private int AdjustBoardHeight(int row, BoardDef board)
        {
            //(Board being all the squares)
            int heightAdjustment = this.statusBar.Height - adjust2;//unknown why I need this.  is this a total of cumulative errors??
            int controlsHeight = this.chessMenu.Height + heightAdjustment + this.tabControl1.Height + adjust2;
            int chessBoardHeight = ClientSize.Height - controlsHeight - heightAdjustment -adjust2;

            int y = 0;

            y = (row * chessBoardHeight) / board.Rows;
            y = y + (heightAdjustment * row);
            y = y + this.chessMenu.Height; //adding in the chessMenu Height controls where the grid begi

            return y;
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
    }
}

