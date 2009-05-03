using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.Settings.Types
{
    public class SettingsBase
    {
        string _category;
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }


        internal void Get(string category, string key)
        {

        }

        internal void Set(string category, string key, string value)
        {

        }
    }
}
