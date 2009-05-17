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

                    foreach (PieceDef piece in piecesToSet)
                    {
                        switch (piece.Name)
                        {
                            case "All":
                                break;

                            default:
                                configFileMenu.MenuItems.Add(NewMenuItem(piece.Name, piece.Name + "Piece"));
                                configFileMenu.MenuItems[piece.Name + "Piece"].Click += pieceMenuItem_Click;
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
    }
}


//using System;
//using System.Collections.Generic;
//using System.Text;

//using System.IO;
//using System.Xml;

//using System.Windows.Forms;
//using ChessMangler.Engine.Types;
//using ChessMangler.Engine.Config;

//namespace ChessMangler.WinUIParts.Xml
//{
//    /// <summary>
//    /// The purpose of this class is to build a Windows.Form.MenuItem tree containing piece names & images from each stored Config File
//    /// </summary>
//    public class XmlConfigFileMenu
//    {
//        UISquare _clickedSquare;

//        PieceDef _selectedPieceDef;
//        public PieceDef SelectedPieceDef
//        {
//            get
//            {
//                return _selectedPieceDef;
//            }
//            set
//            {
//                _selectedPieceDef = value;
//            }
//        }

//        //TODO: put this in a "Common Menu Tasks" object
//        public static MenuItem NewMenuItem(string caption, string name)
//        {
//            MenuItem addPieceMenu = new MenuItem();
//            addPieceMenu.Name = name;
//            addPieceMenu.Text = caption;

//            return addPieceMenu;
//        }

//        //TODO: put this in a "Common Menu Tasks" object
//        public static PieceMenuItem NewPieceMenuItem(string caption, string name)
//        {
//            PieceMenuItem addPieceMenu = new PieceMenuItem();
//            addPieceMenu.Name = name;
//            addPieceMenu.Text = caption;

//            return addPieceMenu;
//        }

//        //Proof of concept
//        //TODO: Refactor this
//        public void Build_ConfigFile_PieceMenu(UISquare clickedSquare)
//        {
//            PieceMenuItem configFileMenu = new PieceMenuItem();

//            this._clickedSquare = clickedSquare;

//            MenuItem addPieceFromMenu = NewMenuItem("Add Piece from: ", "AddPieceFromMenu");
//            clickedSquare.ContextMenu.MenuItems.Add(addPieceFromMenu);

//            try
//            {
//                //Looks through config directory, and list what Config files are found
//                string configDir = Directory.GetParent(Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString() + "\\Config";

//                List<string> configFiles = new List<string>();
                
//                if (Directory.Exists(configDir))
//                {
//                    foreach (string file in Directory.GetFiles(configDir, "*.config"))
//                    {
//                        configFiles.Add(file);
//                    }
//                }

//                foreach (string file in configFiles)
//                {
//                    string name = Path.GetFileName(file).Replace(".config", "");

//                    configFileMenu = (PieceMenuItem)NewPieceMenuItem(name, name + "ConfigFile");

//                    XmlDocument rulesDocument = Config.LoadXML(file);
//                    List<PieceDef> piecesToSet = ConfigParser.GetUniquePieces(rulesDocument);

//                    foreach (PieceDef pieceDef in piecesToSet)
//                    {
//                        switch (pieceDef.Name)
//                        {
//                            case "All":
//                                break;

//                            default:
//                                PieceMenuItem newItem = (PieceMenuItem)NewPieceMenuItem(pieceDef.Name, pieceDef.Name + "Piece");
//                                newItem.Click += pieceMenuItem_Click;
//                                newItem.PieceDef = pieceDef;

//                                configFileMenu.MenuItems.Add(newItem);
                               
//                                //configFileMenu.MenuItems[pieceDef.Name + "Piece"].PieceDef = pieceDef;
//                                //configFileMenu.MenuItems[pieceDef.Name + "Piece"].Click += pieceMenuItem_Click;
//                                break;
//                        }
//                    }

//                    clickedSquare.ContextMenu.MenuItems[addPieceFromMenu.Name].MenuItems.Add(configFileMenu);
//                }
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        public void pieceMenuItem_Click(object sender, EventArgs e)
//        {
//            MenuItem currentMenuItem;

//            try
//            {
//                currentMenuItem = (MenuItem)sender;

//                //This is called for every clicked Piece..

//                //first of all, what Piece was clicked?  Because we can create a new instance of it if needed

//                Piece newPiece = new Piece();
//                newPiece.Row = this._clickedSquare.Row;
//                newPiece.Column = this._clickedSquare.Column;
//                //newPiece.Definition = ?;
//                //newPiece.Movement = ?;
//                //newPiece.Capture = ?;
//                //newPiece.

//                //newPiece.Image;
//                //newPiece.Color;
//                //newPiece.Name;
                
//                this._clickedSquare.CurrentPiece = newPiece;
//            }
//            catch (Exception ex)
//            {

//            }
//        }
//    }

//    public class PieceMenuItem : MenuItem
//    {
//        PieceMenuItemCollection _menuItems;
//        public new PieceMenuItemCollection MenuItems
//        {
//            get
//            {
//                return _menuItems;
//            }
//            set
//            {
//                _menuItems = value;
//            }
//        }

//        PieceDef _pieceDef;
//        public PieceDef PieceDef
//        {
//            get
//            {
//                return _pieceDef;
//            }
//            set
//            {
//                _pieceDef = value;
//            }
//        }

//        public PieceMenuItem()
//        {
//            this._menuItems = new PieceMenuItemCollection(this);
//        }
//   }

//    public class PieceMenuItemCollection : Menu.MenuItemCollection
//    {
//        private List<PieceMenuItem> _list = new List<PieceMenuItem>();
//        public List<PieceMenuItem> List
//        {
//            get
//            {
//                return _list;
//            }
//            set
//            {
//                _list = value;
//            }
//        }

//        public PieceMenuItemCollection(Menu owner) : base(owner)
//        {
//        }

//        public new PieceMenuItem this[int index]
//        {
//            get
//            {
//                return _list[index]; 
//            }
//            set
//            {
//                _list[index] = value;
//            }
//        }
//        public int IndexOf(PieceMenuItem item)
//        {
//            return this.List.IndexOf(item);
//        }
//        public void Add(PieceMenuItem item)
//        {
//            this.List.Add(item);
//        }
//        public void Remove(PieceMenuItem item)
//        {
//            this.List.Remove(item);
//        }
//        public void CopyTo(PieceMenuItem[] array, int index)
//        {
//            this.List.CopyTo(array, index);
//        }
//        public void AddRange(PieceMenuItem[] collection)
//        {
//            this.AddRange(collection);
//        }
//        public bool Contains(PieceMenuItem item)
//        {
//            return this.List.Contains(item);
//        }
//        public void Insert(int index, PieceMenuItem item)
//        {
//            this.List.Insert(index, item);
//        }
//    }
//}

