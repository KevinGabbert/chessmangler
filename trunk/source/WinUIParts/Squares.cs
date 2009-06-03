using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace ChessMangler.WinUIParts
{
    public class Squares : List<UISquare>
    {
        bool _enabled = false;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        public void Disable()
        {
            this.Enabled = false;
            this.EnableSquares(this.Enabled);
        }
        public void Enable()
        {
            this.Enabled = true;
            this.EnableSquares(this.Enabled);
        }

        //fie, fie, how franticly I square my talk!
        private delegate void SquareDelegate(bool enabled);
        public void EnableSquares(bool enabled)
        {
            foreach (UISquare square in this)
            {
                if (square.InvokeRequired)
                {
                    square.Invoke(new SquareDelegate(this.EnableSquares), enabled);
                }
                else
                {
                    square.Enabled = enabled;
                }
            }
        }
        public void LockMovement(UISquare startSquare, UISquare endSquare)
        {
            foreach (UISquare square in this)
            {
                if ((square != startSquare) && (square != endSquare))
                {
                    square.AllowDrop = false;
                }
            }
        }

        public void UnlockAll()
        {
            foreach (UISquare square in this)
            {
               square.AllowDrop = true;
            }
        }

        public void ResetColors()
        {
            //TODO: this doesn't work very well
            foreach (UISquare square in this)
            {
                if ((square.PreviousColor == Color.LightBlue) || (square.PreviousColor == Color.Blue))
                {
                    square.Color = square.PreviousColor;
                }
            }
        }
    }
}
