using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        public enum ReplicateOrNewTree { Repl, DontRepl, NewTree }

        private enum SearchFor { Element, Attribute }

        private List<string> Child { get; set; }

        private List<string> Node { get; set; }

        private ReplicateOrNewTree ReplicateChoice;

        private ReplicateOrNewTree WriteTreeChoice;

<<<<<<< HEAD
        private string ParentAboveChildnoAttr { get; set; }
=======
        private string NodeNoValue { get; set; }
>>>>>>> da3b8cb895f91fe487d926b137c07b20137dbe46

        public List<string> FromRoot { get; set; }

        public List<string> InfoToFind { get; set; }

        public void XmlSearchInfo(string child = null,
                                  string node = null,
                                  ReplicateOrNewTree replicatechoice = ReplicateOrNewTree.DontRepl,
                                  ReplicateOrNewTree writetreechoice = ReplicateOrNewTree.DontRepl,
<<<<<<< HEAD
                                  string parent_above_child_no_attr = null,
=======
                                  string nodenovalue = null,
>>>>>>> da3b8cb895f91fe487d926b137c07b20137dbe46
                                  string fromroot = null,
                                  string infotofind = null)        
        {
            Child= child?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            Node =  node?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            InfoToFind = infotofind?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            ReplicateChoice = replicatechoice;
            WriteTreeChoice = writetreechoice;
<<<<<<< HEAD
            ParentAboveChildnoAttr = parent_above_child_no_attr;
            FromRoot = fromroot?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
=======
            NodeNoValue = nodenovalue;
            var FromRootTemp = fromroot?.Trim() + ",All";
            FromRoot = FromRootTemp?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
>>>>>>> da3b8cb895f91fe487d926b137c07b20137dbe46
            EnumConditions();
        }        
    }
}
