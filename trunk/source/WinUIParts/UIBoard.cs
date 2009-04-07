using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using System;

using Rules.Interfaces;
using Engine.Interfaces;
using Engine.Types;

namespace WinUIParts
{
    /// <summary>
    /// This board does not enforce rules, it simply draws the board and moves pieces where it is told to..
    /// </summary>
    public class UIBoard
    {
        #region Properties

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

        public void CreateBoard(Form formForBoard, XmlDocument configFile)
        {
            //*****  This should probably be Done in Engine.Board and not here!
            BoardDef boardDef = this.GetBoardDef(configFile); // TODO: if BoardDef is null then throw custom exception
            Int16 squareSize = this.GetSquareSize(configFile); //TODO: if squaresize is -1 then throw custom exception
            //*****  This should probably be Done in Engine.Board and not here!


            formForBoard.Width = (squareSize * boardDef.Columns) + 12;
            formForBoard.Height = (squareSize * boardDef.Rows) + 30;

            this.EngineBoard = new Board2D(boardDef.Columns, boardDef.Rows, configFile);
            this.BuildUISquares(formForBoard, boardDef.Rows, boardDef.Columns, squareSize);
        }

        protected void BuildUISquares(Form formForBoard, Int16 rows, Int16 columns, Int16 squareSize)
        {
            int squareR = 0;
            int squareC = 0;

            //Use board logic to iterate through the board.
            //Translates Engine stuff to UI Stuff
            foreach (Square2D currentSquare in this.EngineBoard.SquareLogicWithTestPiece(columns, rows))
            {
                #region Temporary UI Code
                ////**** This is disposable test code, as the pieces will be set in Engine.Board (XmlDocument) *****
                //UIPiece newUIPiece = new UIPiece("Rook");
                //newUIPiece.Image = new Bitmap(Environment.CurrentDirectory + "\\images\\wr.gif");

                UISquare newUISquare = new UISquare(new Point(squareR, squareC), squareSize);
                //**** This is disposable test code, as the Squares will be set in Engine.Board (XmlDocument) *****

                #endregion

                UIBoard.TranslateEngineStuffToUI(currentSquare, newUISquare);

                formForBoard.Controls.Add(newUISquare); //Place our newly built square on the grid

                //**** This is disposable test code, as the UI SquarePositions will be set in Engine.Board (XmlDocument) *****
                //Set the position of our new square to be drawn
                if (currentSquare.Column == columns - 1)
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
            newUISquare.Piece = currentSquare.CurrentPiece; //Ah, the power of interfaces..
        }

        #region Xml

        public Int16 GetSquareSize(XmlDocument configFile)
        {
            XmlNode defNode = this.GetDefNode(configFile, "UIDef");

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
        public BoardDef GetBoardDef(XmlDocument configFile)
        {
            BoardDef gotBoardDef = null;

            XmlNode boardDefNode = this.GetDefNode(configFile, "BoardDef");

            if (boardDefNode != null)
            {
                gotBoardDef = new BoardDef();

                XmlAttributeCollection attributes = boardDefNode.Attributes;
                foreach (XmlAttribute currentAttribute in attributes)
                {
                    string currentName = currentAttribute.Name;

                    if (currentName == "rows")
                    {
                        gotBoardDef.Rows = Convert.ToInt16(currentAttribute.Value);
                    }

                    if (currentName == "columns")
                    {
                        gotBoardDef.Columns = Convert.ToInt16(currentAttribute.Value);
                    }
                }
            }

            return gotBoardDef;
        }
        public XmlNode GetDefNode(XmlDocument configFile, string defNode)
        {
            XmlNode gotDefNode = null;

            foreach (XmlNode xmlNode in configFile)
            {
                if (xmlNode.Name == "ChessConfig")
                {
                    foreach (XmlNode childNode in xmlNode)
                    {
                        if (childNode.Name == defNode)
                        {
                            gotDefNode = childNode;
                        }
                    }
                }
            }

            return gotDefNode;
        }

        #endregion
    }
}
