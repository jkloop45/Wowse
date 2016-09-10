using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Language
{
    public class LanguagePackage
    {
        Dictionary<string, string> LanguageStrMap = new Dictionary<string, string>();
        XmlReader mXmlReader;
        
        public LanguagePackage(string path)
        {
            if (File.Exists(path))
            {
                mXmlReader = XmlReader.Create(new FileStream(path, FileMode.Open));
            } else
                throw new Exception("File did not exist!");
        }

        public string this[string sign]{
            get
            {
                return LanguageStrMap[sign];
            }
        }

        public int Count { get { return LanguageStrMap.Count; } }

        public void LoadPackage()
        {
            while (mXmlReader.Read())
            {
                if(mXmlReader.LocalName != "" && mXmlReader.LocalName != "xml")
                {
                    string localName;
                    if (mXmlReader.NodeType == XmlNodeType.Element)
                    {
                        localName = mXmlReader.LocalName;
                        while (mXmlReader.NodeType != XmlNodeType.Text)
                        {
                            if (mXmlReader.NodeType == XmlNodeType.Element)
                                localName = mXmlReader.LocalName;
                            mXmlReader.Read();
                        }
                        LanguageStrMap.Add(localName, mXmlReader.Value);
                    }
                }
            }
        }
    }
}
