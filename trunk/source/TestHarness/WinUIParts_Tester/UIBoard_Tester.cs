using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using System.Windows.Forms;
using NUnit.Framework;

using ChessMangler.WinUIParts;

namespace ChessMangler.TestHarness.WinUIParts_Tester
{
    [TestFixture]
    public class UIBoard_Tester
    {
        XmlDocument _testSetup = new XmlDocument();

        string _sourceDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString();
        string _uiDirectory;
        string _imagesDirectory;
        string _configFile;

        [SetUp]
        public void SetUp()
        {
            //TODO: sourceDir and the hardcoded bits need to come from ProgramSettings
            _uiDirectory = _sourceDir + "\\UI";
            _imagesDirectory = _uiDirectory + "\\images";
            _configFile = _sourceDir + "\\Config\\Standard Chess.config"; 

            //before we start, lets verify everything..
            Assert.IsTrue(File.Exists(_configFile), "can't find config file! TestHarness looked here: " + _configFile);
            Assert.IsTrue(Directory.Exists(_uiDirectory), "can't find UI Directory! TestHarness looked here: " + _configFile);
            Assert.IsTrue(Directory.Exists(_imagesDirectory), "can't find Images Directory! TestHarness looked here: " + _uiDirectory + "\\images");

            _testSetup = Config.LoadXML(_configFile);
        }

        [Test]
        public void test()
        {
            UIBoard newBoard = new UIBoard(0, 0, 25);
            newBoard.Create(new Form(), _testSetup, _uiDirectory); //get these from XML file 
        }
    }
}
