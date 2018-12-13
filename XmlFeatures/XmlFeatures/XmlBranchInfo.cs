using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFeatures
{
    public class XmlBranchInfo
    {
        public string Generation { get; set; }

        public string[] ParentName { get; set; }

        public XmlBranchInfo(string generation,
                            string[] parentname = null)              
        {
            Generation = generation;
            ParentName = parentname;
        }
    }

    public class XmlBranchName
    {
        public string[] Node { get; set; }

        public XmlBranchName(params string[] node)
        {
            Node = node;
        }
    }
}

