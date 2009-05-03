using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ChessManger.Communications.Types
{
    public class MovePacket : MoveResponsePacket
    {
        //- Rules Hash - A unique ID for the game played, probably pulled from the rules file.
        string _rulesHash;
        public string RulesHash
        {
            get
            {
                return _rulesHash;
            }
            set
            {
                _rulesHash = value;
            }
        }

        //- Previous Location (denoted by at least, but not limited to 2 characters)
        string _previousLocation;
        public string PreviousLocation
        {
            get
            {
                return _previousLocation;
            }
            set
            {
                _previousLocation = value;
            }
        }

        bool _useRules = true;
        public bool UseRules
        {
            get
            {
                return _useRules;
            }
            set
            {
                _useRules = value;
            }
        }
    }
}
