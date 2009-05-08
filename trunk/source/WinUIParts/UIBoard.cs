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
        public UIBoard(int columnStart, int rowStart, int topMenuOffset)
        {
            _currentColumn = columnStart;
            _currentRow = rowStart;
            _topMenuOffset = topMenuOffset;
        }

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

        List<UISquare> _squares = new List<UISquare>();
        public List<UISquare> Squares
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

        BoardMode _boardMode = new BoardMode();
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

        int _topMenuOffset = 0;
        int _currentColumn = 0;
        int _currentRow = 0;

        public void CreateBoard(Form formForBoard, XmlDocument configFile, string directory)
        {
            this.EngineBoard = new Board2D(configFile, directory);
            Int16 squareSize = ConfigParser.GetSquareSize(configFile);

            //TODO:  keep these "adjustment numbers" somewhere
            formForBoard.Width = (squareSize * this.EngineBoard.Definition.Columns);
            formForBoard.Height = (squareSize * this.EngineBoard.Definition.Rows) + _topMenuOffset + 50;

            this.BuildUISquares(formForBoard, this.EngineBoard.Definition, squareSize);
        }
        public void CreateBoard(Form formForBoard, BoardDef boardDef, short squareSize)
        {
            this.EngineBoard = new Board2D(boardDef);

            //TODO:  keep these "adjustment numbers" somewhere
            formForBoard.Width = (squareSize * this.EngineBoard.Definition.Columns);
            formForBoard.Height = (squareSize * this.EngineBoard.Definition.Rows) + _topMenuOffset + 50;

            this.BuildUISquares(formForBoard, boardDef, squareSize);
        }

        protected void BuildUISquares(Form formForBoard, BoardDef boardDef, Int16 squareSize)
        {
            //Use board logic to iterate through the board.
            //Translates Engine stuff to UI Stuff

            //Hmmmmm.. the alternative is to do a for..i.. using BoardDef and GetByLocation, but SquareLogic already does this!, also, this allows for less complication in the UI..
            foreach (Square2D currentSquare in this.EngineBoard.SquareLogic(boardDef))
            {
                UISquare newUISquare = new UISquare(new Point(_currentColumn, _currentRow), squareSize);

                if (this.DebugMode)
                {
                    //newUISquare.Image = UISquare.CreateBitmapImage(currentSquare.BoardLocation + ".col" + currentSquare.Column + ".row" + currentSquare.Row, "Arial", 10);
                    newUISquare.Image = UISquare.CreateBitmapImage(currentSquare.BoardLocation, "Arial", 10);
                }

                //xfer variables over
                newUISquare.X = _currentColumn;
                newUISquare.Y = _currentRow;
                newUISquare.SquareSize = squareSize;

                UIBoard.TranslateEngineStuffToUI(currentSquare, newUISquare);

                formForBoard.Controls.Add(newUISquare); //Place our newly built square on the grid
                this.Squares.Add(newUISquare);


                //**** This is disposable test code, as the UI SquarePositions will be set in Engine.Board (XmlDocument) *****

                //They will??  Oh yes.. They will need to be if we are going to have custom boards, such as a chess board in
                //the shape of a triangle or something..

                //Set the position of our new square to be drawn
                if (currentSquare.Column == boardDef.Columns - 1)
                {
                    _currentRow = _currentRow + squareSize;
                    _currentColumn = 0 - squareSize;
                }

                _currentColumn = _currentColumn + squareSize;
                //**** This is disposable test code, as the UI SquarePositions will be set in Engine.Board (XmlDocument) *****
            }
        }

        #endregion
        #region Square Stuff

        //These should all map 1 to 1..
        public static void TranslateEngineStuffToUI(ISquare currentSquare, UISquare newUISquare)
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
        public UISquare GetByBoardLocation(int row, int column)
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
