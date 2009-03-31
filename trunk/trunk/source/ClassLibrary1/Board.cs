using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Board
{
    public class Square
    {
        public Square(int x, int y)
        {
            int[] position = new int[2] { x, y };
        }

        int _color;
        int _piece;
        int _number;
        string _name;

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
        public int Color
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
        public int Piece
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
    }

    public class board
    {
        board(int x, int y)
        {
            // constructor takes two ints, makes the board
            Square[,] board = new Square[x, y];

            //initialize all squares created

            for (int row = 0; row == x; row++)
            {
                for (int column = 0; column == y; column++)
                {
                    board[row, column] = new Square(row, column);
                    board[row, column].Piece = 0;
                    board[row, column].Number = (x * y) + x;
                    board[row, column].Name = (char)(97 + row) + column.ToString();

                    if (((board[row, column].Number) % 2) == 0)
                    {
                        board[row, column].Color = 1;
                    }
                    else
                    {
                        board[row, column].Color = 0;
                    }
                }
            }
        }
    }
}
