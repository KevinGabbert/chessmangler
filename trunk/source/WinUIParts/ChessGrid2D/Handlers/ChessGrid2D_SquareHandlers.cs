using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Windows.Forms;
using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;
using ChessMangler.Engine.Enums;

namespace ChessMangler.WinUIParts
{
    public class ChessGrid2D_SquareHandlers: ChessGrid2D_HandlerBase
    {
        #region Properties

        UISquare _dragStartSquare;
        public UISquare DragStart_Square
        {
            get
            {
                return _dragStartSquare;
            }
            set
            {
                _dragStartSquare = value;
            }
        }

        #endregion

        /// <summary>
        /// All Squares must have at least these events
        /// (The others can be attached on the fly)
        /// </summary>
        public void Add_Required_Square_Handlers(ChessGrid2D_Form chessForm)
        {
            this.ChessGrid2D_Form = chessForm;

            foreach (Control control in chessForm.Controls)
            {
                string controlType = control.GetType().ToString();

                if (controlType == "ChessMangler.WinUIParts.UISquare")
                {
                    UISquare currentSquare = ((UISquare)control);
                    currentSquare.MouseDown += this.CellMouseDown;
                    currentSquare.MouseMove += this.CellMouseMove;
                    currentSquare.DragEnter += this.CellDragEnter;
                    currentSquare.DragDrop += this.CellDragDrop;
                    currentSquare.MouseClick += this.CellMouseClick;
                }
            }
        }

        #region Cell Event Handlers

        private void CellMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == (MouseButtons.Right))
            {
                return;
            }

            _dragStartSquare = (UISquare)sender;

            //Make the piece vanish right away. CurrentPiece needs to stay until the end of the DragDrop operation
            UISquare blank = new UISquare(_dragStartSquare.Location, _dragStartSquare.SquareSize);

            //No need to do anything if the user didn't click on a piece!
            if (_dragStartSquare.CurrentPiece == null)
            {
                return;
            }
            else
            {
                //Hide what we are doing from the user (clone wont work!)
                blank.Height = _dragStartSquare.Height - 5;  //why do we have to make this adjustment?
                blank.Width = _dragStartSquare.Width;
                blank.BackColor = _dragStartSquare.BackColor;

                ChessGrid2D_Form.Controls.Add(blank);
                blank.BringToFront();
            }

            ChessPieceCursor.ShowPieceCursor((UISquare)sender);

            if (_dragStartSquare.CurrentPiece != null)
            {
                _dragStartSquare.DoDragDrop(_dragStartSquare.Image, DragDropEffects.Copy);
            }

            ChessGrid2D_Form.Controls.Remove(blank);

            blank.Dispose();
        }
        private void CellMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != (MouseButtons.Left | MouseButtons.XButton1))
            {
                return;
            }
        }
        private void CellDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Bitmap)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Copy;
        }
        private void CellDragDrop(object sender, DragEventArgs e)
        {
            this.DebugForm.debugTextBox.Text += "\r\n ++ Drop Start";

            try
            {
                UISquare dragEndSquare;
                dragEndSquare = (UISquare)sender;

                bool weCanMove = Board2D.IsThisMoveOkay(_dragStartSquare, dragEndSquare);

                if (weCanMove)
                {
                    //Set the new piece
                    dragEndSquare.CurrentPiece = _dragStartSquare.CurrentPiece;
                    this.DebugForm.debugTextBox.Text += "\r\n Set Piece";

                    ChessGrid2D_Form.UIBoard.ClearSquare(_dragStartSquare, true);
                    this.DebugForm.debugTextBox.Text += "\r\n Clear Square";
                }
                else
                {
                    //put it back
                    _dragStartSquare.Image = _dragStartSquare.CurrentPiece.Image;
                    this.DebugForm.debugTextBox.Text += "\r\n Putting back piece";
                }

                this.DebugForm.debugTextBox.Text += "\r\n -- Drop End";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CellMouseClick(object sender, MouseEventArgs e)
        {
            if (this.ChessGrid2D_Form.BoardMode == BoardMode.FreeForm)
            {
                UISquare clickedSquare = (UISquare)sender;

                if (e.Button == MouseButtons.Right)
                {
                    clickedSquare.ContextMenu = new ContextMenu();

                    if (clickedSquare.CurrentPiece != null)
                    {
                        //Show Piece context menu
                        clickedSquare.ContextMenu.MenuItems.Add("Transform Piece");
                        clickedSquare.ContextMenu.MenuItems.Add("Delete");
                        clickedSquare.ContextMenu.MenuItems.Add("Open Piece 'Tool Window'");

                        //clickedSquare.ContextMenu.MenuItems.Add("Rules for this piece"); // > .5 feature
                        //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("Add Rule for this piece"); // > 1.0 feature
                        //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("Delete Rule for this piece"); // > 1.0 feature
                        //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("Change Rule for this piece"); // > 1.0 feature
                        //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("View Piece Rules"); // > 1.0 feature

                        clickedSquare.ContextMenu.Show(clickedSquare, new Point(clickedSquare.Height / 2, clickedSquare.Width / 2)); //Cursor.Current.HotSpot 
                    }
                    else
                    {
                        clickedSquare.ContextMenu.MenuItems.Add("Add New Piece");
                        clickedSquare.ContextMenu.MenuItems.Add("Open Square 'Tool Window'");
                        //clickedSquare.ContextMenu.MenuItems.Add("Resize Square"); //> 1.0 feature
                        clickedSquare.ContextMenu.Show(clickedSquare, new Point(clickedSquare.Height / 2, clickedSquare.Width / 2)); //This needs to come from config db
                    }
                }

            }
        }

        #endregion

        #region Cell Context Menus

        #endregion
    }
}
