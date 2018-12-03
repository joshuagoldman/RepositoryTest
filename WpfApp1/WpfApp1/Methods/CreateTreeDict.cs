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
        public Controls Information { get; set; }

        public CreateTreeDict()
        {

        }
        public Dictionary<String[], XmlBranchInfo> GetTreeDict()
        {
            return new Dictionary<string[], XmlBranchInfo>
            {
                {new string[] {"SearchKey" },
                   new XmlBranchInfo("1") },

                {new string[] {"Information" },
                   new XmlBranchInfo("2", new string[] {"Information" }) },

                {new string[]{ Information.InputDateWithIndex.NameProp , "Value", Information.InputDateWithIndex.Text },
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new string[]{ Information.CriteriaReferenceWithRevision.NameProp, "Value", Information.CriteriaReferenceWithRevision.Text },
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new string[]{ Information.Responsible.NameProp, "Value", Information.Responsible.Text },
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new string[] {"SearchSettings" },
                   new XmlBranchInfo("2") },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text },
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new string[] {"Report" },
                   new XmlBranchInfo("1") },
            };
        }
    }
}
