using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Globalization;
using NGettext;

namespace Language
{
    public class LanguagePackage
    {
        ICatalog catalog;
        public LanguagePackage(string path, string lang)
        {
            catalog = new Catalog(@"global", path, new CultureInfo(lang));
        }
        public string this[string sign]
        {
            get
            {
                return catalog.GetString(sign);
            }
        }
    }
}
