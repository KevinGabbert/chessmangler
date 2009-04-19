using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.Engine.Types
{
    public class Location
    {
        Int64 _current;
        Int64 _default;

        public Int64 Default
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }
        public Int64 Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }      
    }
}
