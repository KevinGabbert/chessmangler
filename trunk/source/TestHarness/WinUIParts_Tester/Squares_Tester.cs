using System;
using System.Collections.Generic;
using ChessMangler.WinUIParts;

using NUnit.Framework;

namespace ChessMangler.TestHarness.WinUIParts_Tester
{
    [TestFixture]
    public class Squares_Tester
    {

        [Test]
        public void CreateObject()
        {
            Squares testSquares = new Squares();

            Assert.IsInstanceOfType(typeof(Squares), testSquares);
        }

        #region Props
        [Test]
        public void Enabled()
        {
            Squares testSquares = new Squares();

            Assert.IsFalse(testSquares.Enabled);
        }

        #endregion

        [Test]
        public void Enable()
        {
            Squares testSquares = new Squares();

            testSquares.Add(new UISquare());
            testSquares.Add(new UISquare());
            testSquares.Add(new UISquare());

            Assert.IsFalse(testSquares.Enabled);

            testSquares.Enable();

            foreach (UISquare square in testSquares)
            {
                Assert.IsTrue(square.Enabled);
            }

        }

        [Test]
        public void Disable()
        {
            Squares testSquares = new Squares();

            testSquares.Add(new UISquare());
            testSquares.Add(new UISquare());
            testSquares.Add(new UISquare());
            testSquares.Add(new UISquare());
            testSquares.Add(new UISquare());
            testSquares.Add(new UISquare());

            Assert.IsFalse(testSquares.Enabled);

            testSquares.Disable();

            foreach (UISquare square in testSquares)
            {
                Assert.IsFalse(square.Enabled);
            }
        }

    }
}
