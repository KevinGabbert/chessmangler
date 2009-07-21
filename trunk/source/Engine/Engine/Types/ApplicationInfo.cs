using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChessMangler.Engine.Xml;

namespace ChessMangler.Engine.Types
{
    public class ApplicationInfo
    {
        public static string GetVersion(string configFilePath)
        {
            //XmlParser.ConfigFile = this.AppConfigFile;
            return "AppVersion Not Implemented Yet";   //XmlParser.Peek("ChessConfig").Attribute("version").ToString();
        }
    }
}
