using System.Xml;
using NUnit.Framework;
using ChessMangler.Communications.Types.Packets;

namespace ChessMangler.TestHarness.Engine.Types.Packets
{
    [TestFixture]
    public class Version_Packet_Tester
    {
        [Test]
        public void Generate()
        {
            XmlElement testPacket = VersionPacket.GenerateMyVersion("testJabberID");

            Assert.IsTrue(testPacket.HasAttributes);
            Assert.AreEqual("version", testPacket.Attributes[0].Name, "Expected an attribute named 'version'");
            Assert.AreEqual("alpha", testPacket.Attributes["version"].InnerText, "Expected 'version' attribute innertext to be 'alpha'");
        }
    }
}
