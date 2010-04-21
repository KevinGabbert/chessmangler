﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using bedrock.util;

using ChessMangler.Communications.Handlers;
using ChessMangler.Communications.Types;
using ChessMangler.Communications.Types.Packets;
using ChessMangler.Engine.Types;
using ChessMangler.Options.Interfaces;
using ChessMangler.WinUIParts.ChessGrid2D;

using jabber.client;
using jabber.protocol.client;
using jabber.protocol.iq;

namespace ChessMangler.WinUIParts
{
    public partial class GameList : Form, IVersion
    {
        #region Stuff to move into ICommsHandler
        //TODO: Jabber stuff to move into ICommsHandler
        protected JabberClient jc;
        protected RosterManager rosterManager;

        //TODO: this needs to be in ICommsHandler
        protected PresenceManager presenceManager;
        protected IdleTime idler;
        protected JabberClient jabberClient = new JabberClient();
        //TODO: Jabber stuff to move into ICommsHandler
        #endregion
        #region Properties

        public bool LoggedIn { get; set; }
        public string ConfigFilePath { get; set; }
        public string Version { get; set; }

        #endregion

        protected Comms _comms = new Comms();
        protected BindingList<OpponentList> opponentList = new BindingList<OpponentList>();
        protected string _opponent;

        public GameList()
        {
            InitializeComponent();

            this.LoggedIn = false;

            #region Temporary test button (that will be removed later
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(166, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            this.Controls.Add(this.button2);

            #endregion

            //TODO: Load up my version packet:  <ChessMangler version="alpha" />
            //ApplicationInfo.GetMyGameVersion(); //this is different than Comms.GetOpponentVersion
        }

        #region Events
        #region Form Events

        //Temporary
        protected void button2_Click(object sender, EventArgs e)
        {
            _comms.CommsHandler.RequestOpponentCurrentGameVersion("test.chess.mangler", _comms.User); //@gmail.com
        }
        private void test_Click(object sender, EventArgs e)
        {
            //opens up a test form
            this.StartGame(true);

        }
        protected void GameList_Load(object sender, EventArgs e)
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

        protected void txtOpponent_Leave(object sender, EventArgs e)
        {
            this.CheckForStart();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
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
        protected void btnOpenGrid_Click(object sender, EventArgs e)
        {
            //TODO: Check the other guy's version packet:  <ChessMangler version="alpha" />
            //This can also be done upon "Game Resume", which is planned later.

            //we've already chosen our game by this point
            _comms.CommsHandler.RequestOpponentCurrentGameVersion("clickedUserName", this.dgvOpponents.SelectedCells[1].Value.ToString());

            //things could pause at this point as we wait for a response from the other side.. it should be instantaneous,
            //but what if they are not online?

            //Message:  Waiting for response from opponent

            //If no response.. (because person is offline)
            //   - Show the game as it currently exists. If not started yet, indicate with a message that the game has
            //     not been accepted yet.

            //If no response (and person is online) OR version = "Unknown"
            //      Tell user that their client is either not there or is broken

            this.StartGame(false);
        }

        protected void configList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckForStart();
        }
        protected void configList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.LoggedIn == false)
            {
                MessageBox.Show("You need to be logged in to choose a game");
                return;
            }

            if (_opponent != null)
            {
                this.OpenChosenConfigFile(false);
            }
            else
            {
                MessageBox.Show("An Opponent has not been selected.");
            }
        }

        protected void tabControlGames_SelectedIndexChanged(object sender, EventArgs e)
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
        protected void dgvOpponents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string cellContents = ((DataGridView)sender).CurrentCell.Value.ToString();

            //TODO:  Do they have ChessMangler?
            //string x = ((DataGridView)sender).CurrentRow.Cells["HasChessMangler"].ToString();

            this._opponent = cellContents + "@gmail.com";

            this.StartGame(false);
        }

        #endregion
        #region Comms Events

        protected void GameListAuthenticate(object sender)
        {
            this.SetStatus("Authenticated @ " + DateTime.Now.ToString());
            this.LoggedIn = true;

            ((JabberClient)this._comms.CommsHandler.originalHandler).Presence(PresenceType.available, "ChessMangler Start", "show", 2);
        }

        protected delegate void StatusDelegate(string message);
        protected void SetStatus(string message)
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

        protected void rosterManager_OnSubscription(RosterManager manager, Item ri, Presence pres)
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
        protected void rosterManager_OnUnsubscription(RosterManager manager, Presence pres, ref bool remove)
        {
            MessageBox.Show(pres.From + " has removed you from their roster.", "Unsubscription notification", MessageBoxButtons.OK);
        }
        protected void rosterManager_OnBegin(object sender)
        {
            //this.SetRoster(opponentRoster);
        }
        protected void rosterManager_OnItem(object sender, Item item)
        {
            string user = item.JID.User;
            
            if (user != null)
            {
                if (user.Length > 0)
                {
                    OnlineType _onlineType = JabberHandler.GetItemPresence(item, this.presenceManager);

                    //TODO: param for if they have chessMangler

                    //Sending our User name to contact will get us a version packet if they are running ChessMangler 
                    opponentList.Add(new OpponentList(user, "GTalk", _onlineType, _comms.CommsHandler.RequestOpponentCurrentGameVersion(user, _comms.User))); 
                }
            }
        }
        protected void rosterManager_OnRosterEnd(object sender)
        {
            this.rosterManager = (RosterManager)sender;

            //TODO: is the DGV formatted by this point?  it should be..  (columnsize, text color, etc)
            this.SetOpponentsDataSource(opponentList);
        }

        #endregion
        #region FreeForm Game Tab events
        protected void udGridX_ValueChanged(object sender, EventArgs e)
        {
            this.btnOpenGrid.Enabled = this.ValidFreeFormGame();
        }
        protected void udGridY_ValueChanged(object sender, EventArgs e)
        {
            this.btnOpenGrid.Enabled = this.ValidFreeFormGame();
        }
        protected void udSquareSize_ValueChanged(object sender, EventArgs e)
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

        #region TODO: this needs to be send to JabberHandlers
        protected void jc_OnIQ(object sender, IQ iq)
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
        protected void idler_OnUnIdle(object sender, TimeSpan span)
        {
            if (jc != null)
            {
                if (this.jc.IsAuthenticated)
                {
                    jc.Presence(PresenceType.available, "ChessMangler Available", null, 0);
                }
            }
        }

        //TODO:  Idler event needs to be put into CommsHandlers
        protected void idler_OnIdle(object sender, TimeSpan span)
        {
            if (jc != null)
            {
                if (this.jc.IsAuthenticated)
                {
                    jc.Presence(PresenceType.available, "ChessMangler Auto-away", "away", 0);
                }
            }
        }

        #endregion

        protected bool ValidFreeFormGame()
        {
            bool validGrid = (udGridX.Value > 0) || (udGridY.Value > 0);
            bool validSquares = (udSquareSize.Value > 0);

            return validGrid && validSquares;
        }
        protected bool ValidSelectedFile()
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

        protected void StartGame(bool testMode)
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
                this.OpenChosenConfigFile(testMode);
            }
        }

        protected void Init_RosterManager()
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
        protected void Init_PresenceManager()
        {
            this.presenceManager = new jabber.client.PresenceManager(this.components);
            this.presenceManager.Stream = (JabberClient)this._comms.CommsHandler.originalHandler;
            this.presenceManager.OnPrimarySessionChange += new PrimarySessionHandler(this.presenceManager_OnPrimarySessionChange);
        }

        public bool GetGoogleComms()
        {
            //TODO:  At the moment, this event can't be reached via the interface.
            //This needs to be doen correctly.
            //For now, we are going to pass our authentication delegate directly into the Comms Handler
            return _comms.Connect(CommsType.Google, new Comms_Authenticate(GameListAuthenticate)); //TODO: later this will be assigned via a saved value in the DB, or User selection
        }
        protected void CheckForStart()
        {
            this.btnOpenGrid.Enabled = (this._comms.CommsHandler != null) && 
                                       (_opponent != "") && 
                                       (_opponent != null) &&
                                       (this.ValidSelectedFile());
        }
        protected void OpenChosenConfigFile(bool testMode)
        {
            ChessGrid2D_Form gridForm = null;

            if (!testMode)
            {
                gridForm = new ChessGrid2D_Form(this.presenceManager, this._comms.CommsHandler, _opponent, this.Version);
            }
            else
            {
                gridForm = new ChessGrid2D_Form(new PresenceManager(), this._comms.CommsHandler, "Test Opponent", this.Version);
            }


            if (this.configList.SelectedValue.ToString() != null)
            {
                gridForm.Grid.RulesFilePath = this.ConfigFilePath + "\\" + this.configList.SelectedValue.ToString() + ".config";

                if (!File.Exists(this.ConfigFilePath))
                {
                    this.Visible = false;
                    gridForm.Show();
                }
                else
                {
                    MessageBox.Show("Config file not found: " + gridForm.Grid.RulesFilePath);
                }

            }
        }

        protected delegate void RosterDelegate(BindingList<OpponentList> dataSource);
        public void SetOpponentsDataSource(BindingList<OpponentList> dataSource)
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
