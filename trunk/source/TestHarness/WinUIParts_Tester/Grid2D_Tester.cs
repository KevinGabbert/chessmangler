using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ChessMangler.Engine.Config;
using ChessMangler.Engine.Enums;
using ChessMangler.Engine.Types;

using ChessMangler.WinUIParts.ChessGrid2D;
using ChessMangler.Communications.Interfaces;
using ChessMangler.Communications.Types;

using NUnit.Framework;

namespace ChessMangler.TestHarness.WinUIParts_Tester
{
    [TestFixture]
    public class Grid2D_Tester
    {
        Comms _comms = new Comms();

        [Test]
        public void CreateBasicGrid()
        {
            Grid2D testGrid = new Grid2D(new ChessGrid2D_Form(this.GetComms()));
            Assert.IsInstanceOfType(typeof(Grid2D), testGrid);
        }

        [Test]
        public void CreateBasicGridForm()
        {
            //As you see, it makes its own grid
            ChessGrid2D_Form testForm = new ChessGrid2D_Form(this.GetComms());
            Assert.IsInstanceOfType(typeof(Grid2D), testForm.Grid);
        }

        private ICommsHandler GetComms()
        {
            return _comms.GetHandler(CommsType.Google); //TODO: later this will be assigned via a saved value in the DB, or User selection
        }
    }
}
