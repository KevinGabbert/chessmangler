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

            //Use our good friend BoardEnumerator to help us find all the squares on the board, and reset their locations

            if (this.UIBoard != null)
            {
                int newRow = 0;
                int columnCount = 0;

                BoardDef board = this.UIBoard.EngineBoard.Definition;
                foreach (Square2D currentSquare in this.UIBoard.EngineBoard.BoardEnumerator(board))
                {
                    UISquare currentUISquare = this.UIBoard.GetSquare_ByLocation(currentSquare.Column, currentSquare.Row);

                    if (currentUISquare != null)
                    {
                        //Can these go into UISquare?
                        this.Square_SetLocation(newRow, board, currentSquare, currentUISquare);
                        this.Square_SetSize(board, currentUISquare);
                        Grid2D.Square_SetPiece(currentSquare, currentUISquare);

                        this.Square_DoDebugStuff(currentSquare, currentUISquare);
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

        public void Square_SetSize(BoardDef board, UISquare currentUISquare)
        {
            currentUISquare.Height = (this.ChessGrid2D_Form.ClientSize.Height / board.Columns) - _adjust2;
            currentUISquare.Width = (this.ChessGrid2D_Form.ClientSize.Width) / board.Rows;
        }
        public void Square_SetLocation(int newRow, BoardDef boardDef, Square2D currentSquare, UISquare currentUISquare)
        {
            //Adjusts "Board Width" (Board being all the squares)
            int x = currentSquare.Column * this.ChessGrid2D_Form.ClientSize.Width / boardDef.Columns;
            int y = Grid_AdjustHeight(newRow, boardDef);

            currentUISquare.Location = new Point(x, y);
            currentUISquare.BoardLocation = currentSquare.BoardLocation; //sync up square name with engine square
        }
        private static void Square_SetPiece(Square2D currentSquare, UISquare currentUISquare)
        {
            currentUISquare.CurrentPiece = currentSquare.CurrentPiece;

            //pull a new piece image from a cached image
            //currentUISquare.CurrentPiece.Image = new Bitmap(directory + "\\images\\" + currentSquare.CurrentPiece.Name);
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
