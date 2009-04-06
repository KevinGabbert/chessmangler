using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace Engine.Types
{
    public class Board2D
    {
        #region Properties

        public string _findSquareName;

        List<Square2D> _squares = new List<Square2D>();
        public List<Square2D> Squares
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

        BoardDef _boardDefinition = new BoardDef();
        public BoardDef Definition
        {
            get
            {
                return _boardDefinition;
            }
            set
            {
                _boardDefinition = value;
            }
        }

        #endregion

        public Square2D GetByName(string squareName)
        {
            //look up the square by its name.  ex. name:  "A2"
            this._findSquareName = squareName.ToLower();
            Square2D foundSquare = this.Squares.Find(foundByName);

            return foundSquare;
        }
        protected bool foundByName(Square2D find)
        {
            if (find.Name == this._findSquareName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Board2D(UInt16 columns, UInt16 rows, XmlDocument startingPosition)
        {
            this.CreateBoard(columns, rows);
        }
        public Board2D(UInt16 columns, UInt16 rows)
        {
            this.CreateBoard(columns, rows);
        }

        protected void CreateBoard(UInt16 columns, UInt16 rows)
        {
            //BoardDef newBoardDef = new BoardDef();
            //newBoardDef.Columns = columns;
            //newBoardDef.Rows = rows;

            //this.InitializeSquares(newBoardDef);
            this.SquareLogicWithTestPiece(columns, rows);
        }

        protected static void SetSquareColor(Square2D squareToColor)
        {
            if (((squareToColor.Number) % 2) == 0)
            {
                squareToColor.Color = Color.Black;
            }
            else
            {
                squareToColor.Color = Color.White;
            }
        }

        public void AddNewSquare(BoardDef boardDef, int col, int row, out Square2D newlyAddedSquare)
        {
            Square2D newSquare = new Square2D();
            newSquare.Number = (boardDef.Columns * row) + col;
            newSquare.Name = (char)(97 + col) + (row + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6
            Square2D.SetColor(newSquare, boardDef.Columns, boardDef.Rows);
            
            this.Squares.Add(newSquare);
            newlyAddedSquare = newSquare;
        }

        //Expose our inner board logic so the UI can use it.
        //Remove "withTestPiece" once XML file is parsed.
        public IEnumerable SquareLogicWithTestPiece(int columns, int rows)
        {
            for (int currentRow = 0; currentRow < rows; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    BoardDef newBoardDef = new BoardDef();
                    newBoardDef.Columns = columns;
                    newBoardDef.Rows = rows;

                    Square2D newSquare;
                    this.AddNewSquare(newBoardDef, currentColumn, currentRow, out newSquare);

                    newSquare.Column = currentColumn;
                    newSquare.Row = currentRow;


                    //**** This is disposable test code, as the pieces will be set in Engine.Board (XmlDocument) *****
                    newSquare.CurrentPiece = new Piece("Rook");
                    newSquare.CurrentPiece.Image = new Bitmap(Environment.CurrentDirectory + "\\images\\wr.gif");
                    //**** This is disposable test code, as the pieces will be set in Engine.Board (XmlDocument) *****


                    yield return newSquare; //Hand out this current square to those who want it..
                }
            }
        }
    }
}
