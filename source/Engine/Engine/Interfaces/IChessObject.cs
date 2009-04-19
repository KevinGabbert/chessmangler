using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace ChessMangler.Engine.Interfaces
{
    /// <summary>
    /// Attributes common to all Pieces and Squares
    /// </summary>
    public interface IChessObject
    {
        int Row
        {
            get;
            set;
        }
        int Column
        {
            get;
            set;
        }
        string Name
        {
            get;
            set;
        }
        bool Disabled
        {
            get;
            set;
        }

        Color Color
        {
            get;
            set;
        }
        Image Image
        {
            get;
            set;
        }
    }
}
