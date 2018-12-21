using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchKey_GUI.Models
{
    public class TreeDict
    {
        public string Generation { get; set; }

        public string[] ParentName { get; set; }

        public Dictionary<string[], string[]> SevTags { get; set; }

        public TreeDict(string generation,
                        string[] parentname = null,
                        Dictionary<string[], string[]> sev_tags = null)
        {

        }
    }

}
