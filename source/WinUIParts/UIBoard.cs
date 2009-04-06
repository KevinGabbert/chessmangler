﻿using System;
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

        Board _engineBoard;
        public Board EngineBoard
        {
            get
            {
                return _engineBoard;
            }
            set
            {
                _engineBoard = value;
            }
        }

        public void CreateBoard(Form formForBoard, ushort rows, ushort columns, int squareSize)
        {
            //throw down a bunch of pictureboxen

            //*** mod Engine.CreateBoard so that it will use the same code to create this board ***

            //This grid needs to bound to Engine.Board somehow.
            //I guess we could have an Engine.Board prop..  (this.RawBoard)

            formForBoard.Width = (squareSize * columns) + 12;
            formForBoard.Height = (squareSize * rows) + 30;

            this.EngineBoard = new Board(columns, rows);
            this.BuildSquares(formForBoard, rows, columns, squareSize);
        }

        private void BuildSquares(Form formForBoard, int rows, int columns, int squareSize)
        {
            int squareR = 0;
            int squareC = 0;

            //Use board logic to iterate through the board.
            //(meaning:  Board.InitializeSquares() helps make the UI board)
            foreach (Square currentSquare in this.EngineBoard.SquareLogic(columns, rows))
            {
                //The picture passed here needs to be a property of the ChessPiece for this square
                UISquare newSquare = new UISquare(new Point(squareR, squareC), squareSize, Environment.CurrentDirectory + "\\images\\wr.gif");

                /* -----------------------------------------------------------------------------------------
                 * all this should come from (a yet unmade) Board.SquareLogic
                 * 
                 * 
                 * 
                 * What is this square's address?
                 * When you get that, then go look up the address in the config file's Starting position for this board.
                 * 
                 * Something like this?? ChessPiece pieceForThisSquare = new ChessPiece(ConfigurationManager.AppSettings["BoardName_DefaultSetup_SquareAddress"]);
                 * (inside ChessPiece:   this.Image = Environment.CurrentDirectory + "\\images\\wr.gif"; //obviously here pull from XML
                 * 
                 * you know.. I'd like to get the Piece from Engine.Board.SquarePositionLogic...
                 * newSquare.Piece = pieceForThisSquare
                 * ----------------------------------------------------------------------------------------- */

                //you know.. I'd like to get the squareColor from Engine.Board.SquarePositionLogic...
                this.squareColor = !this.squareColor;

                if (currentSquare.Col == 0)
                {
                    this.squareColor = !this.squareColor;
                }

                formForBoard.Controls.Add(newSquare);


                //Set the position of our new square to be drawn
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
