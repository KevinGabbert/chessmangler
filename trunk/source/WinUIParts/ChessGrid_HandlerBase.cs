using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;

namespace ChessMangler.WinUIParts
{
    public class ChessGrid_HandlerBase
    {
        protected ChessGrid _chessGrid;
        public ChessGrid ChessGrid_Form
        {
            get
            {
                return _chessGrid;
            }
            set
            {
                _chessGrid = value;
            }
        }

        protected DebugForm _debugForm;
        public DebugForm DebugForm
        {
            get
            {
                return _debugForm;
            }
            set
            {
                _debugForm = value;
            }
        }
    }
}
