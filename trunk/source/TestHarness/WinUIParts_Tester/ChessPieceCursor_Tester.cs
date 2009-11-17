using NUnit.Framework;
using ChessMangler.WinUIParts;

using System.Windows.Forms;
using ChessMangler.Engine.Types;
using System.Drawing;
using System.IO;

namespace ChessMangler.TestHarness.WinUIParts_Tester
{
    [TestFixture]
    public class ChessPieceCursor_Tester
    {
        string _sourceDir;
        string _uiDirectory;
        string _imagesDirectory;
        string _testCursorImage;

        [SetUp]
        public void SetUp()
        {
            _sourceDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString();
            _uiDirectory = _sourceDir + "\\UI";
            _imagesDirectory = _uiDirectory + "\\images";
            _testCursorImage = _imagesDirectory + "\\ww.png";

            //before we start, lets verify everything..
            Assert.IsTrue(Directory.Exists(_imagesDirectory), "can't find Images Directory! TestHarness looked here: " + _uiDirectory + "\\images");
            Assert.IsTrue(File.Exists(_testCursorImage), "can't find test image");
        }

        [Test]
        public void CreateObject()
        {
            ChessPieceCursor test = new ChessPieceCursor();
            Assert.IsInstanceOfType(typeof(ChessPieceCursor), test);
        }

        /// <summary>
        /// This is how you create a new cursor..
        /// </summary>
        [Test]
        public void Test_CreateCursor()
        {
            Bitmap bitmap = new Bitmap(new Bitmap(_testCursorImage), new Size(8,8));
            Cursor.Current = ChessPieceCursor.CreateCursor(bitmap, 35, 35);

            bitmap.Dispose();

            //so... what's the test?

            //Well the good news is that if you got this far, then the API call was made.. but How do we test?
        }

        [Test]
        public void Test_ShowPieceCursor()
        {
            UISquare square = new UISquare();
            square.CurrentPiece = new Piece();
            square.CurrentPiece.Image = new Bitmap(_testCursorImage);

            ChessPieceCursor.ShowPieceCursor(square);

            //Well I actually saw it flash on the screen during the run-through..

            //so... what's the test?

            //Well the good news is that if you got this far, then the API call was made.. but How do we test?
        }

        [Test]
        public void Test_ShowPieceCursor2()
        {
            ChessPieceCursor.ShowPieceCursor(new Bitmap(_testCursorImage), new Size(78,78));

            //Well I actually saw it flash on the screen during the run-through..

            //so... what's the test?

            //Well the good news is that if you got this far, then the API call was made.. but How do we test?
        }

    }
}
