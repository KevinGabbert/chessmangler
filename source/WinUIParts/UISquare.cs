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
    public class UISquare : PictureBox, ISquare 
    {
        private UISquare()
        {

        }

        public UISquare(Point formlocation, int size, string picture)
        {
            this.MakeSquare(formlocation, size, picture);
        }

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
        public int Col
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
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        protected IConfigurablePiece _piece;
        public IConfigurablePiece Piece
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

        private void MakeSquare(Point formlocation, int size, string picture)
        {
            this.Location = formlocation;
            this.Name = "Square" + (formlocation.X + formlocation.Y).ToString();
            this.Size = new System.Drawing.Size(size, size); //this *is* a square after all

            //it would be nice to enable keyboard support (depending on an option in the config file)
            //newBox.pictureBox1.TabIndex = 0;
            //newBox.pictureBox1.TabStop = false;

            this.Image = new Bitmap(picture);
            this.SizeMode = PictureBoxSizeMode.CenterImage;
            this.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
