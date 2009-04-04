using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Interfaces
{
    public interface ISquare
    {
        int x
        {
            get;
            set;
        }
        int y
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
    }
}
