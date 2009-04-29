using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using ChessMangler.Engine.Types;

namespace ChessMangler.Engine.Interfaces
{
    public interface ISquare : IChessObject
    {
        IPiece CurrentPiece
        {
            get;
            set;
        }
    }
}
