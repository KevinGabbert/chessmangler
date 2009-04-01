﻿using System.Windows.Forms;
using System;
using Rules.Interfaces;

namespace WinUIParts
{
    public class ChessColumn : DataGridViewColumn
    {
        public ChessColumn(): base(new ChessSquare())
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
            ChessColumn col = base.Clone() as ChessColumn;
            col.ChessPiece = _chessPiece;
            return col;
        }
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                if ((value == null) || !(value is ChessSquare))
                {
                    throw new ArgumentException("Invalid cell type, ChessColumns can only contain ChessSquares");
                }
            }
        }
    }
}