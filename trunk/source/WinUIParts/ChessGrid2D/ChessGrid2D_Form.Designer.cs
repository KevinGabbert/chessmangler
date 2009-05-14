﻿namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public partial class ChessGrid2D_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chessMenu = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardChessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monsterChessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.jabberToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.yahooToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aIMFredFarkelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleJoeblowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.movesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.annotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capturedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uIConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoshouldThisBeSetInTheRulesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleDebugModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPiecesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.connectionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.modeButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvMoves = new System.Windows.Forms.DataGridView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnChatSend = new System.Windows.Forms.Button();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.clearPiecesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chessMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoves)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chessMenu
            // 
            this.chessMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.movesToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.chatToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.chessMenu.Location = new System.Drawing.Point(0, 0);
            this.chessMenu.Name = "chessMenu";
            this.chessMenu.Size = new System.Drawing.Size(669, 24);
            this.chessMenu.TabIndex = 0;
            this.chessMenu.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.configToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.connectionToolStripMenuItem.Text = "Game";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneToolStripMenuItem,
            this.entryToolStripMenuItem,
            this.forToolStripMenuItem,
            this.eachToolStripMenuItem,
            this.configToolStripMenuItem1,
            this.fileToolStripMenuItem,
            this.standardChessToolStripMenuItem,
            this.monsterChessToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // oneToolStripMenuItem
            // 
            this.oneToolStripMenuItem.Name = "oneToolStripMenuItem";
            this.oneToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.oneToolStripMenuItem.Text = "one";
            // 
            // entryToolStripMenuItem
            // 
            this.entryToolStripMenuItem.Name = "entryToolStripMenuItem";
            this.entryToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.entryToolStripMenuItem.Text = "entry";
            // 
            // forToolStripMenuItem
            // 
            this.forToolStripMenuItem.Name = "forToolStripMenuItem";
            this.forToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.forToolStripMenuItem.Text = "for";
            // 
            // eachToolStripMenuItem
            // 
            this.eachToolStripMenuItem.Name = "eachToolStripMenuItem";
            this.eachToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.eachToolStripMenuItem.Text = "each";
            // 
            // configToolStripMenuItem1
            // 
            this.configToolStripMenuItem1.Name = "configToolStripMenuItem1";
            this.configToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.configToolStripMenuItem1.Text = "config";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.fileToolStripMenuItem.Text = "file";
            // 
            // standardChessToolStripMenuItem
            // 
            this.standardChessToolStripMenuItem.Name = "standardChessToolStripMenuItem";
            this.standardChessToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.standardChessToolStripMenuItem.Text = "Standard Chess";
            // 
            // monsterChessToolStripMenuItem
            // 
            this.monsterChessToolStripMenuItem.Name = "monsterChessToolStripMenuItem";
            this.monsterChessToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.monsterChessToolStripMenuItem.Text = "Monster Chess";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.configToolStripMenuItem.Text = "Config";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountsToolStripMenuItem1,
            this.optionsToolStripMenuItem1});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.gameToolStripMenuItem.Text = "Connection";
            // 
            // accountsToolStripMenuItem1
            // 
            this.accountsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jabberToolStripMenuItem1,
            this.yahooToolStripMenuItem1,
            this.aIMFredFarkelToolStripMenuItem,
            this.googleJoeblowToolStripMenuItem});
            this.accountsToolStripMenuItem1.Name = "accountsToolStripMenuItem1";
            this.accountsToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.accountsToolStripMenuItem1.Text = "Accounts";
            // 
            // jabberToolStripMenuItem1
            // 
            this.jabberToolStripMenuItem1.Name = "jabberToolStripMenuItem1";
            this.jabberToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.jabberToolStripMenuItem1.Text = "Manage";
            // 
            // yahooToolStripMenuItem1
            // 
            this.yahooToolStripMenuItem1.Name = "yahooToolStripMenuItem1";
            this.yahooToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.yahooToolStripMenuItem1.Text = "(Yahoo) joe.blow";
            // 
            // aIMFredFarkelToolStripMenuItem
            // 
            this.aIMFredFarkelToolStripMenuItem.Name = "aIMFredFarkelToolStripMenuItem";
            this.aIMFredFarkelToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.aIMFredFarkelToolStripMenuItem.Text = "(AIM) Fred Farkel";
            // 
            // googleJoeblowToolStripMenuItem
            // 
            this.googleJoeblowToolStripMenuItem.Name = "googleJoeblowToolStripMenuItem";
            this.googleJoeblowToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.googleJoeblowToolStripMenuItem.Text = "(Google) joeblow";
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.optionsToolStripMenuItem1.Text = "Options";
            // 
            // movesToolStripMenuItem
            // 
            this.movesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.movesToolStripMenuItem1,
            this.annotateToolStripMenuItem,
            this.capturedToolStripMenuItem,
            this.uIConfigToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.movesToolStripMenuItem.Name = "movesToolStripMenuItem";
            this.movesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.movesToolStripMenuItem.Text = "Window";
            // 
            // movesToolStripMenuItem1
            // 
            this.movesToolStripMenuItem1.Name = "movesToolStripMenuItem1";
            this.movesToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.movesToolStripMenuItem1.Text = "Moves";
            // 
            // annotateToolStripMenuItem
            // 
            this.annotateToolStripMenuItem.Name = "annotateToolStripMenuItem";
            this.annotateToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.annotateToolStripMenuItem.Text = "Annotate";
            // 
            // capturedToolStripMenuItem
            // 
            this.capturedToolStripMenuItem.Name = "capturedToolStripMenuItem";
            this.capturedToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.capturedToolStripMenuItem.Text = "Captured";
            // 
            // uIConfigToolStripMenuItem
            // 
            this.uIConfigToolStripMenuItem.Name = "uIConfigToolStripMenuItem";
            this.uIConfigToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.uIConfigToolStripMenuItem.Text = "UI Config";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flipBoardToolStripMenuItem,
            this.rotateBoardToolStripMenuItem,
            this.undoshouldThisBeSetInTheRulesFileToolStripMenuItem,
            this.toggleDebugModeToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // flipBoardToolStripMenuItem
            // 
            this.flipBoardToolStripMenuItem.Name = "flipBoardToolStripMenuItem";
            this.flipBoardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.flipBoardToolStripMenuItem.Text = "Flip Board";
            // 
            // rotateBoardToolStripMenuItem
            // 
            this.rotateBoardToolStripMenuItem.Name = "rotateBoardToolStripMenuItem";
            this.rotateBoardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rotateBoardToolStripMenuItem.Text = "Rotate Board";
            // 
            // undoshouldThisBeSetInTheRulesFileToolStripMenuItem
            // 
            this.undoshouldThisBeSetInTheRulesFileToolStripMenuItem.Name = "undoshouldThisBeSetInTheRulesFileToolStripMenuItem";
            this.undoshouldThisBeSetInTheRulesFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.undoshouldThisBeSetInTheRulesFileToolStripMenuItem.Text = "Undo";
            // 
            // toggleDebugModeToolStripMenuItem
            // 
            this.toggleDebugModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetPiecesToolStripMenuItem,
            this.clearPiecesToolStripMenuItem});
            this.toggleDebugModeToolStripMenuItem.Name = "toggleDebugModeToolStripMenuItem";
            this.toggleDebugModeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toggleDebugModeToolStripMenuItem.Text = "Toggle Debug Mode";
            // 
            // resetPiecesToolStripMenuItem
            // 
            this.resetPiecesToolStripMenuItem.Name = "resetPiecesToolStripMenuItem";
            this.resetPiecesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resetPiecesToolStripMenuItem.Text = "Reset Pieces";
            // 
            // chatToolStripMenuItem
            // 
            this.chatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messageWindowToolStripMenuItem,
            this.configToolStripMenuItem2,
            this.showLogToolStripMenuItem});
            this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
            this.chatToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.chatToolStripMenuItem.Text = "Chat";
            // 
            // messageWindowToolStripMenuItem
            // 
            this.messageWindowToolStripMenuItem.Name = "messageWindowToolStripMenuItem";
            this.messageWindowToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.messageWindowToolStripMenuItem.Text = "Show Message Window";
            // 
            // configToolStripMenuItem2
            // 
            this.configToolStripMenuItem2.Name = "configToolStripMenuItem2";
            this.configToolStripMenuItem2.Size = new System.Drawing.Size(197, 22);
            this.configToolStripMenuItem2.Text = "Config";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.showLogToolStripMenuItem.Text = "Show Log";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionLabel,
            this.modeButton});
            this.statusBar.Location = new System.Drawing.Point(0, 599);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(669, 22);
            this.statusBar.TabIndex = 1;
            // 
            // connectionLabel
            // 
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(135, 17);
            this.connectionLabel.Text = "Connected (Joe FakeUser)";
            // 
            // modeButton
            // 
            this.modeButton.BackColor = System.Drawing.SystemColors.Control;
            this.modeButton.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.modeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.modeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.modeButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.modeButton.Name = "modeButton";
            this.modeButton.Size = new System.Drawing.Size(80, 19);
            this.modeButton.Text = "ModeButton";
            this.modeButton.Click += new System.EventHandler(this.modeButton_Click);
            // 
            // dgvMoves
            // 
            this.dgvMoves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMoves.Location = new System.Drawing.Point(0, 4);
            this.dgvMoves.Name = "dgvMoves";
            this.dgvMoves.Size = new System.Drawing.Size(216, 78);
            this.dgvMoves.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Location = new System.Drawing.Point(3, 6);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvMoves);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.btnChatSend);
            this.splitContainer.Panel2.Controls.Add(this.txtChat);
            this.splitContainer.Size = new System.Drawing.Size(657, 85);
            this.splitContainer.SplitterDistance = 219;
            this.splitContainer.TabIndex = 3;
            // 
            // btnChatSend
            // 
            this.btnChatSend.Location = new System.Drawing.Point(376, 6);
            this.btnChatSend.Name = "btnChatSend";
            this.btnChatSend.Size = new System.Drawing.Size(55, 75);
            this.btnChatSend.TabIndex = 1;
            this.btnChatSend.Text = "Send";
            this.btnChatSend.UseVisualStyleBackColor = true;
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(3, 4);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(368, 77);
            this.txtChat.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 482);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(669, 117);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(661, 91);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(661, 91);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // clearPiecesToolStripMenuItem
            // 
            this.clearPiecesToolStripMenuItem.Name = "clearPiecesToolStripMenuItem";
            this.clearPiecesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearPiecesToolStripMenuItem.Text = "Clear Pieces";
            // 
            // ChessGrid2D_Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 621);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.chessMenu);
            this.MainMenuStrip = this.chessMenu;
            this.Name = "ChessGrid2D_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChessGrid2D";
            this.Load += new System.EventHandler(this.ChessGrid2D_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChessGrid2D_Form_FormClosed);
            this.Resize += new System.EventHandler(this.ChessGrid2D_Resize);
            this.chessMenu.ResumeLayout(false);
            this.chessMenu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMoves)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem jabberToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem yahooToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem standardChessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem annotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monsterChessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capturedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uIConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoshouldThisBeSetInTheRulesFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messageWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        public System.Windows.Forms.MenuStrip chessMenu;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aIMFredFarkelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleJoeblowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleDebugModeToolStripMenuItem;
        public System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel connectionLabel;
        public System.Windows.Forms.ToolStripStatusLabel modeButton;
        private System.Windows.Forms.DataGridView dgvMoves;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.Button btnChatSend;
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem resetPiecesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPiecesToolStripMenuItem;



    }
}