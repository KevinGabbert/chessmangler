using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public class ChessGrid2D_HandlerBase
    {
        protected ChessGrid2D_Form _ChessGrid2D;
        public ChessGrid2D_Form ChessGrid2D_Form
        {
            get
            {
                return _ChessGrid2D;
            }
            set
            {
                _ChessGrid2D = value;
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
