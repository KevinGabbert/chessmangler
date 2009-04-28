using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChessMangler.Engine.Interfaces;

namespace ChessMangler.Engine.Types.Rules
{
    public class Condition
    {
        private bool _override;
        public bool Override
        {
            get
            {
                return _override;
            }
            set
            {
                _override = value;
            }
        }

        private IConditionType _type;
        public IConditionType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        private List<IPiece> _applyTo;
        public List<IPiece> ApplyTo
        {
            get
            {
                return _applyTo;
            }
            set
            {
                _applyTo = value;
            }
        }
    }
}
