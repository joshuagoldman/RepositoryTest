using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using XmlFeatures;

namespace WpfApp1.Methods
{
    public class CreateTreeDict
    {
        public TextBoxAppearance Information { get; set; }

        public CreateTreeDict()
        {

        }
        public Dictionary<String[], XmlBranchInfo> GetTreeDict()
        {
            return new Dictionary<string[], XmlBranchInfo>
            {
                {new string[] {"SearchKey", "Value", Information.SearchKey.Text },
                   new XmlBranchInfo("1") },

                {new string[]{ Information.InputDateWithIndexTextBoxObject.NameProp.Replace("TextBoxObject","").Replace("TextBoxObject","") , "Value", Information.InputDateWithIndexTextBoxObject.Text },
                   new XmlBranchInfo("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.CriteriaReferenceWithRevisionTextBoxObject.NameProp.Replace("TextBoxObject", "").Replace("TextBoxObject", ""), "Value", Information.CriteriaReferenceWithRevisionTextBoxObject.Text },
                   new XmlBranchInfo("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.ResponsibleTextBoxObject.NameProp.Replace("TextBoxObject", "").Replace("TextBoxObject", ""), "Value", Information.ResponsibleTextBoxObject.Text },
                   new XmlBranchInfo("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },

                {new string[]{ Information.ReasonTextBoxObject.NameProp.Replace("TextBoxObject", "").Replace("TextBoxObject", ""), "Value", Information.ReasonTextBoxObject.Text },
                   new XmlBranchInfo("2", new string[] {"SearchKey", "Value", Information.SearchKey.Text }) },
            };
        }
    }
}
