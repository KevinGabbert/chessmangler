using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using ChessMangler.Engine.Types;

namespace ChessMangler.Engine.Config
{
    /// <summary>
    /// This is the class intended for use in parsing all chess config files.
    /// </summary>
    public static class ConfigParser
    {
        const string _chessConfig = "ChessConfig";

        public static XmlNode GetConfigDefNode(XmlDocument configFile, string defNode)
        {
            return Engine.Xml.XmlParser.GetDefNode(configFile, _chessConfig, defNode);
        }
        public static BoardDef GetBoardDef(XmlDocument configFile)
        {
            BoardDef gotBoardDef = null;

            XmlNode boardDefNode = ConfigParser.GetConfigDefNode(configFile, "BoardDef");

            if (boardDefNode != null)
            {
                gotBoardDef = new BoardDef();

                XmlAttributeCollection attributes = boardDefNode.Attributes;
                foreach (XmlAttribute currentAttribute in attributes)
                {
                    string currentName = currentAttribute.Name;

                    if (currentName == "rows")
                    {
                        gotBoardDef.Rows = Convert.ToInt16(currentAttribute.Value);
                    }

                    if (currentName == "columns")
                    {
                        gotBoardDef.Columns = Convert.ToInt16(currentAttribute.Value);
                    }
                }
            }

            return gotBoardDef;
        }
        public static Int16 GetSquareSize(XmlDocument configFile)
        {
            //TODO: if squaresize is -1 then throw custom exception
            XmlNode defNode = ConfigParser.GetConfigDefNode(configFile, "UIDef");

            Int16 gotSquareSize = -1;

            if (defNode != null)
            {
                foreach (XmlNode squareLayoutNode in defNode)
                {
                    if (squareLayoutNode.Name == "UISquareLayout")
                    {
                        XmlAttributeCollection attributes = squareLayoutNode.Attributes;
                        foreach (XmlAttribute currentAttribute in attributes)
                        {
                            string currentName = currentAttribute.Name;

                            if (currentName == "SquareSize")
                            {
                                gotSquareSize = Convert.ToInt16(currentAttribute.Value);
                            }
                        }
                    }
                }
            }

            return gotSquareSize;
        }

        public static List<PieceDef> GetPieces(XmlDocument startingPosition)
        {
            List<PieceDef> gotPieceDefs = null;

            XmlNode pieceDefs = ConfigParser.GetConfigDefNode(startingPosition, "PieceDef");

            if (pieceDefs != null)
            {
                gotPieceDefs = new List<PieceDef>();

                foreach (XmlNode currentPiece in pieceDefs)
                {
                    if (currentPiece.Attributes != null)
                    {
                        PieceDef newPiece = new PieceDef();

                        XmlAttributeCollection attributes = currentPiece.Attributes;
                        foreach (XmlAttribute currentAttribute in attributes)
                        {
                            string currentName = currentAttribute.Name;

                            ConfigParser.XmlToPieceDef(newPiece, currentAttribute, currentName);
                        }
                        gotPieceDefs.Add(newPiece);
                    }
                }
            }

            return gotPieceDefs;
        }
        public static List<PieceDef> GetUniquePieces(XmlDocument configDocument)
        {
            //TODO: Does this need to be cached somewhere?
            List<PieceDef> gotPieceDefs = null;
            List<string> pieceNames = new List<string>();
            bool unique = false;

            XmlNode pieceDefs = ConfigParser.GetConfigDefNode(configDocument, "PieceDef");

            if (pieceDefs != null)
            {
                gotPieceDefs = new List<PieceDef>();

                foreach (XmlNode currentPiece in pieceDefs)
                {
                    PieceDef newPiece = new PieceDef();

                    if (currentPiece.Attributes != null)
                    {
                        string pieceName = ConfigParser.GetPieceName(currentPiece.Attributes);
                        string pieceColor = ConfigParser.GetPieceColor(currentPiece.Attributes);

                        unique = (pieceName != "All") && (!pieceNames.Contains(pieceColor + "." + pieceName));

                        XmlAttributeCollection attributes = currentPiece.Attributes;
                        foreach (XmlAttribute currentAttribute in attributes)
                        {
                            string currentName = currentAttribute.Name;
                            if (unique && currentName != "All")
                            {
                                pieceNames.Add(pieceColor + "." + pieceName);
                                ConfigParser.XmlToPieceDef(newPiece, currentAttribute, currentName);
                            }
                        }
                    }

                    if (unique) { gotPieceDefs.Add(newPiece); }
                }
            }

            return gotPieceDefs;
        }

        public static void XmlToPieceDef(PieceDef newPiece, XmlAttribute currentAttribute, string currentName)
        {
            if (currentName == "name")
            {
                newPiece.Name = currentAttribute.Value;
            }

            if (currentName == "StartingLocation")
            {
                newPiece.StartingLocation = currentAttribute.Value;
            }

            if (currentName == "ImageName")
            {
                newPiece.ImageName = currentAttribute.Value;
            }

            if (currentName == "Player")
            {
                newPiece.Player = Convert.ToInt16(currentAttribute.Value);
            }

            if (currentName == "Color")
            {
                newPiece.Color = Color.FromName(currentAttribute.Value);
            }

            if (currentName == "ImageDirectory")
            {
                newPiece.ImageDirectory = currentAttribute.Value;
            }
        }

        //TODO: Refactor these "Get" functions
        public static string GetPieceName(XmlAttributeCollection attributes)
        {
            string retVal = "Not Found";

            foreach (XmlAttribute currentAttribute in attributes)
            {
                string currentName = currentAttribute.Name;

                if (currentName == "name")
                {
                    retVal = currentAttribute.Value;
                    break;
                }
            }

            return retVal;
        }
        public static string GetPieceColor(XmlAttributeCollection attributes)
        {
            string retVal = "Not Found";

            foreach (XmlAttribute currentAttribute in attributes)
            {
                string currentName = currentAttribute.Name;

                if (currentName == "Color")
                {
                    retVal = currentAttribute.Value;
                    break;
                }
            }

            return retVal;
        }

        //TODO:
        //An alternate way to filter, if necessary: what we really need to to be able to search gotPieceDefs for a piece with specific 
        //attributes..  
        //These "Get" function might wind up being fine for now..
        //now.. Filter: This likely means to make a "Contains" function for List<PieceDef>
        //1. Create new class:  PieceDefs.
        //2. Inherit from List<PieceDef>
        //3. Overload Contains(). Make it so you can pass an attribute, like PieceDef.Color
    }
}
