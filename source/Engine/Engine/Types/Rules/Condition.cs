using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Engine.Interfaces;

namespace Engine.Types.Rules
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

        private ConditionType _type;
        public ConditionType Type
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
