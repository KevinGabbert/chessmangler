namespace ChessMangler.WinUIParts
{
    partial class GameList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameList));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.configList = new System.Windows.Forms.ListBox();
            this.btnOpenGrid = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlGames = new System.Windows.Forms.TabControl();
            this.tabRulesGame = new System.Windows.Forms.TabPage();
            this.tabFreeForm = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.udSquareSize = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImages = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udGridX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.udGridY = new System.Windows.Forms.NumericUpDown();
            this.tabFreeFormPresets = new System.Windows.Forms.TabPage();
            this.lstFreeFormPresets = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.roster = new muzzle.RosterTree();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtOpponent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControlGames.SuspendLayout();
            this.tabRulesGame.SuspendLayout();
            this.tabFreeForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSquareSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGridX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridY)).BeginInit();
            this.tabFreeFormPresets.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(290, 83);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // configList
            // 
            this.configList.FormattingEnabled = true;
            this.configList.Location = new System.Drawing.Point(3, 25);
            this.configList.Name = "configList";
            this.configList.Size = new System.Drawing.Size(276, 121);
            this.configList.TabIndex = 1;
            this.configList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.configList_MouseDoubleClick);
            this.configList.SelectedIndexChanged += new System.EventHandler(this.configList_SelectedIndexChanged);
            // 
            // btnOpenGrid
            // 
            this.btnOpenGrid.Location = new System.Drawing.Point(93, 467);
            this.btnOpenGrid.Name = "btnOpenGrid";
            this.btnOpenGrid.Size = new System.Drawing.Size(114, 23);
            this.btnOpenGrid.TabIndex = 2;
            this.btnOpenGrid.Text = "Start Game";
            this.btnOpenGrid.UseVisualStyleBackColor = true;
            this.btnOpenGrid.Click += new System.EventHandler(this.btnOpenGrid_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Games to play";
            // 
            // tabControlGames
            // 
            this.tabControlGames.Controls.Add(this.tabRulesGame);
            this.tabControlGames.Controls.Add(this.tabFreeForm);
            this.tabControlGames.Controls.Add(this.tabFreeFormPresets);
            this.tabControlGames.Location = new System.Drawing.Point(2, 116);
            this.tabControlGames.Name = "tabControlGames";
            this.tabControlGames.SelectedIndex = 0;
            this.tabControlGames.Size = new System.Drawing.Size(290, 182);
            this.tabControlGames.TabIndex = 4;
            this.tabControlGames.SelectedIndexChanged += new System.EventHandler(this.tabControlGames_SelectedIndexChanged);
            // 
            // tabRulesGame
            // 
            this.tabRulesGame.Controls.Add(this.configList);
            this.tabRulesGame.Controls.Add(this.label1);
            this.tabRulesGame.Location = new System.Drawing.Point(4, 22);
            this.tabRulesGame.Name = "tabRulesGame";
            this.tabRulesGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabRulesGame.Size = new System.Drawing.Size(282, 156);
            this.tabRulesGame.TabIndex = 0;
            this.tabRulesGame.Text = "Rules Game";
            this.tabRulesGame.UseVisualStyleBackColor = true;
            // 
            // tabFreeForm
            // 
            this.tabFreeForm.Controls.Add(this.label6);
            this.tabFreeForm.Controls.Add(this.label5);
            this.tabFreeForm.Controls.Add(this.udSquareSize);
            this.tabFreeForm.Controls.Add(this.button1);
            this.tabFreeForm.Controls.Add(this.label4);
            this.tabFreeForm.Controls.Add(this.txtImages);
            this.tabFreeForm.Controls.Add(this.label3);
            this.tabFreeForm.Controls.Add(this.groupBox1);
            this.tabFreeForm.Location = new System.Drawing.Point(4, 22);
            this.tabFreeForm.Name = "tabFreeForm";
            this.tabFreeForm.Padding = new System.Windows.Forms.Padding(3);
            this.tabFreeForm.Size = new System.Drawing.Size(282, 156);
            this.tabFreeForm.TabIndex = 1;
            this.tabFreeForm.Text = "Free Form Game";
            this.tabFreeForm.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(196, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "(if left blank, default images will be used)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Square Size";
            // 
            // udSquareSize
            // 
            this.udSquareSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udSquareSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udSquareSize.Location = new System.Drawing.Point(203, 52);
            this.udSquareSize.Name = "udSquareSize";
            this.udSquareSize.Size = new System.Drawing.Size(61, 29);
            this.udSquareSize.TabIndex = 4;
            this.udSquareSize.ValueChanged += new System.EventHandler(this.udSquareSize_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 20);
            this.button1.TabIndex = 9;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(261, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Folder with \"Chess Piece\" images to use in your game";
            // 
            // txtImages
            // 
            this.txtImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImages.Location = new System.Drawing.Point(3, 130);
            this.txtImages.Name = "txtImages";
            this.txtImages.Size = new System.Drawing.Size(240, 20);
            this.txtImages.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Set Up a Custom Game (does not use a rules file)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.udGridX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.udGridY);
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 54);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Grid";
            // 
            // udGridX
            // 
            this.udGridX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udGridX.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udGridX.Location = new System.Drawing.Point(21, 19);
            this.udGridX.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udGridX.Name = "udGridX";
            this.udGridX.Size = new System.Drawing.Size(43, 29);
            this.udGridX.TabIndex = 1;
            this.udGridX.ValueChanged += new System.EventHandler(this.udGridX_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X";
            // 
            // udGridY
            // 
            this.udGridY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udGridY.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udGridY.Location = new System.Drawing.Point(89, 19);
            this.udGridY.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.udGridY.Name = "udGridY";
            this.udGridY.Size = new System.Drawing.Size(45, 29);
            this.udGridY.TabIndex = 2;
            this.udGridY.ValueChanged += new System.EventHandler(this.udGridY_ValueChanged);
            // 
            // tabFreeFormPresets
            // 
            this.tabFreeFormPresets.Controls.Add(this.lstFreeFormPresets);
            this.tabFreeFormPresets.Location = new System.Drawing.Point(4, 22);
            this.tabFreeFormPresets.Name = "tabFreeFormPresets";
            this.tabFreeFormPresets.Size = new System.Drawing.Size(282, 156);
            this.tabFreeFormPresets.TabIndex = 2;
            this.tabFreeFormPresets.Text = "Free Form Presets";
            this.tabFreeFormPresets.UseVisualStyleBackColor = true;
            // 
            // lstFreeFormPresets
            // 
            this.lstFreeFormPresets.FormattingEnabled = true;
            this.lstFreeFormPresets.Location = new System.Drawing.Point(3, 15);
            this.lstFreeFormPresets.Name = "lstFreeFormPresets";
            this.lstFreeFormPresets.Size = new System.Drawing.Size(276, 134);
            this.lstFreeFormPresets.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(294, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
            this.toolStripMenuItem1.Text = "Options";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Select Opponent";
            // 
            // roster
            // 
            this.roster.AllowDrop = true;
            this.roster.Client = null;
            this.roster.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.roster.ImageIndex = 1;
            this.roster.Location = new System.Drawing.Point(2, 317);
            this.roster.Name = "roster";
            this.roster.PresenceManager = null;
            this.roster.RosterManager = null;
            this.roster.SelectedImageIndex = 0;
            this.roster.ShowLines = false;
            this.roster.ShowRootLines = false;
            this.roster.Size = new System.Drawing.Size(290, 75);
            this.roster.Sorted = true;
            this.roster.StatusColor = System.Drawing.Color.Teal;
            this.roster.TabIndex = 7;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(93, 438);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(114, 23);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "GTalk Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtOpponent
            // 
            this.txtOpponent.Location = new System.Drawing.Point(2, 407);
            this.txtOpponent.Name = "txtOpponent";
            this.txtOpponent.Size = new System.Drawing.Size(289, 20);
            this.txtOpponent.TabIndex = 9;
            this.txtOpponent.Leave += new System.EventHandler(this.txtOpponent_Leave);
            // 
            // GameList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 502);
            this.Controls.Add(this.txtOpponent);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.roster);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControlGames);
            this.Controls.Add(this.btnOpenGrid);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GameList";
            this.Text = "GameList";
            this.Load += new System.EventHandler(this.GameList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControlGames.ResumeLayout(false);
            this.tabRulesGame.ResumeLayout(false);
            this.tabFreeForm.ResumeLayout(false);
            this.tabFreeForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSquareSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGridX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridY)).EndInit();
            this.tabFreeFormPresets.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox configList;
        private System.Windows.Forms.Button btnOpenGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlGames;
        private System.Windows.Forms.TabPage tabRulesGame;
        private System.Windows.Forms.TabPage tabFreeForm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown udGridX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udGridY;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImages;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udSquareSize;
        private System.Windows.Forms.TabPage tabFreeFormPresets;
        private System.Windows.Forms.ListBox lstFreeFormPresets;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private muzzle.RosterTree roster;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtOpponent;
    }
}