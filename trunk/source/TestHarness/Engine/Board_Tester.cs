using System.Drawing;
using NUnit.Framework;
using Engine.Types;

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
        }

        [Test(Description = "Lets see if we can kick out a board of the size we want")]
        public void Verify_Square_Count()
        {
            Assert.AreEqual(64, _testBoard.Squares.Count);

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

        [Test(Description = "Put this in your pipe & smoke it")]
        public void VerifyGetByName()
        {
            //verify that x square has the intended Name
            Assert.AreEqual("e5", _testBoard.GetByName("E5").Name);

            //and yes.. we can do it so we look up by number.. you want that?
        }
    }
}
