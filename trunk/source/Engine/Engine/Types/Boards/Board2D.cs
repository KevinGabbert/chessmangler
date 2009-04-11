using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

using Engine.Config;

namespace Engine.Types
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

        public Board2D(XmlDocument configFile, string directory)
        {
            this.CreateBoard(configFile);
            this.SetPieces(configFile, directory);
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
                Square2D squareForPiece = this.GetByName(piece.StartingLocation);
                squareForPiece.CurrentPiece = new Piece(piece.Name);

                //this needs to be set up in Piece..
                squareForPiece.CurrentPiece.Image = new Bitmap(directory + "\\images\\" + piece.ImageName);
            }
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
            //look up the square by its name.  ex. name:  "A2"
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
            newSquare.Number = (boardDef.Columns * row) + col;
            newSquare.Name = (char)(97 + col) + (row + 1).ToString(); //lowercase is PGN format... i.e. a6, not A6

            //TODO: IF the config file TELLS us to do this, then...
            Square2D.SetCheckerboardStyle(newSquare, boardDef.Columns, boardDef.Rows);
            
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
                for (Int16 currentColumn = 0; currentColumn < boardDef.Columns; currentColumn++)
                {
                    //This IF may be temporary, as I'm evaluating whether this is a good idea..
                    if (this.IsNew)
                    {
                        Square2D newSquare;
                        this.AddNewSquare(boardDef, currentColumn, currentRow, out newSquare);

                        yield return newSquare; //Hand out this new square to those who want it..
                    }
                    else
                    {
                        //This would normally be called by the UI via a for loop.
                        yield return this.GetByLocation(currentRow, currentColumn);
                    }
                }
            }

            this.IsNew = false;
        }

        #endregion
    }
}
