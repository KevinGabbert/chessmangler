﻿using System.Xml;
using ChessMangler.Communications.Handlers;

namespace ChessMangler.Communications.Interfaces
{
    public interface ICommsHandlers
    {
        event OpponentMove_Handler OpponentMove_Recieved;
        event OpponentChat OpponentChat_Recieved;

        void On_OpponentChat_RCV(string chatMessage);
        void On_OpponentMove(object sender);

        void Write(string opponent, XmlElement stuffToWrite);
        void Message(string to, string body);
    }
}
