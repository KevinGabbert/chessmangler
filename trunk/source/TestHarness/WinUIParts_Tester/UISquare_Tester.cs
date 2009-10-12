using System;
using System.Collections.Generic;
using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;

using System.Drawing;

using NUnit.Framework;                                          

namespace ChessMangler.TestHarness.WinUIParts_Tester
{
    [TestFixture]   
    public class UISquare_Tester
    {
        [Test]
        public void CreateObject()
        {
            UISquare testSquare = new UISquare();

            Assert.IsInstanceOfType(typeof(UISquare), testSquare);
        }

        #region Props

        [Test]
        public void Enabled()
        {
            UISquare testSquare = new UISquare();

            Assert.IsTrue(testSquare.Enabled);

            testSquare.Enabled = false;

            Assert.IsFalse(testSquare.Enabled);
        }

        [Test]
        public void X()
        {
            UISquare testSquare = new UISquare();

            Assert.AreEqual(0, testSquare.X);

            testSquare.X = 47;

            Assert.AreEqual(47, testSquare.X);
        }

        [Test]
        public void Y()
        {
            UISquare testSquare = new UISquare();

            Assert.AreEqual(0, testSquare.Y);

            testSquare.Y = 52;

            Assert.AreEqual(52, testSquare.Y);
        }

        [Test]
        public void Color()
        {
            UISquare testSquare = new UISquare();

            Assert.AreEqual("Control", testSquare.Color.Name);

            testSquare.Color = System.Drawing.Color.Green;

            Assert.AreEqual(System.Drawing.Color.Green, testSquare.Color);
        }

        [Test]
        public void PreviousColor()
        {
            UISquare testSquare = new UISquare();

            Assert.AreEqual("Control", testSquare.Color.Name);

            testSquare.Color = System.Drawing.Color.Purple;

            Assert.AreEqual("Control", testSquare.PreviousColor.Name);

            testSquare.Color = System.Drawing.Color.Blue;

            Assert.AreEqual(System.Drawing.Color.Blue, testSquare.Color);
            Assert.AreEqual(System.Drawing.Color.Purple, testSquare.PreviousColor);
        }

        [Test]
        public void CurrentPiece()
        {
            UISquare testSquare = new UISquare();

            PieceDef testFromPieceDef = new PieceDef();
            testFromPieceDef.Name = "Fred";
            testFromPieceDef.Player = 1;
            testFromPieceDef.Color = System.Drawing.Color.Purple;
            testFromPieceDef.BoardLocation = "A1";

            testSquare.CurrentPiece = new Piece(testFromPieceDef, false);

            Assert.IsInstanceOfType(typeof(Piece), testSquare.CurrentPiece);
        }

        #endregion

        //Test OnGiveFeedback here separately
    }
}
