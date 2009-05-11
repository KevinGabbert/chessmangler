using System.Windows.Forms;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public class GridForm : Form
    {
        public ChessGrid2D_SquareHandlers _squareHandlers = new ChessGrid2D_SquareHandlers();

        //TODO: Subscribe to the Comm Recieve Event
        //ChessGrid2D_CommHandlers _commHandlers = new ChessGrid2D_CommHandlers();

        #region Properties

        Grid2D _grid;
        public Grid2D Grid
        {
            get
            {
                return _grid;
            }
            set
            {
                _grid = value;
            }
        }

        #endregion
    }
}
