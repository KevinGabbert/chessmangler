using System;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

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

        //TODO: Move to Jabber_EventHandler **** jabber proto code ****

        //We should know all this by this point (will be set in options form
        JabberClient jabberClient = new JabberClient();

        //TODO:  this goes in Jabber_EventHandler we will wait on this event until we're done sending
        static ManualResetEvent done = new ManualResetEvent(false);

        //TODO: **** jabber proto code **** This target will eventually be set by the Start Form when the user chooses who to play.
        const string TARGET = "kevingabbert@gmail.com";

        ChessGrid2D_MenuBarHandlers _menuBarHandlers;
        ChessGrid2D_Settings _gridOptions = new ChessGrid2D_Settings();

        public ChessGrid2D_Form()
        {
            InitializeComponent();

            this.InitJabber();
            this.InitGrid();
            this.InitForms();
        }
        public ChessGrid2D_Form(BoardDef board, string imagesDirectory, short squareSize)
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

            //extra
            jabberClient.OnWriteText += new bedrock.TextHandler(j_OnWriteText);

            // listen for XMPP wire protocol
            //jabberClient.OnWriteText += new bedrock.TextHandler(j_OnWriteText);
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
        }
        private void ChessGrid2D_Resize(object sender, EventArgs e)
        {
            this.Grid.Redraw();
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
            //TODO: Move this to a "JabberTasks" static class in Comms. (later this will be abstracted out as well)
            JabberMessage message = new JabberMessage(new XmlDocument()); //Should MovePacket be here??
            message.To = new jabber.JID(TARGET);
            message.AddChild(MovePacket.SetupPacket("1234", "5678", "King", "E1", "E2", false));

            jabberClient.Write(message);
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
            //jabber.protocol.x.Data x = msg["x", URI.XDATA] as jabber.protocol.x.Data;
            //if (x != null)
            //{
            //    //muzzle.XDataForm f = new muzzle.XDataForm(msg);
            //    //f.ShowDialog(this);
            //    //j.Write(f.GetResponse());
            //}

            //if (msg.Supports(typeof(MovePacket)))
            //{

            //    //MovePacket move = msg.SelectSingleNode(typeof(MovePacket)) as MovePacket;

            //    //Console.WriteLine(move.Hash.InnerText);
            //}

            this.AddChat(this.txtChat.Text + msg.Body);
        }
        private void j_OnAuthenticate(object sender)
        {
            // Sender is always the JabberClient.
            JabberClient j = (JabberClient)sender;
            j.Message(TARGET, "ChessMangler <Session> Connected @ " + DateTime.Now.ToString());

            done.Set(); // Finished sending.  Shut down.
        }
        private void j_OnError(object sender, Exception ex)
        {
            // There was an error!
            MessageBox.Show("Error: " + ex.ToString(), "Jabber-Net TestHarness");

            // Shut down.
            done.Set();
        }
        static void j_OnWriteText(object sender, string txt)
        {
            if (txt == " ") return;  // ignore keep-alive spaces
        }

        #endregion
        #endregion

        public void InitGrid()
        {
            this.Grid = new Grid2D(this);
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
            jabberClient.Message(TARGET, this.txtChat.Text);

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
    }
}