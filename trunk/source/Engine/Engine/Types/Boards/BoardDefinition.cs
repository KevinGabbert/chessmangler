using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Types
{
    public class BoardDef
    {
        int _rows;
        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        int _cols;
        public int Columns
        {
            get
            {
                return _cols;
            }
            set
            {
                _cols = value;
            }
        }
    }
}
