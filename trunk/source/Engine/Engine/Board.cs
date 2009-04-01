using System;
using System.Drawing;
using Pieces.Interfaces;
using System.Collections.Generic;
using System.Collections;

namespace Engine.Board
{
    //public class Square
    //{
    //    public Square(UInt16 x, UInt16 y)
    //    {
    //        UInt16[] position = new UInt16[2] { x, y };
    //    }

    //    Color _color;
    //    int _number;
    //    string _name;
    //    IConfigurablePiece _piece;

    //    public string Name
    //    {
    //        get
    //        { 
    //            return _name; 
    //        }
    //        set
    //        { 
    //            _name = value; 
    //        }
    //    }
    //    public int Number
    //    {
    //        get
    //        { 
    //            return _number; 
    //        }
    //        set
    //        { 
    //            _number = value; 
    //        }
    //    }
    //    public Color Color
    //    {
    //        get
    //        {
    //            return _color;
    //        }
    //        set
    //        {
    //            _color = value;
    //        }
    //    }
    //    public IConfigurablePiece Piece
    //    {
    //        get
    //        { 
    //            return _piece; 
    //        }
    //        set
    //        { 
    //            _piece = value; 
    //        }
    //    }
    //}

    public class Square
    {
        public Square()
        {
            
        }

        Color _color;
        int _number;
        string _name;
        IConfigurablePiece _piece;

        int[] _position = new int[2]{0,0};
        public int x
        {
            get
            {
                return _position[0];
            }
            set
            {
                _position[0] = value;
            }
        }
        public int y
        {
            get
            {
                return _position[1];
            }
            set
            {
                _position[1] = value;
            }
        }
        public string Name
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
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        public IConfigurablePiece Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
            }
        }

        public static void SetColor(Square squareToColor, int column, int row)
        {
            if (((squareToColor.Number) % 2) == 0)
            {
                squareToColor.Color = Color.White;
            }
            else
            {
                squareToColor.Color = Color.Black;
            }
        }
    }

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
            this._findSquareName = squareName;
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
                    newSquare.Number = (column * i) + j; //corrected equation... my bad.
                    newSquare.Name = (char)(97 + j) + (i + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6
                    Square.SetColor(newSquare, column, row);
                    this.Squares.Add(newSquare);
                }
            }
        }
    }
}
