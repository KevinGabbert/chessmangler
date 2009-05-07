using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.Engine.Enums;

namespace ChessMangler.Engine.Interfaces
{
    public interface IBoardMode
    {
        BoardMode BoardMode
        {
            get;
            set;
        }
    }
}
