using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using ChessMangler.Engine.Enums;
using ChessMangler.Engine.Types;


namespace ChessMangler.WinUIParts.ChessGrid2D
{
    /// <summary>
    /// 
    /// </summary>
    public class Grid2D : ChessGrid2D_Base
    {
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

        #endregion

        public Grid2D(Form formWithGrid)
        {
            this.ChessGrid2D_Form = (ChessGrid2D_Form)formWithGrid;
            this.DebugForm = new DebugForm();
        }

        int _adjust2 = 20;
        public void Redraw()
        {
            if (this.ConstrainProportions)
            {
                this.Form_KeepSquare();
            }

            //Use our good friend SquareLogic to help us find all the squares on the board, and reset their locations

            if (this.UIBoard != null)
            {
                int newRow = 0;
                int columnCount = 0;

                BoardDef board = this.UIBoard.EngineBoard.Definition;
                foreach (Square2D currentSquare in this.UIBoard.EngineBoard.BoardEnumerator(board))
                {
                    UISquare currentUISquare = this.UIBoard.GetByBoardLocation(currentSquare.Column, currentSquare.Row);

                    if (currentUISquare != null)
                    {
                        this.Set_SquareLocation(newRow, board, currentSquare, currentUISquare);
                        this.Set_SquareSize(board, currentUISquare);
                        Grid2D.Set_SquarePiece(currentSquare, currentUISquare);

                        this.Do_DebugStuff(currentSquare, currentUISquare);
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

        private void Set_SquareSize(BoardDef board, UISquare currentUISquare)
        {
            currentUISquare.Height = (this.ChessGrid2D_Form.ClientSize.Height / board.Columns) - _adjust2;
            currentUISquare.Width = (this.ChessGrid2D_Form.ClientSize.Width) / board.Rows;
        }
        public void Set_SquareLocation(int newRow, BoardDef board, Square2D currentSquare, UISquare currentUISquare)
        {
            //Adjusts "Board Width" (Board being all the squares)
            int x = currentSquare.Column * this.ChessGrid2D_Form.ClientSize.Width / board.Columns;
            int y = Grid_AdjustHeight(newRow, board);

            currentUISquare.Location = new Point(x, y);
        }
        private static void Set_SquarePiece(Square2D currentSquare, UISquare currentUISquare)
        {
            currentUISquare.CurrentPiece = currentSquare.CurrentPiece;
        }

        private int Grid_AdjustHeight(int row, BoardDef board)
        {
            int heightAdjustment = this.ChessGrid2D_Form.statusBar.Height - _adjust2;//unknown why I need this.  is this a total of cumulative errors??
            
            int controlsHeight = this.ChessGrid2D_Form.chessMenu.Height + heightAdjustment + this.ChessGrid2D_Form.tabControl1.Height + _adjust2;
            int chessBoardHeight = this.ChessGrid2D_Form.ClientSize.Height - controlsHeight - heightAdjustment - _adjust2;

            int y = 0;

            y = (row * chessBoardHeight) / board.Rows;
            y = y + (heightAdjustment * row);
            y = y + this.ChessGrid2D_Form.chessMenu.Height; //adding in the chessMenu Height controls where the grid begi

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
            return new UIBoard(0, 0, 0);
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
                    this.UIBoard.CreateBoard(this.ChessGrid2D_Form, rulesDocument, uiDirectory);
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
            this.UIBoard.CreateBoard(formToPlaceBoard, board, squareSize);
            this.Set_BoardMode(BoardMode.FreeForm);

            Grid2D.SetUp_Common(formToPlaceBoard);
        }
        public static void SetUp_Common(GridForm formToPlaceBoard)
        {
            formToPlaceBoard._squareHandlers.Add_Required_Square_Handlers(formToPlaceBoard, formToPlaceBoard.Grid.DebugForm);

            //Set initial size.  This also fires the resize event, which gives the form its final shape.
            formToPlaceBoard.ClientSize = new Size(formToPlaceBoard.ClientSize.Width, formToPlaceBoard.ClientSize.Height + 130);  //this value should come from 1. game config or if that is not there, then 2. Program Config
        }

        #endregion

        private void Do_DebugStuff(Square2D currentSquare, UISquare currentUISquare)
        {
            if (this.UIBoard.DebugMode)
            {
                if (currentUISquare.CurrentPiece == null)
                {
                    currentUISquare.Image = UISquare.CreateBitmapImage(currentSquare.BoardLocation, "Arial", 25);
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
