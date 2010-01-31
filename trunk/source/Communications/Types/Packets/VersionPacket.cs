using System.Xml;
using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Jabber_Net;

namespace ChessMangler.Communications.Types.Packets
{
    public class VersionPacket: IPacket
    {

        //This is what we want
        //<message id="JN_4" to="xx@gmail.com">
        //   <ChessMangler version="alpha" />
        //</message>

        public static XmlElement Generate()
        {
            XmlDocument __newDoc = new XmlDocument();

            XmlElement __root = new JabberElement("prefix", new XmlQualifiedName("ChessMangler"), __newDoc);

            //XmlAttribute version = new XmlAttribute();
           // version.Value = "alpha";

            //__root.Attributes.Append(version);

            return __root;

            //Here's what this makes so far..
            //<message id="JN_4" to="xx@gmail.com">
            //   <ChessMangler />
            //</message>
        }
    }
}
