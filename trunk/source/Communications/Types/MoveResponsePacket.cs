using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.Communications.Types
{
    public class MoveResponsePacket: MoveBase
    {
        ///- Piece (denoted by up to 2 characters)
        string _piece;
        public string Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
            }
        }
    }
}
