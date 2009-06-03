using System;
using System.Xml;
using System.Threading;

using ChessMangler.Communications.Types;

using jabber.protocol.client; //presence
using jabber.client; 
using JabberMessage = jabber.protocol.client.Message;

using ChessMangler.Communications.Interfaces;

namespace ChessMangler.Communications.Handlers
{
    public class JabberHandler : IM_Handler_Base, ICommsHandlers 
    {
        // we will wait on this event until we're done sending
        static ManualResetEvent done = new ManualResetEvent(false);

        #region Properties

        string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        string _server;
        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        string _networkHost;
        public string NetworkHost
        {
            get
            {
                return _networkHost;
            }
            set
            {
                _networkHost = value;
            }
        }

        #endregion

        public JabberHandler()
        {
            this.InitJabber();
        }

        JabberClient jabberClient = new JabberClient();

        private void InitJabber()
        {
            //if Login infor isn't filled out by this point, then pop up the login form

            jabberClient.AutoReconnect = 3F;
            jabberClient.AutoStartCompression = true;
            jabberClient.AutoStartTLS = true;

            //Is this even needed here? 
            jabberClient.InvokeControl = this;
            jabberClient.LocalCertificate = null;
            jabberClient.KeepAlive = 30F;


            // what user/pass to log in as
            jabberClient.User = "Test.Chess.Mangler";
            jabberClient.Server = "gmail.com";  // use gmail.com for GoogleTalk
            jabberClient.Password = "Ch3$$Mangl3r";
            jabberClient.NetworkHost = "talk.l.google.com";  // Note: that's an "L", not a "1".

            // don't do extra stuff, please.
            jabberClient.AutoPresence = false;
            jabberClient.AutoRoster = false;
            jabberClient.AutoReconnect = -1;

            // listen for errors.  Always do this!
            jabberClient.OnError += new bedrock.ExceptionHandler(j_OnError);

            // what to do when login completes
            jabberClient.OnAuthenticate += new bedrock.ObjectHandler(j_OnAuthenticate);

            // listen for XMPP wire protocol
            jabberClient.OnMessage += new MessageHandler(j_OnMessage);


            // Set everything in motion
            jabberClient.Connect();

            //wait until sending a message is complete
            done.WaitOne();
        }

        #region Events

        private void j_OnMessage(object sender, jabber.protocol.client.Message msg)
        {
            if (msg.Body != null)
            {
                Console.Beep(37, 70);
                this.On_OpponentChat_RCV(msg.Body);              
            }
            else
            {
                //*** If DebugMode
                //this.AddChat("Move Recieved from: " + this.JabberOpponent + "\r\n" + msg.OuterXml);
                Console.Beep(37, 70);
                //*** If DebugMode

                MovePacket recievedMove = new MovePacket(msg);

                if (recievedMove.Invalid)
                {
                    //if we have problems here, send a REJECT packet or something
                    //////this._squareHandlers.OutBox = recievedMove.GenerateRejectPacket();

                    //so.. at this point, we are back to our previous state.  Nothing gained.

                    //Tell the user about it, however..

                    //Flash something yellow, like the title bar to the chat window or something.
                    //This would be good for all erroneous move communication

                    //also, cue up the chat window.  Select its tab.  Make the text red with asterisks.
                    ///////this.AddChat("Invalid Move recieved. " + recievedMove.InvalidMoveReason + " Try again?");
                }
                else
                {
                    this.On_OpponentMove(recievedMove);
                }
            }

            done.Set();
        }
        private void j_OnAuthenticate(object sender)
        {
            Console.Beep(1000, 20);
            done.Set(); // Finished sending.  Shut down.

            jabberClient.Presence(PresenceType.available, "ChessMangler Online", "show", 2);
        }
        private void j_OnError(object sender, Exception ex)
        {
            // Shut down.
            done.Set();
        }

        #endregion

        public void Write(string opponent, XmlElement stuffToWrite)
        {
            JabberMessage message = new JabberMessage(new XmlDocument()); //Should MovePacket be here??
            message.To = new jabber.JID(opponent);
            message.AddChild(stuffToWrite);

            jabberClient.Write(message);

            done.Set();
        }
        public void Message(string to, string body)
        {
            jabberClient.Message(to, body);

            done.Set();
        }
    }
}
