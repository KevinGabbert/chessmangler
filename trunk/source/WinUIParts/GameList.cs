using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;

using ChessMangler.Options.Interfaces;
using ChessMangler.Engine.Types;
using ChessMangler.Engine.Xml;
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
    public partial class GameList : Form, IVersion
    {
        //TODO: Jabber stuff to move into ICommsHandler
        private JabberClient jc;
        private RosterManager rosterManager;

        //TODO: this needs to be in ICommsHandler
        private PresenceManager presenceManager;
        private IdleTime idler;
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

        public string Version { get; set; }

        #endregion

        Comms _comms = new Comms();
        string _opponent;
        BindingList<OpponentList> opponentList = new BindingList<OpponentList>();

        public GameList()
        {
            InitializeComponent();

            //TODO: Load up my version packet:  <ChessMangler version="alpha" />

            //ApplicationInfo.GetVersion();
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

            this.idler = new bedrock.util.IdleTime();
            this.idler.InvokeControl = this;
            this.idler.OnIdle += new bedrock.util.SpanEventHandler(this.idler_OnIdle);
            this.idler.OnUnIdle += new bedrock.util.SpanEventHandler(this.idler_OnUnIdle);
        }

        private void txtOpponent_Leave(object sender, EventArgs e)
        {
            this.CheckForStart();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            jc = (JabberClient)this._comms.CommsHandler.originalHandler;
            jc.OnIQ += new IQHandler(this.jc_OnIQ);

            bool gotComms = this.GetGoogleComms();

            if (gotComms)
            {
                this.Init_RosterManager();
                this.Init_PresenceManager();

                if (this._comms.CommsHandler == null)
                {
                    this.SetStatus("Login Aborted: " + DateTime.Now.ToString());
                }
                else
                {
                    this.CheckForStart();
                }
            }

            this.btnLogin.Enabled = !gotComms;
            
        }
        private void btnOpenGrid_Click(object sender, EventArgs e)
        {
            //TODO: Check the other guy's version packet:  <ChessMangler version="alpha" />
            //This can also be done upon "Game Resume", which is planned later.
            _comms.CommsHandler.RequestOpponentVersion(this.dgvOpponents.SelectedCells[1].Value.ToString());

            //things could pause at this point as we wait for a response from the other side.. it should be instantaneous,
            //but what if they are not online?

            //Message:  Waiting for response from opponent

            //If no response.. (because person is offline)
            //   - Show the game as it currently exists. If not started yet, indicate with a message that the game has
            //     not been accepted yet.

            //If no response (and person is online) OR version = "Unknown"
            //      Tell user that their client is either not there or is broken

            this.StartGame();
        }

        private void StartGame()
        {
            //TODO: we should probably send a quick chat to start the game.
            if (this.tabControlGames.SelectedTab == tabFreeForm)
            {
                BoardDef board = new BoardDef((short)udGridX.Value, (short)udGridY.Value);

                //TODO: That last argument needs to be the selected item from a list of opponents on this page:

                //List<string> opponents = new List<string>();
                //opponents.Add("");

                //this.cboOpponents.DataSource = opponents;
                //this.cboOpponents.SelectedItem = this.cboOpponents[0];

                ChessGrid2D_Form open = new ChessGrid2D_Form(this.presenceManager, this._comms.CommsHandler, board, this.txtImages.Text, (short)udSquareSize.Value, _opponent, this.Version);
                open.Show();
            }

            if (this.tabControlGames.SelectedTab == tabRulesGame)
            {
                this.OpenChosenConfigFile();
            }
        }

        private void configList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckForStart();
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
        private void dgvOpponents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string cellContents = ((DataGridView)sender).CurrentCell.Value.ToString();

            //TODO:  Do they have ChessMangler?
            //string x = ((DataGridView)sender).CurrentRow.Cells["HasChessMangler"].ToString();

            this._opponent = cellContents + "@gmail.com";

            this.StartGame();
        }

        #endregion
        #region Comms Events

        private void GameListAuthenticate(object sender)
        {
            this.SetStatus("Authenticated @ " + DateTime.Now.ToString());

            ((JabberClient)this._comms.CommsHandler.originalHandler).Presence(PresenceType.available, "ChessMangler Start", "show", 2);

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
            //this.SetRoster(opponentRoster);
        }
        private void rosterManager_OnItem(object sender, Item item)
        {
            string user = item.JID.User;
            
            if (user != null)
            {
                if (user.Length > 0)
                {
                    OnlineType _onlineType = JabberHandler.GetItemPresence(item, this.presenceManager);

                    //TODO: param for if they have chessMangler

                    opponentList.Add(new OpponentList(user, "GTalk", _onlineType, _comms.CommsHandler.RequestOpponentVersion(user))); //TODO: comms.GetOpponentVersion 
                }
            }
        }
        private void rosterManager_OnRosterEnd(object sender)
        {
            this.rosterManager = (RosterManager)sender;

            //TODO: is the DGV formatted by this point?  it should be..  (columnsize, text color, etc)
            this.SetOpponentsDataSource(opponentList);
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
        #region PresenceManager Events

        public void presenceManager_OnPrimarySessionChange(object sender, jabber.JID bare)
        {
            //TODO:  This works correctly.

            //Go into the Roster and update this user's presence field 
            //(later this will be: color their text, or change icon)
        }

        #endregion

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
        private void idler_OnUnIdle(object sender, TimeSpan span)
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
                }
            }
        }

        private bool ValidFreeFormGame()
        {
            bool validGrid = (udGridX.Value > 0) || (udGridY.Value > 0);
            bool validSquares = (udSquareSize.Value > 0);

            return validGrid && validSquares;
        }
        private bool ValidSelectedFile()
        {
            bool retval = File.Exists(this.ConfigFilePath + "\\" + this.configList.SelectedValue.ToString() + ".config");

            if (!retval)
            {
                MessageBox.Show("Invalid config file for selected item");
            }

            return retval;
        }

        #endregion
        #region Functions

        private void Init_RosterManager()
        {
            this.rosterManager = new jabber.client.RosterManager(this.components);
            this.rosterManager.AutoAllow = jabber.client.AutoSubscriptionHanding.AllowAll;
            this.rosterManager.AutoSubscribe = true;
            this.rosterManager.Stream = (JabberClient)this._comms.CommsHandler.originalHandler;

            this.rosterManager.OnRosterBegin += new bedrock.ObjectHandler(this.rosterManager_OnBegin);
            this.rosterManager.OnRosterItem += new RosterItemHandler(this.rosterManager_OnItem);
            this.rosterManager.OnRosterEnd += new bedrock.ObjectHandler(this.rosterManager_OnRosterEnd);
            this.rosterManager.OnSubscription += new jabber.client.SubscriptionHandler(this.rosterManager_OnSubscription);
            this.rosterManager.OnUnsubscription += new jabber.client.UnsubscriptionHandler(this.rosterManager_OnUnsubscription);                  
        }
        private void Init_PresenceManager()
        {
            this.presenceManager = new jabber.client.PresenceManager(this.components);
            this.presenceManager.Stream = (JabberClient)this._comms.CommsHandler.originalHandler;
            this.presenceManager.OnPrimarySessionChange += new PrimarySessionHandler(this.presenceManager_OnPrimarySessionChange);
        }

        private bool GetGoogleComms()
        {
            //TODO:  At the moment, this event can't be reached via the interface.
            //This needs to be doen correctly.
            //For now, we are going to pass our authentication delegate directly into the Comms Handler
            return _comms.Connect(CommsType.Google, new Comms_Authenticate(GameListAuthenticate)); //TODO: later this will be assigned via a saved value in the DB, or User selection
        }
        private void CheckForStart()
        {
            this.btnOpenGrid.Enabled = (this._comms.CommsHandler != null) && 
                                       (_opponent != "") && 
                                       (_opponent != null) &&
                                       (this.ValidSelectedFile());
        }
        private void OpenChosenConfigFile()
        {
            ChessGrid2D_Form open = new ChessGrid2D_Form(this.presenceManager, this._comms.CommsHandler, _opponent, this.Version);

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

        private delegate void RosterDelegate(BindingList<OpponentList> dataSource);
        private void SetOpponentsDataSource(BindingList<OpponentList> dataSource)
        {
            if (this.dgvOpponents.InvokeRequired)
            {
                this.dgvOpponents.Invoke(new RosterDelegate(this.SetOpponentsDataSource), dataSource);
            }
            else
            {
                this.dgvOpponents.DataSource = dataSource;
            }
        }

        #endregion
    }
}
