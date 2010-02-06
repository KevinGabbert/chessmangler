using System;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

using ChessMangler.Communications.Types;

using jabber.protocol.client; //presence, messageType
using jabber.client; 
using JabberMessage = jabber.protocol.client.Message;

using jabber.protocol.iq;

using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Types.Packets;

namespace ChessMangler.Communications.Handlers
{
    public class JabberHandler : IM_Handler_Base, ICommsHandler, IJabberCredentials
    {
        public const string CHESSMANGLER_COMMS = "ChessManglerComms";

        // we will wait on this event until we're done sending
        static ManualResetEvent done = new ManualResetEvent(false);
        static Dictionary<string, XmlElement> _potentialOpponents = new Dictionary<string, XmlElement>();

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

            //jabberClient1.OnDisconnect += new bedrock.ObjectHandler(jabberClient1_OnDisconnect);
            //jabberClient1.OnError += new bedrock.ExceptionHandler(jabberClient1_OnError);
            //jabberClient1.OnAuthError += new jabber.protocol.ProtocolHandler(jabberClient1_OnAuthError);

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
            Console.Beep(37, 70);

            if (msg.Body != null)
            {
                this.On_OpponentChat_RCV(msg.Body);              
            }
            else
            {
                this.ParseMsg(msg);
            }

            done.Set();
        }

        private void ParseMsg(jabber.protocol.client.Message msg)
        {
            if (msg.Type != MessageType.error)
            {
                if (msg.HasAttributes)
                {
                    //*** If DebugMode
                    //this.AddChat("Move Recieved from: " + this.JabberOpponent + "\r\n" + msg.OuterXml);
                    Console.Beep(37, 70);
                    //*** If DebugMode

                    //We need to know who the hell this person is. so we need to read the friend name as well as 
                    //packet type

                    //is this a version packet?
                    if (msg.InnerText.Substring(1, 8) == "?")
                    {
                        this.ParseVersionPacket(msg);
                    }

                    //Is this a Move Packet? 
                    if (msg.InnerText.Substring(0, 8) == "movehash") //the full string looks like this: movehashGameIDPawne7e6False
                    {
                        this.ParseMovePacket(msg);
                    }
                }
            }
        }

        private void ParseVersionPacket(jabber.protocol.client.Message msg)
        {
            XmlElement version;

            //Who is this?

            // string opponent = version.Attributes["userName"].InnerText;
            // _potentialOpponents[opponent] = parseVersionFromMsg2();
        }
        private void ParseMovePacket(jabber.protocol.client.Message msg)
        {
            //*** If DebugMode
            //this.AddChat("Move Recieved from: " + this.JabberOpponent + "\r\n" + msg.OuterXml);
            Console.Beep(300, 50);
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
        public static OnlineType GetItemPresence(Item item, PresenceManager presenceManager)
        {
            //TODO: refactor this to comms layer
            Presence p = presenceManager[item.JID];
            OnlineType _onlineType;

            if (p == null)
            {
                _onlineType = OnlineType.Offline;
            }
            else
            {
                switch (p.Show)
                {
                    case "Online":
                        _onlineType = OnlineType.Online;
                        break;
                    case "away":
                        _onlineType = OnlineType.Away;
                        break;
                    default:
                        _onlineType = OnlineType.Other;
                        break;
                }
            }
            return _onlineType;
        }
        public void Write(string opponent, XmlElement stuffToWrite, MessageType type)
        {
            JabberMessage message = new JabberMessage(new XmlDocument()); //Should MovePacket be here??
            message.Type = type;
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


        //TODO: This needs to be in the base class
        public string RequestOpponentCurrentGameVersion(string opponentJabberName, string myJabberName)
        {
            _potentialOpponents.Clear();
            _potentialOpponents.Add(opponentJabberName, null);
            XmlElement versionPacket = VersionPacket.GenerateMyVersion(myJabberName);

            this.Write(myJabberName, versionPacket, MessageType.normal);

            //Do until (timeout (2 seconds? - set in options DB ~ remember this is gonna be called for all opponents in the DGV..)
            //{
            // Check the Message Recieved Event? to verify reply
            //wait here..
            //}

            int timeout = 1000;
            System.Threading.Thread.Sleep(timeout); //temp hack for now.  j_OnMessage should tell us the answer

            //if we still don't know by this point, come back with version unknown

            string opponentVersion = null;

            XmlElement value;
            _potentialOpponents.TryGetValue(opponentJabberName, out value);
            
            if (value != null) //so why won't this work?
            {
                if (!string.IsNullOrEmpty(_potentialOpponents[opponentJabberName].Attributes["version"].InnerText))
                {
                    opponentVersion = _potentialOpponents[opponentJabberName].Attributes["version"].InnerText;
                }
            }

            return opponentVersion;
        }

        //TODO: This needs to be in the base class
        public string RequestOpponentCMVersion(string opponentName)
        {
            //throw new NotImplementedException();

            //this.write("whats yer version?")

            //Do until (timeout (2 seconds? - set in options DB ~ remember this is gonna be called for all opponents in the DGV..)
            //{
            //wait here..
            //}

            //if we still don't know by this point, come back with version unknown

            return "CMVersion Not Implemented Yet " + opponentName;
        }
    }
}
