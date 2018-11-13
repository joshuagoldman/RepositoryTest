using System;
using System.Collections.Generic;
using Main.XmlDoc;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {

        public string GetXmlInfo()
        {
            var Info = InfoToFind.Split(',').ToList();
            string Value = doc.SelectSingleNode($"//*[@{Info[0]} = '{Info[1]}']")?.
                               SelectSingleNode(Info[2])?.
                               InnerText;
            if (Value != null)
            {
                return Value;
            }
            else
            {
                throw  new Exception("Listen man, aint no value matchin yo search");
            }
        }        
    }
}
