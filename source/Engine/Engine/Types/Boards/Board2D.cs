using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

using ChessMangler.Engine.Config;
using ChessMangler.Engine.Interfaces;

namespace ChessMangler.Engine.Types
{
    public class Board2D
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
            this.SetPieces(configFile, UIDirectory);
        }

        protected void CreateBoard(XmlDocument configFile)
        {
            BoardDef boardDef = ConfigParser.GetBoardDef(configFile); // TODO: if BoardDef is null then throw custom exception
            this.Definition = boardDef;

            //This foreach construct may be temporary, as I'm evaluating whether this is a good idea..
            foreach (Square2D currentSquare in this.SquareLogic(boardDef))
            {
                //this empty foreach executes SquareLogic. We may need to put something here later..
            }
        }

        private void SetPieces(XmlDocument configFile, string directory)
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
                        squareForPiece.CurrentPiece = new Piece(piece.Name);

                        //this needs to be set up in Piece..
                        squareForPiece.CurrentPiece.Image = new Bitmap(directory + "\\images\\" + piece.ImageName);
                        break;
                }
            }
        }

        public static bool IsThisMoveOkay(ISquare startSquare, ISquare endSquare)
        {
            //no rules defined?? Then ALL moves are legal!

            return true;


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
            if (find.Name == this._findSquareName)
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

        public void AddNewSquare(BoardDef boardDef, Int16 col, Int16 row, out Square2D newlyAddedSquare)
        {
            Square2D newSquare = new Square2D();
            newSquare.Name = (char)(97 + col) + (row + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6

            //TODO: IF the config file TELLS us to do this, then...
            Square2D.SetCheckerboardStyle(newSquare, col, row);

            this.Squares.Add(newSquare);
            newlyAddedSquare = newSquare;

            newSquare.Column = col;
            newSquare.Row = row;
        }

        //Expose our inner board logic so the UI can use it.
        public IEnumerable<Square2D> SquareLogic(BoardDef boardDef)
        {
            for (Int16 currentRow = 0; currentRow < boardDef.Rows; currentRow++)
            {
                Int16 refRow = (Int16)(boardDef.Rows - (currentRow + 1));

                for (Int16 currentColumn = 0; currentColumn < boardDef.Columns; currentColumn++)
                {
                    //This IF may be temporary, as I'm evaluating whether this is a good idea..
                    if (this.IsNew)
                    {
                        Square2D newSquare;
                        this.AddNewSquare(boardDef, currentColumn, refRow, out newSquare);

                        yield return newSquare; //Hand out this new square to those who want it..
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
