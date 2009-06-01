using System;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using jabber.protocol;
using jabber.protocol.x;
using jabber.protocol.client; //presence
using jabber.client; //TODO: This needs to be refactored out.
using JabberMessage = jabber.protocol.client.Message; //This is the only one that should stay

using ChessMangler.Engine.Types;
using ChessMangler.Settings.Types.WinUI;
using ChessMangler.Communications.Types;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid2D_Form : GridForm
    {
        static DebugForm _debugForm;

        #region Properties

        string _jabberOpponent;
        public string JabberOpponent
        {
            get
            {
                return _jabberOpponent;
            }
            set
            {
                _jabberOpponent = value;
            }
        }

        string _version = "Alpha";
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }


        //TODO:  This will need to be moved somewhere
        MovePacket _inBox;
        public MovePacket InBox
        {
            get
            {
                return _inBox;
            }
            set
            {
                _inBox = value;
            }
        }

        #endregion

        //TODO: Move to Jabber_EventHandler **** jabber proto code ****

        //We should know all this by this point (will be set in options form
        JabberClient jabberClient = new JabberClient();

        //TODO:  this goes in Jabber_EventHandler we will wait on this event until we're done sending
        static ManualResetEvent done = new ManualResetEvent(false);

        ChessGrid2D_MenuBarHandlers _menuBarHandlers;
        ChessGrid2D_Settings _gridOptions = new ChessGrid2D_Settings();

        public ChessGrid2D_Form()
        {
            InitializeComponent();

            this.InitJabber();
            this.InitGrid();
            this.InitForms();
        }
        public ChessGrid2D_Form(BoardDef board, string imagesDirectory, short squareSize, string jabberOpponent)
        {
            InitializeComponent();

            this.InitJabber();
            this.InitGrid();
            this.Grid.SetUp_FreeFormBoard(this, board, squareSize);

            this.InitHandlers();
            this.InitForms();
        }

        //**** Refactor ****
        //This should probably be in another object
        private void InitJabber()
        {
            jabberClient.AutoReconnect = 3F;
            jabberClient.AutoStartCompression = true;
            jabberClient.AutoStartTLS = true;
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
        
        #region Event Handlers

        private void ChessGrid2D_Load(object sender, EventArgs e)
        {
            this.Grid.SetUp_DefaultUIBoard(this);
            this.InitHandlers();

            this.Text = "Chess Mangler " + this.Version + " ~ Opponent:  " + this.JabberOpponent;
        }
        private void ChessGrid2D_Resize(object sender, EventArgs e)
        {
            this.Grid.Redraw(this.Grid.UIBoard.Flipped);
        }
        private void ChessGrid2D_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: tell the Jabber_EventHandler to Dispose.

            //TODO: Move this code to the Jabber_EventHandler
            if (jabberClient.IsAuthenticated)
            {
                jabberClient.Close();
            }
        }
        private void ChessGrid2D_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            this.Grid.Toggle_BoardMode();
        }
        private void btnSubmitMove_Click(object sender, EventArgs e)
        {
            if (this._squareHandlers.OutBox != null)
            {
                //TODO: Move this to a "JabberTasks" static class in Comms. (later this will be abstracted out as well)
                JabberMessage message = new JabberMessage(new XmlDocument()); //Should MovePacket be here??
                message.To = new jabber.JID(this.JabberOpponent);
                message.AddChild(this._squareHandlers.OutBox);

                jabberClient.Write(message);

                this.Grid.UIBoard.Squares.Disable();  //Until we recieve a good packet from the opponent
            }
        }
        private void btnChatSend_Click(object sender, EventArgs e)
        {
            this.SendChat();
        }
        private void txtChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SendChat();
            }
        }

        #region TODO: These events need to be placed in a Jabber_EventHandler

        private void j_OnMessage(object sender, jabber.protocol.client.Message msg)
        {
            if (msg.Body != null)
            {
                this.AddChat(this.txtChat.Text + msg.Body);
            }
            else
            {
                //*** If DebugMode
                this.AddChat("Move Recieved from: " + this.JabberOpponent + "\r\n" + msg.OuterXml);
                Console.Beep(37, 70);
                //*** If DebugMode

                MovePacket recievedMove = new MovePacket(msg);

                if (recievedMove.Invalid)
                {
                    //if we have problems here, send a REJECT packet or something
                    this._squareHandlers.OutBox = recievedMove.GenerateRejectPacket();

                    //so.. at this point, we are back to our previous state.  Nothing gained.

                    //Tell the user about it, however..

                    //Flash something yellow, like the title bar to the chat window or something.
                    //This would be good for all erroneous move communication

                    //also, cue up the chat window.  Select its tab.  Make the text red with asterisks.
                    this.AddChat("Invalid Move recieved. " + recievedMove.InvalidMoveReason + " Try again?");
                }

                this.InBox = recievedMove; 

                this.Process_InBox();
                this.SendOutBox();
            }
        }

        private void Process_InBox()
        {
            //Load up Move Packet
            MovePacket recievedMove = this.InBox;

            this._squareHandlers.OutBox = recievedMove.GenerateRCVPacket();

            //Make sure this move is for this game (as we can have several going.)
            //not sure how this might be a problem, but I want to be prepared if we ever do..
            //_recievedMove.GameID;

            //This keeps us from getting this move confused with other moves. somebody needs to check this.  I don't know who..
            //_recievedMove.MoveHash;

            //If we got this far then the move packet is good
            //Send out a RECV Packet (no need to wait for any return msg)

            this.Grid.UIBoard.EngineBoard.ExecuteMove(recievedMove);
            this.Grid.Redraw(Grid.UIBoard.Flipped); //Syncs the UIBoard with the newly changed EngineBoard

            this.Grid.UIBoard.Squares.Enable();
            this.Grid.UIBoard.Squares.UnlockAll();
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
            done.Set(); // Finished sending.  Shut down.

            jabberClient.Presence(PresenceType.available, "ChessMangler Online", "show", 2);
        }
        private void j_OnError(object sender, Exception ex)
        {
            // There was an error!
            MessageBox.Show("Error: " + ex.ToString(), "Jabber-Net TestHarness");

            // Shut down.
            done.Set();
        }

        #endregion
        #endregion

        public void InitGrid()
        {
            this.Grid = new Grid2D(this);

            //TODO:  Make this pull from game config DB
            this.Grid.VerticalSquish = 21; //This number is likely due to the TabControl height, but is more likely a divisor of it
            //this.Grid.ClientHeight = this.ClientSize.Height;
        }
        public void InitHandlers()
        {
            _menuBarHandlers = new ChessGrid2D_MenuBarHandlers(_debugForm, this);
            _squareHandlers = new ChessGrid2D_SquareHandlers();

            this._squareHandlers.Add_Required_Square_Handlers(this, this.Grid.DebugForm);

            //Menu Handlers
            //To add a new entry, create one in the form UI, double-click, and then add a handler in _menuBarHandlers.

            this.toggleDebugModeToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.optionsToolStripMenuItem1.Click += new System.EventHandler(_menuBarHandlers.optionsToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);
            this.resetPiecesToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.resetPiecesToolStripMenuItem_Click);
            this.clearPiecesToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.clearPiecesToolStripMenuItem_Click);
            this.flipBoardToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.flipBoardToolStripMenuItem_Click);
            this.constrainBoardProportionsToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.constrainBoardProportionsToolStripMenuItem_Click);
        }
        public void InitForms()
        {
            ChessGrid2D_Form._debugForm = this.Grid.DebugForm;
        }

        //used in some cases to reset things.
        public void DitchHandlers()
        {
            this._squareHandlers.Delete_Required_Square_Handlers(this);

            //Menu Handlers
            this.toggleDebugModeToolStripMenuItem.Click -= new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click -= new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);
            this.resetPiecesToolStripMenuItem.Click -= new System.EventHandler(this._menuBarHandlers.resetPiecesToolStripMenuItem_Click);
        }

        public void SendChat()
        {
            jabberClient.Message(this.JabberOpponent, this.txtChat.Text);

            //This message needs to be logged in the database (remember to also add the ID of the latest move so we know at what point of the game that this was sent..)
            //Make an option later to create a PGN file annotated with chats.

            //if options say so, then beep.

            //TODO: if option is set, then preface with time or contact name

            this.txtChatHistory.Text += Environment.NewLine + this.txtChat.Text;
            this.Scroll_ChatHistory_Box();
            this.txtChat.Clear();

            //If message was sent and not recieved then post back to ChatHistory box as well. (it won't be here of course)
        }

        private delegate void ChatDelegate(string message);
        public void AddChat(string message)
        {
            if (this.txtChat.InvokeRequired)
            {
                this.txtChat.Invoke(new ChatDelegate(this.AddChat), message);
            }
            else
            {
                //if options say so, then beep.

                //TODO: if option is set, then preface with time or contact name

                this.txtChatHistory.Text += Environment.NewLine + message;
                this.Scroll_ChatHistory_Box();
                this.txtChat.Clear();
            }
        }

        private void Scroll_ChatHistory_Box()
        {
            this.txtChatHistory.Select(txtChatHistory.Text.Length + 1, 2);
            this.txtChatHistory.ScrollToCaret();
        }

        //TODO: This button will only be seen in Debug Mode
        private void btnEnableSquares_Click(object sender, EventArgs e)
        {
            //TODO:  We need some visual indicators of all these things!
            this.Grid.UIBoard.Squares.Enable();
            this.Grid.UIBoard.Squares.ResetColors();
            this._squareHandlers.OutBox = null;
        }
    }
}