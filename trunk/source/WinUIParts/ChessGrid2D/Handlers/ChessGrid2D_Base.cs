using ChessMangler.Engine.Enums;
using ChessMangler.Engine.Interfaces;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public class ChessGrid2D_Base : IBoardMode
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

        protected UIBoard _freeFormBoard = null;

        UIBoard _uiBoard;
        public UIBoard UIBoard
        {
            get
            {
                return _uiBoard;
            }
            set
            {
                _uiBoard = value;
            }
        }

        public BoardMode BoardMode
        {
            get
            {
                return this.UIBoard.EngineBoard.BoardMode;
            }
            set
            {
                this.UIBoard.EngineBoard.BoardMode = value;
            }
        }

        string _rulesFilePath;
        public string RulesFilePath
        {
            get
            {
                return _rulesFilePath;
            }
            set
            {
                _rulesFilePath = value;
            }

        }
    }
}
