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

        public int Row
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

        public int Column
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

        public int Number
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

        public Color Color
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
