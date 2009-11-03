using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChessMangler.Engine.Types;

using ChessMangler.WinUIParts.Menus;

namespace ChessMangler.TestHarness.Mocks.Menus
{
    public class UniquePieceMenu_Mock : UniquePieceMenu
    {
        public UniquePieceMenu_Mock(string uniqueName, PieceDef pieceDef)
        {
            UniquePieceMenu._tempCache.Add(uniqueName, pieceDef);
        }
    }
}
