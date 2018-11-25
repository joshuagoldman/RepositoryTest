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

        public string[] Name { get; set; }

        public string[] ParentName { get; set; }

        public XmlBranchInfo(string generation,
                        string[] name,
                        string[] parentname)
        {
            Generation = generation;
            Name = name;
            ParentName = parentname;
        }
    }
}
