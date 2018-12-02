using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq.Expressions;

namespace XmlFeatures.XmlDoc
{

    public class XmlTree
    {

        public XElement NewTree { get; set; }

        public Dictionary<string[],XmlBranchInfo> TreeDict { get; set; }

        private XAttribute[] Attr { get; set; }

        public XmlTree()
        {
        }

        public void GetTree()
        {
            var NumOfGens = TreeDict.Keys.ToList().
                Select(key => int.Parse(TreeDict[key].Generation)).Max();
            NewTree = new XElement("Initial");
            NewTree.Add(TreeDict.Keys.
                        Where(key => int.Parse(TreeDict[key].Generation) == 1 &&
                                key[2] != null).
            Select(key =>key.ToList().Count > 3 ?
                   SeveralAttr(key) :
                   key.ToList().Count == 3 ?
                   new XElement(key[0],new XAttribute(key[1], key[2])) :
                   new XElement(key[0])));
            for (int i = 2; i <= NumOfGens; i++)
            {
                var AllTempNodeElements = TreeDict.Keys.ToList().
                                 Where(key => int.Parse(TreeDict[key].Generation) == i - 1).
                                 Select(key => NewTree.XPathSelectElement($"//{key[0]}") ?? null).ToList();
                AllTempNodeElements.ForEach(element => element.Add(TreeDict.Keys.
                Where(key => int.Parse(TreeDict[key].Generation) == i &&
                             key[2] != null &&
                             element.Name == TreeDict[key]?.ParentName[0]).
                Select(key => key.ToList().Count > 3 ?
                       SeveralAttr(key) :
                       key.ToList().Count == 3 ?
                       new XElement(key[0], new XAttribute(key[1], key[2])) :
                       new XElement(key[0]))));
            }            
        }
        private XElement SeveralAttr(string[] Element)
        {
            var count = (Element.Count() - 1) / 2;
            Attr = new XAttribute[count];
            int i = 0;
            while (i < count)
            {
                Attr[i] = new XAttribute(Element[i*2 + 1], Element[2*i + 2]);
                i++;
            }
            return new XElement(Element[0], Attr);
        }
    }
}
