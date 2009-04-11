using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Types
{
    public class BoardDef
    {
        public BoardDef()
        {
        }

        public BoardDef(Int16 columns, Int16 rows)
        {
            this.Columns = columns;
            this.Rows = rows;
        }

        Int16 _rows;
        public Int16 Rows
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

        Int16 _cols;
        public Int16 Columns
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
