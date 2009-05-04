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

        #endregion

        string _sourceDir;
        string _configFile;

        UIBoard _freeFormBoard = null;

        DebugForm _debugForm = new DebugForm();

        ChessGrid2D_MenuBarHandlers _menuBarHandlers = new ChessGrid2D_MenuBarHandlers();
        ChessGrid2D_SquareHandlers _squareHandlers = new ChessGrid2D_SquareHandlers();
        ChessGrid2D_Settings _gridOptions = new ChessGrid2D_Settings();

        public ChessGrid2D_Form()
        {
            InitializeComponent();

            //---------  This pulls from ProgramSettings DB

            _sourceDir = _sourceDir = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString();
            _configFile =  _sourceDir + "\\Config\\Board2D.config"; //This needs to come from ProgramSettings
            //_sourceDir = _gridOptions.Get("_configFile");

            //---------  This pulls from ProgramSettings DB


            //string x = _gridOptions.Get("x");
        }
        public ChessGrid2D_Form(BoardDef board, string imagesDirectory, short squareSize)
        {
            InitializeComponent();

            this.UIBoard = new UIBoard(0, 25); //adjust for Menu Bar
            this.UIBoard.CreateBoard(this, board, squareSize);

            this.UIBoard.EngineBoard.RulesEnabled = false;

            //imagesDirectory is for a "tools window (like photoshop) that has chess piece images mapped as buttons
        }

        private void ChessGrid2D_Load(object sender, EventArgs e)
        {
            //A test file so we can have something to develop with..
            XmlDocument testSetup = new XmlDocument();

            //TODO: The App should start up with a blank form and a combobox showing what boards are available to load up.
            //If there are none, then the combobox should not be there.  Instead there will be a "Browse" button so
           //the user can point to a directory with config files.

            string uiDirectory = _sourceDir;
            string imagesDirectory = uiDirectory + "\\images";

            bool uiDirectoryExists = Directory.Exists(uiDirectory);
            bool imagesDirectoryExists = Directory.Exists(imagesDirectory);

            bool configFileExists = File.Exists(_configFile);

            if (configFileExists)
            {
                testSetup = Config.LoadXML(_configFile);

                if (_freeFormBoard == null)
                {
                    this.UIBoard = new UIBoard(0, 25); //adjust for Menu Bar //This needs to come from the config file
                    this.UIBoard.CreateBoard(this, testSetup, uiDirectory); //get these from XML file
                }
                else
                {
                    this.UIBoard = _freeFormBoard;
                }

                //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            else
            {
                if (_freeFormBoard != null)
                {
                    //Whine pitifully..
                    MessageBox.Show("Default Board Setup file not found. expected to find: " + _configFile);
                }
            }

            _squareHandlers.DebugForm = _debugForm;
            _squareHandlers.Add_Required_Square_Handlers(this);

            ClientSize = new Size(ClientSize.Width, ClientSize.Height + this.chessMenu.Height);
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

            int newRow = 0;
            int columnCount = 0;

            BoardDef board = this.UIBoard.EngineBoard.Definition;
            foreach (Square2D currentSquare in this.UIBoard.EngineBoard.SquareLogic(board))
            {
                UISquare currentUISquare = this.UIBoard.GetByBoardLocation(currentSquare.Column, currentSquare.Row);

                if (currentUISquare != null)
                {
                    int x = currentSquare.Column * ClientSize.Width / board.Columns;
                    int y = (newRow * (ClientSize.Height - (this.chessMenu.Height - 2)) / board.Rows);

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

        private void KeepSquare()
        {
            //Ensure that the client area is always square
            //TODO: this needs to account for fullscreen
            int iSize = Math.Min(ClientSize.Height, ClientSize.Width);
            ClientSize = new Size(iSize, iSize);
        }

        #endregion
    }
}

