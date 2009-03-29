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
    public enum ChessPiece
    {
        Green,
        Yellow,
        Red
    }

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
            ChessColumn owningCol = OwningColumn as ChessColumn;
            if (owningCol != null)
            {
                pieceOnSquare = owningCol.ChessPiece;
            }
            if (value is IConfigurablePiece || value is int)
            {
                pieceOnSquare = (IConfigurablePiece)value;
            }

            Image img = new System.Drawing.Bitmap(Environment.CurrentDirectory + "\\images\\pawn.jpeg");

            cellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            return img;
        }
    }
}