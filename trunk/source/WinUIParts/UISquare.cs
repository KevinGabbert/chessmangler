using System;
using System.Collections.Generic;

using System.Text;

using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using ChessMangler.Engine.Interfaces;
using ChessMangler.Rules.Interfaces;

namespace ChessMangler.WinUIParts
{
    /// <summary>
    /// This object simply does what is needed to create a square and stick in a piece image 
    /// </summary>
    public class UISquare : PictureBox, ISquare 
    {
        protected const string SQUARE = "Square";
        
        protected UISquare()
        {
            //this.BorderStyle = BorderStyle.None;
        }

        public UISquare(Point formlocation, int size)
        {
            this.MakeSquare(formlocation, size);
            this.AllowDrop = true;
        }

        #region Properties
        #region ISquare Members

        protected short _squareSize;
        public short SquareSize
        {
            get
            {
                return _squareSize;
            }
            set
            {
                _squareSize = value;
            }
        }

        protected int _x;
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        protected int _y;
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
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

        protected string _boardLocation;
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

                //TODO: Find Engine Square and place piece

                if (value != null)
                {
                    this.Image = value.Image;
                }
                else
                {
                    this.Image = null;
                }
            }
        }

        #endregion
        #region Drag & Drop
        //Microsoft.. WHY do I have to override a virtual method on this???
        public override bool AllowDrop 
        { 
            get; 
            set; 
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbEvent)
        {
            // Allow us to use our own cursors when dragging
            gfbEvent.UseDefaultCursors = false; //This could theoretically be in a config file somewhere.
        }

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
        public static Bitmap CreateBitmapImage(string sImageText, string fontName, int fontSize)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create the Font object for the image text drawing.
            Font objFont = new Font(fontName, fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            //This is where the bitmap size is determined.
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
