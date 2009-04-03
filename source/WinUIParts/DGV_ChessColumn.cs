using System.Windows.Forms;
using System;
using Rules.Interfaces;

namespace WinUIParts
{
    public class DGV_ChessColumn : DataGridViewColumn
    {
        public DGV_ChessColumn(): base(new DGV_ChessSquare())
        {
        }

        #region Properties

        private IConfigurablePiece _chessPiece;
        public IConfigurablePiece ChessPiece
        {
            get { return _chessPiece; }
            set { _chessPiece = value; }
        }

        #endregion

        public override object Clone()
        {
            DGV_ChessColumn col = base.Clone() as DGV_ChessColumn;
            col.ChessPiece = _chessPiece;
            return col;
        }
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if ((value == null) || !(value is DGV_ChessSquare))
                {
                    throw new ArgumentException("Invalid cell type, ChessColumns can only contain ChessSquares");
                }
            }
        }
    }
}