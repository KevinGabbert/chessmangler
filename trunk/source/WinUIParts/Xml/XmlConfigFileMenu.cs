using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;

using System.Windows.Forms;
using ChessMangler.Engine.Types;
using ChessMangler.Engine.Config;

namespace ChessMangler.WinUIParts.Xml
{
    /// <summary>
    /// The purpose of this class is to build a Windows.Form.MenuItem tree containing piece names & images from each stored Config File
    /// </summary>
    public class XmlConfigFileMenu
    {
        UISquare _clickedSquare;
        Dictionary<string, PieceDef> _tempCache = new Dictionary<string, PieceDef>();

        //TODO: put this in a "Common Menu Tasks" object
        public static MenuItem NewMenuItem(string caption, string name)
        {
            MenuItem addPieceMenu = new MenuItem();
            addPieceMenu.Name = name;
            addPieceMenu.Text = caption;

            return addPieceMenu;
        }

        //Proof of concept
        //TODO: Refactor this
        public void Build_ConfigFile_PieceMenu(UISquare clickedSquare)
        {
            this._clickedSquare = clickedSquare;
            MenuItem configFileMenu;

            MenuItem addPieceFromMenu = NewMenuItem("Add Piece from: ", "AddPieceFromMenu");
            clickedSquare.ContextMenu.MenuItems.Add(addPieceFromMenu);

            try
            {
                //Looks through config directory, and list what Config files are found
                string configDir = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString() + "\\Config";

                List<string> configFiles = new List<string>();

                if (Directory.Exists(configDir))
                {
                    foreach (string file in Directory.GetFiles(configDir, "*.config"))
                    {
                        configFiles.Add(file);
                    }
                }

                foreach (string file in configFiles)
                {
                    string name = Path.GetFileName(file).Replace(".config", "");

                    configFileMenu = NewMenuItem(name, name + "ConfigFile");

                    XmlDocument rulesDocument = Config.LoadXML(file);
                    List<PieceDef> piecesToSet = ConfigParser.GetUniquePieces(rulesDocument);

                    foreach (PieceDef pieceDef in piecesToSet)
                    {
                        switch (pieceDef.Name)
                        {
                            case "All":
                                break;

                            default:
                                string uniqueName = pieceDef.Name + Guid.NewGuid();
                                configFileMenu.MenuItems.Add(NewMenuItem(pieceDef.Name, uniqueName));
                                configFileMenu.MenuItems[uniqueName].Click += pieceMenuItem_Click;

                                //Store what we've read in, cause we are going to look up what the user selected..
                                this._tempCache.Add(uniqueName, pieceDef); 
                                break;
                        }
                    }

                    clickedSquare.ContextMenu.MenuItems[addPieceFromMenu.Name].MenuItems.Add(configFileMenu);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void pieceMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem currentMenuItem;

            try
            {
                currentMenuItem = (MenuItem)sender;

                //This is called for every clicked Piece..

                //first of all, what Piece was clicked?  Because we can create a new instance of it if needed

                Piece newPiece = new Piece();
                newPiece.Definition = this.GetPieceDef(currentMenuItem.Name);

                newPiece.Row = this._clickedSquare.Row;
                newPiece.Column = this._clickedSquare.Column;
                //newPiece.Definition = ?;
                //newPiece.Movement = ?;
                //newPiece.Capture = ?;
                //newPiece.

                //newPiece.Image;
                //newPiece.Color;
                //newPiece.Name;


                this._clickedSquare.CurrentPiece = newPiece;
            }
            catch (Exception ex)
            {

            }


            int x;
        }

        private PieceDef GetPieceDef(string name)
        {
            PieceDef gotValue;

            _tempCache.TryGetValue(name, out gotValue);
            return gotValue;
        }
    }
}