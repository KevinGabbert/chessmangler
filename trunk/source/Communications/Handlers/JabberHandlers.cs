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
    public class JabberHandler : IM_Handler_Base, ICommsHandler, IJabberCredentials
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

        JabberClient _jabberClient = new JabberClient();
        public JabberClient JabberClient
        {
            get
            {
                return _jabberClient;
            }
            set
            {
                _jabberClient = value;
            }
        }

        public object originalHandler
        {
            get
            {
                return _jabberClient;
            }
        }

        #endregion

        public JabberHandler()
        {

        }

        //TODO:  remove authenticateHandler as a parameter
        public JabberHandler(string userName, string password, string server, string networkHost, Comms_Authenticate authenticateHandler)
        {
            //TODO: if this works, then make this into a prop or an event
            if (authenticateHandler != null)
            {
                this.authenticate += authenticateHandler;
            }

            this.User = userName;
            this.Password = password;
            this.Server = server;
            this.NetworkHost = networkHost;

            this.InitJabber();
        }

        private void InitJabber()
        {
            //TODO: if Login information isn't filled out by this point, then pop up the login form

            _jabberClient.AutoReconnect = 3F;
            _jabberClient.AutoStartCompression = true;
            _jabberClient.AutoStartTLS = true;

            //Is this even needed here? 
            _jabberClient.InvokeControl = this;
            _jabberClient.LocalCertificate = null;
            _jabberClient.KeepAlive = 30F;

            // what user/pass to log in as
            _jabberClient.User = this.User; // "Test.Chess.Mangler";
            _jabberClient.Server = this.Server; // "gmail.com";  // use gmail.com for GoogleTalk
            _jabberClient.Password = this.Password; //sorry.. changed it.
            _jabberClient.NetworkHost = this.NetworkHost; // "talk.l.google.com";  // Note: that's an "L", not a "1".

            _jabberClient.AutoPresence = false;
            _jabberClient.AutoRoster = true;
            _jabberClient.AutoReconnect = -1;

            // listen for errors.  Always do this!
            _jabberClient.OnError += new bedrock.ExceptionHandler(j_OnError);

            // what to do when login completes
            _jabberClient.OnAuthenticate += new bedrock.ObjectHandler(j_OnAuthenticate);

            // listen for XMPP wire protocol
            _jabberClient.OnMessage += new MessageHandler(j_OnMessage);

            _jabberClient.OnDisconnect += new bedrock.ObjectHandler(j_OnDisconnect);

            if ((this.User != "") && (this.Password != ""))
            {
                // Set everything in motion
                _jabberClient.Connect();

                //wait until sending a message is complete
                done.WaitOne();
            }
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
            done.Set(); // Finished sending.  Shut down.
            this.On_Authenticate(sender);
        }
        private void j_OnError(object sender, Exception ex)
        {
            // Shut down.
            done.Set();
        }
        void j_OnDisconnect(object sender)
        {
            //call event.  this is for the client.
        }

        #endregion

        public void Write(string opponent, XmlElement stuffToWrite)
        {
            JabberMessage message = new JabberMessage(new XmlDocument()); //Should MovePacket be here??
            message.To = new jabber.JID(opponent);
            message.AddChild(stuffToWrite);

            _jabberClient.Write(message);

            done.Set();
        }
        public void Message(string to, string body)
        {
            _jabberClient.Message(to, body);

            done.Set();
        }
    }
}
