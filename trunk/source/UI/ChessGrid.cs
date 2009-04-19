using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using ChessMangler.Engine.Interfaces;

using System.Runtime.InteropServices;

using ChessMangler.Engine.Config;
using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;



namespace ChessMangler.WinUIParts
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid : Form
    {
        #region WinAPI Calls

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        #endregion
        #region Properties

        UIBoard _uiBoard = new UIBoard();
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

        #endregion

        public ChessGrid()
        {
            InitializeComponent();
        }

        private void ChessGrid_Load(object sender, EventArgs e)
        {
            //A test file so we can have something to develop with..
            XmlDocument testSetup = new XmlDocument();

            //The App should start up with a blank form and a combobox showing what boards are available to load up.
            //If there are none, then the combobox should not be there.  Instead there will be a "Browse" button so
            //the user can point to a directory with config files.

            string configPath = System.Environment.CurrentDirectory + "\\Board2D.config"; //have it copy local

            string sourceDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString();
            string uiDirectory = sourceDir + "\\UI";
            string imagesDirectory = uiDirectory + "\\images";
            string configFile = sourceDir + "\\UI\\Board2D.config";

            bool exists = false;

            exists = File.Exists(configFile);

            if (exists)
            {
                testSetup = Config.LoadXML(configFile);

                this.UIBoard = new UIBoard();
                this.UIBoard.CreateBoard(this, testSetup, uiDirectory); //get these from XML file 
            }
            else
            {
                //Whine pitifully..
                MessageBox.Show("Default Board Setup file not found");
            }

            this.AddHandlers();   
        }

        public void AddHandlers()
        {
            foreach(UISquare square in this.Controls)
            {
                square.MouseDown += this.CellMouseDown;
                square.MouseMove += this.CellMouseMove;
                square.MouseUp += this.CellMouseUp;

                //square.DragEnter += this.CellDragEnter;
                //square.DragDrop += this.CellDragDrop;
                
            }
        }

        //used for rule evaluation
        private UISquare _dragStartSquare;
        private UISquare _dragEndSquare;
        private static IPiece _currentlyDraggingPiece;
        private UISquare _squareCurrentlyOver;

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new Cursor(ptr);
        }

        private void CellMouseDown(object sender, MouseEventArgs e)
        {
            _dragStartSquare = (UISquare)sender;

            _currentlyDraggingPiece = _dragStartSquare.CurrentPiece;
            _squareCurrentlyOver = (UISquare)sender;


            //If you use this, then comment out MouseMove, and MouseUp
            //this.DoDragDrop(_dragStartSquare.CurrentPiece.Image, DragDropEffects.Copy);

            ChessGrid.ShowPieceCursor((UISquare)sender);

            //this.UIBoard.ClearSquare(_dragStartSquare);
        }
        private void CellMouseMove(object sender, MouseEventArgs e)
        {


            Point defPnt = new Point();
            GetCursorPos(ref defPnt);
            _mouseX = defPnt.X - this.Location.X; //L-R 
            _mouseY = defPnt.Y - this.Location.Y; //U-D 


            ////so we have to do this manually.
            //_squareCurrentlyOver = UIBoard.GetByXY(_mouseX, _mouseY);
            //_squareCurrentlyOver.Visible = false;

            Console.WriteLine(_mouseX.ToString() + "." + _mouseY.ToString());

            //if (e.Button == (MouseButtons.Left | MouseButtons.XButton1))
            //{
            //    _squareCurrentlyOver.Visible = false;
            //}
        }
        private void CellMouseUp(object sender, MouseEventArgs e)
        {  
            
            //yeah, this is crap.  Sender is the start square???
            //_dragEndSquare = (UISquare)sender;


            
            //_squareCurrentlyOver = UIBoard.GetByXY(_mouseX, _mouseY);

           // _squareCurrentlyOver.Visible = false;

            //this.Cursor = new Cursor(Cursor.Current.Handle);
            //Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);

            //_squareCurrentlyOver = UIBoard.GetByXY(this.Cursor.

            //Set the new piece
            _squareCurrentlyOver.CurrentPiece = _dragStartSquare.CurrentPiece; //_currentlyDraggingPiece;
            //_squareCurrentlyDraggingOver.Visible = false;

            //Clear the old..
            //this.UIBoard.ClearSquare(_dragStartSquare);

            //Set the cursor back to normal on the start square
            _dragStartSquare.Cursor = Cursors.Arrow;
        }

        int _mouseX;
        int _mouseY;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Point defPnt = new Point();

            // Call the function and pass the Point, defPnt

            GetCursorPos(ref defPnt);

            // Now after calling the function, defPnt contains the coordinates which we can read

            _mouseX = defPnt.X;
            _mouseY = defPnt.Y;
        }


        private void MoveCursor()
        {
            // Set the Current cursor, move the cursor's Position,
            // and set its clipping rectangle to the form. 

            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
        }



        //private void CellDragEnter(object sender, DragEventArgs e)
        //{
        //    //if (e.Data.GetDataPresent(typeof(Bitmap)))
        //    //{
        //       // _squareCurrentlyDraggingOver = (UISquare)sender;
        //       // _dragEndSquare = (UISquare)sender;

        //        //ChessGrid.ShowPieceCursor((UISquare)sender);
        //    //}
        //    //else
        //    //{
        //        //e.Effect = DragDropEffects.None;
        //    //}
        //}
        //private void CellDragDrop(object sender, DragEventArgs e)
        //{
        //    _dragEndSquare = (UISquare)sender;

        //    bool weCanMove = Board2D.IsThisMoveOkay(_dragStartSquare, _dragEndSquare);

        //    if (weCanMove)
        //    {
        //        //Set the new piece
        //        ((ISquare)sender).CurrentPiece = _dragStartSquare.CurrentPiece; //_currentlyDraggingPiece;

                

        //        //


        //        //Clear the old..
        //        //this.UIBoard.ClearSquare(_dragStartSquare);

                
        //    }
        //    else
        //    {
        //        //Flash the piece you are holding or something like that to show that you can't do that.
        //    }
        //}

        private static void ShowPieceCursor(UISquare senderSquare)
        {
            if (senderSquare.CurrentPiece != null)
            {
                if (senderSquare.CurrentPiece.Image != null)
                {
                    _currentlyDraggingPiece = senderSquare.CurrentPiece;
                    Bitmap bitmap = new Bitmap(_currentlyDraggingPiece.Image, senderSquare.Size);
                    senderSquare.Cursor = CreateCursor(bitmap, 35, 35);

                    bitmap.Dispose();
                }
            }
        }


    }
}
