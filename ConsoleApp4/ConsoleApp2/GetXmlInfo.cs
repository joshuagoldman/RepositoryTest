using System;
using System.Collections.Generic;
using Main.XmlDoc;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        public string NodeValue { get; set; }

        private string GetXmlInfo()
        {            
            var Result = ExistanceCheck();
            if (NeitherExist(Result) || ChildDoesExist(Result))
            {
                throw new Exception("The node could not be found. Try a different searchkey");
            }
            else 
            {
                return "d";
            }            
        }        
    }
}
