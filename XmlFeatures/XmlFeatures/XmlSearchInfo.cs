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
        public enum Replicate { Yes, No }

        private enum SearchFor { Element, Attribute }

        private List<string> Child { get; set; }

        private List<string> Node { get; set; }

        private Replicate ReplicateChoice;

        private string NodeNoValue { get; set; }

        public List<string> FromRoot { get; set; }

        public List<string> InfoToFind { get; set; }

        public void XmlSearchInfo(string child = null,
                                  string node = null,
                                  Replicate replicatechoice = Replicate.No,
                                  string nodenovalue = null,
                                  string fromroot = null,
                                  string infotofind = null)        
        {
            Child= child?.Replace(' ','_')?.Split(',').ToList();
            Node =  node?.Replace(' ', '_')?.Split(',').ToList();
            InfoToFind = infotofind?.Replace(' ', '_')?.Split(',').ToList();
            ReplicateChoice = replicatechoice;
            NodeNoValue = nodenovalue;
            var FromRootTemp = fromroot + ",All";
            FromRoot = FromRootTemp?.Replace(' ', '_')?.Split(',').ToList();
            EnumConditions();
        }        
    }
}
