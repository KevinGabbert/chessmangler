using System;
using System.Collections.Generic;
using System.Text;

namespace ChessMangler.WinUIParts.Menus
{
    public class UISquareDeleteMenu
    {
        UISquare _clickedSquare;
        public UISquare ClickedSquare
        {
            get
            {
                return _clickedSquare;
            }
            set
            {
                _clickedSquare = value;
            }
        }

        internal void CreatePieceDeleteMenu(UISquare clickedSquare)
        {
            this._clickedSquare = clickedSquare;
            clickedSquare.ContextMenu.MenuItems.Add("Delete", deleteMenuItem_Click);
        }

        public void deleteMenuItem_Click(object sender, EventArgs e)
        {
            this._clickedSquare.Clear();
        }
    }
}
