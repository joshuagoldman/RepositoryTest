using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace XmlFeatures.XmlDoc
{
    public class FindXLocation
    {
        public XElement ChildParentElement { get; set; }

        public XDocument XDoc;

        public string ParentAboveChildNoAttr { get; set; }

        public FindXLocation()
        {

        }

        public void FindByElement(List<string> Child)
        {
            ChildParentElement =
                ParentAboveChildNoAttr == null ?
                Child.Count == 3 ?
                XDoc.XPathSelectElement($"*//{Child[0]}[@{Child[1]} = '{Child[2]}']") :
                XDoc.XPathSelectElement($"//{Child[0]}") :
                Child.Count == 3 ?
                XDoc.XPathSelectElement($"*//{Child[0]}[@{Child[1]} = '{Child[2]}']/{ParentAboveChildNoAttr}") :
                XDoc.XPathSelectElement($"//{Child[0]}/{ParentAboveChildNoAttr}");
        }
    }
}
