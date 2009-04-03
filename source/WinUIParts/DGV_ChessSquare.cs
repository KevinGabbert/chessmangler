using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;

//using System.Windows.Media;


using Rules.Interfaces;

namespace WinUIParts
{
    public class DGV_ChessSquare : DataGridViewImageCell
    {
        public bool squareColor = false;

        public DGV_ChessSquare()
        {
            this.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        protected override object GetFormattedValue(object value,
           int rowIndex, ref DataGridViewCellStyle cellStyle,
           TypeConverter valueTypeConverter,
           TypeConverter formattedValueTypeConverter,
           DataGridViewDataErrorContexts context)
        {
            IConfigurablePiece pieceOnSquare;

            // Try to get the piece value from the containing column
            DGV_ChessColumn owningCol = this.OwningColumn as DGV_ChessColumn;
            

            if (owningCol != null)
            {
                pieceOnSquare = owningCol.ChessPiece;
            }
            if (value is IConfigurablePiece || value is int)
            {
                pieceOnSquare = (IConfigurablePiece)value;
            }

            Image img = null;
            string path;
            bool exists;

            if (this.squareColor)
            {
                path = Environment.CurrentDirectory + "\\images\\wr.gif";
                exists = File.Exists(path);
            }
            else
            {
                path = Environment.CurrentDirectory + "\\images\\bp.gif";
                exists = File.Exists(path);
            }

            if (exists)
            {
                img = new System.Drawing.Bitmap(path);
            }
            else
            {

            }

            //move this. This only needs to be done once per column
            owningCol.Width = 45; // img.Width;
            //this.OwningRow.Height = 61; // img.Height;

            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            color_square(cellStyle);

            Padding x = new Padding(2);
            cellStyle.Padding = Padding.Add(x, x);

            return img;
        }

        private void color_square(DataGridViewCellStyle cellStyle)
        {
            if (this.squareColor)
            {
                cellStyle.BackColor = System.Drawing.Color.Black; //assign based on location
            }
            else
            {
                cellStyle.BackColor = System.Drawing.Color.White; //assign based on location
            }

            this.squareColor = !(this.ColumnIndex % 2 == 0);
        }
    }
}