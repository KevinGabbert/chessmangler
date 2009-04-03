using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SKChess
{
    public partial class ChessGrid : Form
    {
        public ChessGrid()
        {
            InitializeComponent();
        }

        public bool squareColor = false;

        private void ChessGrid_Load(object sender, EventArgs e)
        {
            //throw down a bunch of pictureboxen

            //*** mod Engine.CreateBoard so that it will use the same code to create this board ***

            //This grid needs to bound to Board.

            //Create a new class called square that inherits from picturebox, and adds 2 props
            //1. Engine.Board.Square
            //2. IConfigurablePiece


            this.Width = 584;
            this.Height = 606;

            for (int i = 0; i < 576; i = i + 72)
            {
                for (int j = 0; j < 576; j = j + 72)
                {
                    //Square.SetColor(newSquare, column, row);
                    //this.Squares.Add(newSquare);

                    PictureBox newBox = new PictureBox();
                    newBox.Location = new System.Drawing.Point(i, j);
                    newBox.Name = "pictureBox" + (i + j).ToString();
                    newBox.Size = new System.Drawing.Size(72, 72);
                    //newBox.pictureBox1.TabIndex = 0;
                    //newBox.pictureBox1.TabStop = false;

                    if (this.squareColor)
                    {
                        newBox.BackColor = Color.White;
                        //newBox.Image = new Bitmap(Environment.CurrentDirectory + "\\images\\wr.gif");
                    }
                    else
                    {
                        newBox.BackColor = Color.Gray;
                        ///newBox.Image = new Bitmap(Environment.CurrentDirectory + "\\images\\bp.gif");
                    }

                    newBox.Image = new Bitmap(Environment.CurrentDirectory + "\\images\\wr.gif");
                    newBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    newBox.BorderStyle = BorderStyle.FixedSingle;

                    this.Controls.Add(newBox);
                    this.squareColor = !this.squareColor;
                }
                this.squareColor = !this.squareColor;
            }
        }
    }
}
