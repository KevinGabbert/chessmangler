using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ChessMangler.Communications.Interfaces;

namespace ChessMangler.Communications.Forms
{
    /// <summary>
    /// This form is designed to pop up whenever login information is not known (and pre-populated)
    /// normally, the user would never see this form, and if they do, it would be rare.
    /// </summary>
    public partial class Login_Form : Form, IJabberCredentials
    {
        #region Properties

        string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        string _server;
        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        string _networkHost;
        public string NetworkHost
        {
            get
            {
                return _networkHost;
            }
            set
            {
                _networkHost = value;
            }
        }

        #endregion

        public Login_Form()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.User = this.txtLogin.Text;
            this.Password = this.mtxtPassword.Text;
            this.Server = this.txtServer.Text;
            this.NetworkHost = this.txtNetworkHost.Text;

            this.Hide();
        }
    }
}
