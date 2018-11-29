using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class TreeDict
    {        
        public string Generation { get; set; }

        public string[] ParentName { get; set; }

        public TreeDict(string generation, 
                        string[] parentname = null)
        {

        }

    }
}
