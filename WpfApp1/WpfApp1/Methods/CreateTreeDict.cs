using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Methods
{
    public class CreateTreeDict
    {
        public TextBoxAppearance Information { get; set; }

        public CreateTreeDict()
        {

        }
        public Dictionary<String[], TreeDict> GetTreeDict()
        {
            return new Dictionary<string[], TreeDict>
            {
                {new string[]{ Information.InputDateWithIndexTextBoxObject.NameProp, "Value", Information.InputDateWithIndexTextBoxObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.CriteriaReferenceWithRevisionTextBoxObject.NameProp, "Value", Information.CriteriaReferenceWithRevisionTextBoxObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.ResponsibleTextBoxsObject.NameProp, "Value", Information.ResponsibleTextBoxsObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.ReasonTextBoxObject.NameProp, "Value", Information.ReasonTextBoxObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },
            };
        }
    }
}
