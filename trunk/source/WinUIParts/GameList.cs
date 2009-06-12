using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;

using ChessMangler.Engine.Types;
using ChessMangler.WinUIParts.ChessGrid2D;

using jabber.protocol.client;
using jabber.protocol.iq;

namespace ChessMangler.WinUIParts
{
    public partial class GameList : Form
    {
        private jabber.client.RosterManager rm;

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

        public GameList()
        {
            InitializeComponent();
        }

        #region Events

        private void GameList_Load(object sender, EventArgs e)
        {
            this.Init_RosterManager();

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
        }

        private void Init_RosterManager()
        {

            this.rm = new jabber.client.RosterManager(this.components);
            this.rm.AutoAllow = jabber.client.AutoSubscriptionHanding.AllowIfSubscribed;
            this.rm.AutoSubscribe = true;
            //this.rm.Stream = this.jc;

            this.rm.OnRosterEnd += new bedrock.ObjectHandler(this.rm_OnRosterEnd);
            this.rm.OnSubscription += new jabber.client.SubscriptionHandler(this.rm_OnSubscription);
            this.rm.OnUnsubscription += new jabber.client.UnsubscriptionHandler(this.rm_OnUnsubscription);
        }
        private void btnOpenGrid_Click(object sender, EventArgs e)
        {
            if (this.tabControlGames.SelectedTab == tabFreeForm)
            {
                BoardDef board = new BoardDef((short)udGridX.Value, (short)udGridY.Value);

                //TODO: That last argument needs to be the selected item from a list of opponents on this page:

                List<string> opponents = new List<string>();
                opponents.Add("kevingabbert@gmail.com");

                //this.cboOpponents.DataSource = opponents;
                //this.cboOpponents.SelectedItem = this.cboOpponents[0];

                ChessGrid2D_Form open = new ChessGrid2D_Form(board, this.txtImages.Text, (short)udSquareSize.Value, "kevingabbert@gmail.com");
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

        #region Roster Manger Events

        private void rm_OnSubscription(jabber.client.RosterManager manager, Item ri, Presence pres)
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
        private void rm_OnUnsubscription(jabber.client.RosterManager manager, Presence pres, ref bool remove)
        {
            MessageBox.Show(pres.From + " has removed you from their roster.", "Unsubscription notification", MessageBoxButtons.OK);
        }
        private void rm_OnRosterEnd(object sender)
        {
            roster.ExpandAll();
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

        private void OpenChosenConfigFile()
        {
            ChessGrid2D_Form open = new ChessGrid2D_Form();
            open.Opponent = "kevingabbert@gmail.com";

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
