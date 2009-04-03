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

        // Had a big mistake here, mine, you followed suit.  X axis should be columns,
        // as in the a column, b column, etc.. rows or ranks are Y axis, and head upwards.
        // All references to either (row, column) or (rows, columns) have been flipped
        // because of this error.  Also, corrected some bad math on my part as far as 
        // correctly labeling each square with their number... that could be very important
        // as their color is based on that and movement could be as well.

        public Board(UInt16 columns, UInt16 rows)
        {
            this.CreateBoard(columns, rows);
        }
        private void CreateBoard(UInt16 columns, UInt16 rows)
        {
            this.InitializeSquares(columns, rows);
        }

        // corrected this as well, the lowest left square should be black, and will be 0,0..
        // 0,1 next to it is white, so I switched the definitions below to reflect that correctly.
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
        private void InitializeSquares(int column, int row)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Square newSquare = new Square();
                    newSquare.Number = (column * i) + j; 
                    newSquare.Name = (char)(97 + j) + (i + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6
                    Square.SetColor(newSquare, column, row); //square.SetColor can go back to using 1's and zeroes for its squarecolor.  There is no need for that color crap here.  I don't know what I was thinking..
                    this.Squares.Add(newSquare);
                }
            }
        }
    }   
}
