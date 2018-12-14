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

        public Dictionary<XmlBranchName, XmlBranchInfo> TreeDict { get; set; }

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
                                     ExistInTreeCond(key)).
            Select(key => key.Node.Count() % 2 == 0 || key.Node.Count() == 1 ? new XElement(key.Node[0]).XPathSelectElements("//.").ToArray() :
                                                                                SeveralAttr(key.Node)));

            for (int i = 2; i <= NumOfGens; i++)
            {
                var AllTempNodeElements = TreeDict.Keys.ToList().
                                 Where(key => int.Parse(TreeDict[key].Generation) == i - 1 &&
                                                        ExistInTreeCond(key)).
                                 Select(key => key.Node).ToArray().
                                 Select(array => FindElement(array)).
                                 ToList();

                AllTempNodeElements.ToList().
                    ForEach(element => element.Add(TreeDict.Keys.
                Where(key => int.Parse(TreeDict[key].Generation) == i &&
                             ExistInTreeCond(key) &&
                             IsParentToChild(element, key)).
               Select(key => key.Node.Count() % 2 == 0 || key.Node.Count() == 1 ? new XElement(key.Node[0]).XPathSelectElements("//.").ToArray() :
                                                                                SeveralAttr(key.Node))));
            }
        }

        private bool ExistInTreeCond(XmlBranchName Key)
        {
            return Key?.Node?.Count() == 1 ? true :
                   !string.IsNullOrEmpty(Key.Node?[2]);
        }

        private XElement FindElement(string[] Info)
        {
            return Info.Count()%2 == 0 ?
                   NewTree.XPathSelectElement($"//{Info[1]}[@{Info[2]} = '{Info[3]}']/{Info[0]}") :
                   Info.Count() == 1 ?
                   NewTree.XPathSelectElement($"//{Info[0]}") :
                   NewTree.XPathSelectElement($"//{Info[0]}[@{Info[1]} = '{Info[2]}']");
        }
        private XElement[] SeveralAttr(string[] Element)
        {
            var count = (Element.Count() - 1) / 2;
            Attr = new XAttribute[count];
            int i = 0;
            while (i < count)
            {
                Attr[i] = new XAttribute(Element[i*2 + 1], Element[2*i + 2]);
                i++;
            }
            return new XElement(Element[0], Attr).XPathSelectElements("//.").ToArray();
        }

        private bool IsParentToChild(XElement Element, XmlBranchName key)
        {
            var ChosenElement = TreeDict[key]?.ParentName.Length % 2 == 0 ?
                Element.XPathSelectElement("(./..)[last()]") :
                Element;

            var ElementList =  new List<string> { ChosenElement.Name.ToString() };
            var AttrNamesNValues = ChosenElement.Attributes().Select(attr => attr.Name.ToString()).
                Zip(ChosenElement.Attributes().Select(attr => attr.Value.ToString()), (x, y) => new { attr_name = x, attr_value = y }).ToList();

            AttrNamesNValues.ForEach(attr_pair => ElementList.AddRange(new List<string> { attr_pair.attr_name, attr_pair.attr_value }));

            var ParentToCompare = TreeDict[key]?.ParentName.Length % 2 == 0 ?
                TreeDict[key]?.ParentName.Skip(1).ToList() :
                TreeDict[key]?.ParentName.ToList();

            var ElementPairs2Comp = ElementList.Zip(ParentToCompare, (x, y) => new { element_first = x, element_second = y }).ToList();

            return ElementPairs2Comp.All(element_pair => element_pair.element_first == element_pair.element_second);
        }
    }
}
