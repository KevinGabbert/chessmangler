using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Engine.Interfaces;

namespace Engine.Types
{
    public class Square2D: ISquare
    {
        public Square2D()
        {

        }

        Color _color;
        int _number;
        string _name;
        int[] _position = new int[2] { 0, 0 };

        public int Row
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
        public int Column
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

        protected IPiece _currentPiece;
        public IPiece CurrentPiece
        {
            get
            {
                return _currentPiece;
            }
            set
            {
                _currentPiece = value;
            }
        }

        //Not implemented for Square in 1.0
        public bool Disabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //Not implemented for Square in 1.0
        public Image Image
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
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

        public static void SetCheckerboardStyle(Square2D squareToColor, int column, int row)
        {
            
                    if (((column + row) % 2) == 0)
                    {
                        squareToColor.Color = Color.White;
                    }
                    else
                    {
                        squareToColor.Color = Color.Gray;
                   
            }
        }
    }
}
