﻿using System;
using System.Collections.Generic;
using ChessMangler.WinUIParts;

using System.Drawing;

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

        [Test(Description="The whole point of this function is to reset a 'flagged square' back to its previous color")]
        public void ResetColors()
        {
            Squares testSquares = new Squares();

            UISquare coloredSquare = new UISquare();
            coloredSquare.Name = "coloredSquare";
            coloredSquare.BackColor = Color.Yellow; //This is what the square starts out set as
            coloredSquare.Color = Color.Blue; //now lets 'flag' the square for some reason

            Assert.AreEqual("Blue", coloredSquare.Color.Name); //verify color was set
            Assert.AreEqual("Yellow", coloredSquare.PreviousColor.Name); //verify our previous color is still recorded

            UISquare normalSquare = new UISquare(); //This is an unset square
            normalSquare.Name = "Normal";
            Assert.AreEqual("Control", normalSquare.Color.Name); //this is its default value

            testSquares.Add(coloredSquare);
            testSquares.Add(coloredSquare);
            testSquares.Add(coloredSquare);
            testSquares.Add(coloredSquare);
            testSquares.Add(normalSquare);
            testSquares.Add(normalSquare);

            testSquares.ResetColors();

            foreach (UISquare square in testSquares)
            {
                if (square.Name == "coloredSquare")
                {
                    Assert.AreEqual("Yellow", square.Color.Name); //verify that all the colors squares have been reset
                }

                if (square.Name == "Normal")
                {
                    Assert.AreEqual("Control", square.Color.Name);
                }
            }
        }

        [Test(Description = "The whole point of this function is to reset a 'flagged square' back to its previous color")]
        public void UnlockAll()
        {
            Squares testSquares = new Squares();

            UISquare lockedSquare = new UISquare();
            lockedSquare.AllowDrop = false;

            UISquare unLockedSquare = new UISquare();
            unLockedSquare.AllowDrop = true;

            testSquares.Add(lockedSquare);
            testSquares.Add(lockedSquare);
            testSquares.Add(lockedSquare);
            testSquares.Add(lockedSquare);

            foreach (UISquare square in testSquares)
            {
                Assert.IsFalse(square.AllowDrop);
            }

            testSquares.Add(unLockedSquare);
            testSquares.Add(unLockedSquare);

            testSquares.UnlockAll();

            foreach (UISquare square in testSquares)
            {
                Assert.IsTrue(square.AllowDrop);
            }
        }

        [Test(Description = "The whole point of this function is to reset a 'flagged square' back to its previous color")]
        public void Test_LockMovement()
        {
            Squares testSquares = new Squares();

            UISquare unLockedSquare1 = new UISquare();
            unLockedSquare1.Name = "startsquare";

            UISquare unLockedSquare2 = new UISquare();
            UISquare unLockedSquare3 = new UISquare();

            UISquare unLockedSquare4 = new UISquare();
            unLockedSquare4.Name = "endsquare";

            UISquare unLockedSquare5 = new UISquare();

            testSquares.Add(unLockedSquare1);
            testSquares.Add(unLockedSquare2);
            testSquares.Add(unLockedSquare3);
            testSquares.Add(unLockedSquare4);
            testSquares.Add(unLockedSquare5);

            testSquares.LockMovement(unLockedSquare1, unLockedSquare4);

            foreach (UISquare square in testSquares)
            {
                switch(square.Name)
                {
                    case "startsquare":
                    case "endsquare":
                    Assert.IsTrue(square.AllowDrop);
                    break;

                    case "":
                    Assert.IsFalse(square.AllowDrop);
                    break;
                }
            }
        }

    }
}
