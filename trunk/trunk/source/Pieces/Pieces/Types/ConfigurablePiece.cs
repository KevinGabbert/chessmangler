using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using Pieces.Interfaces;

namespace Pieces.Types
{
    public abstract class ChessPiece:IConfigurablePiece
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public Color Color
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public long Location
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public void CheckMy_MovementRule()
        {
            //blah blah blah, call a command in the rules library (which checks xml file with rules in it)
            //MovementRule x = RuleLibrary.GetRule(this.GetType());
        }
        public void Move(long from, long to)
        {
            this.CheckMy_MovementRule();
        }
    }
}
