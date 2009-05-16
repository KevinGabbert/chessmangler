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
        public static void Build_ConfigFile_PieceMenu(UISquare clickedSquare)
        {
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

                    MenuItem configFileMenu = NewMenuItem(name, name + "ConfigFile");

                    XmlDocument rulesDocument = Config.LoadXML(file);
                    List<PieceDef> piecesToSet = ConfigParser.GetUniquePieces(rulesDocument);

                    foreach (PieceDef piece in piecesToSet)
                    {
                        switch (piece.Name)
                        {
                            case "All":
                                break;

                            default:
                                configFileMenu.MenuItems.Add(piece.Name);
                                configFileMenu.Click += configFileMenu_Click;
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

        public static void configFileMenu_Click(object sender, EventArgs e)
        {
            int x;
            //This is called for every clicked Piece..

            //first of all, what Piece was clicked?  Because we can create a new instance of it if needed

            //next.. either the image or the path is already loaded into the piece, so lets grab that pic, 
            //turn it into a bitmap & set the cursor to become that image

            //Done! Now when the cursor clicks on an unoccupied square, then that image will be transferred to that square,
            //but that code is in that square!
        }
    }
}
