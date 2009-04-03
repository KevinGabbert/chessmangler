using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace WinUIParts
{
    public class UISquare : PictureBox
    {
        private UISquare()
        {

        }

        public UISquare(int row, int column, int size, string picture)
        {
            //this = (UISquare) new PictureBox();
            this.MakeSquare(row, column, size, picture);
        }

        private void MakeSquare(int row, int column, int size, string picture)
        {
            this.Location = new System.Drawing.Point(row, column);
            this.Name = "Square" + (row + column).ToString();
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
