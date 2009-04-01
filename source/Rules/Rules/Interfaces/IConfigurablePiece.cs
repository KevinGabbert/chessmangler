using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Rules.Types;
using Engine.Types;

namespace Rules.Interfaces
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

        AttackPackage Attacking
        {
            get;
            set;
        }
        MovementPackage Moves
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
