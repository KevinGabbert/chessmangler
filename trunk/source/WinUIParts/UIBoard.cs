using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using System;

using ChessMangler.Rules.Interfaces;
using ChessMangler.Engine.Interfaces;
using ChessMangler.Engine.Types;
using ChessMangler.Engine.Config;
using ChessMangler.Engine.Enums;

namespace ChessMangler.WinUIParts
{
    /// <summary>
    /// This board does not enforce rules, it simply draws the board and moves pieces where it is told to..
    /// </summary>
    public class UIBoard: IBoardMode
    {
        #region Properties

        bool _debugMode = false;
        public bool DebugMode
        {
            get
            {
                return _debugMode;
            }
            set
            {
                _debugMode = value;
            }
        }


        //TODO: perhaps later (>.5) this can be replaced by an "Orientation" prop.. (can support 4 angles)
        bool _flipped = false;
        public bool Flipped
        {
            get
            {
                return _flipped;
            }
            set
            {
                _flipped = value;
            }
        }

        int _name;
        public int Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        Squares _squares = new Squares();
        public Squares Squares
        {
            get
            {
                return _squares;
            }
            set
            {
                _squares = value;
            }
        }

        Board2D _engineBoard;
        public Board2D EngineBoard
        {
            get
            {
                return _engineBoard;
            }
            set
            {
                _engineBoard = value;
            }
        }
        public BoardMode BoardMode
        {
            get
            {
                return this.EngineBoard.BoardMode;
            }
            set
            {
                this.EngineBoard.BoardMode = value;
            }
        }

        #endregion

        #region Form Stuff

        public void Create(Form formForBoard, XmlDocument configFile, string directory)
        {
            this.EngineBoard = new Board2D(configFile, directory);
            Int16 squareSize = ConfigParser.GetSquareSize(configFile);

            //TODO:  keep these "adjustment numbers" somewhere
            formForBoard.Width = (squareSize * this.EngineBoard.Definition.Columns);
            formForBoard.Height = (squareSize * this.EngineBoard.Definition.Rows) + 50;

            this.BuildUISquares(formForBoard, this.EngineBoard.Definition, squareSize);
        }
        public void Create(Form formForBoard, BoardDef boardDef, short squareSize)
        {
            this.EngineBoard = new Board2D(boardDef);

            //TODO:  keep these "adjustment numbers" somewhere
            formForBoard.Width = (squareSize * this.EngineBoard.Definition.Columns);
            formForBoard.Height = (squareSize * this.EngineBoard.Definition.Rows) + 50;

            this.BuildUISquares(formForBoard, boardDef, squareSize);
        }

        /// <summary>
        /// All this function does is to create the UISquares that will be used in the game.
        /// Square Location, size, columns, rows, etc. are set by Grid2D.Redraw()
        /// </summary>
        /// <param name="formForBoard"></param>
        /// <param name="boardDef"></param>
        /// <param name="squareSize"></param>
        public void BuildUISquares(Form formForBoard, BoardDef boardDef, Int16 squareSize)
        {
            foreach (Square2D currentSquare in this.EngineBoard.EnumerateBoard(boardDef))
            {
                if (currentSquare != null)
                {
                    UISquare newUISquare = new UISquare();
                    UIBoard.TranslateEngineStuffToUI(currentSquare, newUISquare);

                    formForBoard.Controls.Add(newUISquare); //Place our newly built square on the grid
                    this.Squares.Add(newUISquare);
                }
            }
        }

        private void Square_DebugStuff(Square2D currentSquare, UISquare newUISquare)
        {
            if (this.DebugMode)
            {
                //newUISquare.Image = UISquare.CreateBitmapImage(currentSquare.BoardLocation + ".col" + currentSquare.Column + ".row" + currentSquare.Row, "Arial", 10);
                newUISquare.Image = UISquare.CreateBitmapImage(currentSquare.BoardLocation, "Arial", 10);
            }
        }

        #endregion
        #region Square Stuff

        //These should all map 1 to 1..
        public static void TranslateEngineStuffToUI(ISquare currentSquare, UISquare newUISquare)
        {
            if (currentSquare != null)
            {
                //This is the only code that will remain in this loop when we are done.
                newUISquare.Color = currentSquare.Color;
                newUISquare.Row = currentSquare.Row;
                newUISquare.Column = currentSquare.Column;
                newUISquare.Disabled = currentSquare.Disabled;
                newUISquare.BoardLocation = currentSquare.BoardLocation;
                //newUISquare.Image = currentSquare.Image;

                if (currentSquare.CurrentPiece != null)
                {
                    newUISquare.CurrentPiece = currentSquare.CurrentPiece; //Ah, the power of interfaces..
                }
            }
        }

        /// <summary>
        ///             //This is what we use to impose an order on the UISquares.  (The Square2D List is an unordered list)
        /// </summary>
        /// <param name="flipTheBoard"></param>
        /// <param name="board"></param>
        /// <param name="newRow"></param>
        /// <param name="columnCount"></param>
        public static void Set_Square_Order(bool flipTheBoard, BoardDef board, ref int newRow, ref int columnCount)
        {
            if (!flipTheBoard)
            {
                if (++columnCount > board.Columns - 1)
                {
                    columnCount = 0;
                    newRow++;
                }
            }
            else
            {
                if (--columnCount < 0)
                {
                    columnCount = board.Columns - 1;
                    newRow--;
                }
            }
        }
        public void ClearSquare(UISquare squareToClear, bool clearImage)
        {
            //This UISquare is actually a ref to square on a form.. but doesn't *have* to be..
            UISquare formSquare = this.GetByXY(squareToClear.X, squareToClear.Y);
            squareToClear.CurrentPiece = null;

            if (clearImage)
            {
                squareToClear.Image = null;
            }
        }
        public void SetImage(UISquare squareToSet, IPiece piece)
        {
            //This UISquare is actually a ref to square on a form.. but doesn't *have* to be..
            UISquare formSquare = this.GetByXY(squareToSet.X, squareToSet.Y);
            squareToSet.CurrentPiece = piece;
        }

        #region GetByBoardLocation

        int _findRow;
        int _findCol;
        public UISquare GetSquare_ByLocation(int row, int column)
        {
            this._findRow = row;
            this._findCol = column;
            UISquare foundSquare = this.Squares.Find(foundByLocation);

            return foundSquare;
        }
        protected bool foundByLocation(UISquare find)
        {
            if ((find.Row == this._findRow) & (find.Column == this._findCol))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        #region GetByXY
        protected int _findX;
        protected int _findY;

        public UISquare GetByXY(int x, int y)
        {
            this._findX = x;
            this._findY = y;
            UISquare foundSquare = this.Squares.Find(foundByXY);

            return foundSquare;
        }
        protected bool foundByXY(UISquare find)
        {
            //Under Construction
            //This function finishes when it finds its first TRUE

            //if the pointer is *inside* a Square, then return that square.

            int XMult = (find.X * (find.X + 1));
            int YMult = (find.Y * (find.Y + 1));

            bool inRow = (find.X <= this._findX - XMult) & (find.X + XMult <= this._findX);

            bool grZeroY = ((this._findY - YMult) >= 0);
            bool inCol = (find.Y <= this._findY - YMult) & (grZeroY) & (find.Y + YMult <= this._findY);


            if (inRow & inCol)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
    }
}
