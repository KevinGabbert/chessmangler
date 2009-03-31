using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;

using Pieces.Interfaces;

namespace WinUIParts
{
    public class ChessSquare : DataGridViewImageCell
    {
        public ChessSquare()
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
            ChessColumn owningCol = this.OwningColumn as ChessColumn;

            if (owningCol != null)
            {
                pieceOnSquare = owningCol.ChessPiece;
            }
            if (value is IConfigurablePiece || value is int)
            {
                pieceOnSquare = (IConfigurablePiece)value;
            }

            //what's my location?

            //get piece image path from rules file? or a cache somewhere? or is it assigned somewhere else?

            Image img = new System.Drawing.Bitmap(Environment.CurrentDirectory + "\\images\\pawn.jpeg");

            //move this. This only needs to be done once per column
            owningCol.Width = img.Width;

            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cellStyle.BackColor = System.Drawing.Color.Black; //assign based on location
            cellStyle.Padding = Padding.Empty;

            return img;
        }
    }
}