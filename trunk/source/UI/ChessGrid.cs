using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Engine.Interfaces;

using WinUIParts;

namespace SKChess
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid : Form
    {
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

                square.DragEnter += this.CellDragEnter;
                square.DragDrop += this.CellDragDrop;

                square.AllowDrop = true;
            }
        }

        //used for animation
        private MouseEventArgs _dragStart;
        private MouseEventArgs _dragEnd;

        //used for rule evaluation
        private ISquare _dragStartSquare;
        private ISquare _dragEndSquare;

        private bool _mouseDown = true;
        private bool _isDragging = false;

        private void CellMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _dragStart = e;
            _dragStartSquare = (ISquare)sender;

            //do we want it as currentPiece.Image?
            if (_dragStartSquare.CurrentPiece.Image != null)
            {
                this.DoDragDrop(_dragStartSquare.CurrentPiece.Image, DragDropEffects.Copy);
            }
        }
        private void CellMouseMove(object sender, MouseEventArgs e)
        {
            _isDragging = _mouseDown;
            ISquare senderSquare = (ISquare)sender;

            //if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            //{
                //UI Class will do this;
                //    delete picture in old place
                    //if (senderSquare.CurrentPiece != null)
                    //{
                    //    senderSquare.CurrentPiece.Image = null;
                    //}
                //    picture needs to be animated and attached to the cursor.  (is this a windows function?)
            //}
        }
        private void CellMouseUp(object sender, MouseEventArgs e)
        {
            _dragEnd = e;
            _isDragging = false;
            _dragEndSquare = (ISquare)sender;

            //run rules
            bool weCanMove = UIEngine.IsThisMoveOkay(_dragStartSquare, _dragEndSquare);

            if (weCanMove)
            {
                //update the engine..
                //   -set new location in chesspiece.location prop.

                //grab picture to drop
                //System.Drawing.Bitmap _pictureToTransfer = (Bitmap)dataGridView1[_dragStart.ColumnIndex, _dragStart.RowIndex].Value;

                //UI Class will do this;
                //    set the picture in its new place
            }
            else
            {
                //UI Class will do this;
                //    set the picture back in its old place
            }
        }
        private void CellDragEnter(object sender, DragEventArgs e)
        {
            //As we are interested in Image data only we will check this as follows
            if (e.Data.GetDataPresent(typeof(Bitmap)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void CellDragDrop(object sender, DragEventArgs e)
        {
            //target control will accept data here 
            ISquare destination = (ISquare)sender;
            destination.CurrentPiece.Image = _dragStartSquare.CurrentPiece.Image; //(Bitmap)e.Data.GetData(typeof(Bitmap));

            //try
            //{
            //    destination.CurrentPiece.Image = Image.FromFile(e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //}
            //catch (Exception ex)
            //{ }


        }
    }
}
