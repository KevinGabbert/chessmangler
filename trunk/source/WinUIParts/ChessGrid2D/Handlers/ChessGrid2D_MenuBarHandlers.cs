using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;

namespace ChessMangler.WinUIParts.ChessGrid2D
{
    public class ChessGrid2D_MenuBarHandlers : ChessGrid2D_Base
    {
        public ChessGrid2D_MenuBarHandlers(DebugForm debugForm, ChessGrid2D_Form chessGridForm)
        {
            this.DebugForm = debugForm;
            this.ChessGrid2D_Form = chessGridForm;
            this.UIBoard = chessGridForm.Grid.UIBoard;
        }

        bool _userSetDebugMode;

        public void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open options form 
            //Add "Constrain Proportions" option
        }

        public void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _debugForm.Show();
            _debugForm.debugTextBox.Text += "New Debug Form";
        }

        public void toggleDebugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _userSetDebugMode = !this._userSetDebugMode;

            this.UIBoard.DebugMode = this._userSetDebugMode;
            this.ChessGrid2D_Form.Grid.Redraw(this.ChessGrid2D_Form.Grid.UIBoard.Flipped);
        }
        public void resetPiecesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //experimental
            BoardDef board = this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.Definition;
            short squareSize = this.ChessGrid2D_Form.Grid.UIBoard.Squares[1].SquareSize;

            //this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.Squares = new List<Square2D>();
            //this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.IsNew = true;
            //this.ChessGrid2D_Form.Grid.Redraw();

            //this.ChessGrid2D_Form.DitchHandlers();
            //this.ChessGrid2D_Form.Grid = new Grid2D(this.ChessGrid2D_Form);
            //this.ChessGrid2D_Form.InitGrid();
            //this.ChessGrid2D_Form.Grid.SetUp_FreeFormBoard(this.ChessGrid2D_Form, board, squareSize);

            //this.ChessGrid2D_Form.InitHandlers();

            //this.ChessGrid2D_Form.Grid.Redraw();
        }
        public void clearPiecesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //experimental
            BoardDef board = this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.Definition;
            short squareSize = this.ChessGrid2D_Form.Grid.UIBoard.Squares[1].SquareSize;

            this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.Squares = new List<Square2D>();
            this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.IsNew = true;
            this.ChessGrid2D_Form.Grid.Redraw(this.ChessGrid2D_Form.Grid.UIBoard.Flipped);

            //this.ChessGrid2D_Form.DitchHandlers();
            //this.ChessGrid2D_Form.Grid = new Grid2D(this.ChessGrid2D_Form);
            //this.ChessGrid2D_Form.InitGrid();
            //this.ChessGrid2D_Form.Grid.SetUp_FreeFormBoard(this.ChessGrid2D_Form, board, squareSize);

            //this.ChessGrid2D_Form.InitHandlers();

            //this.ChessGrid2D_Form.Grid.Redraw();
        }

        public void flipBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:  this setting needs to be saved in the database.
            this.ChessGrid2D_Form.Grid.UIBoard.Flipped = !this.ChessGrid2D_Form.Grid.UIBoard.Flipped;
            this.ChessGrid2D_Form.Grid.Redraw(this.ChessGrid2D_Form.Grid.UIBoard.Flipped);
        }

        public void ClearPieces()
        {
            BoardDef board = this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.Definition;
            short squareSize = this.ChessGrid2D_Form.Grid.UIBoard.Squares[1].SquareSize;

            this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.Squares = new List<Square2D>();
            this.ChessGrid2D_Form.Grid.UIBoard.EngineBoard.IsNew = true;
            this.ChessGrid2D_Form.Grid.Redraw(this.ChessGrid2D_Form.Grid.UIBoard.Flipped);
        }
    }
}
