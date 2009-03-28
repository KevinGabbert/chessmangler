using System;
using System.Collections.Generic;

using System.Text;

namespace Pieces.Interfaces
{
    public interface IPiece
    {
        Int64 Location
        {
            get;
            set;
        }

        bool Color
        {
            get;
            set;
        }
    }
}
