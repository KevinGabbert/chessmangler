﻿using System;
using System.Windows.Forms;

using ChessMangler.Engine.Types;
using ChessMangler.Settings.Types.WinUI;
using ChessMangler.Communications.Types;
using ChessMangler.Communications.Handlers;

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

        JabberHandler _jabberHandlers = new JabberHandler();
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

        private void InitJabber()
        {
            _jabberHandlers.OpponentChat_Recieved += new JabberHandler.OpponentChat(On_Opponent_RCV);
            _jabberHandlers.OpponentMove_Recieved += new IM_Handler_Base.OpponentMove_Handler(On_OpponentMove_RCV);
        }

        public void On_OpponentMove_RCV(object move)
        {
            Console.Beep(37, 70);

            this.InBox = (MovePacket)move;
            this.Process_InBox();
            this.SendOutBox();
        }
        public void On_Opponent_RCV(string sender)
        {
            Console.Beep(37, 70);
            this.AddChat(this.txtChat.Text + " " + sender.ToString());
        }

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

            ////TODO: Move this code to the Jabber_EventHandler
            //if (jabberClient.IsAuthenticated)
            //{
            //    jabberClient.Close();
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
                _jabberHandlers.Write(this.JabberOpponent, this._squareHandlers.OutBox);

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

        //#region TODO: These events need to be placed in a Jabber_EventHandler

        //private void j_OnMessage(object sender, jabber.protocol.client.Message msg)
        //{
        //    if (msg.Body != null)
        //    {
        //        this.AddChat(this.txtChat.Text + msg.Body);
        //    }
        //    else
        //    {
        //        //*** If DebugMode
        //        //this.AddChat("Move Recieved from: " + this.JabberOpponent + "\r\n" + msg.OuterXml);
        //        Console.Beep(37, 70);
        //        //*** If DebugMode

        //        MovePacket recievedMove = new MovePacket(msg);

        //        if (recievedMove.Invalid)
        //        {
        //            //if we have problems here, send a REJECT packet or something
        //            this._squareHandlers.OutBox = recievedMove.GenerateRejectPacket();

        //            //so.. at this point, we are back to our previous state.  Nothing gained.

        //            //Tell the user about it, however..

        //            //Flash something yellow, like the title bar to the chat window or something.
        //            //This would be good for all erroneous move communication

        //            //also, cue up the chat window.  Select its tab.  Make the text red with asterisks.
        //            this.AddChat("Invalid Move recieved. " + recievedMove.InvalidMoveReason + " Try again?");
        //        }

        //        this.InBox = recievedMove; 

        //        this.Process_InBox();
        //        this.SendOutBox();
        //    }
        //}

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

            this.Grid.UIBoard.EngineBoard.ExecuteMove(recievedMove);
            this.Grid.Redraw(Grid.UIBoard.Flipped); //Syncs the UIBoard with the newly changed EngineBoard

            this.Grid.UIBoard.Squares.Enable();
            this.Grid.UIBoard.Squares.UnlockAll();
        }
        private void SendOutBox()
        {
            //throw new NotImplementedException();
        }

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
            _jabberHandlers.Message(this.JabberOpponent, this.txtChat.Text);

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