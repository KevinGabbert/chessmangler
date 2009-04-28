using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChessMangler.Engine.Types;
using ChessMangler.Rules.Interfaces;

namespace ChessMangler.Rules.Types
{
    /// <summary>
    /// 
    /// </summary>
    public class RulesPackageBase: IRulesPackage
    {
        #region Properties

        List<Board2DRules> _boardRules = new List<Board2DRules>();
        public List<Board2DRules> BoardRules
        {
            get
            {
                return _boardRules;
            }
            set
            {
                _boardRules = value;
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
