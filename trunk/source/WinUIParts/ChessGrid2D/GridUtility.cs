using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.Engine.Interfaces;
using ChessMangler.Engine.Config;
using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;
using ChessMangler.Settings.Types.WinUI;
using ChessMangler.Engine.Enums;

using System.Drawing;
using System.Windows.Forms;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public class GridUtility : ChessGrid2D_HandlerBase
    {
        public GridUtility(Form formWithGrid)
        {
            this.ChessGrid2D_Form = (ChessGrid2D_Form)formWithGrid;
        }

        int adjust2 = 20;
        public void Redraw()
        {
            if (this.ChessGrid2D_Form.ConstrainProportions)
            {
                this.KeepSquare();
            }

            //Use our good friend SquareLogic to help us find all the squares on the board, and reset their locations

            if (this.ChessGrid2D_Form.UIBoard != null)
            {
                int newRow = 0;
                int columnCount = 0;

                BoardDef board = this.ChessGrid2D_Form.UIBoard.EngineBoard.Definition;
                foreach (Square2D currentSquare in this.ChessGrid2D_Form.UIBoard.EngineBoard.SquareLogic(board))
                {
                    UISquare currentUISquare = this.ChessGrid2D_Form.UIBoard.GetByBoardLocation(currentSquare.Column, currentSquare.Row);

                    if (currentUISquare != null)
                    {
                        //Adjusts "Board Width" (Board being all the squares)
                        int x = currentSquare.Column * this.ChessGrid2D_Form.ClientSize.Width / board.Columns;
                        int y = AdjustBoardHeight(newRow, board);

                        currentUISquare.Location = new Point(x, y);
                        currentUISquare.CurrentPiece = currentSquare.CurrentPiece;

                        currentUISquare.Height = (this.ChessGrid2D_Form.ClientSize.Height / board.Columns) - adjust2;
                        currentUISquare.Width = (this.ChessGrid2D_Form.ClientSize.Width) / board.Rows;

                        if (this.ChessGrid2D_Form.UIBoard.DebugMode)
                        {
                            if (currentUISquare.CurrentPiece == null)
                            {
                                currentUISquare.Image = UISquare.CreateBitmapImage(currentSquare.BoardLocation, "Arial", 25);
                            }
                        }
                    }

                    //This is what we use to impose a new order (different than the Square2D list)
                    if (++columnCount > board.Columns - 1)
                    {
                        columnCount = 0;
                        newRow++;
                    }
                }
            }
        }
        private int AdjustBoardHeight(int row, BoardDef board)
        {
            //(Board being all the squares)
            int heightAdjustment = this.ChessGrid2D_Form.statusBar.Height - adjust2;//unknown why I need this.  is this a total of cumulative errors??
            int controlsHeight = this.ChessGrid2D_Form.chessMenu.Height + heightAdjustment + this.ChessGrid2D_Form.tabControl1.Height + adjust2;
            int chessBoardHeight = this.ChessGrid2D_Form.ClientSize.Height - controlsHeight - heightAdjustment - adjust2;

            int y = 0;

            y = (row * chessBoardHeight) / board.Rows;
            y = y + (heightAdjustment * row);
            y = y + this.ChessGrid2D_Form.chessMenu.Height; //adding in the chessMenu Height controls where the grid begi

            return y;
        }
        private void KeepSquare()
        {
            //Ensure that the client area is always square
            //TODO: this needs to account for fullscreen
            int iSize = Math.Min(this.ChessGrid2D_Form.ClientSize.Height, this.ChessGrid2D_Form.ClientSize.Width);
            this.ChessGrid2D_Form.ClientSize = new Size(iSize, iSize);
        }
    }
}
