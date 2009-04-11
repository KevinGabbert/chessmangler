using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Engine.Xml
{
    /// <summary>
    /// This is a class intended for generic XML parsing functions used by this library.
    /// Functions intended to be exposed to other libraries are in Engine.Config.Parser
    /// </summary>
    internal static class XmlParser
    {
        public static XmlNode GetDefNode(XmlDocument configFile, string rootNode, string defNode)
        {
            XmlNode gotDefNode = null;

            foreach (XmlNode xmlNode in configFile)
            {
                if (xmlNode.Name == rootNode)
                {
                    foreach (XmlNode childNode in xmlNode)
                    {
                        if (childNode.Name == defNode)
                        {
                            gotDefNode = childNode;
                        }
                    }
                }
            }

            return gotDefNode;
        }
    }
}
