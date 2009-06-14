using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ChessMangler.Communications.Handlers
{
    public delegate void Comms_Authenticate(object sender);
    public delegate void OpponentChat(string chatMessage);
    public delegate void OpponentMove_Handler(object move);

    public class IM_Handler_Base: Control
    {
        public event Comms_Authenticate authenticate;
        public event OpponentMove_Handler OpponentMove_Recieved;
        public event OpponentChat OpponentChat_Recieved;

        public void On_Authenticate(object sender)
        {
            this.authenticate(sender);
        }

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
