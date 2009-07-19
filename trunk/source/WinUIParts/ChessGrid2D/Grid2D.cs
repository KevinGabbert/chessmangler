using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using ChessMangler.Options.Interfaces;
using ChessMangler.Engine.Config;
using ChessMangler.Engine.Enums;
using ChessMangler.Engine.Types;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    /// <summary>
    /// 
    /// </summary>
    public class Grid2D : ChessGrid2D_Base, IVersion
    {
        //TODO: refactor all references of ChessGrid2D out of here!
        #region Properties

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

        int _verticalSquish = 0;
        public int VerticalSquish
        {
            get
            {
                return _verticalSquish;
            }
            set
            {
                _verticalSquish = value;
            }
        }

        public string Version { get; set; }

        #endregion

        public Grid2D(ChessGrid2D_Form formWithGrid, string version)
        {
            this.ChessGrid2D_Form = formWithGrid;
            this.DebugForm = new DebugForm();
        }

        /// <summary>
        /// This function is all about resetting the location of the UISquares that were originally created in UIBoard.BuildUISquares
        /// </summary>
        public void Redraw(bool flipTheBoard)
        {
            if (this.ConstrainProportions)
            {
                this.Form_KeepSquare();
            }

            if (this.UIBoard != null)
            {
                BoardDef board = this.UIBoard.EngineBoard.Definition;

                int newRow = 0;
                int columnCount = 0;

                if (!flipTheBoard)
                {
                    newRow = 0;
                    columnCount = 0;
                }
                else
                {
                    newRow = board.Rows - 1;
                    columnCount = board.Columns - 1;
                }

                //Use our good friend BoardEnumerator to help us find all the squares on the board, and reset their locations
                foreach (Square2D currentSquare in this.UIBoard.EngineBoard.EnumerateBoard(board))
                {
                    if (currentSquare != null)
                    {
                        this.Set_And_PlaceUISquare(board, newRow, currentSquare, _verticalSquish);
                        UIBoard.Set_Square_Order(flipTheBoard, board, ref newRow, ref columnCount);
                    }
                }
            }
        }

        //TODO: I'd like to eventually move this to UIBoard, if possible
        public void Set_And_PlaceUISquare(BoardDef board, int newRow, Square2D currentSquare, int verticalSquish)
        {
            UISquare currentUISquare = this.UIBoard.GetSquare_ByLocation(currentSquare.Column, currentSquare.Row);

            if (currentUISquare != null)
            {
                int menuHeight = this.ChessGrid2D_Form.chessMenu.Height;
                int clientHeight = this.ChessGrid2D_Form.ClientSize.Height;
                int clientWidth = this.ChessGrid2D_Form.ClientSize.Width;

                this.Square_SetLocation(menuHeight, clientWidth, verticalSquish, newRow, board, currentSquare, currentUISquare);

                UISquare.Square_SetSize(clientHeight, clientWidth, verticalSquish, board, currentUISquare);
                UISquare.Square_SetPiece(currentSquare, currentUISquare);

                currentUISquare.SizeMode = PictureBoxSizeMode.CenterImage; //Centers the piece in the PictureBox

                this.Square_DoDebugStuff(currentSquare, currentUISquare);
            }
        }

        public void Square_SetLocation(int menuHeight, int width, int verticalSquish, int newRow, BoardDef boardDef, Square2D currentSquare, UISquare currentUISquare)
        {
            //Adjusts "Board Width" (Board being all the squares)
            int x = currentSquare.Column * width / boardDef.Columns;
            int y = Grid_AdjustHeight(menuHeight, newRow, boardDef, verticalSquish);

            currentUISquare.Location = new Point(x, y);
            currentUISquare.BoardLocation = currentSquare.BoardLocation; //sync up square name with engine square
        }
        private int Grid_AdjustHeight(int menuHeight, int row, BoardDef board, int verticalSquish)
        {
            //TODO:  this needs to be simplified.  Why is verticalSquish used multiple times??
            int heightAdjustment = menuHeight - verticalSquish;

            int controlsHeight = menuHeight + heightAdjustment + this.ChessGrid2D_Form.tabControl1.Height + verticalSquish;
            int chessBoardHeight = this.ChessGrid2D_Form.ClientSize.Height - controlsHeight - heightAdjustment - verticalSquish;

            int y = 0;

            y = (row * chessBoardHeight) / board.Rows;
            y = y + (heightAdjustment * row);
            y = y + menuHeight;

            return y;
        }
        public void Form_KeepSquare()
        {
            //Ensure that the client area is always square
            //TODO: this needs to account for fullscreen
            int iSize = Math.Min(this.ChessGrid2D_Form.ClientSize.Height, this.ChessGrid2D_Form.ClientSize.Width);
            this.ChessGrid2D_Form.ClientSize = new Size(iSize, iSize);
        }

        public void Toggle_BoardMode()
        {
            if (this.BoardMode == BoardMode.Standard)
            {
                this.Set_BoardMode(BoardMode.FreeForm);
            }
            else
            {
                this.Set_BoardMode(BoardMode.Standard);
            }
        }
        public void Set_BoardMode(BoardMode boardMode)
        {
            this.BoardMode = boardMode;
            this.ChessGrid2D_Form.modeButton.Text = boardMode.ToString();
        }

        #region Board Setup

        public static UIBoard SetUp_NewUIBoard()
        {
            return new UIBoard();
        }
        public void SetUp_DefaultUIBoard(GridForm formToPlaceBoard)
        {
            //TODO: Get these 2 paths from program or game config file.
            string uiDirectory = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString();
            string imagesDirectory = uiDirectory + "\\images";

            Grid2D.Check_Paths(uiDirectory, imagesDirectory);

            if (File.Exists(this.RulesFilePath))
            {
                XmlDocument rulesDocument = Config.LoadXML(this.RulesFilePath);

                if (_freeFormBoard == null)
                {
                    this.UIBoard = Grid2D.SetUp_NewUIBoard();
                    this.UIBoard.Create(this.ChessGrid2D_Form, rulesDocument, uiDirectory);
                    this.Set_BoardMode(BoardMode.Standard);
                }
                else
                {
                    this.UIBoard = _freeFormBoard;
                    this.Set_BoardMode(BoardMode.FreeForm);
                }

                Grid2D.SetUp_Common(formToPlaceBoard);
            }
        }
        public void SetUp_FreeFormBoard(GridForm formToPlaceBoard, BoardDef board, short squareSize)
        {
            this.UIBoard = Grid2D.SetUp_NewUIBoard();
            this.UIBoard.Create(formToPlaceBoard, board, squareSize);
            this.Set_BoardMode(BoardMode.FreeForm);

            Grid2D.SetUp_Common(formToPlaceBoard);
        }
        public static void SetUp_Common(GridForm formToPlaceBoard)
        {
            //Set initial size.  This also fires the resize event, which gives the form its final shape.
            formToPlaceBoard.ClientSize = new Size(formToPlaceBoard.ClientSize.Width, formToPlaceBoard.ClientSize.Height + 130);  //this value should come from 1. game config or if that is not there, then 2. Program Config
        }

        #endregion

        private void Square_DoDebugStuff(Square2D currentSquare, UISquare currentUISquare)
        {
            if (this.UIBoard.DebugMode)
            {
                if (currentUISquare.CurrentPiece == null)
                {
                    currentUISquare.Image = UISquare.CreateBitmapImage("E:" + currentSquare.BoardLocation + "\\UI:" + currentUISquare.BoardLocation, "Arial", 10);
                }
            }
        }

        #region Move this elsewhere

        //This needs to be refactored to another object
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
