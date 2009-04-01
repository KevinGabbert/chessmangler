using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rules.Interfaces;

namespace Rules.Types
{
    public class ChessPiece: IConfigurablePiece
    {
        #region IConfigurablePiece Members

        public Engine.Types.Location Location
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

        public System.Drawing.Color Color
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

        public List<System.Drawing.Color> Team
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

        public AttackPackage Attacking
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

        public MovementPackage Moves
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

        public void CheckMy_MovementRule()
        {
            throw new NotImplementedException();
        }

        public void Move(long from, long to)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
