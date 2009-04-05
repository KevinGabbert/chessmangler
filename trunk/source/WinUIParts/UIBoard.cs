using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

using Engine.Types;
using Engine.Interfaces;
using Rules.Interfaces;

using System.Configuration;

namespace WinUIParts
{
    public class UIBoard
    {
        public bool squareColor = false;

        int _rows;
        public int Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        int _columns;
        public int Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                _columns = value;
            }
        }

        int _name;
        public int Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        List<UISquare> _squares = new List<UISquare>();
        public List<UISquare> Squares
        {
            get
            {
                return _squares;
            }
            set
            {
                _squares = value;
            }
        }

        public void CreateBoard(Form formForBoard, int rows, int columns, int squareSize)
        {
            //throw down a bunch of pictureboxen

            //*** mod Engine.CreateBoard so that it will use the same code to create this board ***

            //This grid needs to bound to Engine.Board somehow.
            //I guess we could have an Engine.Board prop..  (this.RawBoard)

            formForBoard.Width = (squareSize * columns) + 12;
            formForBoard.Height = (squareSize * rows) + 30;

            BuildSquares(formForBoard, rows, columns, squareSize);
        }

        private void BuildSquares(Form formForBoard, int rows, int columns, int squareSize)
        {
            int squareR = 0;
            int squareC = 0;

            //Use board logic to iterate through the board.
            //(meaning:  Board.InitializeSquares() helps make the UI board)
            foreach (BoardIterator currentSquare in Board.SquarePositionLogic(columns, rows))
            {
                //The picture passed here needs to be a property of the ChessPiece for this square
                UISquare newSquare = new UISquare(new Point(squareR, squareC), squareSize, Environment.CurrentDirectory + "\\images\\wr.gif");

                //What is this square's address?

                //When you get that, then go look up the address in the config file's Starting position for this board.

                //Something like this?? ChessPiece pieceForThisSquare = new ChessPiece(ConfigurationManager.AppSettings["BoardName_DefaultSetup_SquareAddress"]);
                //(inside ChessPiece:   this.Image = Environment.CurrentDirectory + "\\images\\wr.gif"; //obviously here pull from XML

                //newSquare.Piece = pieceForThisSquare

                formForBoard.Controls.Add(newSquare);
                this.squareColor = !this.squareColor;

                if (currentSquare.Col == 0)
                {
                    this.squareColor = !this.squareColor;
                }


                //Set the position of our new square
                if (currentSquare.Col == columns - 1)
                {
                    squareC = squareC + squareSize;
                    squareR = 0 - squareSize;
                }

                squareR = squareR + squareSize;
            }
        }
    }
}
