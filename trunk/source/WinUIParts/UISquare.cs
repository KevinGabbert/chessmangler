using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

using Engine.Interfaces;
using Rules.Interfaces;

namespace WinUIParts
{
    /// <summary>
    /// This object simply does what is needed 
    /// </summary>
    public class UISquare : PictureBox, ISquare 
    {
        protected const string SQUARE = "Square";
        
        protected UISquare()
        {

        }

        public UISquare(Point formlocation, int size)
        {
            this.MakeSquare(formlocation, size);
        }

        #region Properties
        #region ISquare Members

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

        public Color Color
        {
            get
            {
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;
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

        #endregion

        protected IChessObject _piece;
        public IChessObject Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
                this.Image = value.Image;
            }
        }

        public IPiece CurrentPiece
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


        #region IChessObject Members

        int IChessObject.Row
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

        int IChessObject.Column
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

        string IChessObject.Name
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

        int IChessObject.Number
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

        Color IChessObject.Color
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

        bool IChessObject.Disabled
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

        Image IChessObject.Image
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

        #endregion

        #endregion

        private void MakeSquare(Point formLocation, int size)
        {
            this.Location = formLocation;
            this.Name = SQUARE + (formLocation.X + formLocation.Y).ToString();
            this.Size = new System.Drawing.Size(size, size); //this *is* a square after all

            //it would be nice to enable keyboard support (depending on an option in the config file)
            //newBox.pictureBox1.TabIndex = 0;
            //newBox.pictureBox1.TabStop = false;

            //What is this square's address?
            //When you get that, then go look up the address in the config file's Starting position for this board.


            //this.Piece = piece;
            //this.Image = this.Piece.Image;
            this.SizeMode = PictureBoxSizeMode.CenterImage;
            this.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
