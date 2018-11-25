using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace XmlFeatures.XmlDoc
{
    public partial class ExEmEl
    {
        delegate bool ElementAncestorExistanceDelegate(XElement node); 

    private ElementExistance ExistanceCheck()
        {
            List<string> SearchKeys = new List<string>
            {
                FromRoot == null || FromRoot.Count !=3 ?
                null : String.Format("*//{0}[@{1} = '{2}']", FromRoot[0], FromRoot[1], FromRoot[2]), 
                String.Format("*//{0}[@{1} = '{2}']", Child[0], Child[1], Child[2]),
                String.Format("//{0}", Node[0]) 
            };
            var xmlFromRoot = FromRoot  == null ? null :
                FromRoot.Count != 3 ?
                XDoc.XPathSelectElement(String.Format("//{0}", FromRoot[0])) :
                XDoc.XPathSelectElement(SearchKeys[0]);

            var xmlChild = XDoc.XPathSelectElement(SearchKeys[1]) ?? null;

            var xmlNode = XDoc.XPathSelectElement(SearchKeys[2]) ?? null;

            ElementAncestorExistanceDelegate NodeWSpecificAncestorsDoesExist = 
                (XElement node) => node != null && node.Parent == xmlChild ? true : false;

            ElementAncestorExistanceDelegate ChildDoesExist = 
                (XElement node) => node != null ? true : false;

            ElementAncestorExistanceDelegate ChosenRootDoesExist = 
                (XElement node) => node != null ? true : false;

            ElementExistance Result = NodeWSpecificAncestorsDoesExist(xmlNode) ?
                                      ElementExistance.NodeExists :
                                      ChildDoesExist(xmlChild) ?
                                      ElementExistance.ChildExists :
                                      ChosenRootDoesExist(xmlFromRoot) ?
                                      ElementExistance.RootExists :
                                      ElementExistance.NoneExist;                                      
            return Result; 
        }
    }
}
