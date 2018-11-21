using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        delegate bool ElementAncestorExistanceDelegate(XmlNode node); 

    private ElementExistance ExistanceCheck()
        {
            List<string> SearchKeys = new List<string>
            {
                string.IsNullOrEmpty(FromRoot[0]) ?
                null : String.Format("*//{0}[@Value = '{1}']", FromRoot[0], FromRoot[1]), 
                String.Format("*//{0}[@Value = '{1}']", Child[0], Child[1]),
                String.Format("//{0}", Node[0]) 
            };
            var xmlFromRoot = string.IsNullOrEmpty(FromRoot[0]) ? null : doc.SelectSingleNode(SearchKeys[0]);

            var xmlChild = doc.SelectSingleNode(SearchKeys[1]);

            var xmlNode = doc.SelectSingleNode(SearchKeys[2]);

            ElementAncestorExistanceDelegate NodeWSpecificAncestorsDoesExist = 
                (XmlNode node) => node != null && node.ParentNode == xmlChild && node.ParentNode.ParentNode == xmlFromRoot ? true : false;

            ElementAncestorExistanceDelegate ChildDoesExist = 
                (XmlNode node) => node != null ? true : false;

            ElementAncestorExistanceDelegate ChosenRootDoesExist = 
                (XmlNode node) => node != null ? true : false;

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
