using ChessMangler.WinUIParts.ChessGrid2D;
using NUnit.Framework;
using jabber.client;
using ChessMangler.TestHarness.Mocks.Comms;

namespace ChessMangler.TestHarness.WinUIParts_Tester.ChessGrid2D
{
    [TestFixture]
    public class ChessGrid2D_Form_Tester
    {
        [Test]
        public void ChessGrid2D_Object()
        {
            ChessGrid2D_Form x = new ChessGrid2D_Form(new PresenceManager(), new Comms_Mock(), "opponentName", "vtest");
            Assert.IsInstanceOfType(typeof(ChessGrid2D_Form), x);
        }
    }
}
