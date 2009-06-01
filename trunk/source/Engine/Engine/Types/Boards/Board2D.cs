using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.IO;

using ChessMangler.Engine.Config;
using ChessMangler.Engine.Interfaces;
using ChessMangler.Engine.Enums;

using ChessMangler.Communications.Types;

namespace ChessMangler.Engine.Types
{
    public class Board2D: IBoardMode
    {
        #region Properties

        public string _findSquareName;

        public int _findRow;
        public int _findCol;

        List<Square2D> _squares = new List<Square2D>();
        public List<Square2D> Squares
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

        BoardDef _boardDefinition = new BoardDef();
        public BoardDef Definition
        {
            get
            {
                return _boardDefinition;
            }
            set
            {
                _boardDefinition = value;
            }
        }

        BoardMode _boardMode = new BoardMode();
        public BoardMode BoardMode
        {
            get
            {
                return _boardMode;
            }
            set
            {
                _boardMode = value;
            }
        }

        bool _isNew = true;
        public bool IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
            }
        }

        #endregion

        public Board2D(XmlDocument configFile, string UIDirectory)
        {
            this.CreateBoard(configFile);
            this.MapPieces(configFile, UIDirectory);
        }
        public Board2D(BoardDef boardDef)
        {
            this.CreateBoard(boardDef);
        }

        protected void CreateBoard(XmlDocument configFile)
        {
            BoardDef boardDef = ConfigParser.GetBoardDef(configFile); // TODO: if BoardDef is null then throw custom exception
            this.BuildSquares(boardDef);
        }
        protected void CreateBoard(BoardDef boardDef)
        {
            this.BuildSquares(boardDef);
        }
        private void BuildSquares(BoardDef boardDef)
        {
            this.Definition = boardDef;

            //This foreach construct may be temporary, as I'm evaluating whether this is a good idea..
            foreach (Square2D currentSquare in this.EnumerateBoard(boardDef))
            {
                //this empty foreach executes BoardEnumerator. We may need to put something here later..
            }
        }
        private void MapPieces(XmlDocument configFile, string directory)
        {
            List<PieceDef> piecesToSet = ConfigParser.GetPieces(configFile);

            foreach (PieceDef pieceDef in piecesToSet)
            {
                switch (pieceDef.Name)
                {
                    case "All":
                        break;

                    default:
                        Square2D squareForPiece = this.GetByName(pieceDef.StartingLocation);

                        //TODO:  Temporary Code (as this will be eventually pulled from a config file)
                        pieceDef.ImageDirectory = directory + @"\images\";

                        if (squareForPiece != null)
                        {
                            //TODO: >v1.0? this dir can be overridden by a config file setting
                            squareForPiece.CurrentPiece = new Piece(pieceDef);
                        }
                        break;
                }
            }
        }
        public static bool IsThisMoveOkay(ISquare startSquare, ISquare endSquare)
        {
            //Temporary hardcoded rule.. (this will be in XML)

            if ((endSquare.CurrentPiece != null) && (startSquare.CurrentPiece != null))
            {
                if (startSquare.CurrentPiece.Color == endSquare.CurrentPiece.Color)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            //Talk to the engine.. see what it has to say.  This function might not even
        }

        #region Squares

        public Square2D GetByName(string squareName)
        {
            //look up the square by its name.  ex. name:  "A2"
            this._findSquareName = squareName.ToLower();
            Square2D foundSquare = this.Squares.Find(foundByName);

            return foundSquare;
        }
        public Square2D GetByLocation(int row, int column)
        {
            this._findRow = row;
            this._findCol = column;
            Square2D foundSquare = this.Squares.Find(foundByLocation);

            return foundSquare;
        }

        protected bool foundByName(Square2D find)
        {
            if (find.BoardLocation == this._findSquareName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool foundByLocation(Square2D find)
        {
            if ((find.Row == this._findRow) & (find.Column == this._findCol))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Square2D AddNewSquare(BoardDef boardDef, Int16 col, Int16 row)
        {
            Square2D newSquare = new Square2D();
            newSquare.BoardLocation = (char)(97 + col) + (row + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6

            //TODO: IF the config file TELLS us to do this, then...
            Square2D.SetCheckerboardStyle(newSquare, col, row);

            this.Squares.Add(newSquare);

            newSquare.Column = col;
            newSquare.Row = row;

            return newSquare;
        }


        /// <summary>
        /// Expose our inner board logic so the UI can use it.
        /// This function enumerates through our board for those functions that want to traverse all the squares
        /// </summary>
        /// <param name="boardDef"></param>
        /// <returns></returns>
        public IEnumerable<Square2D> EnumerateBoard(BoardDef boardDef)
        {
            //Rows
            for (Int16 currentRow = 0; currentRow < boardDef.Rows; currentRow++)
            {
                Int16 refRow = (Int16)(boardDef.Rows - (currentRow + 1));

                //Columns
                for (Int16 currentColumn = 0; currentColumn < boardDef.Columns; currentColumn++)
                {
                    if (this.IsNew)
                    {
                        //Hand out this new square to those who want it..
                        yield return this.AddNewSquare(boardDef, currentColumn, refRow);
                    }
                    else
                    {
                        //This would normally be called by the UI via a for loop.
                        yield return this.GetByLocation(refRow, currentColumn);
                    }
                }
            }

            this.IsNew = false;
        }

        #endregion
        #region Utility Functions

        public bool isDiagonal(Point p1, Point p2)
        {
            if (p2.Equals(p1))
            {
                return false;
            }
            else if (Math.Abs(p2.X - p1.X) == Math.Abs(p2.Y - p1.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isHorizontal(Point p1, Point p2)
        {
            if (p2.Equals(p1))
            {
                return false;
            }
            else if (p2.Y == p1.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isVertical(Point p1, Point p2)
        {
            if (p2.Equals(p1))
            {
                return false;
            }
            else if (p2.X == p1.X)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //IsPinned

        #endregion

        public void ExecuteMove(MovePacket _recievedMove)
        {
            ISquare from = this.GetByName(_recievedMove.Previous);
            ISquare to = this.GetByName(_recievedMove.New);

            if (_recievedMove.Rules)
            {
                if (Board2D.IsThisMoveOkay(from, to))
                {
                    Board2D.MoveThePieceOver(from, to);
                }
                else
                {
                    //Make a big stink.  Throw exception?
                    throw new SystemException("MoveException here");
                }
            }
            else
            {
                Board2D.MoveThePieceOver(from, to);
            }
        }

        //nobody but this class needs this function.
        private static void MoveThePieceOver(ISquare from, ISquare to)
        {
            to.CurrentPiece = from.CurrentPiece;
            from.CurrentPiece = null; //Piece has move.  Dump it from the old square
        }
    }
}
