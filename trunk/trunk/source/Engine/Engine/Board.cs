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

        int _position;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
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

        public static void SetColor(Square squareToColor, int row, int column)
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

        public Board(UInt16 rows, UInt16 columns)
        {
            this.CreateBoard(rows, columns);
        }
        private void CreateBoard(UInt16 rows, UInt16 columns)
        {
            this.InitializeSquares(rows, columns);
        }
        private static void SetSquareColor(Square squareToColor)
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
        private void InitializeSquares(int row, int column)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Square newSquare = new Square();
                    newSquare.Number = (i * j) + i;
                    newSquare.Name = (char)(65 + i) + (j + 1).ToString(); //What is this about?
                    Square.SetColor(newSquare, row, column);
                    this.Squares.Add(newSquare);
                }
            }
        }
    }
}
