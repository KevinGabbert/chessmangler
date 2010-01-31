using System.Xml;

using jabber.protocol;

namespace ChessMangler.Communications.Jabber_Net
{
    public class JabberElement : Element
    {
        public const string NAMESPACE = "ChessMangler:Communications";

        // used when creating elements to send
        public JabberElement(XmlDocument doc) : base("query", NAMESPACE, doc) { }

        // used to create elements for inbound protocol
        public JabberElement(string prefix, XmlQualifiedName qname, XmlDocument doc) : base(prefix, qname, doc) { }
    }
}
