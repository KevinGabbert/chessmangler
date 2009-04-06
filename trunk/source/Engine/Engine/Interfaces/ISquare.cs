using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Engine.Types;

namespace Engine.Interfaces
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
