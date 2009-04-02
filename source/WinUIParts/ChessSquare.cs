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

            //// Open a Stream and decode a PNG image
            //Stream imageStreamSource = new FileStream(Environment.CurrentDirectory + "\\images\\pawn.jpeg", FileMode.Open, FileAccess.Read, FileShare.Read);
            //PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            //BitmapSource bitmapSource = decoder.Frames[0];

            //// Draw the Image
            //Image myImage = new Image();
            //myImage.Source = bitmapSource;
            ////myImage.Stretch = Stretch.None;
            ////myImage.Margin = new Thickness(20);


            string path = Environment.CurrentDirectory + "\\images\\wp.jpeg";
            bool exists = File.Exists(path);

            Image img = new System.Drawing.Bitmap(path);

            //move this. This only needs to be done once per column
            owningCol.Width = img.Width;

            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cellStyle.BackColor = System.Drawing.Color.Black; //assign based on location
            cellStyle.Padding = Padding.Empty;

            return img;
        }
    }
}