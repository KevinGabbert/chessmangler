using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Types
{
    public class BoardIterator
    {
        int _row;
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        int _col;
        public int Col
        {
            get
            {
                return _col;
            }
            set
            {
                _col = value;
            }
        }
    }
}
