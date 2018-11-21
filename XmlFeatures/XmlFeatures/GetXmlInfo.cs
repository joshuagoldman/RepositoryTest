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
            string Value = doc.SelectSingleNode($"*//{InfoToFind[0]}[@Value = '{InfoToFind[1]}']")?.
                               SelectSingleNode(InfoToFind[2])?.
                               InnerText;
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
