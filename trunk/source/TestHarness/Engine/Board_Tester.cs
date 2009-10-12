using System.Drawing;
using NUnit.Framework;
using ChessMangler.Engine.Types;
using ChessMangler.Engine.Enums;
using ChessMangler.Engine.Interfaces;

using System.Collections.Generic;

using System.IO;
using System.Xml;

namespace ChessMangler.TestHarness.Engine
{
    [TestFixture]
    public class Board2D_Tester
    {
        Board2D _testBoard;

        [SetUp]
        public void SetUp()
        {
            XmlDocument loader = new XmlDocument();

            string sourceDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString();
            string uiDirectory = sourceDir + "\\UI";
            string imagesDirectory = uiDirectory + "\\images";
            string configFile = uiDirectory + "\\Config\\Standard Chess.config"; //This needs to come from ProgramSettings
            
            //before we start, lets verify everything..
            Assert.IsTrue(File.Exists(configFile), "can't find config file! TestHarness looked here: " + configFile);
            Assert.IsTrue(Directory.Exists(uiDirectory), "can't find UI Directory! TestHarness looked here: " + configFile);
            Assert.IsTrue(Directory.Exists(imagesDirectory), "can't find Images Directory! TestHarness looked here: " + uiDirectory + "\\images");

            using (StreamReader __fileToLoad = new StreamReader(configFile))
            {
                loader.Load(__fileToLoad);
                __fileToLoad.Close();
            }

            _testBoard = new Board2D(loader, uiDirectory);
            Assert.IsFalse(_testBoard.IsNew);
        }

        //TODO: IsThisMoveOK?
        [Ignore]
        [Test]
        public void IsThisMoveOK()
        {

        }

        [Test(Description = "")]
        public void Verify_BoardDef_get()
        {
            BoardDef testBoardDef = _testBoard.Definition;

            Assert.IsInstanceOfType((new BoardDef()).GetType(), testBoardDef);
        }

        [Test(Description = "")]
        public void CreateBoard()
        {
            BoardDef testBoardDef = _testBoard.Definition;

            Board2D testBoard = new Board2D(testBoardDef);
            testBoard.BoardMode = BoardMode.Standard;
            Assert.IsFalse(_testBoard.IsNew);

            Assert.AreEqual(64, _testBoard.Squares.Count);
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
                if (((currentSquare.Row + currentSquare.Column) % 2) == 0)
                {
                    Assert.AreEqual(Color.Gray, currentSquare.Color);
                }
                else
                {
                    Assert.AreEqual(Color.White, currentSquare.Color);
                }
            }
        }

        [Test(Description = "This function verifies that the 'yield return' clause is called.")]
        public void VerifySquareLogicElse()
        {
            foreach (Square2D currentSquare in _testBoard.EnumerateBoard(new BoardDef(8,8)))
            {
               Assert.IsFalse(_testBoard.IsNew); 
               Assert.AreEqual(_testBoard.GetByLocation(7, 0).BoardLocation, currentSquare.BoardLocation);
               break;
            }
        }

        [Test(Description = "Verify Square Names ")]
        public void VerifySquareNames()
        {
            //ok.. lets pick a couple and make sure they match up..
            Assert.AreEqual("b8", _testBoard.Squares[1].BoardLocation);
            Assert.AreEqual("h1", _testBoard.Squares[63].BoardLocation);
        }

        [Test(Description = "")]
        public void VerifyGetByName()
        {
            //verify that x square has the intended Name
            Assert.AreEqual("e5", _testBoard.GetByName("E5").BoardLocation);

            //and yes.. we can do it so we look up by number.. you want that?
        }

        [Test()]
        public void VerifyGetByLocation()
        {
            Assert.AreEqual("a1", _testBoard.GetByLocation(0, 0).BoardLocation);
            Assert.AreEqual("b2", _testBoard.GetByLocation(1, 1).BoardLocation);
        }

        [Test(Description = "This test also explores what it takes to map a piece to a square.")]
        public void Test_MoveThePieceOver()
        {
            //Hmmmm.. why is a PieceDef a Piece, and an IPiece so different?

            BoardDef testBoardDef = _testBoard.Definition;
            Board2D testBoard = new Board2D(testBoardDef);

            ISquare from = new Square2D();
     
            PieceDef testFromPieceDef = new PieceDef();
            testFromPieceDef.Name = "Fred";
            testFromPieceDef.Player = 1;
            testFromPieceDef.Color = Color.Purple;
            testFromPieceDef.ImageDirectory = @"C:\";
            testFromPieceDef.ImageName = "FredImage";
            testFromPieceDef.BoardLocation = "A1";

            from.CurrentPiece = new Piece(testFromPieceDef, false);


            ISquare to = new Square2D();
            PieceDef testToPieceDef = new PieceDef();
            testToPieceDef.Name = "Barney";
            testToPieceDef.Player = 2;
            testToPieceDef.Color = Color.Green;
            testToPieceDef.ImageDirectory = "D:";
            testToPieceDef.ImageName = "BarneyImage";
            testToPieceDef.BoardLocation = "D1";

            to.CurrentPiece = new Piece(testFromPieceDef, false);

            Board2D.MoveThePieceOver(from, to);

            //"Barney" has now been "taken" and replaced by "Fred"
            Assert.AreEqual(to.CurrentPiece.Name, "Fred");
            Assert.AreEqual(to.CurrentPiece.Player, 1);
            Assert.AreEqual(to.CurrentPiece.Color, Color.Purple);
            Assert.AreEqual(to.CurrentPiece.BoardLocation, "A1");
            Assert.AreEqual(to.CurrentPiece.Image, null); //This test didn't verify the image
        }

        #region Check Properties

        [Test(Description = "")]
        public void BoardMode_Prop()
        {
            BoardDef testBoardDef = _testBoard.Definition;

            Board2D testBoard = new Board2D(testBoardDef);
            testBoard.BoardMode = BoardMode.FreeForm;

            Assert.AreEqual(BoardMode.FreeForm, testBoard.BoardMode);
        }

        #endregion
    }
}
