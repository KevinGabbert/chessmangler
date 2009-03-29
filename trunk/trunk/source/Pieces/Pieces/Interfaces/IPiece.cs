using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pieces.Interfaces
{
    public interface IConfigurablePiece
    {
        Int64 Location
        {
            get;
            set;
        }

        Color Color
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        void CheckMy_MovementRule();
        void Move(Int64 from, Int64 to);
    }
}
