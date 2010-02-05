using System.Xml;
using ChessMangler.Communications.Handlers;
using jabber.protocol.client; //presence, messageType

namespace ChessMangler.Communications.Interfaces
{
    public interface ICommsHandler: IQueryVersion
    {
        //TODO: don't know if I want to keep this.. 
        object originalHandler
        {
            get;
        }

        event Comms_Authenticate authenticate;
        event OpponentMove_Handler OpponentMove_Recieved;
        event OpponentChat OpponentChat_Recieved;

        void On_Authenticate(object sender);
        void On_OpponentChat_RCV(string chatMessage);
        void On_OpponentMove(object sender);

        void Write(string opponent, XmlElement stuffToWrite, MessageType type);
        void Message(string to, string body);
    }
}
