using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using System;

using Rules.Interfaces;
using Engine.Interfaces;
using Engine.Types;
using Engine.Config;

namespace WinUIParts
{
    /// <summary>
    /// This board does not enforce rules, it simply draws the board and moves pieces where it is told to..
    /// </summary>
    public class UIBoard
    {
        #region Properties

        bool _debugMode = true;
        public bool DebugMode
        {
            get
            {
                return _debugMode;
            }
            set
            {
                _debugMode = value;
            }
        }

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

        Board2D _engineBoard;
        public Board2D EngineBoard
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

        #endregion

        public void CreateBoard(Form formForBoard, XmlDocument configFile, string directory)
        {
            this.EngineBoard = new Board2D(configFile, directory);
            Int16 squareSize = this.GetSquareSize(configFile); //TODO: if squaresize is -1 then throw custom exception

            formForBoard.Width = (squareSize * this.EngineBoard.Definition.Columns) + 12;
            formForBoard.Height = (squareSize * this.EngineBoard.Definition.Rows) + 30;

            this.BuildUISquares(formForBoard, this.EngineBoard.Definition, squareSize);
        }

        protected void BuildUISquares(Form formForBoard, BoardDef boardDef, Int16 squareSize)
        {
            int squareR = 0;
            int squareC = 0;

            //Use board logic to iterate through the board.
            //Translates Engine stuff to UI Stuff

            //Hmmmmm.. the alternative is to do a for..i.. using BoardDef and GetByLocation, but SquareLogic already does this!, also, this allows for less complication in the UI..
            foreach (Square2D currentSquare in this.EngineBoard.SquareLogic(boardDef))
            {
                #region Temporary UI Code
                ////**** This is disposable test code, as the pieces will be set in Engine.Board (XmlDocument) *****
                //UIPiece newUIPiece = new UIPiece("Rook");
                //newUIPiece.Image = new Bitmap(Environment.CurrentDirectory + "\\images\\wr.gif");

                UISquare newUISquare = new UISquare(new Point(squareR, squareC), squareSize);
                //**** This is disposable test code, as the Squares will be set in Engine.Board (XmlDocument) *****

                #endregion

                if (this.DebugMode)
                {
                    newUISquare.Image = UISquare.CreateBitmapImage(currentSquare.Name + ".col" + currentSquare.Column + ".row" + currentSquare.Row, "Arial", 10);
                }

                UIBoard.TranslateEngineStuffToUI(currentSquare, newUISquare);

                formForBoard.Controls.Add(newUISquare); //Place our newly built square on the grid

                //**** This is disposable test code, as the UI SquarePositions will be set in Engine.Board (XmlDocument) *****

                //They will??  Oh yes.. They will need to be if we are going to have custom boards, such as a chess board in
                //the shape of a triangle or something..

                //Set the position of our new square to be drawn
                if (currentSquare.Column == boardDef.Columns - 1)
                {
                    squareC = squareC + squareSize;
                    squareR = 0 - squareSize;
                }

                squareR = squareR + squareSize;
                //**** This is disposable test code, as the UI SquarePositions will be set in Engine.Board (XmlDocument) *****
            }
        }

        //These should all map 1 to 1..
        protected static void TranslateEngineStuffToUI(ISquare currentSquare, UISquare newUISquare)
        {
            //This is the only code that will remain in this loop when we are done.
            newUISquare.Color = currentSquare.Color;

            if (currentSquare.CurrentPiece != null)
            {
                newUISquare.CurrentPiece = currentSquare.CurrentPiece; //Ah, the power of interfaces..
            }
        }

        #region Xml

        public Int16 GetSquareSize(XmlDocument configFile)
        {
            XmlNode defNode = ConfigParser.GetConfigDefNode(configFile, "UIDef");

            Int16 gotSquareSize = -1;

            if (defNode != null)
            {
                foreach (XmlNode squareLayoutNode in defNode)
                {
                    if (squareLayoutNode.Name == "UISquareLayout")
                    {
                        XmlAttributeCollection attributes = squareLayoutNode.Attributes;
                        foreach (XmlAttribute currentAttribute in attributes)
                        {
                            string currentName = currentAttribute.Name;

                            if (currentName == "SquareSize")
                            {
                                gotSquareSize = Convert.ToInt16(currentAttribute.Value);
                            }
                        }
                    }
                }
            }

            return gotSquareSize;
        }

        #endregion
    }
}
