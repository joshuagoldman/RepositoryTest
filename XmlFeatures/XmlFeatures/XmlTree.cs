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
                                (!string.IsNullOrEmpty(key?.Elements?[2]) || key?.SevTags != null)).
            Select(key => key?.SevTags != null ?
                   SeveralTags(key.SevTags) :
                   key?.Elements.ToList().Count > 3 ?
                   SeveralAttr(key.Elements) :
                   key.Elements.ToList().Count == 3 ?
                   new XElement(key.Elements[0],new XAttribute(key.Elements[1], key.Elements[2])) :
                   new XElement(key.Elements[0])));
            for (int i = 2; i <= NumOfGens; i++)
            {
                var AllTempNodeElements = TreeDict.Keys.ToList().
                                 Where(key => int.Parse(TreeDict[key].Generation) == i - 1).
                                 Select(key => AllStringArraySelect(key)).
                                 Select(array_of_array => array_of_array.
                                 Select(array => FindElement(array)).ToList()).
                                 ToList();

                AllTempNodeElements.ForEach(elementlist => elementlist.
                ForEach(element => element.Add(TreeDict.Keys.
                Where(key => int.Parse(TreeDict[key].Generation) == i &&
                             (!string.IsNullOrEmpty(key?.Elements?[2]) || key?.SevTags != null ) &&
                             element.Name == TreeDict[key]?.ParentName[0]).
                Select(key =>key?.SevTags != null ?
                       SeveralTags(key.SevTags) :
                       key?.Elements.ToList().Count > 3 ?
                       SeveralAttr(key.Elements) :
                       key.Elements.ToList().Count == 3 ?
                       new XElement(key.Elements[0], new XAttribute(key.Elements[1], key.Elements[2])) :
                       new XElement(key.Elements[0])))));
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
        private XElement SeveralTags(string[][] SevTags)
        {
            var Tags = new XElement("intitial");
            Tags.Add(SevTags.Select(sevtag => sevtag.Count() > 3  ?
                                                   SeveralAttr(sevtag) :
                                                   sevtag.Count() == 3 ?
                                                   new XElement(sevtag[0], new XAttribute(sevtag[1], sevtag[2])) :
                                                   new XElement(sevtag[0])));
            return Tags;
        }

        private string[][] AllStringArraySelect(XmlBranchName Key)
        {
            return Key.Elements != null ?
                new string[][] { Key.Elements } :
                Key.SevTags;
        }

        private XElement FindElement(string[] Info)
        {
            return NewTree.XPathSelectElement($"//{Info[0]}[@{Info[1]} = '{Info[2]}']");
        }

    }
}
