using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Xml;

using System.Drawing;

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
                        }
                        gotPieceDefs.Add(newPiece);
                    }
                }
            }

            return gotPieceDefs;
        }

        public static List<PieceDef> GetUniquePieces(XmlDocument configDocument)
        {

            //TODO: Does this need to be cached somewhere??

            List<PieceDef> gotPieceDefs = null;
            List<string> pieceNames = new List<string>();

            XmlNode pieceDefs = ConfigParser.GetConfigDefNode(configDocument, "PieceDef");

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

                            if (currentName == "name")
                            {
                                newPiece.Name = currentAttribute.Value;
                            }

                            if (!pieceNames.Contains(newPiece.Name))
                            {
                                pieceNames.Add(newPiece.Name);

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

                                gotPieceDefs.Add(newPiece);
                            }
                        }
                    }
                }
            }

            return gotPieceDefs;
        }
    }
}
