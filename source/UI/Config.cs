using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;

namespace SKChess
{
    //This class will probably be moved around later..
    public class Config
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

        public static void LoadXML2(string Path_And_Filename)
        {
            XmlDocument loader = new XmlDocument();

            using (StreamReader __fileToLoad = new StreamReader(Path_And_Filename))
            {
                loader.Load(__fileToLoad);

                foreach (XmlNode __xmlNode in loader)
                {
                    if (__xmlNode.Name == "configuration")
                    {
                        foreach (XmlNode __childNode in __xmlNode)
                        {
                            if (__childNode.Name == "subnode1")
                            {
                                //blah blah blah
                            }

                            if (__childNode.Name == "subnode2")
                            {
                                foreach (XmlNode __grandChildNode in __childNode)
                                {
                                    XmlAttributeCollection attributes = __grandChildNode.Attributes;
                                    foreach (XmlAttribute __xaCurrent in attributes)
                                    {
                                        string currentName = __xaCurrent.Name;
                                        string currentVal = __xaCurrent.Value;
                                    }
                                }
                            }
                        }
                    }
                }

                __fileToLoad.Close();
            }
        }
    }
}
