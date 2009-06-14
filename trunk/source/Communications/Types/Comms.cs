using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Handlers;
using ChessMangler.Communications.Forms;

namespace ChessMangler.Communications.Types
{
    public class Comms : IJabberCredentials
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

        //TODO:  remove authenticateHandler as a parameter
        public ICommsHandler Connect(CommsType commsType, Comms_Authenticate authenticateHandler)
        {
            ICommsHandler returnVal = null;

            switch (commsType)
            {
                case CommsType.Google:

                    //TODO:  Is this the best place for this?
                    Login_Form getUserInfo = new Login_Form(); //Is this a "Jabber Only" Login Form?

                    getUserInfo.ShowDialog();  //Code Execution will stop at this point and wait until user has dismissed the Login form.

                    this.User = getUserInfo.User; // "Test.Chess.Mangler";
                    this.Password = getUserInfo.Password;
                    this.Server = getUserInfo.Server; // "gmail.com";
                    this.NetworkHost = getUserInfo.NetworkHost; // "talk.l.google.com";

                    bool success = ((this.User != null) && (this.User != "")) &&
                                   ((this.Password != null) && (this.Password != "")) &&
                                   ((this.Server != null) && (this.Server != "")) &&
                                   ((this.NetworkHost != null) && (this.NetworkHost != ""));

                    if (success)
                    {
                        returnVal = new JabberHandler(this.User, this.Password, this.Server, this.NetworkHost, authenticateHandler);
                    }
                    else
                    {
                        returnVal = null;
                    }

                    getUserInfo.Close();
                    getUserInfo.Dispose();

                    break;
            }

            return returnVal;
        }
    }
}
