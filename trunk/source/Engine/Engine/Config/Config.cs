using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;

using ChessMangler.Engine.Xml;

namespace ChessMangler.Engine.Config
{
    //This class will probably be moved around later..
    public static class Config
    {
        public static XmlDocument LoadXML(string path)
        {
            XmlDocument loader = new XmlDocument();

            using (StreamReader __fileToLoad = new StreamReader(path))
            {
                loader.Load(__fileToLoad);
                __fileToLoad.Close();
            }

            return loader;
        }

        public static string GetConfigFileVersion(string configFilePath)
        {
            return XmlParser.Peek("ChessConfig").Attribute("version").ToString();
        }
    }
}
