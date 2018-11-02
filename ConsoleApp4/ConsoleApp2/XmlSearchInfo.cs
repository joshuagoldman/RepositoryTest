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

        public void XmlSearchInfo(List<string> child,
                                  List<string> node = null,
                                  Replicate replicatechoice = Replicate.No,
                                  string nodenovalue = null,
                                  List<string> fromroot = null)        
        {
            Child= child;
            Node =  node;
            ReplicateChoice = replicatechoice;
            NodeNoValue = nodenovalue;
            FromRoot = fromroot;
            EnumConditions();
        }        
    }
}
