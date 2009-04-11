using System.Drawing;
using NUnit.Framework;
using Engine.Types;

using System.Collections.Generic;

using System.IO;
using System.Xml;

namespace TestHarness.Engine
{
    [TestFixture]
    public class Board_Tester
    {
        Board2D _testBoard;

        [SetUp]
        public void SetUp()
        {
          //using a temporary path

            XmlDocument loader = new XmlDocument();

            using (StreamReader __fileToLoad = new StreamReader(System.Environment.CurrentDirectory + "\\Board2D.config"))
            {
                loader.Load(__fileToLoad);
                __fileToLoad.Close();
            }

            _testBoard = new Board2D(loader, System.Environment.CurrentDirectory);
            Assert.IsFalse(_testBoard.IsNew);
        }

        [Test(Description = "")]
        public void Verify_BoardDef_get()
        {
            BoardDef testBoardDef = _testBoard.Definition;

            Assert.IsInstanceOfType((new BoardDef()).GetType(), testBoardDef);
        }

        [Test(Description = "Tests the Get & Set of the List<Squares> prop")]
        public void Verify_Squares_getSet()
        {
            List<Square2D> testSquares = new List<Square2D>();

            _testBoard.Squares = testSquares;

            Assert.AreEqual(testSquares, _testBoard.Squares);
        }

        [Test(Description = "Lets see if we can kick out a board of the size we want")]
        public void Verify_Square_Count()
        {
            Assert.AreEqual(64, _testBoard.Squares.Count);
        }

        [Test(Description = "Verifies the default colors of a chessboard")]
        public void VerifyDefaultColors()
        {
            foreach (Square2D currentSquare in _testBoard.Squares)
            {
                if (((currentSquare.Number) % 2) == 0)
                {
                    Assert.AreEqual(Color.White, currentSquare.Color);
                }
                else
                {
                    Assert.AreEqual(Color.Black, currentSquare.Color);
                }
            }
        }

        [Test(Description = "This function verifies that the 'yield return' clause is called.")]
        public void VerifySquareLogicElse()
        {
            foreach (Square2D currentSquare in _testBoard.SquareLogic(new BoardDef(8,8)))
            {
               Assert.IsFalse(_testBoard.IsNew); 
               Assert.AreEqual(_testBoard.GetByLocation(0, 0).Name, currentSquare.Name);
               break;
            }
        }

        [Test(Description = "So what is this for??")]
        public void VerifySquareNumbers()
        {
            //verify that x square has the intended number
            Assert.AreEqual(27, _testBoard.Squares[27].Number);
        }

        [Test(Description = "Verify Square Names ")]
        public void VerifySquareNames()
        {
            //ok.. lets pick a couple and make sure they match up..
            Assert.AreEqual("b1", _testBoard.Squares[1].Name);
            Assert.AreEqual("h8", _testBoard.Squares[63].Name);
        }

        [Test(Description = "")]
        public void VerifyGetByName()
        {
            //verify that x square has the intended Name
            Assert.AreEqual("e5", _testBoard.GetByName("E5").Name);

            //and yes.. we can do it so we look up by number.. you want that?
        }

        [Test()]
        public void VerifyGetByLocation()
        {
            Assert.AreEqual("a1", _testBoard.GetByLocation(0, 0).Name);
            Assert.AreEqual("b2", _testBoard.GetByLocation(1, 1).Name);
        }
    }
}
