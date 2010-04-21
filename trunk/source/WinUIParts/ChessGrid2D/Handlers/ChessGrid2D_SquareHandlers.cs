using System;
using System.Drawing;
using System.Windows.Forms;
using ChessMangler.Engine.Enums;
using ChessMangler.Engine.Types;
using ChessMangler.WinUIParts.Menus;

using System.Xml;

using ChessMangler.Communications.Types;
using ChessMangler.Communications.Types.Packets;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public class ChessGrid2D_SquareHandlers: ChessGrid2D_Base
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

        XmlElement _outBox;
        public XmlElement OutBox
        {
            get
            {
                return _outBox;
            }
            set
            {
                _outBox = value;
            }
        }


        //MovePacket _outBox;
        //public MovePacket OutBox
        //{
        //    get
        //    {
        //        return _outBox;
        //    }
        //    set
        //    {
        //        _outBox = value;
        //    }
        //}

        #endregion

        /// <summary>
        /// All Squares must have at least these events
        /// (The others can be attached on the fly)
        /// </summary>
        public void Add_Required_Square_Handlers(GridForm form)
        {
            this.ChessGrid2D_Form = (ChessGrid2D_Form)form;

            foreach (Control control in form.Controls)
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
        public void Add_Required_Square_Handlers(GridForm form, DebugForm debugForm)
        {
            this.DebugForm = debugForm;
            this.Add_Required_Square_Handlers(form);
            this.UIBoard = form.Grid.UIBoard;
        }
        public void Delete_Required_Square_Handlers(GridForm form)
        {
            this.ChessGrid2D_Form = (ChessGrid2D_Form)form;

            foreach (Control control in form.Controls)
            {
                string controlType = control.GetType().ToString();

                if (controlType == "ChessMangler.WinUIParts.UISquare")
                {
                    UISquare currentSquare = ((UISquare)control);
                    currentSquare.MouseDown -= this.CellMouseDown;
                    currentSquare.MouseMove -= this.CellMouseMove;
                    currentSquare.DragEnter -= this.CellDragEnter;
                    currentSquare.DragDrop -= this.CellDragDrop;
                    currentSquare.MouseClick -= this.CellMouseClick;
                }
            }
        }

        #region Cell Event Handlers

        private void CellMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == (MouseButtons.Right))
            {
                ChessGrid2D_SquareHandlers.DitchMenu(sender);
                return;
            }

            this.DragStart_Square = (UISquare)sender;

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
                blank.Height = _dragStartSquare.Height;
                blank.Width = _dragStartSquare.Width;
                blank.BackColor = _dragStartSquare.BackColor;

                ChessGrid2D_Form.Controls.Add(blank);
                blank.BringToFront();
            }

            //TODO:  Fix the cursor size on this (
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
                UISquare senderSquare = (UISquare)sender;

                string address = this.UIBoard.EngineBoard.GetByName(senderSquare.BoardLocation).BoardLocation;

                if (this.ChessGrid2D_Form.Grid.UIBoard.DebugMode)
                {
                    //this.DebugForm.debugTextBox.Text += ((UISquare)sender).Row + " " + ((UISquare)sender).Column + " ~E:" + square2DRow + " " + square2DColumn;
                }

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
            try
            {
                if (this.ChessGrid2D_Form.Grid.UIBoard.DebugMode)
                {
                    this.DebugForm.debugTextBox.Text += "\r\n ++ Drop Start";
                }

                UISquare dragEndSquare;
                dragEndSquare = (UISquare)sender;

                if (this.DragStart_Square != null && dragEndSquare != null)
                {
                    bool weCanMove = this.UIBoard.EngineBoard.IsThisMoveOkay(_dragStartSquare, dragEndSquare);

                    if (weCanMove)
                    {
                        this.SetPiece(dragEndSquare);
                    }
                    else
                    {
                        //put it back
                        _dragStartSquare.Image = _dragStartSquare.CurrentPiece.Image;

                        if (this.ChessGrid2D_Form.Grid.UIBoard.DebugMode)
                        {
                            this.DebugForm.debugTextBox.Text += "\r\n Putting back piece";
                        }
                    }
                }
                else
                {
                    //TODO: uh oh.. this is a problem.  You shouldn't be here
                    throw new System.Exception("uh oh.. this is a problem.  You shouldn't be here");
                }

                if (this.ChessGrid2D_Form.Grid.UIBoard.DebugMode)
                {
                    this.DebugForm.debugTextBox.Text += "\r\n -- Drop End";
                }

                //Data_Layer.Record_Move_In_DB
                //Communications_Layer.Send_Move_To_opponent

                //TODO: refactor this

                this.OutBox = MovePacket.Generate("movehash", "GameID", dragEndSquare.CurrentPiece.Name, _dragStartSquare.BoardLocation, dragEndSquare.BoardLocation, false); //no rules for now

                //dragEndSquare.CurrentPiece.Image.Visibility = 50%


                //TODO: Cache the Squares' previous colors.
                //_dragStartSquare.BackColor = Color.LightBlue;
                //dragEndSquare.BackColor = Color.Blue;

                //Disable any further movement (RCV Handler will reenable)
                this.UIBoard.Squares.LockMovement(_dragStartSquare, dragEndSquare);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CellMouseClick(object sender, MouseEventArgs e)
        {
            UISquare clickedSquare = (UISquare)sender;

            if (this.BoardMode == BoardMode.FreeForm)
            {
                if (e.Button == MouseButtons.Right)
                {
                    clickedSquare.ContextMenu = new ContextMenu();

                    if (clickedSquare.CurrentPiece != null)
                    {
                        ChessGrid2D_SquareHandlers.BuildAndShowPieceMenu(clickedSquare);
                    }
                    else
                    {
                        ChessGrid2D_SquareHandlers.ShowSquareMenu(clickedSquare);
                    }
                }
            }
            else
            {
                ChessGrid2D_SquareHandlers.DitchMenu(sender);
            }
        }

        #endregion
        #region Cell Context Menus

            private static void ShowSquareMenu(UISquare clickedSquare)
            {
                (new UniquePieceMenu()).Init_ConfigFile_PieceMenu(clickedSquare, "Add Piece from: ", "AddPieceFromMenu");

                //clickedSquare.ContextMenu.MenuItems.Add("Open Square 'Tool Window'");
                //TODO: clickedSquare.ContextMenu.MenuItems.Add("Resize Square"); //> 1.0 feature
                clickedSquare.ContextMenu.Show(clickedSquare, new Point(clickedSquare.Height / 2, clickedSquare.Width / 2)); //This needs to come from config db
            }

            private static void BuildAndShowPieceMenu(UISquare clickedSquare)
            {
                //Show Piece context menu
                (new UniquePieceMenu()).Init_ConfigFile_PieceMenu(clickedSquare, "Transform Piece", "TransFormPieceMenu");

                //Change Style menu should be in both freeform and standard.
                MenuItem addPieceFromMenu = ChessGrid2D_SquareHandlers.NewMenuItem("Change Style", "ChangeStyle");
                clickedSquare.ContextMenu.MenuItems.Add(addPieceFromMenu);
                clickedSquare.ContextMenu.MenuItems["ChangeStyle"].MenuItems.Add(ChessGrid2D_SquareHandlers.NewMenuItem("All Pieces on Board", "apob"));
                clickedSquare.ContextMenu.MenuItems["ChangeStyle"].MenuItems.Add(ChessGrid2D_SquareHandlers.NewMenuItem("All Pieces of this color", "apotc"));
                clickedSquare.ContextMenu.MenuItems["ChangeStyle"].MenuItems.Add(ChessGrid2D_SquareHandlers.NewMenuItem("All Pieces in this set", "apit"));
                clickedSquare.ContextMenu.MenuItems["ChangeStyle"].MenuItems.Add(ChessGrid2D_SquareHandlers.NewMenuItem("This Piece Only", "tpo"));

                //TODO: Each one of these should have a submenu (built on the fly like Build_ConfigFile_PieceMenu) that lists out the names of all sets in the images (or pieces) folder

                (new UISquareDeleteMenu()).CreatePieceDeleteMenu(clickedSquare);

                //future features:
                //clickedSquare.ContextMenu.MenuItems.Add("Rules for this piece"); // > .5 feature
                //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("Add Rule for this piece"); // > 1.0 feature
                //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("Delete Rule for this piece"); // > 1.0 feature
                //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("Change Rule for this piece"); // > 1.0 feature
                //(submenuitem):  clickedSquare.ContextMenu.MenuItems.Add("View Piece Rules"); // > 1.0 feature

                clickedSquare.ContextMenu.Show(clickedSquare, new Point(clickedSquare.Height / 2, clickedSquare.Width / 2));
            }
            private static void DitchMenu(object sender)
            {
                //Ditch the menu.  User might decide to set the board to "standard", and the menu shouldn't be there at that point..
                try
                {
                    UISquare clickedSquare = (UISquare)sender;
                    clickedSquare.ContextMenu = null;
                }
                catch
                {
                }
            }

            //TODO: put this in a "Common Menu Tasks" object
            public static MenuItem NewMenuItem(string caption, string name)
            {
                MenuItem addPieceMenu = new MenuItem();
                addPieceMenu.Name = name;
                addPieceMenu.Text = caption;

                return addPieceMenu;
            }

        #endregion

        public void SetPiece(UISquare dragEndSquare)
        {
            //Set the new piece
            dragEndSquare.CurrentPiece = _dragStartSquare.CurrentPiece;

            //On the engineboard too.
            this.UIBoard.EngineBoard.GetByName(dragEndSquare.BoardLocation).CurrentPiece = dragEndSquare.CurrentPiece;
            this.UIBoard.EngineBoard.GetByName(_dragStartSquare.BoardLocation).CurrentPiece = null;

            if (this.ChessGrid2D_Form.Grid.UIBoard.DebugMode)
            {
                this.DebugForm.debugTextBox.Text += "\r\n Set Piece";
            }

            this.UIBoard.ClearSquare(_dragStartSquare, true);

            if (this.ChessGrid2D_Form.Grid.UIBoard.DebugMode)
            {
                this.DebugForm.debugTextBox.Text += "\r\n Clear Square";
            }
        }
    }
}
