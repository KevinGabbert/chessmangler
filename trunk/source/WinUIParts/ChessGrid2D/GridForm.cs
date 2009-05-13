using System.Windows.Forms;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public class GridForm : Form
    {
        protected ChessGrid2D_SquareHandlers _squareHandlers;

        //TODO: Subscribe to the Comm Recieve Event
        //ChessGrid2D_CommHandlers _commHandlers;

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
