using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlFeatures.XmlDoc
{
    public partial class ExEmEl
    {
        public enum InstantiateXDocument { Yes, No }

        public FindXLocation Find { get; set; }

        public enum Instantiate { XmlTree, FindXElement, None, Both }

        public XmlTree TreeCreation { get; set; }

        public enum ReplicateOrNewTree { Repl, DontRepl, NewTree }

        private enum SearchFor { Element, Attribute }

        private List<string> Child { get; set; }

        private List<string> Node { get; set; }

        private ReplicateOrNewTree ReplicateChoice;

        private ReplicateOrNewTree WriteTreeChoice;

        private string ParentAboveChildnoAttr { get; set; }

        public List<string> FromRoot { get; set; }

        public List<string> TreeRoot { get; set; }

        public List<string> InfoToFind { get; set; }

        public Dictionary<string[], XmlBranchInfo> TreeDict { get; set; }

        public void XmlSearchInfo(string child = null,
                                  string node = null,
                                  ReplicateOrNewTree replicatechoice = ReplicateOrNewTree.DontRepl,
                                  ReplicateOrNewTree writetreechoice = ReplicateOrNewTree.DontRepl,
                                  string parent_above_child_no_attr = null,
                                  string nodenovalue = null,
                                  string fromroot = null,
                                  string infotofind = null,
                                  string tree_root = null,
                                  Dictionary<string[],XmlBranchInfo> tree_dict = null,
                                  Instantiate instantiate_choice = Instantiate.None)        
        {
            Child= child?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            Node =  node?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            InfoToFind = infotofind?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            ReplicateChoice = replicatechoice;
            WriteTreeChoice = writetreechoice;
            ParentAboveChildnoAttr = parent_above_child_no_attr;
            FromRoot = fromroot?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            FromRoot = fromroot?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            TreeRoot = tree_root?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            TreeDict = tree_dict;
            TreeCreation = instantiate_choice == Instantiate.XmlTree ||
                   instantiate_choice == Instantiate.Both ?
                   new XmlTree()
                   {
                       TreeDict = TreeDict,
                       TreeRoot = TreeRoot
                   } : null;
            Find = instantiate_choice == Instantiate.FindXElement ||
                   instantiate_choice == Instantiate.Both ?
                   new FindXLocation()
                   {                       
                       XDoc = XDoc,
                       ParentAboveChildNoAttr = parent_above_child_no_attr ?? null
                   } : null;

            if (Node != null) { EnumConditions(); }

        }        
    }
}
