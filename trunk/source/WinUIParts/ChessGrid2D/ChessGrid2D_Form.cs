using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Threading;

using System.Collections.Generic;

using jabber.client;
using jabber.protocol;
using jabber.protocol.client;

using ChessMangler.Engine.Interfaces;
using ChessMangler.Engine.Config;
using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;
using ChessMangler.Settings.Types.WinUI;
using ChessMangler.Engine.Enums;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid2D_Form : GridForm
    {
        static DebugForm _debugForm;

        //**** jabber proto code ****

        //We should know all this by this point (will be set in options form
        JabberClient jabberClient = new JabberClient();

        // we will wait on this event until we're done sending
        static ManualResetEvent done = new ManualResetEvent(false);

        //Our proto Target..
        const string TARGET = "kevingabbert@gmail.com";

        //**** jabber proto code ****

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
        private void ChessGrid2D_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            this.Grid.Toggle_BoardMode();
        }

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


        private void btnChatSend_Click(object sender, EventArgs e)
        {
            //jabber.protocol.client.Message msg = new jabber.protocol.client.Message(m_jc.Document);
            //msg.To = txtTo.Text;
            //if (txtSubject.Text != "")
            //    msg.Subject = txtSubject.Text;
            //msg.Body = txtBody.Text;
            //m_jc.Write(msg);
            //this.Close();

            this.SendChat();
        }
        private void txtChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SendChat();
            }
        }

        public void SendChat()
        {
            jabberClient.Message(TARGET, this.txtChat.Text);

            //if options say so, then beep.

            //TODO: if option is set, then preface with time or contact name

            this.txtChatHistory.Text += Environment.NewLine + this.txtChat.Text;
            this.Scroll_ChatHistory_Box();
            this.txtChat.Clear();

            //If message was sent and not recieved then post back to ChatHistory box as well. (it won't be here of course)
        }

        //**** Refactor ****
        //jabber events

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

        private void j_OnMessage(object sender, jabber.protocol.client.Message msg)
        {
            //jabber.protocol.x.Data x = msg["x", URI.XDATA] as jabber.protocol.x.Data;
            //if (x != null)
            //{
            //    //muzzle.XDataForm f = new muzzle.XDataForm(msg);
            //    //f.ShowDialog(this);
            //    //j.Write(f.GetResponse());
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
        private void ChessGrid2D_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (jabberClient.IsAuthenticated)
            {
                jabberClient.Close();
            }
        }

        private void btnSubmitMove_Click(object sender, EventArgs e)
        {
            jabber.protocol.client.Message message = new jabber.protocol.client.Message(new XmlDocument()); 

            message.To = new jabber.JID(TARGET);

            your.protocol.YourQuery yq = new your.protocol.YourQuery(message.OwnerDocument); 
            yq.InnerText = "I am a test"; 

            message.AddChild(yq);

            jabberClient.Write(message);

            //<message id="JN_4" to="kevingabbert@gmail.com">
            //    <query xmlns="your:namespace">I am a test</query>
            //</message>


            //jabberClient.Message(TARGET, this.txtChat.Text);
        }

        static void j_OnWriteText(object sender, string txt)
        {
            if (txt == " ") return;  // ignore keep-alive spaces
            //Console.WriteLine("SEND: " + txt);
            ///System.Windows.Forms.MessageBox.Show(txt, "--- WRITE ---");
        }
    }
}

//**more proto code

//<!-- get your own list of all your objects... -->
//<iq type='get' to='self' id='n0'>
// <query xmlns='your:namespace'/>
//</iq>

//<!-- reply with list of objects... -->
//<iq type='result' to='self' id='n0'>
// <query xmlns='your:namespace'>
//  <yourobj key='Object1' other='value1'/>
// </query>
//</iq>

namespace your.protocol
{
    public class YourQuery : Element
    {
        public const string YOUR_NS = "your:namespace";

        // used when creating elements to send
        public YourQuery(XmlDocument doc)
            : base("query", YOUR_NS, doc)
        { }

        // used to create elements for inbound protocol
        public YourQuery(string prefix, XmlQualifiedName qname, XmlDocument doc)
            : base(prefix, qname, doc)
        { }

        // put your accessor methods here
    }

    public class Factory : jabber.protocol.IPacketTypes
    {
        private static QnameType[] s_qnt = new QnameType[] 
        {
            new QnameType("query", YourQuery.YOUR_NS, typeof(your.protocol.YourQuery))
            // Add other types here, perhaps sub-elements of query...
        };

        QnameType[] IPacketTypes.Types { get { return s_qnt; } }

        private void jabberClient_OnStreamInit(object sender, ElementStream stream)
        {
            stream.AddFactory(new your.protocol.Factory());
        }
    }
}

