using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlFeatures.XmlDoc
{

    public class XmlTree
    {
        public XDocument XDoc;

        public XmlDocument Doc;

        public FindXLocation find;

        public string FilePath { get; set; }

        public XmlTree()
        {
        }

        public void GetTree(Dictionary<string,XmlBranchInfo> TreeDict,
                            List<string> TreeRoot)
        {
            var NumOfGens = TreeDict.Values.ToList().
                            Select(branch => Int32.Parse(branch.Generation)).Max();
            var NewTree = new XElement("Initial");
            NewTree.Add(TreeDict.Values.
                   Where(branch => Int32.Parse(branch.Generation) == 1).
                   Where(branch => branch.Name[2] != "NotIncluded").
                   Select(branch =>branch.Name.ToList().Count == 3 ?
                   new XElement(branch.Name[0],new XAttribute(branch.Name[1], branch.Name[2])) :
                   new XElement(branch.Name[0])));
            for (int i = 2; i <= NumOfGens; i++)
            {
                NewTree.Elements().ToList().
                Where(element => element.Name == TreeDict.Values.ToList().
                                                 Where(branch => Int32.Parse(branch.Generation) == i-1).
                                                 First().Name.ToString()).ToList().
                ForEach(element => element.Add(TreeDict.Values.
                                               Where(branch => Int32.Parse(branch.Generation) == i).
                                               Where(branch => branch.Name[2] != "NotIncluded").
                                               Where(branch => element.Name == branch.ParentName.ToString()).
                Select(branch => branch.Name.ToList().Count == 3 ?
                new XElement(branch.Name[0], new XAttribute(branch.Name[1], branch.Name[2])) :
                new XElement(branch.Name[0]))));
            }
            find.FindByElement(TreeRoot);
            XDoc.Element(find.ChildParentElement.Name).Add(NewTree);
            XDoc.Save(FilePath);
        }
    }
}
