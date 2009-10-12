using ChessMangler.Engine.Types;
using ChessMangler.Engine.Interfaces;

using NUnit.Framework;

namespace ChessMangler.TestHarness.Engine.Types
{
    [TestFixture]
    public class Piece_Tester
    {
        //Hmmmm.. why is a PieceDef a Piece, and an IPiece so different?

        [ExpectedException(typeof(System.IO.FileNotFoundException), ExpectedMessage = @"unable to find Piece Image:  C:\FredImage")]
        [Test()]
        public void Test_Image_Verification_Fail()
        {
            ISquare from = new Square2D();

            PieceDef testFromPieceDef = new PieceDef();
            testFromPieceDef.ImageDirectory = @"C:\";
            testFromPieceDef.ImageName = "FredImage";

            from.CurrentPiece = new Piece(testFromPieceDef, true);
        }

        [ExpectedException(typeof(System.IO.DirectoryNotFoundException), ExpectedMessage = @"no image directory has been set up (null encountered)")]
        [Test()]
        public void Test_Piece_Directory_Verification_Fail()
        {
            ISquare from = new Square2D();

            PieceDef testFromPieceDef = new PieceDef();
            testFromPieceDef.ImageName = "FredImage";

            from.CurrentPiece = new Piece(testFromPieceDef, true);
        }
    }
}
