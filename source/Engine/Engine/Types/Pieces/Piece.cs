using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

using Engine.Types.Rules;
using Engine.Interfaces;

namespace Engine.Types
{
    public class Piece: IPiece
    {
        public Piece(string name)
        {
            this.Name = name;

            //Look up attributes in passed config file and assign (such as image, and movement rules, etc..)
        }

        protected PieceDef _pieceDef = new PieceDef();
        public PieceDef Definition
        {
            get
            {
                return _pieceDef;
            }
            set
            {
                _pieceDef = value;
            }
        }

        protected int _row;
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        protected int _column;
        public int Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }

        protected string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
            }
        }

        protected int _player;
        public int Player
        {
            get
            {
                return this.Definition.Player;
            }
            set
            {
                this.Definition.Player = value;
            }
        }

        public Color Color
        {
            get
            {
                return this.Definition.Color;
            }
            set
            {
                this.Definition.Color = value;
            }
        }

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

        protected Image _image;
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        protected Movement _movement;
        public Movement Movement
        {
            get
            {
                return _movement;
            }
            set
            {
                _movement = value;
            }
        }

        protected Capture _capture;
        public Capture Capture
        {
            get
            {
                return _capture;
            }
            set
            {
                _capture = value;
            }
        }
    }
}
