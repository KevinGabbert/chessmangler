using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

using Engine.Interfaces;

namespace Engine.Types
{
    public class Piece: IPiece
    {
        public Piece(string name)
        {
            this.Name = name;
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
    }
}
