using System;
using System.Windows.Forms;

using ChessMangler.Options.Interfaces;
using ChessMangler.Engine.Types;
using ChessMangler.Settings.Types.WinUI;
using ChessMangler.Communications.Types;
using ChessMangler.Communications.Handlers;
using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Types.Packets;

//TODO: this needs to be in ICommsHandler
//This is temporary
using jabber.client;
using jabber.protocol.client;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid2D_Form : GridForm, IVersion
    {
        static DebugForm _debugForm;

        #region Properties

        ICommsHandler _comms;
        public ICommsHandler Comms
        {
            get
            {
                return _comms;
            }
            set
            {
                _comms = value;
            }
        }

        string _opponent;
        public string Opponent
        {
            get
            {
                return _opponent;
            }
            //set
            //{
            //    _opponent = value;
            //}
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

        ChessGrid2D_MenuBarHandlers _menuBarHandlers;
        ChessGrid2D_Settings _gridOptions = new ChessGrid2D_Settings();

        //TODO: this needs to be in ICommsHandler
        private PresenceManager localPresenceManager;

        public ChessGrid2D_Form(PresenceManager presenceManager, ICommsHandler comms, string opponent, string version)
        {
            InitializeComponent();

            this._opponent = opponent;

            this.InitComms(comms);
            this.InitGrid();
            this.InitForms();

            //This will need to move to the ICommsHandler
            this.localPresenceManager = presenceManager;
            this.Init_PresenceManager();
        }
        public ChessGrid2D_Form(PresenceManager presenceManager, ICommsHandler comms, BoardDef board, string imagesDirectory, short squareSize, string opponent, string version)
        {
            InitializeComponent();

            this._opponent = opponent;

            this.InitComms(comms);
            this.InitGrid();
            this.Grid.SetUp_FreeFormBoard(this, board, squareSize);

            this.InitHandlers();
            this.InitForms();

            //This will need to move to the ICommsHandler
            this.localPresenceManager = presenceManager;
            this.Init_PresenceManager();
        }

        private void InitComms(ICommsHandler newComms)
        {
            _comms = newComms;
            _comms.OpponentChat_Recieved += new OpponentChat(On_OpponentChat_RCV);
            _comms.OpponentMove_Recieved += new OpponentMove_Handler(On_OpponentMove_RCV);
        }

        //TODO: this is temporary code, and needs to be moved to ICommsHandler
        public void Init_PresenceManager()
        {
            this.localPresenceManager.OnPrimarySessionChange += new PrimarySessionHandler(this.presenceManager_OnPrimarySessionChange);

            this.UpdateStatusText(this.connectionLabel, "**");
            this.GetOpponentStatus();
        }

        #region PresenceManager Events

        public void presenceManager_OnPrimarySessionChange(object sender, jabber.JID bare)
        {
            //sender is a presenceManager
            string here = this.GetOpponentStatus();
            this.AddChat(bare.User + " has changed status to " + here); //TODO: This message needs to show the time.
        }

        #endregion
        #region Comms Events

        public void On_OpponentMove_RCV(object move)
        {
            Console.Beep(37, 70);

            this.InBox = (MovePacket)move;

            this.Process_InBox();
            this.SendOutBox();
        }
        public void On_OpponentChat_RCV(string sender)
        {
            Console.Beep(37, 70);
            this.AddChat(this.txtChat.Text + " " + sender.ToString());
        }

        #endregion
        #region Form Events

        private void ChessGrid2D_Load(object sender, EventArgs e)
        {
            this.Grid.SetUp_DefaultUIBoard(this);
            this.InitHandlers();
            //this.Init_PresenceManager();

            this.Text = "Chess Mangler " + this.Version + " ~ Opponent:  " + this.Opponent;
        }
        private void ChessGrid2D_Resize(object sender, EventArgs e)
        {
            this.Grid.Redraw(this.Grid.UIBoard.Flipped);
        }
        private void ChessGrid2D_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: tell the Comms_EventHandler to Dispose. //Is this still needed?

            ////TODO: Move this code to the Comms_EventHandler
            //if (_comms.IsAuthenticated)
            //{
            //    _comms.Close();
            //}
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
                _comms.Write(this.Opponent, this._squareHandlers.OutBox, MessageType.normal);

               // this.Grid.UIBoard.Squares.Disable();  //Until we recieve a good packet from the opponent
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

        //TODO: This button will only be seen in Debug Mode
        private void btnEnableSquares_Click(object sender, EventArgs e)
        {
            //TODO:  We need some visual indicators of all these things!
            this.Grid.UIBoard.Squares.Enable();
            this.Grid.UIBoard.Squares.ResetColors();
            this._squareHandlers.OutBox = null;
        }

        #endregion

        private void Process_InBox()
        {
            //Load up Move Packet
            MovePacket recievedMove = this.InBox;

            //this._squareHandlers.OutBox = recievedMove.GenerateRCVPacket();

            //Make sure this move is for this game (as we can have several going.)
            //not sure how this might be a problem, but I want to be prepared if we ever do..
            //_recievedMove.GameID;

            //This keeps us from getting this move confused with other moves. somebody needs to check this.  I don't know who..
            //_recievedMove.MoveHash;

            //If we got this far then the move packet is good
            //Send out a RECV Packet (no need to wait for any return msg)

            MoveBase.ExecuteMove(recievedMove, this.Grid.UIBoard.EngineBoard);
            this.Grid.Redraw(Grid.UIBoard.Flipped); //Syncs the UIBoard with the newly changed EngineBoard

            this.Grid.UIBoard.Squares.Enable();
            this.Grid.UIBoard.Squares.UnlockAll();
        }
        private void SendOutBox()
        {
            //throw new NotImplementedException();
        }

        public void InitForms()
        {
            ChessGrid2D_Form._debugForm = this.Grid.DebugForm;
        }
        public void InitGrid()
        {
            this.Grid = new Grid2D(this, this.Version);

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
            this.resetSquareColorsToolStripMenuItem.Click += new EventHandler(_menuBarHandlers.resetSquareColorsToolStripMenuItem_Click);
            this.toggleDebugModeToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.optionsToolStripMenuItem1.Click += new System.EventHandler(_menuBarHandlers.optionsToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click += new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);
            this.resetPiecesToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.resetPiecesToolStripMenuItem_Click);
            this.clearPiecesToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.clearPiecesToolStripMenuItem_Click);
            this.flipBoardToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.flipBoardToolStripMenuItem_Click);
            this.constrainBoardProportionsToolStripMenuItem.Click += new System.EventHandler(this._menuBarHandlers.constrainBoardProportionsToolStripMenuItem_Click);
        }
        public void DitchHandlers()
        {
            this._squareHandlers.Delete_Required_Square_Handlers(this);

            //Menu Handlers
            this.toggleDebugModeToolStripMenuItem.Click -= new System.EventHandler(_menuBarHandlers.toggleDebugModeToolStripMenuItem_Click);
            this.debugToolStripMenuItem.Click -= new System.EventHandler(_menuBarHandlers.debugToolStripMenuItem_Click);
            this.resetPiecesToolStripMenuItem.Click -= new System.EventHandler(this._menuBarHandlers.resetPiecesToolStripMenuItem_Click);
            this.resetSquareColorsToolStripMenuItem.Click -= new EventHandler(_menuBarHandlers.resetSquareColorsToolStripMenuItem_Click);
        }

        //TODO: it would be very nice to combine these delegate functions
        private delegate void TextDelegate(string message);

        /// <summary>
        /// When a chat is recieved, this guy is called
        /// </summary>
        /// <param name="message"></param>
        public void AddChat(string chatMessage)
        {
            if (this.txtChat.InvokeRequired)
            {
                this.txtChat.Invoke(new TextDelegate(this.AddChat), chatMessage);
            }
            else
            {
                //if options say so, then beep.

                //TODO: if option is set, then preface with time or contact name

                this.txtChatHistory.Text += Environment.NewLine + chatMessage;
                this.Scroll_ChatHistory_Box();
                this.txtChat.Clear();
            }
        }

        //private delegate void ControlTextDelegate(ToolStripStatusLabel tssLabel, Presence presence);
        private void UpdateStatusText(ToolStripStatusLabel tssLabel, Presence presence)    
        {
            string presenceType = String.Empty;

            switch (presence.Type)
            {
                case PresenceType.available:

                    if ((presence.Show == "dnd") || (presence.Show == "away"))
                    {
                        //see the following for more info:  http://code.google.com/p/jabber-net/issues/detail?id=70#c1
                        presenceType = "*Busy*";
                    }
                    else
                    {
                        presenceType = "(Available)";
                    }

                    break;

                case PresenceType.invisible:
                    presenceType = "(Invisible)";
                    break;

                case PresenceType.error:
                    presenceType = "(Error)";
                    break;

                case PresenceType.unsubscribed:
                    presenceType = "(Unsubscribed)";
                    break;

                case PresenceType.probe:
                    presenceType = "(probe)";
                    break;
            }

            //string status = presence.Status.Substring(0, 50); 

            tssLabel.Text = presence.Status + " " + presenceType;
        }
        private void UpdateStatusText(ToolStripStatusLabel tssLabel, string status)
        {
            tssLabel.Text = status;
        } 

        /// <summary>
        /// When the user hits "Send" in the Chat box, this is where it goes
        /// </summary>
        public void SendChat()
        {
            _comms.Message(this.Opponent, this.txtChat.Text);

            //This message needs to be logged in the database (remember to also add the ID of the latest move so we know at what point of the game that this was sent..)
            //Make an option later to create a PGN file annotated with chats.

            //if options say so, then beep.

            //TODO: if option is set, then preface with time or contact name

            this.txtChatHistory.Text += Environment.NewLine + this.txtChat.Text;
            this.Scroll_ChatHistory_Box();
            this.txtChat.Clear();

            //If message was sent and not recieved then post back to ChatHistory box as well. (it won't be here of course)
        }

        private void Scroll_ChatHistory_Box()
        {
            this.txtChatHistory.Select(txtChatHistory.Text.Length + 1, 2);
            this.txtChatHistory.ScrollToCaret();
        }

        private string GetOpponentStatus()
        {
            string here = "---";

            //TODO:  This  really needs to come in from the ICommsHandler, not passed in from the previous form.  

            if (this.Opponent != null)
            {
                Presence p = localPresenceManager[this.Opponent];

                if (p != null)
                {
                    here = p.Status;
                    this.UpdateStatusText(this.connectionLabel, p);
                }
                else
                {
                    this.UpdateStatusText(this.connectionLabel, "unknown");
                }
            }

            return here;
        }

        private void resetSquareColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}