﻿using System;
using System.Threading;
using NUnit.Framework;
using jabber.client;

using System.IO;

using System.Xml;

using jabber.protocol;
using jabber.protocol.client;

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
            JabberClient jabberClient = new JabberClient();
            // what user/pass to log in as
            jabberClient.User = "Test.Chess.Mangler";
            jabberClient.Server = "gmail.com";  // use gmail.com for GoogleTalk
            jabberClient.Password = "$00p3r$3cr3t";
            //jabberClient.Password = "Ch3$$Mangl3r";

            jabberClient.NetworkHost = "talk.l.google.com";  // Note: that's an "L", not a "1".

            // don't do extra stuff, please.
            jabberClient.AutoPresence = false;
            jabberClient.AutoRoster = false;
            jabberClient.AutoReconnect = -1;

            // listen for errors.  Always do this!
            jabberClient.OnError += new bedrock.ExceptionHandler(j_OnError);

            // what to do when login completes
            jabberClient.OnAuthenticate += new bedrock.ObjectHandler(j_OnAuthenticate);

            // listen for XMPP wire protocol
            //if (VERBOSE)
            //{
                //jabberClient.OnReadText += new bedrock.TextHandler(j_OnReadText);
                jabberClient.OnWriteText += new bedrock.TextHandler(j_OnWriteText);
                jabberClient.OnMessage += new MessageHandler(j_OnMessage);
            //}

            // Set everything in motion
            jabberClient.Connect();

            //j.Write("This is a test");

            //j.Message(TARGET, "Test Message: " + DateTime.Now.ToString());


            //wait until sending a message is complete
            done.WaitOne();



            //*********  send message
            //jabber.protocol.client.Message msg = new jabber.protocol.client.Message(m_jc.Document);
            //msg.To = txtTo.Text;
            //if (txtSubject.Text != "")
            //    msg.Subject = txtSubject.Text;
            //msg.Body = txtBody.Text;
            //m_jc.Write(msg);
            //this.Close();

            //*********  send messa

            System.Threading.Thread.Sleep(10000);

            // logout cleanly
            jabberClient.Close();
        }

        private void j_OnMessage(object sender, jabber.protocol.client.Message msg)
        {
            jabber.protocol.x.Data x = msg["x", URI.XDATA] as jabber.protocol.x.Data;
            if (x != null)
            {
                //muzzle.XDataForm f = new muzzle.XDataForm(msg);
                //f.ShowDialog(this);
                //j.Write(f.GetResponse());
            }
            else
                System.Windows.Forms.MessageBox.Show(msg.Body, msg.From);
        }

        static void j_OnWriteText(object sender, string txt)
        {
            if (txt == " ") return;  // ignore keep-alive spaces
            //Console.WriteLine("SEND: " + txt);
            ///System.Windows.Forms.MessageBox.Show(txt, "--- WRITE ---");
        }

        //this guy is not necessary
        //static void j_OnReadText(object sender, string txt)
        //{
        //    if (txt == " ") return;  // ignore keep-alive spaces
        //    //Console.WriteLine("RECV: " + txt);
        //    //System.Windows.Forms.MessageBox.Show(txt, "*** READ ***");

        //    //can we use jabber to parse an XML message?


        //    XmlDocument xx = new XmlDocument();


        //    //Message x = new Message(txt);

        //    //System.Windows.Forms.MessageBox.Show(x.Body);

        //}
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

