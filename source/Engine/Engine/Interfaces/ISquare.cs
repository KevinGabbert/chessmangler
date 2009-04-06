using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Engine.Interfaces
{
    public interface ISquare
    {
        int Row
        {
            get;
            set;
        }
        int Col
        {
            get;
            set;
        }
        string Name
        {
            get;
            set;
        }
        int Number
        {
            get;
            set;
        }

        Color Color
        {
            get;
            set;
        }
    }
}
