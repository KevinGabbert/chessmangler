using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;

namespace ChessMangler.WinUIParts
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
    }
}
