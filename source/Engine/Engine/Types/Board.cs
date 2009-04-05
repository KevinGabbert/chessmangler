using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;

namespace Engine.Types
{
    public class Board
    {
        #region Properties

        public string _findSquareName;

        List<Square> _squares = new List<Square>();
        public List<Square> Squares
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

        public Square GetByName(string squareName)
        {
            //look up the square by its name.  ex. name:  "A2"
            this._findSquareName = squareName.ToLower();
            Square foundSquare = this.Squares.Find(foundByName);

            return foundSquare;
        }
        private bool foundByName(Square find)
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

        public Board(UInt16 columns, UInt16 rows)
        {
            this.CreateBoard(columns, rows);
        }
        private void CreateBoard(UInt16 columns, UInt16 rows)
        {
            BoardDef newBoardDef = new BoardDef();
            newBoardDef.Columns = columns;
            newBoardDef.Rows = rows;

            this.InitializeSquares(newBoardDef);
        }

        private static void SetSquareColor(Square squareToColor)
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

        public void InitializeSquares(BoardDef boardDef)
        {
            foreach (BoardIterator currentSquare in Board.SquarePositionLogic(boardDef.Columns, boardDef.Rows))
            {
                this.AddNewSquare(boardDef, currentSquare.Col, currentSquare.Row);
            }
        }

        private void AddNewSquare(BoardDef boardDef, int col, int row)
        {
            Square newSquare = new Square();
            newSquare.Number = (boardDef.Columns * row) + col;
            newSquare.Name = (char)(97 + col) + (row + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6
            Square.SetColor(newSquare, boardDef.Columns, boardDef.Rows); //square.SetColor can go back to using 1's and zeroes for its squarecolor.  There is no need for that color crap here.  I don't know what I was thinking..
            
            this.Squares.Add(newSquare);
        }

        //Expose our inner board logic so the UI can use it.
        //Right now we are only exposing board coordinates, but soon we will expose the piece as well
        //This function should probably be copied (as some functions might just want to iterate the board only?
        public static IEnumerable SquarePositionLogic(int columns, int rows)
        {
            for (int currentRow = 0; currentRow < rows; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    //Replace BoardIterator with a a Square object
                    //so we can expose the Square object (so the UI board can get all that data too!
                    BoardIterator returnIterator = new BoardIterator();
                    returnIterator.Col = currentColumn;
                    returnIterator.Row = currentRow;

                    //Add a new square here (once you add it, then change the name of this function to SquareLogic)
                    //this.AddNewSquare(boardDef, currentSquare.Col, currentSquare.Row);

                    yield return returnIterator;
                }
            }
        }
    }
}
