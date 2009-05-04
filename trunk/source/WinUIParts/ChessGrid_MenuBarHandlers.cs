﻿using System;
using System.Collections.Generic;
using System.Text;

using ChessMangler.WinUIParts;
using ChessMangler.Engine.Types;

namespace ChessMangler.WinUIParts
{
    public class ChessGrid_MenuBarHandlers : ChessGrid_HandlerBase
    {
        bool _userSetDebugMode;

        public void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _debugForm = new DebugForm();
            _debugForm.Show();
            _debugForm.debugTextBox.Text += "New Debug Form";
        }

        public void toggleDebugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _userSetDebugMode = !this._userSetDebugMode;

            ChessGrid_Form.UIBoard.DebugMode = this._userSetDebugMode;
            ChessGrid_Form.Redraw_UIBoard();
        }

    }
}