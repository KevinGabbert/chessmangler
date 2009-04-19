using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace ChessMangler.Engine.Types
{
    public class PieceDef
    {
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

        string _startingLocation;
        public string StartingLocation
        {
            get
            {
                return _startingLocation;
            }
            set
            {
                _startingLocation = value;
            }
        }

        string _imageName;
        public string ImageName
        {
            get
            {
                return _imageName;
            }
            set
            {
                _imageName = value;
            }
        }

        protected int _player;
        public int Player
        {
            get
            {
                return _player;
            }
            set
            {
                this._player = value;
            }
        }

        protected Color _color;
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                this._color = value;
            }
        }
    }
}
