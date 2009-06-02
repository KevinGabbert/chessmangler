using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.Windows.Forms;

//using ChessMangler.Engine.Types;
using ChessMangler.Settings.Types.WinUI;
using ChessMangler.Communications.Types;
using ChessMangler.Communications.Handlers;

using jabber.protocol;
using jabber.protocol.x;
using jabber.protocol.client; //presence
using jabber.client; //TODO: This needs to be refactored out.
using JabberMessage = jabber.protocol.client.Message;
using System.ComponentModel; //This is the only one that should stay

namespace ChessMangler.Communications.Handlers
{
    public class ChessGrid2D_JabberHandlers : System.Windows.Forms.Control //ISynchronizeInvoke
    {
        //TODO:  this goes in Jabber_EventHandler we will wait on this event until we're done sending
        static ManualResetEvent done2 = new ManualResetEvent(false);

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

        //We should know all this by this point (will be set in options form
        JabberClient jabberClient = new JabberClient();

        //**** Refactor ****
        //This should probably be in another object
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
            done2.WaitOne();
        }

        #region TODO: These events need to be placed in a Jabber_EventHandler

        private void j_OnMessage(object sender, jabber.protocol.client.Message msg)
        {
            if (msg.Body != null)
            {
                ///////this.AddChat(this.txtChat.Text + msg.Body);
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

                /////this.InBox = recievedMove;

                /////this.Process_InBox();
                this.SendOutBox();
            }
        }

        private void SendOutBox()
        {
            //throw new NotImplementedException();
        }

        private void j_OnAuthenticate(object sender)
        {
            //JabberClient j = (JabberClient)sender;
            //j.Message(this.JabberOpponent, "Chess Mangler " + this.Version + " <Proto> Connecting @ " + DateTime.Now.ToString());
            Console.Beep(1000, 20);
            done2.Set(); // Finished sending.  Shut down.

            jabberClient.Presence(PresenceType.available, "ChessMangler Online", "show", 2);
        }
        private void j_OnError(object sender, Exception ex)
        {
            // There was an error!
            MessageBox.Show("Error: " + ex.ToString(), "Jabber-Net TestHarness");

            // Shut down.
            done2.Set();
        }

        #endregion

        //#region ISynchronizeInvoke Members

        //public IAsyncResult BeginInvoke(Delegate method, object[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public object EndInvoke(IAsyncResult result)
        //{
        //    throw new NotImplementedException();
        //}

        //public object Invoke(Delegate method, object[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool InvokeRequired
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //#endregion
    }
}
