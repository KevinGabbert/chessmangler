using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Handlers;

namespace ChessMangler.Communications.Types
{
    public class Comms
    {
        public static ICommsHandlers GetHandler(CommsType commsType)
        {
            ICommsHandlers returnVal = null;

            switch (commsType)
            {
                case CommsType.Google:
                    returnVal = new JabberHandler();
                    break;
            }

            return returnVal;
        }
    }
}
