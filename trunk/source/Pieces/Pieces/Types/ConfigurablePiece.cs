using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using Pieces.Interfaces;
using Engine.Board;

namespace Pieces.Types
{
    public abstract class ChessPiece:IConfigurablePiece
    {
        #region Properties

        public const string NOT_ASSIGNED = "Not Assigned";
        public Location _location = new Location();
        public List<Square> _attacking = new List<Square>();
        public List<Color> _team = new List<Color>();

        public Location Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
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

        public List<Color> Team
        {
            get
            {
                return _team;
            }
            set
            {
                _team = value;
            }
        }

        //prop MyMovementRule

        public List<Square> Attacking
        {
            get
            {
                return _attacking;
            }
            set
            {
                _attacking = value;
            }
        }

        #endregion

        public void CheckMy_MovementRule()
        {
            //blah blah blah, call a command in the rules library (which checks xml file with rules in it)
            //Rules.MovementRule x = RuleLibrary.GetRule(this.GetType());
        }
        public void Move(long from, long to)
        {
            this.CheckMy_MovementRule();
        }

        public string _type = NOT_ASSIGNED;
        public string Type
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
    }
}
