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
            //BoardDef newBoardDef = new BoardDef();
            //newBoardDef.Columns = columns;
            //newBoardDef.Rows = rows;

            //this.InitializeSquares(newBoardDef);
            this.SquareLogic(columns, rows);
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

        private void AddNewSquare(BoardDef boardDef, int col, int row, out Square newlyAddedSquare)
        {
            Square newSquare = new Square();
            newSquare.Number = (boardDef.Columns * row) + col;
            newSquare.Name = (char)(97 + col) + (row + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6
            Square.SetColor(newSquare, boardDef.Columns, boardDef.Rows); //square.SetColor can go back to using 1's and zeroes for its squarecolor.  There is no need for that color crap here.  I don't know what I was thinking..
            
            this.Squares.Add(newSquare);

            newlyAddedSquare = newSquare;
        }

        //Expose our inner board logic so the UI can use it.
        public IEnumerable SquareLogic(int columns, int rows)
        {
            for (int currentRow = 0; currentRow < rows; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < columns; currentColumn++)
                {
                    BoardDef newBoardDef = new BoardDef();
                    newBoardDef.Columns = columns;
                    newBoardDef.Rows = rows;

                    Square newSquare;
                    this.AddNewSquare(newBoardDef, currentColumn, currentRow, out newSquare);

                    newSquare.Col = currentColumn;
                    newSquare.Row = currentRow;

                    yield return newSquare; //Hand out this current square to those who want it..
                }
            }
        }
    }
}
