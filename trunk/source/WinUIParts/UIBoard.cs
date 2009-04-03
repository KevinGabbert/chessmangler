using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace WinUIParts
{
    public class UIBoard
    {
        public bool squareColor = false;

        public void CreateBoard(Form formtoPlaceBoard)
        {
            //throw down a bunch of pictureboxen

            //*** mod Engine.CreateBoard so that it will use the same code to create this board ***

            //This grid needs to bound to Board.

            //Create a new class called square that inherits from picturebox, and adds 2 props
            //1. Engine.Board.Square
            //2. IConfigurablePiece

            formtoPlaceBoard.Width = 584;
            formtoPlaceBoard.Height = 606;


            //Temporary hardcode
            for (int i = 0; i < 576; i = i + 72)
            {
                for (int j = 0; j < 576; j = j + 72)
                {
                    PictureBox newBox = new UISquare(i, j, 72, Environment.CurrentDirectory + "\\images\\wr.gif");

                    formtoPlaceBoard.Controls.Add(newBox);
                    this.squareColor = !this.squareColor;
                }
                this.squareColor = !this.squareColor;
            }
        }
    }
}
