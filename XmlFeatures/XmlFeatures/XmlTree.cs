using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XmlFeatures.XmlDoc
{

    public class XmlTree
    {
        public XDocument TreeXDoc;

        public FindXLocation TreeFind;

        public string FilePath { get; set; }

        public List<string> TreeRoot { get; set; }

        public Dictionary<string[],XmlBranchInfo> TreeDict { get; set; }

        public XmlTree()
        {
        }

        public void GetTree()
        {
            var NumOfGens = TreeDict.Keys.ToList().
                Select(key => int.Parse(TreeDict[key].Generation)).Max();
            var NewTree = new XElement("Initial");
                NewTree.Add(TreeDict.Keys.
                            Where(key => int.Parse(TreeDict[key].Generation) == 1 &&
                                  key[2] != "NotIncluded").
                Select(key =>key.ToList().Count == 3 ?
                       new XElement(key[0],new XAttribute(key[1], key[2])) :
                       new XElement(key[0])));
            for (int i = 2; i <= NumOfGens; i++)
            {
                var AllTempNodeElements = TreeDict.Keys.ToList().
                                 Where(key => int.Parse(TreeDict[key].Generation) == i - 1).
                                 Select(key => NewTree.XPathSelectElement($"//{key[0]}") ?? null).ToList();
                AllTempNodeElements.ForEach(element => element.Add(TreeDict.Keys.
                Where(key => int.Parse(TreeDict[key].Generation) == i &&
                             key[2] != "NotIncluded" &&
                             element.Name == TreeDict[key]?.ParentName[0]).
                Select(key => key.ToList().Count == 3 ?
                              new XElement(key[0], new XAttribute(key[1], key[2])) :
                              new XElement(key[0]))));
            }
            TreeFind.FindByElement(TreeRoot);
            TreeFind.ChildParentElement.Add(NewTree.Nodes());
            TreeXDoc.Save(FilePath);
        }
    }
}
