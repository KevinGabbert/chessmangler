using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jabber.client;
using ChessMangler.Communications.Interfaces;
using jabber.protocol.client; //presence, messageType

namespace ChessMangler.TestHarness.Mocks.Comms
{
    public class Comms_Mock : ICommsHandler
    {
        #region ICommsHandler Members

        public object originalHandler
        {
            get { throw new NotImplementedException(); }
        }

        public event ChessMangler.Communications.Handlers.Comms_Authenticate authenticate;

        public event ChessMangler.Communications.Handlers.OpponentMove_Handler OpponentMove_Recieved;

        public event ChessMangler.Communications.Handlers.OpponentChat OpponentChat_Recieved;

        public void On_Authenticate(object sender)
        {
            throw new NotImplementedException();
        }

        public void On_OpponentChat_RCV(string chatMessage)
        {
            throw new NotImplementedException();
        }

        public void On_OpponentMove(object sender)
        {
            throw new NotImplementedException();
        }

        public void Write(string opponent, System.Xml.XmlElement stuffToWrite, MessageType type)
        {
            throw new NotImplementedException();
        }

        public void Message(string to, string body)
        {
            throw new NotImplementedException();
        }

        public string RequestOpponentCurrentGameVersion(string opponentJabberName, string myJabberName)
        {
            throw new NotImplementedException();
        }

        public string RequestOpponentCMVersion(string opponentName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
