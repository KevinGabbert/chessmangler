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
            this.InitializeSquares(columns, rows);
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


        public void InitializeSquares(int column, int row)
        {
            foreach (BoardIterator currentSquare in Board.SquarePositionLogic(column, row))
            {
                this.AddNewSquare(column, row, currentSquare.Row, currentSquare.Col);
            }
        }

        private void AddNewSquare(int column, int row, int r, int c)
        {
            Square newSquare = new Square();
            newSquare.Number = (column * r) + c;
            newSquare.Name = (char)(97 + c) + (r + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6
            Square.SetColor(newSquare, column, row); //square.SetColor can go back to using 1's and zeroes for its squarecolor.  There is no need for that color crap here.  I don't know what I was thinking..
            this.Squares.Add(newSquare);
        }

        //Expose our inner board logic so the UI can use it.
        public static IEnumerable SquarePositionLogic(int column, int row)
        {
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    BoardIterator returnIterator = new BoardIterator();
                    returnIterator.Col = c;
                    returnIterator.Row = r;

                    yield return returnIterator;
                }
            }
        }
    }
}
