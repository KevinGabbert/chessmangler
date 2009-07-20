using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.Communications.Types
{
    public class OpponentList
    {
        private string _name;
        private string _service;
        private OnlineType _online;
        private string _cmVersion;

        public OpponentList(string opponentName, string imService, OnlineType online, string cmVersion)
        {
            this.Name = opponentName;
            this.Service = imService;
            this.Online = online;
            this.CMVersion = cmVersion;
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Service
        {
            get { return _service; }
            set { _service = value; }
        }
        public OnlineType Online
        {
            get { return _online; }
            set { _online = value; }
        }
        public string CMVersion
        {
            get
            {
                return _cmVersion;
            }
            set
            {
                _cmVersion = value;
            }
        }
    }
}
