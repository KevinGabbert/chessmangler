using System;
using System.Collections.Generic;
using System.Text;

using jabber.client;

namespace ChessMangler.TestHarness.JabberNet
{
    public class JabberNet_Tester
    {
        public void TestConnect()
        {
            JabberClient jc = new JabberClient();
            jc.User = "Test.Chess.Mangler";   // just the username, not including the @domain.
            jc.Server = "gmail.com";
            jc.Password = "Ch3$$Mangl3r";

            jc.Connect();

        }
    }
}
