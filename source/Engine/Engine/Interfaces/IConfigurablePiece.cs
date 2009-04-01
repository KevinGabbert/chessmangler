using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Engine.Types;

namespace Engine.Interfaces
{
    public interface IConfigurablePiece
    {
        Location Location
        {
            get;
            set;
        }
        Color Color
        {
            get;
            set;
        }

        //This might be helpful for versions of chess where you can keep an opponents pieces.
        List<Color> Team
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }
        //List<Square> Attacking
        //{
        //    get;
        //    set;
        //}

        void CheckMy_MovementRule();
        void Move(Int64 from, Int64 to);
    }
}
