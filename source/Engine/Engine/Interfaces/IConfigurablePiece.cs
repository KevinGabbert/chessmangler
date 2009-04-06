using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Rules.Types;
using Engine.Types;
using Engine.Interfaces;

namespace Rules.Interfaces
{
    public interface IConfigurablePiece: IChessObject
    {
        Location Location
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

        //Who is going to used these packages? the engine??

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

        void CheckMy_MovementRule();
        void Move(Int64 from, Int64 to);
    }
}
