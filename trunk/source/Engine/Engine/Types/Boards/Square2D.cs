using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using ChessMangler.Engine.Interfaces;

namespace ChessMangler.Engine.Types
{
    public class Square2D: ISquare
    {
        public Square2D()
        {

        }

        Color _color;
        string _boardLocation;
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

        protected bool _disabled;
        public bool Disabled
        {
            get
            {
                return _disabled;
            }
            set
            {
                _disabled = value;
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

        public string BoardLocation
        {
            get
            {
                return _boardLocation;
            }
            set
            {
                _boardLocation = value;
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
                squareToColor.Color = Color.Gray; //This will be set in config
            }
            else
            {
                squareToColor.Color = Color.White; //This will be set in config
            }
        }
    }
}
