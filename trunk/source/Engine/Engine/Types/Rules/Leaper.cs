using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Types.Rules
{
    public class Leaper
    {
        private int _diagonal;
        public int Diagonal
        {
            get
            {
                return _diagonal;
            }
            set
            {
                _diagonal = value;
            }
        }

        private int _orthogonal;
        public int Orthogonal
        {
            get
            {
                return _orthogonal;
            }
            set
            {
                _orthogonal = value;
            }
        }
    }
}
