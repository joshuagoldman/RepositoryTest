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
                {new string[]{ Information.InputDateWithIndexObject.NameProp, "Value", Information.InputDateWithIndexObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.CriteriaReferenceWithRevisionViewsettingsObject.NameProp, "Value", Information.CriteriaReferenceWithRevisionViewsettingsObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.ResponsibleViewsettingsObject.NameProp, "Value", Information.ResponsibleViewsettingsObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.ReasonViewsettingsObject.NameProp, "Value", Information.ReasonViewsettingsObject.Text },
                   new TreeDict("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },
            };
        }
    }
}
