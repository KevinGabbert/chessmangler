using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using ChessMangler.Engine.Interfaces;

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

        string _boardLocation;
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

        string _imageDirectory;
        public string ImageDirectory
        {
            get
            {
                return _imageDirectory;
            }
            set
            {
                _imageDirectory = value;
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
