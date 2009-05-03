using System;
using System.Collections.Generic;
using System.Text;

namespace ChessManger.Communications.Types
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
    }
}
