using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using Engine.Types;
using Rules.Types;
using Rules.Interfaces;

namespace WinUIParts
{
    public class UIPiece: IConfigurablePiece
    {
        public UIPiece()
        {

        }

        public UIPiece(string name)
        {
            this.Name = name;
        }

        public Location Location
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

        public List<Color> Team
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

        public BoardAttackPackage Attacking
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

        public BoardMovementPackage Moves
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
                return this._image;
            }
            set
            {
                this._image = value;
            }
        }

        public void CheckMy_MovementRule()
        {
            throw new NotImplementedException();
        }

        public void Move(long from, long to)
        {
            throw new NotImplementedException();
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
    }
}
