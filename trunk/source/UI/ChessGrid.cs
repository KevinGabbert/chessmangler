using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;

using WinUIParts;

namespace SKChess
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid : Form
    {
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

                UIBoard newBoard = new UIBoard();
                newBoard.CreateBoard(this, testSetup, uiDirectory); //get these from XML file 
 
                //Steve, see those screwed up cell colors?? that's you..
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
                square.MouseDown += CellMouseDown;
                square.MouseMove += CellMouseMove;
                square.MouseUp += CellMouseUp;
            }
        }

        private MouseEventArgs _dragStart;
        private MouseEventArgs _dragEnd;

        private bool _mouseDown = true;
        private bool _isDragging = false;

        private void CellMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _dragStart = e;
        }

        private void CellMouseMove(object sender, MouseEventArgs e)
        {
            _isDragging = _mouseDown;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //UI Class will do this;
                //    delete picture in old place
                //    picture needs to be animated and attached to the cursor.  (is this a windows function?)
            }
        }

        private void CellMouseUp(object sender, MouseEventArgs e)
        {
            _dragEnd = e;
            _isDragging = false;

            //get start & end locations
            //ChessSquare startSquare = (ChessSquare)dataGridView1[_dragStart.ColumnIndex, _dragStart.RowIndex];
            //ChessSquare endSquare = (ChessSquare)dataGridView1[_dragEnd.ColumnIndex, _dragEnd.RowIndex];

            //run rules

            bool weCanMove = false; //= UIEngine.IsThisMoveOkay(startSquare, endSquare);

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
    }
}
