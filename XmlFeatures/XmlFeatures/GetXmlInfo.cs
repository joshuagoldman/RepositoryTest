using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Threading.Tasks;

namespace XmlFeatures.XmlDoc
{
    public partial class ExEmEl
    {

        public string GetXmlInfo()
        {
            string Value = XDoc.XPathSelectElement($"*//{InfoToFind[0]}[@Value = '{InfoToFind[1]}']")?.
                               XPathSelectElement(InfoToFind[2])?.Value;        
            if (Value != null)
            {
                return Value.Replace('_', ' ');
            }
            else
            {
                throw  new Exception("Listen dawg, aint no value matchin yo search");
            }
        }        
    }
}
