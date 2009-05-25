using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using jabber.client;
using jabber.protocol;
using jabber.protocol.client;

using ChessMangler.Communications.Interfaces;

namespace ChessMangler.Communications.Types
{
    //This is what we want:
    //<ChessMangler version="Alpha">
    //   <MovePacket>
    //      <Hash>1234</Hash>
    //      <GameID>5678</GameID>
    //      <Piece>King</Piece>
    //      <Prev>E1</Prev>
    //      <New>E2</New>
    //      <Rules>No</Rules>; 
    //   <MovePacket>
    //<ChessMangler />

    public class JabberElement : Element, IMovePacket
    {
        public const string NAMESPACE = "ChessMangler:Communications";

        // used when creating elements to send
        public JabberElement(XmlDocument doc) : base("query", NAMESPACE, doc) { }

        // used to create elements for inbound protocol
        public JabberElement(string prefix, XmlQualifiedName qname, XmlDocument doc) : base(prefix, qname, doc) { }
    }

    //This needs to evolve into an instance class with properties
    public class MovePacket
    {
        public static XmlElement SetupPacket(string hash, string gameID, string piece, string prevMove, string newMove, bool useRules)
        {
            XmlDocument __newDoc = new XmlDocument();

            XmlElement __root = new JabberElement("prefix", new XmlQualifiedName("ChessMangler"), __newDoc);
            XmlElement __movePacket = new JabberElement("prefix", new XmlQualifiedName("MovePacket"), __newDoc);

            __root.AppendChild(__movePacket);

            __movePacket.AppendChild(MovePacket.CreateElement("MoveHash", hash, __newDoc));
            __movePacket.AppendChild(MovePacket.CreateElement("GameID", gameID, __newDoc));
            __movePacket.AppendChild(MovePacket.CreateElement("Piece", piece, __newDoc));
            __movePacket.AppendChild(MovePacket.CreateElement("PreviousMove", prevMove, __newDoc));
            __movePacket.AppendChild(MovePacket.CreateElement("NewMove", newMove, __newDoc));
            __movePacket.AppendChild(MovePacket.CreateElement("UseRules", useRules.ToString(), __newDoc));

            return __root;

            //Here's what this makes so far..
            //<message id="JN_4" to="kevingabbert@gmail.com">
            //   <ChessMangler>
            //      <MovePacket>
            //         <MoveHash>1234</MoveHash>
            //         <GameID>5678</GameID>
            //         <Piece>King</Piece>
            //         <PreviousMove>E1</PreviousMove>
            //         <NewMove>E2</NewMove>
            //         <UseRules>False</UseRules>
            //      </MovePacket>
            //   </ChessMangler>
            //</message>
        }

        //public string Body
        //{
        //    get { return GetAttribute("Body"); } set { MovePacket.SetAttribute("Body", value); } 
        //} 

        public static XmlElement CreateElement(string name, string innerText, XmlDocument document)
        {
            XmlElement element = new JabberElement("prefix", new XmlQualifiedName(name), document);
            element.InnerText = innerText;

            return element;
        }
    }

    public class JabberFactory : jabber.protocol.IPacketTypes
    {
        private static QnameType[] s_qnt = new QnameType[] 
        {
            new QnameType("query", JabberElement.NAMESPACE, typeof(JabberElement))
            // Add other types here, perhaps sub-elements of query...
        };

        QnameType[] IPacketTypes.Types { get { return s_qnt; } }

        private void jabberClient_OnStreamInit(object sender, ElementStream stream)
        {
            stream.AddFactory(new JabberFactory());
        }
    }

    //public class MovePacket : MoveResponsePacket
    //{
    //    //- Rules Hash - A unique ID for the game played, probably pulled from the rules file.
    //    string _rulesHash;
    //    public string RulesHash
    //    {
    //        get
    //        {
    //            return _rulesHash;
    //        }
    //        set
    //        {
    //            _rulesHash = value;
    //        }
    //    }

    //    //- Previous Location (denoted by at least, but not limited to 2 characters)
    //    string _previousLocation;
    //    public string PreviousLocation
    //    {
    //        get
    //        {
    //            return _previousLocation;
    //        }
    //        set
    //        {
    //            _previousLocation = value;
    //        }
    //    }

    //    bool _useRules = true;
    //    public bool UseRules
    //    {
    //        get
    //        {
    //            return _useRules;
    //        }
    //        set
    //        {
    //            _useRules = value;
    //        }
    //    }
    //}
}
