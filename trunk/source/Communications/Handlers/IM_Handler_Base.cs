using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.Communications.Handlers
{
    public class IM_Handler_Base: System.Windows.Forms.Control
    {
        public delegate void OpponentChat(string chatMessage);
        public delegate void OpponentMove_Handler(object move); //MovePacket?

        public event OpponentMove_Handler OpponentMove_Recieved;
        public event OpponentChat OpponentChat_Recieved;

        public void On_OpponentChat_RCV(string chatMessage)
        {
            this.OpponentChat_Recieved(chatMessage);
        }

        public void On_OpponentMove(object sender)
        {
            this.OpponentMove_Recieved(sender);
        }
    }
}
