using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ChessMangler.WinUIParts.ChessGrid2D;

namespace ChessMangler.TestHarness.WinUIParts_Tester.ChessGrid2D
{
    [TestFixture]
    public class ChessGrid2D_Tester
    {
        [Test]
        public void ChessGrid2D_Object()
        {
            ChessGrid2D_Base x = new ChessGrid2D_Base();
            Assert.IsInstanceOfType(typeof(ChessGrid2D_Base), x);
        }

        [Test]
        public void ChessGrid2D_Object2()
        {
            ChessGrid2D_Base x = new ChessGrid2D_Base();
            
        }
    }
}
