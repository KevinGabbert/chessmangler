using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using System.Windows.Forms;

using ChessMangler.WinUIParts.Menus;

namespace ChessMangler.TestHarness.WinUIParts_Tester.Menus
{
    [TestFixture]
    public class UniquePieceMenu_Tester
    {
        [Test]
        public void CreateObject()
        {
            UniquePieceMenu x = new UniquePieceMenu();
            Assert.IsInstanceOfType(typeof(UniquePieceMenu), x);
        }


        //This guy should move to another object
        [Test]
        public void NewMenuItem()
        {
            UniquePieceMenu x = new UniquePieceMenu();

            MenuItem y = x.NewMenuItem("testMenuitem", "Fred");

            Assert.AreEqual(y.Name, "Fred");
            Assert.AreEqual(y.Text, "testMenuitem");
        }

        [Ignore]
        [Test]
        public void GetPieceDef()
        {
            //mock object and load tempcache

           // call GetPieceDef
        }
    }
}
