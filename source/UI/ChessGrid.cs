using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;

using System.Collections.Generic;
using ChessMangler.Engine.Interfaces;
using ChessMangler.Engine.Config;
using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;

using ChessMangler.WinUI;

namespace ChessMangler.WinUIParts
{
    /// <summary>
    /// This form only captures events from the form & scripts WinUIParts.  Nothing else.
    /// </summary>
    public partial class ChessGrid : Form
    {
        #region Properties

        UIBoard _uiBoard;
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

        private UISquare _dragStartSquare;
        bool _done = true;

        string _sourceDir;
        string _configFile;

        Image _recoveredImage; //for when people decide to drop a piece off the side of the board;

        DebugForm debugForm = new DebugForm();

        public ChessGrid()
        {
            InitializeComponent();

            _sourceDir = _sourceDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString();
            _configFile = _sourceDir + "\\UI\\Board2D.config";
        }

        private void ChessGrid_Load(object sender, EventArgs e)
        {
            //A test file so we can have something to develop with..
            XmlDocument testSetup = new XmlDocument();

            //The App should start up with a blank form and a combobox showing what boards are available to load up.
            //If there are none, then the combobox should not be there.  Instead there will be a "Browse" button so
            //the user can point to a directory with config files.

            string configPath = System.Environment.CurrentDirectory + "\\Board2D.config"; //have it copy local

            string uiDirectory = _sourceDir + "\\UI";
            string imagesDirectory = uiDirectory + "\\images";

            bool uiDirectoryExists = Directory.Exists(uiDirectory);
            bool imagesDirectoryExists = Directory.Exists(imagesDirectory);

            bool configFileExists = File.Exists(_configFile);

            if (configFileExists)
            {
                testSetup = Config.LoadXML(_configFile);

                //TODO: get default size from 


                this.UIBoard = new UIBoard(0, 25); //This needs to come from the config file
                this.UIBoard.CreateBoard(this, testSetup, uiDirectory); //get these from XML file 

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            else
            {
                //Whine pitifully..
                MessageBox.Show("Default Board Setup file not found. expected to find: " + _configFile);
            }

            this.Add_Required_Square_Handlers();   
        }

        /// <summary>
        /// All Squares must have at least these events
        /// (The others can be attached on the fly)
        /// </summary>
        public void Add_Required_Square_Handlers()
        {
            this.chessMenu.MouseMove += this.chessMenu_MouseMove;

            foreach(Control control in this.Controls)
            {
                string controlType = control.GetType().ToString();
                
                if (controlType == "ChessMangler.WinUIParts.UISquare")
                {
                    UISquare currentSquare = ((UISquare)control);
                    currentSquare.MouseDown += this.CellMouseDown;
                    currentSquare.MouseMove += this.CellMouseMove;
                    currentSquare.DragEnter += this.CellDragEnter;
                    currentSquare.DragDrop += this.CellDragDrop;
                }
            }
        }

        #region Cell Event Handlers

        private void chessMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.PieceIsLost(e)) { return; }
        }

        private void CellMouseDown(object sender, MouseEventArgs e)
        {
            _done = false;
            _dragStartSquare = (UISquare)sender;

            //Make the piece vanish right away. CurrentPiece needs to stay until the end of the DragDrop operation
            UISquare blank = new UISquare(_dragStartSquare.Location, _dragStartSquare.SquareSize);
            blank.BackColor = _dragStartSquare.BackColor;
            this.Controls.Add(blank);
            blank.BringToFront();

            //No need to do anything if the user didn't click on a piece!
            if (_dragStartSquare.CurrentPiece == null)
            {
                return;
            }

            ChessPieceCursor.ShowPieceCursor((UISquare)sender);

            _recoveredImage = _dragStartSquare.Image;

            if (_dragStartSquare.CurrentPiece != null)
            {
                _dragStartSquare.DoDragDrop(_recoveredImage, DragDropEffects.Copy); //_dragStartSquare.CurrentPiece.Image
            }



            this.Controls.Remove(blank);
        }
        private void CellMouseMove(object sender, MouseEventArgs e)
        {
            if (this.PieceIsLost(e)) { return; }

            if (e.Button != (MouseButtons.Left | MouseButtons.XButton1))
            {
                UISquare senderSquare = (UISquare)sender;

                //restore any lost images
                if ((_recoveredImage != null) & (senderSquare.CurrentPiece != null) & (senderSquare.Image == null))
                {
                    senderSquare.Image = senderSquare.CurrentPiece.Image;
                }

                return;
            }

            //if (_dragStartSquare.CurrentPiece != null)
            //{
            //    _dragStartSquare.DoDragDrop(_dragStartSquare.CurrentPiece.Image, DragDropEffects.Copy);
            //}
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
            debugForm.textBox1.Text += "\r\n ++ Drop Start";

            try
            {
                UISquare dragEndSquare;

                dragEndSquare = (UISquare)sender;

                bool weCanMove = Board2D.IsThisMoveOkay(_dragStartSquare, dragEndSquare);

                if (weCanMove)
                {
                    //Set the new piece
                    dragEndSquare.CurrentPiece = _dragStartSquare.CurrentPiece;
                    debugForm.textBox1.Text += "\r\n Set Piece";

                    this.UIBoard.ClearSquare(_dragStartSquare, true);
                    debugForm.textBox1.Text += "\r\n Clear Square";
                }
                else
                {
                    //put it back
                    _dragStartSquare.Image = _dragStartSquare.CurrentPiece.Image;

                    debugForm.textBox1.Text += "\r\n Putting back piece";
                }

                _recoveredImage = null;
                _done = true;

                debugForm.textBox1.Text += "\r\n -- Drop End";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Form Event Handlers


        private void ChessGrid_Resize(object sender, EventArgs e)
        {
            //ChessGrid thisForm = (ChessGrid)sender;

            ////for the moment, we will pull this from config.  (we'll use a pre-loaded prop later)
            ////Int16 defaultSquareSize  = this.UIBoard.get(Config.LoadXML(_configFile));

            ////Ensure that the client area is always square
            ////int iSize = Math.Min(ClientSize.Height, ClientSize.Width);
            ////ClientSize = new Size(iSize, iSize);

            ////Use our good friend SquareLogic to help us find all the squares on the board, and reset their locations

            //BoardDef board = new BoardDef(8, 8);
            //foreach (Square2D currentSquare in this.UIBoard.EngineBoard.SquareLogic(board))
            //{
            //    UISquare currentUISquare = this.UIBoard.GetByBoardLocation(currentSquare.Column, currentSquare.Row);

            //    if (currentUISquare != null)
            //    {
            //        //Square Location
            //        currentUISquare.Location = new Point(currentSquare.Row * ClientSize.Width / 8, currentSquare.Column * ClientSize.Height / 8);
            //        currentUISquare.CurrentPiece = currentSquare.CurrentPiece;

            //        //Square Size
            //        currentUISquare.Height = ClientSize.Height / 8;
            //        currentUISquare.Width = ClientSize.Width / 8;
            //    }
            //}

            //this.SyncBoard();
        }

        #endregion

        public bool PieceIsLost(MouseEventArgs e)
        {
            if ((e.Button != (MouseButtons.Left | MouseButtons.XButton1)) & (_recoveredImage!= null) & (_done == false))
            {
                //uhhh... How did you do that? Did you drop the piece off the form? (or between the squares?)
                _dragStartSquare.Image = _recoveredImage;
                _recoveredImage = null;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CheckForMissingPieces()
        {
            BoardDef board = new BoardDef(8, 8);
            foreach (Square2D currentSquare in this.UIBoard.EngineBoard.SquareLogic(board))
            {
                UISquare currentUISquare = this.UIBoard.GetByBoardLocation(currentSquare.Column, currentSquare.Row);

                //restore any lost images
                if ((currentUISquare.CurrentPiece != null) & (currentUISquare.Image == null))
                {
                    currentUISquare.Image = currentUISquare.CurrentPiece.Image;
                }
            }
        }
        public void SyncBoard()
        {
            //BoardDef board = new BoardDef(8, 8);
            //foreach (Square2D currentSquare in this.UIBoard.EngineBoard.SquareLogic(board))
            //{
            //    UISquare currentUISquare = this.UIBoard.GetByLocation(currentSquare.Column, currentSquare.Row);

            //    if (currentUISquare != null)
            //    {
            //        currentUISquare.CurrentPiece = currentSquare.CurrentPiece;
            //        currentUISquare.Image = currentSquare.CurrentPiece.Image;
            //    }
            //}
        }
        public void TurnBoard()
        {
            int i = 0;
            //cycle through all the squares on the board..
            BoardDef board = new BoardDef(8, 8);
            foreach (Square2D currentSquare in this.UIBoard.EngineBoard.SquareLogic(board))
            {
                UISquare currentUISquare = this.UIBoard.GetByBoardLocation(currentSquare.Row, currentSquare.Column);

                if (currentUISquare != null)
                {
                    //turns
                    currentUISquare.Location = new Point(currentSquare.Row * 72, currentSquare.Column * 72);

                    //reverses
                    //currentUISquare.Location = new Point(currentSquare.Column * 72, currentSquare.Row * 72);
                }

                i++;
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugForm = new DebugForm();
            debugForm.Show();

            debugForm.textBox1.Text += "New Debug Form";
        }
    }
}




