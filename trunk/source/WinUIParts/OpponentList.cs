using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.WinUIParts
{
    public class OpponentList
    {
        private string _name;
        private string _service;

        public OpponentList(string opponentName, string imService)
        {
            this.Name = opponentName;
            this.Service = imService;
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
    }
}
