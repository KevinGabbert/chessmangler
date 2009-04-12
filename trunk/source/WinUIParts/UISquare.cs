using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
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
                this.Image = value.Image;
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

        //This will probably need to be refactored to a generic UI library
        public static Bitmap CreateBitmapImage(string sImageText)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create the Font object for the image text drawing.
            Font objFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
            objGraphics.Flush();

            return (objBmpImage);
        }
    }
}
