using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;

using ChessMangler.Engine.Types;
using ChessMangler.Communications.Handlers;
using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Types;
using ChessMangler.WinUIParts.ChessGrid2D;

using jabber.client;
using jabber.connection;
using jabber.protocol.client;
using jabber.protocol.iq;

using bedrock.util;

namespace ChessMangler.WinUIParts
{
    public partial class GameList : Form
    {
        //TODO: Jabber stuff to move into ICommsHandler
        private JabberClient jc;
        private RosterManager rosterManager;
        private PresenceManager presenceManager;

        private DiscoManager discoManager;
        private CapsManager capsManager;
        private PubSubManager pubSubManager;
        private IdleTime idler;

        jabber.connection.Ident ident1 = new jabber.connection.Ident();

        private JabberClient jabberClient = new JabberClient();
        //TODO: Jabber stuff to move into ICommsHandler

        #region Properties

        string _configFilePath;
        public string ConfigFilePath
        {
            get
            {
                return _configFilePath;
            }
            set
            {
                _configFilePath = value;
            }
        }

        #endregion

        ICommsHandler _comms;

        public GameList()
        {
            InitializeComponent();


        }

        #region Events

        #region Form Events
        private void GameList_Load(object sender, EventArgs e)
        {
            //Looks through config directory, and list what Config files are found
            string configDir = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString() + "\\Config";

            List<string> configFiles = new List<string>();

            if (Directory.Exists(configDir))
            {
                foreach (string file in Directory.GetFiles(configDir, "*.config"))
                {
                    configFiles.Add(Path.GetFileName(file).Replace(".config", ""));
                }

                this.ConfigFilePath = configDir;
            }
            else
            {
                MessageBox.Show("Config Directory not found: " + configDir, "ChessMangler");
            }

            this.configList.DataSource = configFiles;
            this.btnOpenGrid.Enabled = false;
        }
        private void txtOpponent_Leave(object sender, EventArgs e)
        {
            this.CheckForStart();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this._comms = this.GetGoogleComms();
            this.Init_RosterManager();
            this.Init_PresenceManager();

            this.idler = new bedrock.util.IdleTime();
            this.idler.InvokeControl = this;
            this.idler.OnIdle += new bedrock.util.SpanEventHandler(this.idler_OnIdle);
            this.idler.OnUnIdle += new bedrock.util.SpanEventHandler(this.idler_OnUnIdle);

            jc = (JabberClient)this._comms.originalHandler;
            jc.OnIQ += new jabber.client.IQHandler(this.jc_OnIQ);

            this.discoManager = new jabber.connection.DiscoManager(this.components);
            this.discoManager.Stream = this.jc;

            this.capsManager = new jabber.connection.CapsManager(this.components);
            this.capsManager.DiscoManager = this.discoManager;
            this.capsManager.Features = new string[0];
            //this.capsManager.FileName = "caps.xml";

            ident1.Category = "client";
            ident1.Lang = "en";
            ident1.Name = "Jabber-Net Test Client";
            ident1.Type = "pc";
            this.capsManager.Identities = new jabber.connection.Ident[] {ident1};
            this.capsManager.Node = "ChessManglerCapsMan";
            this.capsManager.Stream = this.jc;

            //this.pubSubManager = new jabber.connection.PubSubManager(this.components);
            //this.muc = new jabber.connection.ConferenceManager(this.components);

            this.init_RosterTree();

            if (this._comms == null)
            {
                this.SetStatus("Login Aborted: " + DateTime.Now.ToString());
            }
            else
            {
                this.CheckForStart();
            }

            
            opponentRoster.Refresh();
        }



        private void btnOpenGrid_Click(object sender, EventArgs e)
        {
            if (this.tabControlGames.SelectedTab == tabFreeForm)
            {
                BoardDef board = new BoardDef((short)udGridX.Value, (short)udGridY.Value);

                //TODO: That last argument needs to be the selected item from a list of opponents on this page:

                //List<string> opponents = new List<string>();
                //opponents.Add("");

                //this.cboOpponents.DataSource = opponents;
                //this.cboOpponents.SelectedItem = this.cboOpponents[0];

                ChessGrid2D_Form open = new ChessGrid2D_Form(this._comms, board, this.txtImages.Text, (short)udSquareSize.Value, this.txtOpponent.Text);
                open.Show();
            }

            if (this.tabControlGames.SelectedTab == tabRulesGame)
            {
                this.OpenChosenConfigFile();
            }
        }
        private void configList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOpenGrid.Enabled = this.ValidSelectedFile();
        }
        private void configList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.OpenChosenConfigFile();
        }
        private void tabControlGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControlGames.SelectedTab == tabFreeForm)
            {
                this.btnOpenGrid.Enabled = this.ValidFreeFormGame();
            }

            if (this.tabControlGames.SelectedTab == tabRulesGame)
            {
                this.btnOpenGrid.Enabled = this.btnOpenGrid.Enabled = this.ValidSelectedFile();
            }

            if (this.tabControlGames.SelectedTab == tabFreeFormPresets)
            {
                this.btnOpenGrid.Enabled = false;
            }
        }
        #endregion
        #region Comms Events

        private void GameListAuthenticate(object sender)
        {
            this.SetStatus("Authenticated @ " + DateTime.Now.ToString());

            //TODO:  I'd like this to be here.. Right now it is in the JabberHandler.
            //This means we are going to need to access the client somehow, so make a method in the interface to 
            //deal with this.
            //jabberClient.Presence(PresenceType.available, "ChessMangler Online", "show", 2);
        }

        private delegate void StatusDelegate(string message);
        private void SetStatus(string message)
        {
            if (this.lblStatus.InvokeRequired)
            {
                this.lblStatus.Invoke(new StatusDelegate(this.SetStatus), message);
            }
            else
            {
                this.lblStatus.Text = message;
            }
        }

        #endregion
        #region Roster Manger Events

        private void rosterManager_OnSubscription(RosterManager manager, Item ri, Presence pres)
        {
            DialogResult res = MessageBox.Show("Allow incoming presence subscription request from: " + pres.From,
                "Subscription Request",
                MessageBoxButtons.YesNoCancel);
            switch (res)
            {
                case DialogResult.Yes:
                    manager.ReplyAllow(pres);
                    break;
                case DialogResult.No:
                    manager.ReplyDeny(pres);
                    break;
                case DialogResult.Cancel:
                    // do nothing;
                    break;
            }
        }
        private void rosterManager_OnUnsubscription(RosterManager manager, Presence pres, ref bool remove)
        {
            MessageBox.Show(pres.From + " has removed you from their roster.", "Unsubscription notification", MessageBoxButtons.OK);
        }

        private void rosterManager_OnBegin(object sender)
        {
            
        }
        private void rosterManager_OnRosterEnd(object sender)
        {
            opponentRoster.ExpandAll();
        }

        //TODO: this needs to be send to JabberHandlers
        private void jc_OnIQ(object sender, IQ iq)
        {
            if (iq.Type != IQType.get)
                return;

            XmlElement query = iq.Query;
            if (query == null)
                return;

            // <iq id="jcl_8" to="me" from="you" type="get"><query xmlns="jabber:iq:version"/></iq>
            if (query is jabber.protocol.iq.Version)
            {
                iq = iq.GetResponse(jc.Document);
                jabber.protocol.iq.Version ver = iq.Query as jabber.protocol.iq.Version;
                if (ver != null)
                {
                    ver.OS = Environment.OSVersion.ToString();
                    ver.EntityName = Application.ProductName;
                    ver.Ver = Application.ProductVersion;
                }
                jc.Write(iq);
                return;
            }

            if (query is Time)
            {
                iq = iq.GetResponse(jc.Document);
                Time tim = iq.Query as Time;
                if (tim != null) tim.SetCurrentTime();
                jc.Write(iq);
                return;
            }

            if (query is Last)
            {
                iq = iq.GetResponse(jc.Document);
                Last last = iq.Query as Last;
                if (last != null) last.Seconds = (int)IdleTime.GetIdleTime();
                jc.Write(iq);
                return;
            }
        }

        //TODO:  Idler event needs to be put into CommsHandlers
        void idler_OnUnIdle(object sender, TimeSpan span)
        {
            if (jc != null)
            {
                if (this.jc.IsAuthenticated)
                {
                    jc.Presence(PresenceType.available, "ChessMangler Available", null, 0);
                    //pnlPresence.Text = "Available";
                }
            }
        }

        //TODO:  Idler event needs to be put into CommsHandlers
        private void idler_OnIdle(object sender, TimeSpan span)
        {
            if (jc != null)
            {
                if (this.jc.IsAuthenticated)
                {
                    jc.Presence(PresenceType.available, "ChessMangler Auto-away", "away", 0);
                    //pnlPresence.Text = "Away";
                }
            }
        }

        #endregion
        #region OpponentRoster Events

        private void opponentRoster_DoubleClick(object sender, EventArgs e)
        {

        }

        #endregion
        #region FreeForm Game Tab events
        private void udGridX_ValueChanged(object sender, EventArgs e)
        {
            this.btnOpenGrid.Enabled = this.ValidFreeFormGame();
        }

        private void udGridY_ValueChanged(object sender, EventArgs e)
        {
            this.btnOpenGrid.Enabled = this.ValidFreeFormGame();
        }

        private void udSquareSize_ValueChanged(object sender, EventArgs e)
        {
            this.btnOpenGrid.Enabled = this.ValidFreeFormGame();
        }

        #endregion

        private bool ValidFreeFormGame()
        {
            bool validGrid = (udGridX.Value > 0) || (udGridY.Value > 0);
            bool validSquares = (udSquareSize.Value > 0);

            return validGrid && validSquares;
        }
        private bool ValidSelectedFile()
        {
            return File.Exists(this.ConfigFilePath + "\\" + this.configList.SelectedValue.ToString() + ".config");
        }

        #endregion
        #region Functions

        private void Init_RosterManager()
        {
            if (this._comms != null)
            {
                if (this._comms.originalHandler != null)
                {
                    this.rosterManager = new jabber.client.RosterManager(this.components);
                    this.rosterManager.AutoAllow = jabber.client.AutoSubscriptionHanding.AllowIfSubscribed;
                    this.rosterManager.AutoSubscribe = true;
                    this.rosterManager.Stream = (JabberClient)this._comms.originalHandler;

                    this.rosterManager.OnRosterBegin += new bedrock.ObjectHandler(this.rosterManager_OnBegin);
                    this.rosterManager.OnRosterEnd += new bedrock.ObjectHandler(this.rosterManager_OnRosterEnd);
                    this.rosterManager.OnSubscription += new jabber.client.SubscriptionHandler(this.rosterManager_OnSubscription);
                    this.rosterManager.OnUnsubscription += new jabber.client.UnsubscriptionHandler(this.rosterManager_OnUnsubscription);

                   
                }
                else
                {
                    //TODO:  throw some kind of error telling the programmer he's a moron
                }
            }
        }
        private void Init_PresenceManager()
        {
            this.presenceManager = new jabber.client.PresenceManager(this.components);

            if (this._comms != null)
            {
                if (this._comms.originalHandler != null)
                {
                    this.presenceManager.Stream = (JabberClient)this._comms.originalHandler;
                    ((JabberClient)this._comms.originalHandler).Presence(PresenceType.available, "ChessMangler Online", "show", 2);
                }
                else
                {
                    //TODO:  throw some kind of error telling the programmer he's a moron
                }
            }
        }

        private void init_RosterTree()
        {
            //TODO: This should ref ICommsHandlers props
            if (this._comms != null)
            {
                if (this._comms.originalHandler != null)
                {
                    //This guys all need to be initialized by the time we get this far..
                    this.opponentRoster.Client = (JabberClient)this._comms.originalHandler;
                    this.opponentRoster.RosterManager = this.rosterManager;
                    this.opponentRoster.PresenceManager = this.presenceManager;
                }
                else
                {
                    //TODO:  throw some kind of error telling the programmer he's a moron
                }
            }
        }

        private ICommsHandler GetGoogleComms()
        {
            //TODO:  At the moment, this event can't be reached via the interface.
            //This needs to be doen correctly.
            //For now, we are going to pass our authentication delegate directly into the Comms Handler
            _comms = (new Comms()).Connect(CommsType.Google, new Comms_Authenticate(GameListAuthenticate)); //TODO: later this will be assigned via a saved value in the DB, or User selection
            
            return _comms;
        }
        private void CheckForStart()
        {
            this.btnOpenGrid.Enabled = (this._comms != null) && (this.txtOpponent.Text != "");
        }
        private void OpenChosenConfigFile()
        {
            ChessGrid2D_Form open = new ChessGrid2D_Form(this._comms);
            open.Opponent = this.txtOpponent.Text;

            if (this.configList.SelectedValue.ToString() != null)
            {
                open.Grid.RulesFilePath = this.ConfigFilePath + "\\" + this.configList.SelectedValue.ToString() + ".config";

                if (!File.Exists(this.ConfigFilePath))
                {
                    this.Visible = false;
                    open.Show();
                }
                else
                {
                    MessageBox.Show("Config file not found: " + open.Grid.RulesFilePath);
                }

            }
        }

        #endregion
    }
}
