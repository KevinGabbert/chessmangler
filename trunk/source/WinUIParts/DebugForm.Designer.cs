namespace ChessMangler.WinUIParts
{
    partial class DebugForm
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
            this.debugTextBox = new System.Windows.Forms.TextBox();
            this.clickedObject = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // debugTextBox
            // 
            this.debugTextBox.Location = new System.Drawing.Point(327, 28);
            this.debugTextBox.Multiline = true;
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.Size = new System.Drawing.Size(456, 450);
            this.debugTextBox.TabIndex = 0;
            // 
            // clickedObject
            // 
            this.clickedObject.AutoSize = true;
            this.clickedObject.Location = new System.Drawing.Point(9, 12);
            this.clickedObject.Name = "clickedObject";
            this.clickedObject.Size = new System.Drawing.Size(76, 13);
            this.clickedObject.TabIndex = 1;
            this.clickedObject.Text = "Clicked Object";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(277, 479);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "roll out the props of the currently selected object into this area  (nicely forma" +
                "tted of course)";
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 539);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.clickedObject);
            this.Controls.Add(this.debugTextBox);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox debugTextBox;
        private System.Windows.Forms.Label clickedObject;
        public System.Windows.Forms.TextBox textBox1;

    }
}