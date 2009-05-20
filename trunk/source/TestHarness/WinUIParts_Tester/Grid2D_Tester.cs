using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ChessMangler.Engine.Config;
using ChessMangler.Engine.Enums;
using ChessMangler.Engine.Types;

using ChessMangler.WinUIParts.ChessGrid2D;

using NUnit.Framework;

namespace ChessMangler.TestHarness.WinUIParts_Tester
{
    [TestFixture]
    public class Grid2D_Tester
    {
        [Test]
        public void CreateBasicGrid()
        {
            Grid2D testGrid = new Grid2D(new ChessGrid2D_Form());
            Assert.IsInstanceOfType(typeof(Grid2D), testGrid);
        }

        [Test]
        public void CreateBasicGridForm()
        {
            //As you see, it makes its own grid
            ChessGrid2D_Form testForm = new ChessGrid2D_Form();
            Assert.IsInstanceOfType(typeof(Grid2D), testForm.Grid);
        }
    }
}
