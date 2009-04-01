using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinUIParts
{
    public class UIEngine
    {
        public static bool IsThisMoveOkay(ChessSquare startSquare, ChessSquare endSquare)
        {
            //get start chesspiecetype
            //string xxx = ((IConfigurablePiece)startSquare.Value).Type;

            //talk to Engine.  Find out if we can do the move.  if not, then we need to read the broken rules Collection



            return true;
        }

        //possible alternative:
        //public static bool IsThisMoveOkay(ChessSquare startSquare, ChessSquare endSquare, out BrokenRulesCollection)
        //{
        //    //get start chesspiecetype
        //    string xxx = ((IConfigurablePiece)startSquare.Value).Type;

        //    //talk to Engine.  Find out if we can do the move.  if not, then we need to read the broken rules Collection

        //   
        //}
    }
}
