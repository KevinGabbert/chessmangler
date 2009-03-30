using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Pieces.Interfaces
{
    public interface IConfigurablePiece
    {
        Int64 CurrentLocation
        {
            get;
            set;
        }
        Int64 DefaultLocation
        {
            get;
            set;
        }

        Color Color
        {
            get;
            set;
        }

        //Do we want to use a string for this?  I guess we'd have to, as this would be pulled from the rules file (piece config section)
        string Type
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
