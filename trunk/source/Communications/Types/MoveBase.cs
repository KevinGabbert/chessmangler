using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.Engine.Types;
using ChessMangler.Engine.Interfaces;
using ChessMangler.Communications.Types.Packets;

namespace ChessMangler.Communications.Types
{
    public class MoveBase
    {
        ///- Move Hash - A unique identifier for the move
        string _moveHash;
        public string MoveHash
        {
            get
            {
                return _moveHash;
            }
            set
            {
                _moveHash = value;
            }
        }

        //- New Location (denoted by at least, but not limited to 2 characters)
        string _newLocation;
        public string NewLocation
        {
            get
            {
                return _newLocation;
            }
            set
            {
                _newLocation = value;
            }
        }

        //I'll put this here for now.. It might need to be moved elsewhere later.
        //I really did it to resolve an architectural issue..
        public static void ExecuteMove(MovePacket recievedMove, Board2D board)
        {
            ISquare from = board.GetByName(recievedMove.Previous);
            ISquare to = board.GetByName(recievedMove.New);

            if (recievedMove.Rules)
            {
                if (board.IsThisMoveOkay(from, to))
                {
                    Board2D.MoveThePieceOver(from, to);

                    //Log the move to the Sqlite database

                    //use recievedMove.GameID to figure out where to store it
                }
                else
                {
                    //Make a big stink.  Throw exception?
                    throw new SystemException("MoveException here");
                }
            }
            else
            {
                Board2D.MoveThePieceOver(from, to);
            }
        }
    }
}
