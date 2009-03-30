using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

using WinUIParts;
using Pieces.Interfaces;

namespace SKChess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DataGridViewCellMouseEventArgs _dragStart;
        private DataGridViewCellMouseEventArgs _dragEnd;

        private bool _mouseDown = true;
        private bool _isDragging = false;

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            _mouseDown = true;
            _dragStart = e;
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            _isDragging = _mouseDown;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //UI Class will do this;
                //    delete picture in old place
                //    picture needs to be animated and attached to the cursor.  (is this a windows function?)
            }
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _dragEnd = e;
            _isDragging = false;

            //get start & end locations
            ChessSquare startSquare = (ChessSquare)dataGridView1[_dragStart.ColumnIndex, _dragStart.RowIndex];
            ChessSquare endSquare = (ChessSquare)dataGridView1[_dragEnd.ColumnIndex, _dragEnd.RowIndex];
            
            //run rules

            bool weCanMove = UIEngine.IsThisMoveOkay(startSquare, endSquare);

            if (weCanMove)
            {
                //update the engine..
                //   -set new location in chesspiece.location prop.

                //grab picture to drop
                System.Drawing.Bitmap _pictureToTransfer = (Bitmap)dataGridView1[_dragStart.ColumnIndex, _dragStart.RowIndex].Value;

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
