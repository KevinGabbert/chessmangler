using System.Xml;
using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Jabber_Net;

namespace ChessMangler.Communications.Types.Packets
{
    public class VersionPacket: IPacket
    {
        //<message id="JN_4" to="xx@gmail.com">
        //   <ChessMangler version="alpha" />
        //</message>

        public static XmlElement GenerateMyVersion(string userName)
        {
            XmlDocument newDoc = new XmlDocument();
            XmlElement root = new JabberElement("prefix", new XmlQualifiedName("ChessMangler"), newDoc);

            XmlElement __movePacket = new JabberElement("prefix", new XmlQualifiedName("VersionPacket"), newDoc);

            root.AppendChild(__movePacket);

            XmlAttribute userNameID;
            userNameID = newDoc.CreateAttribute("userName");
            userNameID.InnerText = userName;

            XmlAttribute version;
            version = newDoc.CreateAttribute("version");
            version.InnerText = "alpha";

            root.Attributes.Append(version);
            root.Attributes.Append(userNameID);

            return root;
        }
    }
}
