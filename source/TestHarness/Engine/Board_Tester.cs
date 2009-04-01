using System.Drawing;
using NUnit.Framework;
using Engine.Types;

namespace TestHarness.Engine
{
    [TestFixture]
    public class Board_Tester
    {
        [Test(Description = "Lets see if we can kick out a board of the size we want")]
        public void Verify_Square_Count()
        {
            Board testBoard = new Board(4, 3);

            Assert.AreEqual(12, testBoard.Squares.Count);

            foreach(Square currentSquare in testBoard.Squares)
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
            Board testBoard = new Board(7, 4);

            foreach (Square currentSquare in testBoard.Squares)
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
            Board testBoard = new Board(8, 8);
 
            //verify that x square has the intended number

            Assert.AreEqual(27, testBoard.Squares[27].Number);
        }

        [Test(Description = "Verify Square Names ")]
        public void VerifySquareNames()
        {
            Board testBoard = new Board(8, 8);

            //This so.. sucks.. zero-based arrays.  you want to look up pieces this way?? screw that..
            

            //ok.. lets pick a couple and make sure they match up..
            Assert.AreEqual("b1", testBoard.Squares[1].Name);
            Assert.AreEqual("h8", testBoard.Squares[63].Name);
        }

        [Test(Description = "Put this in your pipe & smoke it")]
        public void VerifyGetByName()
        {
            Board testBoard = new Board(8, 8);

            //verify that x square has the intended Name
            Assert.AreEqual("e5", testBoard.GetByName("E5").Name);

            //and yes.. we can do it so we look up by number.. you want that?
        }
    }
}
