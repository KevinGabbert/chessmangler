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

        List<Board2DRules> _rules = new List<Board2DRules>();
        public List<Board2DRules> Rules
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

        List<Square2D> _squares = new List<Square2D>();
        public List<Square2D> Squares
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
