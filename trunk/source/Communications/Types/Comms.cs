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

        ICommsHandler _commsHandler = new JabberHandler();
        public ICommsHandler CommsHandler
        {
            get
            {
                return _commsHandler;
            }
            set
            {
                _commsHandler = value;
            }
        }

        #endregion

        public Comms()
        {
            _commsHandler.OpponentMove_Recieved += new OpponentMove_Handler(On_OpponentMove_RCV);
            //_commsHandler.OnMySend +=
        }

        //at the moment, can't find any purpose for this..
        public void On_OpponentMove_RCV(object move)
        {
            MovePacket currentMove = (MovePacket)move;

            

        }


        public string RequestOpponentVersion(string opponentName)
        {
            //throw new NotImplementedException();

            //this.write("whats yer version?")

            //Do until (timeout (2 seconds? - set in options DB ~ remember this is gonna be called for all opponents in the DGV..)
            //{
            //wait here..
            //}

            //if we still don't know by this point, come back with version unknown

            return "ROV Not Implemented Yet " + opponentName;
        }

        //TODO:  remove authenticateHandler as a parameter
        public bool Connect(CommsType commsType, Comms_Authenticate authenticateHandler)
        {
            bool success = false;

            switch (commsType)
            {
                case CommsType.Google:

                    //TODO:  Is this the best place for this?
                    Login_Form getUserInfo = new Login_Form(commsType); //Is this a "Jabber Only" Login Form?

                    getUserInfo.ShowDialog();  //Code Execution will stop at this point and wait until user has dismissed the Login form.

                    this.User = getUserInfo.User; // "Test.Chess.Mangler";
                    this.Password = getUserInfo.Password;
                    this.Server = getUserInfo.Server; // "gmail.com";
                    this.NetworkHost = getUserInfo.NetworkHost; // "talk.l.google.com";

                    success = ((this.User != null) && (this.User != "")) &&
                                   ((this.Password != null) && (this.Password != "")) &&
                                   ((this.Server != null) && (this.Server != "")) &&
                                   ((this.NetworkHost != null) && (this.NetworkHost != ""));

                    if (success)
                    {
                        this.CommsHandler = new JabberHandler(this.User, this.Password, this.Server, this.NetworkHost, authenticateHandler);
                    }

                    getUserInfo.Close();
                    getUserInfo.Dispose();

                    break;
            }

            return success;
        }
    }
}
