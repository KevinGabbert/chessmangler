using System;
using System.Threading;
using NUnit.Framework;
using jabber.client;

namespace ChessMangler.TestHarness.JabberNet
{
    [TestFixture]
    public class Program
    {
        // we will wait on this event until we're done sending
        static ManualResetEvent done = new ManualResetEvent(false);
        // if true, output protocol trace to stdout
        const bool VERBOSE = true;
        const string TARGET = "kevingabbert@gmail.com";

        //http://code.google.com/p/jabber-net/wiki/GettingStarted
        //http://code.google.com/p/jabber-net/w/list

        [Test]
        public void TestJabber()
        {
            JabberClient j = new JabberClient();
            // what user/pass to log in as
            j.User = "Test.Chess.Mangler";
            j.Server = "gmail.com";  // use gmail.com for GoogleTalk
            j.Password = "Ch3$$Mangl3r";

            j.NetworkHost = "talk.l.google.com";  // Note: that's an "L", not a "1".

            // don't do extra stuff, please.
            j.AutoPresence = false;
            j.AutoRoster = false;
            j.AutoReconnect = -1;

            // listen for errors.  Always do this!
            j.OnError += new bedrock.ExceptionHandler(j_OnError);

            // what to do when login completes
            j.OnAuthenticate += new bedrock.ObjectHandler(j_OnAuthenticate);

            // listen for XMPP wire protocol
            if (VERBOSE)
            {
                j.OnReadText += new bedrock.TextHandler(j_OnReadText);
                j.OnWriteText += new bedrock.TextHandler(j_OnWriteText);
            }

            // Set everything in motion
            j.Connect();

            //j.Write("This is a test");

            j.Message(TARGET, "Test Message: " + DateTime.Now.ToString());


            //wait until sending a message is complete
            done.WaitOne();


            j.Message(TARGET, "Test Message: " + DateTime.Now.ToString());

            // logout cleanly
            j.Close();
        }

        static void j_OnWriteText(object sender, string txt)
        {
            if (txt == " ") return;  // ignore keep-alive spaces
            //Console.WriteLine("SEND: " + txt);
            System.Windows.Forms.MessageBox.Show(txt, "--- WRITE ---");
        }
        static void j_OnReadText(object sender, string txt)
        {
            if (txt == " ") return;  // ignore keep-alive spaces
            //Console.WriteLine("RECV: " + txt);
            System.Windows.Forms.MessageBox.Show(txt, "*** READ ***");
        }
        static void j_OnAuthenticate(object sender)
        {
            // Sender is always the JabberClient.
            JabberClient j = (JabberClient)sender;
            j.Message(TARGET, "Authenticate: " + DateTime.Now.ToString());

            j.Message(TARGET, "Test Message: " + DateTime.Now.ToString());

            // Finished sending.  Shut down.
            done.Set();
        }
        static void j_OnError(object sender, Exception ex)
        {
            // There was an error!
            System.Windows.Forms.MessageBox.Show("Error: " + ex.ToString(), "Jabber-Net TestHarness");

            // Shut down.
            done.Set();
        }
    }
}

