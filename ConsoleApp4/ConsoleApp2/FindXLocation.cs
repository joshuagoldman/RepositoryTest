using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.XmlDoc
{
    public class FindXLocation
    {
        public XElement ChildParentElement { get; set; }

        public XDocument XDoc;

        public XmlDocument Doc;

        List<string> RootSearchKeyList;

        public FindXLocation()
        {

        }

        public void FindByElement(List<string> Child)
        {
            List<XmlNode> TempList = new List<XmlNode>
            {
                Doc.SelectSingleNode($"*//{Child[0]}[@{Child[1]} = '{Child[2]}']/..")
            };
            RootSearchKeyList = new List<string>
            {
                TempList[0].Name
            };
            int i = 0;
            while (i < 10)
            {
                TempList.Add(TempList[i]?.ParentNode);
                if (TempList[i +1].Name == "#document")
                {
                    break;
                }
                RootSearchKeyList.Add(TempList[i + 1].Name);
                i++;
            }
            ChildParentElement = XDoc.Root;
            int Len = RootSearchKeyList.Count() - 1;
            for (int j = 0; j < Len; j++)
            {
                ChildParentElement = ChildParentElement.Element(RootSearchKeyList[Len - (j+1)]);
            }
            ChildParentElement = ChildParentElement.Element(Child[0]);
        }
    }
}
