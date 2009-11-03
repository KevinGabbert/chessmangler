using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using System.Windows.Forms;
using ChessMangler.Engine.Types;

using ChessMangler.WinUIParts.Menus;
using ChessMangler.TestHarness.Mocks.Menus;

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

        /// <summary>
        /// Tests just GetPieceDef.  Does the minimum needed to get a piece object, pre-loads a mock with the piece we want.
        /// </summary>
        [Test]
        public void GetPieceDef()
        {
            MenuItem currentMenuItem = new MenuItem();

            currentMenuItem.Name = "Bishopc414a911-3bca-4d01-a1d7-5716961b410c";

            Piece newPiece = new Piece((new UniquePieceMenu_Mock(currentMenuItem.Name, new PieceDef())).GetPieceDef(currentMenuItem.Name), false);

            Assert.IsInstanceOfType(typeof(Piece), newPiece);
        }
    }

}
