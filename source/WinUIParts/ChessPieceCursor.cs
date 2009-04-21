using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ChessMangler.WinUIParts
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    /// <summary>
    /// This class makes use of Win API calls in order to replace the Cursor with a Chess Piece
    /// </summary>
    public class ChessPieceCursor
    {
        #region WinAPI Calls

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        #endregion

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }
        public static void ShowPieceCursor(UISquare senderSquare)
        {
            if (senderSquare.CurrentPiece != null)
            {
                if (senderSquare.CurrentPiece.Image != null)
                {
                    Bitmap bitmap = new Bitmap(senderSquare.CurrentPiece.Image, senderSquare.Size);
                    Cursor.Current = CreateCursor(bitmap, 35, 35);
                    bitmap.Dispose();
                }
            }
        }
    }
}
