using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.Communications.Interfaces
{
    public interface IJabberCredentials
    {
        string User
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        string Server
        {
            get;
            set;
        }

        string NetworkHost
        {
            get;
            set;
        }
    }
}
