using System.Windows.Forms;
using ChessMangler.Options.Interfaces;
using ChessMangler.WinUIParts;
using ChessMangler.TestHarness.Mocks.Forms;

using NUnit.Framework;

namespace ChessMangler.TestHarness.WinUIParts_Tester.GameList
{
    [TestFixture]
    public class GameList_Tester
    {
        [Test]
        public void newForm()
        {
            ChessMangler.WinUIParts.GameList x = new ChessMangler.WinUIParts.GameList();

            Assert.IsInstanceOfType(typeof(ChessMangler.WinUIParts.GameList), x);       
        }

        [Test]
        public void OpenForm2()
        {
            ChessMangler.WinUIParts.GameList x = new ChessMangler.WinUIParts.GameList();
            x.Show();
            x.Close(); 
        }
    }
}
