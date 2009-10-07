using System.Drawing;
using NUnit.Framework;
using ChessMangler.Engine.Types;

using System.Collections.Generic;

using System.IO;
using System.Xml;
using System.Xml.Linq;

using ChessMangler.Engine.Xml;

namespace ChessMangler.TestHarness.Engine
{
    [TestFixture]
    public class Xml_Tester
    {
        [Ignore]
        [Test]
        public void Peek()
        {
            string configDir = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString() + "\\Config";
            string configFilePath = configDir + "\\proposed.config";

            Assert.IsTrue(File.Exists(configFilePath), "can't find test config file");

            XElement peekd = XmlParser.Peek(configFilePath, "ChessMangler", "version");

            Assert.IsNotNull(peekd, "This means it didn't find the node..");
            Assert.AreEqual("testversion", peekd.Value.ToString());
        }
    }
}
