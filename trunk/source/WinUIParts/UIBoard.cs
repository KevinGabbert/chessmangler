using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using Rules.Interfaces;
using Engine.Interfaces;
using Engine.Types;


namespace WinUIParts
{
    /// <summary>
    /// This board does not enforce rules, it simply draws the board and moves pieces where it is told to..
    /// </summary>
    public class UIBoard
    {
        #region Properties

        int _rows;
        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        int _columns;
        public int Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                _columns = value;
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

        #endregion

        public void CreateBoard(Form formForBoard, ushort rows, ushort columns, int squareSize, XmlDocument startingPosition)
        {
            //pull the following info from XMLDocument

            formForBoard.Width = (squareSize * columns) + 12;
            formForBoard.Height = (squareSize * rows) + 30;

            this.EngineBoard = new Board2D(columns, rows, startingPosition);
            this.BuildUISquares(formForBoard, rows, columns, squareSize);
        }


        private void BuildUISquares(Form formForBoard, int rows, int columns, int squareSize)
        {
            int squareR = 0;
            int squareC = 0;

            //Use board logic to iterate through the board.
            //Translates Engine stuff to UI Stuff
            foreach (Square2D currentSquare in this.EngineBoard.SquareLogicWithTestPiece(columns, rows))
            {
                #region Temporary UI Code
                ////**** This is disposable test code, as the pieces will be set in Engine.Board (XmlDocument) *****
                //UIPiece newUIPiece = new UIPiece("Rook");
                //newUIPiece.Image = new Bitmap(Environment.CurrentDirectory + "\\images\\wr.gif");

                UISquare newUISquare = new UISquare(new Point(squareR, squareC), squareSize);
                //**** This is disposable test code, as the Squares will be set in Engine.Board (XmlDocument) *****

                #endregion

                UIBoard.TranslateEngineStuffToUI(currentSquare, newUISquare);

                formForBoard.Controls.Add(newUISquare); //Place our newly built square on the grid

                //**** This is disposable test code, as the UI SquarePositions will be set in Engine.Board (XmlDocument) *****
                //Set the position of our new square to be drawn
                if (currentSquare.Column == columns - 1)
                {
                    squareC = squareC + squareSize;
                    squareR = 0 - squareSize;
                }

                squareR = squareR + squareSize;
                //**** This is disposable test code, as the UI SquarePositions will be set in Engine.Board (XmlDocument) *****
            }
        }

        //These should all map 1 to 1..
        protected static void TranslateEngineStuffToUI(ISquare currentSquare, UISquare newUISquare)
        {
            //This is the only code that will remain in this loop when we are done.
            newUISquare.Color = currentSquare.Color;
            newUISquare.Piece = currentSquare.CurrentPiece; //Ah, the power of interfaces..
        }
    }
}
