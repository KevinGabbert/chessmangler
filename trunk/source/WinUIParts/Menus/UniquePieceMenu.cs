using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.IO;
using System.Xml;

using System.Windows.Forms;
using ChessMangler.Engine.Types;
using ChessMangler.Engine.Config;

namespace ChessMangler.WinUIParts.Menus
{
    /// <summary>
    /// The purpose of this class is to build a Windows.Form.MenuItem tree containing piece names & images from each stored Config File
    /// </summary>
    public class UniquePieceMenu
    {
        static UISquare _clickedSquare;
        static Dictionary<string, PieceDef> _tempCache = new Dictionary<string, PieceDef>();

        //TODO: put this in a "Common Menu Tasks" object
        public MenuItem NewMenuItem(string caption, string name)
        {
            MenuItem addPieceMenu = new MenuItem();
            addPieceMenu.Name = name;
            addPieceMenu.Text = caption;

            return addPieceMenu;
        }

        //TODO: Refactor this
        public void Build_ConfigFile_PieceMenu(UISquare clickedSquare)
        {
            try
            {
                this.Add_Config_Menus(clickedSquare);
            }
            catch (Exception ex)
            {
                //TODO: This should be reported on screen somewhere (like a red box or a message to show that the config
                //file is corrupt or missing.
            }
        }

        public void Add_Config_Menus(UISquare clickedSquare)
        {
            UniquePieceMenu._clickedSquare = clickedSquare;
            MenuItem configFileMenu;

            MenuItem addPieceFromMenu = NewMenuItem("Add Piece from: ", "AddPieceFromMenu");
            clickedSquare.ContextMenu.MenuItems.Add(addPieceFromMenu);

            //Looks through config directory, and list what Config files are found
            string configDir = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString() + "\\Config";

            List<string> configFiles = new List<string>();
            UniquePieceMenu.Get_Config_Files(configDir, configFiles);

            foreach (string filePath in configFiles)
            {
                string fileNameOnly = Path.GetFileName(filePath).Replace(".config", "");
                configFileMenu = NewMenuItem(fileNameOnly, fileNameOnly + "ConfigFile");

                this.Add_Piece_Menu(clickedSquare, configFileMenu, addPieceFromMenu, filePath);
            }
        }
        public void Add_Piece_Menu(UISquare clickedSquare, MenuItem configFileMenu, MenuItem addPieceFromMenu, string filePath)
        {
            //This is the main focus of this function... Getting the unique pieces
            List<PieceDef> piecesToSet = ConfigParser.GetUniquePieces(Config.LoadXML(filePath));

            foreach (PieceDef pieceDef in piecesToSet)
            {
                switch (pieceDef.Name)
                {
                    case "All":
                        break;

                    default:
                        Add_Piece_MenuItem(configFileMenu, filePath, pieceDef);
                        break;
                }
            }

            clickedSquare.ContextMenu.MenuItems[addPieceFromMenu.Name].MenuItems.Add(configFileMenu);
        }
        public void Add_Piece_MenuItem(MenuItem configFileMenu, string filePath, PieceDef pieceDef)
        {
            string uniqueName = pieceDef.Name + Guid.NewGuid();
            configFileMenu.MenuItems.Add(NewMenuItem("(" + pieceDef.Color.Name + ") " + pieceDef.Name, uniqueName));
            configFileMenu.MenuItems[uniqueName].Click += pieceMenuItem_Click;

            //TODO:  Temporary Code (as this will be eventually pulled from a config file)
            pieceDef.ImageDirectory = Directory.GetParent(Path.GetDirectoryName(filePath)).FullName + @"\images\";

            //Store what we've read in, cause we are going to look up what the user selected..
            UniquePieceMenu._tempCache.Add(uniqueName, pieceDef);
        }

        public static void Get_Config_Files(string configDir, List<string> configFiles)
        {
            if (Directory.Exists(configDir))
            {
                foreach (string file in Directory.GetFiles(configDir, "*.config"))
                {
                    configFiles.Add(file);
                }
            }
        }

        private PieceDef GetPieceDef(string name)
        {
            PieceDef gotValue;

            _tempCache.TryGetValue(name, out gotValue);
            return gotValue;
        }

        #region Event Handlers

        public void pieceMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem currentMenuItem;

            try
            {
                currentMenuItem = (MenuItem)sender;

                //This is called for every clicked Piece..

                ////first of all, what Piece was clicked?  Because we can create a new instance of it if needed
                Piece newPiece = new Piece((new UniquePieceMenu()).GetPieceDef(currentMenuItem.Name));

                newPiece.Row = UniquePieceMenu._clickedSquare.Row;
                newPiece.Column = UniquePieceMenu._clickedSquare.Column;

                ////newPiece.Movement = ?;
                ////newPiece.Capture = ?;

                UniquePieceMenu._clickedSquare.CurrentPiece = newPiece;
            }
            catch (Exception ex)
            {
                //TODO: This should be reported on screen somewhere (like a red box or a message to show that the config
                //file is corrupt or missing.

                //Whatever the case.. at this point the action should be cancelled.
            }
        }

        #endregion
    }
}