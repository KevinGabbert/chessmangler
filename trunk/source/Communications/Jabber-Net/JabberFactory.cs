using jabber.protocol;

namespace ChessMangler.Communications.Jabber_Net
{
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
}
