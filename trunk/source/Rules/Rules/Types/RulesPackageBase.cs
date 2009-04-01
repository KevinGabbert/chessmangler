using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Types;
using Rules.Interfaces;

namespace Rules.Types
{
    /// <summary>
    /// 
    /// </summary>
    public class RulesPackageBase: IRulesPackage
    {
        #region Properties

        List<Rules> _rules = new List<Rules>();
        public List<Rules> Rules
        {
            get
            {
                return _rules;
            }
            set
            {
                _rules = value;
            }
        }

        List<Square> _squares = new List<Square>();
        public List<Square> Squares
        {
            get
            {
                return _squares;
            }
            set
            {
                _squares = value;
            }
        }

        #endregion
    }
}
