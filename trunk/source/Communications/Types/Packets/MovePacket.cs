using System;
using System.Xml;
using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Jabber_Net;
using jabber.protocol.client;

namespace ChessMangler.Communications.Types.Packets
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

    //This needs to evolve into an instance class with properties
    public class MovePacket: IMovePacket
    {
        #region Properties

        string _gameID;
        public string GameID
        {
            get
            {
                return _gameID;
            }
            set
            {
                _gameID = value;
            }
        }

        string _moveHash;
        public string MoveHash
        {
            get
            {
                return _moveHash;
            }
            set
            {
                _moveHash = value;
            }
        }

        string _piece;
        public string Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
            }
        }

        string _previous;
        public string Previous
        {
            get
            {
                return _previous;
            }
            set
            {
                _previous = value;
            }
        }

        string _new;
        public string New
        {
            get
            {
                return _new;
            }
            set
            {
                _new = value;
            }
        }

        bool _rules;
        public bool Rules
        {
            get
            {
                return _rules;
            }
            set
            {
                _rules = value;
            }
        }

        #region Internal props

        bool _invalid;
        public bool Invalid
        {
            get
            {
                return _invalid;
            }
        }

        string _invalidMoveReason;
        public string InvalidMoveReason
        {
            get
            {
                return _invalidMoveReason;
            }
        }

        #endregion

        #endregion

        public MovePacket(Message jabberMessage)
        {
            XmlDocument msgDoc = new XmlDocument();
            msgDoc.LoadXml(jabberMessage.OuterXml);

            this.Parse(msgDoc, "message");
        }

        public static XmlElement Generate(string hash, string gameID, string piece, string prevMove, string newMove, bool useRules)
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
            //<message id="JN_4" to="xx@gmail.com">
            //   <ChessMangler> //TODO: Add:  version="alpha" />
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

        public XmlElement GenerateRejectPacket()
        {
            throw new NotImplementedException();
        }
        public XmlElement GenerateRCVPacket()
        {
            throw new NotImplementedException();
        }

        public void Parse(XmlDocument messageXml, string rootNode)
        {
            foreach (XmlNode xmlNode in messageXml)
            {
                if (xmlNode.Name == rootNode)
                {
                    foreach (XmlNode childNode in xmlNode)
                    {
                        if (childNode.Name == "ChessMangler")
                        {
                            foreach (XmlNode gameFeatureNode in childNode)
                            {
                                if (gameFeatureNode.Name == "MovePacket")
                                {
                                    Parse_MovePacket(gameFeatureNode);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Parse_MovePacket(XmlNode gameFeatureNode)
        {
            foreach (XmlNode MovePacketNode in gameFeatureNode)
            {
                switch (MovePacketNode.Name)
                {
                    case "MoveHash":
                        this.MoveHash = MovePacketNode.InnerText;
                        break;

                    case "GameID":
                        this.GameID = MovePacketNode.InnerText;
                        break;

                    case "Piece":
                        this.Piece = MovePacketNode.InnerText;
                        break;

                    case "PreviousMove":
                        this.Previous = MovePacketNode.InnerText;
                        break;

                    case "NewMove":
                        this.New = MovePacketNode.InnerText;
                        break;

                    case "UseRules":
                        this.Rules = Convert.ToBoolean(MovePacketNode.InnerText);
                        break;
                }
            }
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
