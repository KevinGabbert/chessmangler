using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Types
{
    public class PieceDef
    {
        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        string _startingLocation;
        public string StartingLocation
        {
            get
            {
                return _startingLocation;
            }
            set
            {
                _startingLocation = value;
            }
        }

        string _imageName;
        public string ImageName
        {
            get
            {
                return _imageName;
            }
            set
            {
                _imageName = value;
            }
        }
    }
}
