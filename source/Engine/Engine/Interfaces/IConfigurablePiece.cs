using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using ChessMangler.Rules.Types;
using ChessMangler.Engine.Types;
using ChessMangler.Engine.Interfaces;

namespace ChessMangler.Rules.Interfaces
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

        //int Player
        //{
        //    get;
        //    set;
        //}

        void CheckMy_MovementRule();
        void Move(Int64 from, Int64 to);
    }
}
