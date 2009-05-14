using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

using ChessMangler.Engine.Config;
using ChessMangler.Engine.Interfaces;
using ChessMangler.Engine.Enums;

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
            foreach (Square2D currentSquare in this.BoardEnumerator(boardDef))
            {
                //this empty foreach executes BoardEnumerator. We may need to put something here later..
            }
        }
        private void MapPieces(XmlDocument configFile, string directory)
        {
            List<PieceDef> piecesToSet = ConfigParser.GetPieces(configFile);

            foreach (PieceDef piece in piecesToSet)
            {
                switch (piece.Name)
                {
                    case "All":
                        break;

                    default:
                        Square2D squareForPiece = this.GetByName(piece.StartingLocation);
                        squareForPiece.CurrentPiece = new Piece();
                        squareForPiece.CurrentPiece.Name = piece.Name;

                        //this needs to be set up in Piece..
                        squareForPiece.CurrentPiece.Image = new Bitmap(directory + "\\images\\" + piece.ImageName);
                        squareForPiece.CurrentPiece.Color = piece.Color;

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
        public IEnumerable<Square2D> BoardEnumerator(BoardDef boardDef)
        {
            for (Int16 currentRow = 0; currentRow < boardDef.Rows; currentRow++)
            {
                Int16 refRow = (Int16)(boardDef.Rows - (currentRow + 1));

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
    }
}
