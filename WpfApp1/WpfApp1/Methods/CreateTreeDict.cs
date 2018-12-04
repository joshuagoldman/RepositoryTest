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
        public Dictionary<XmlBranchName, XmlBranchInfo> GetTreeDict()
        {

            return new Dictionary<XmlBranchName, XmlBranchInfo>
            {
                {new XmlBranchName(new string[] {"SearchKey" }),
                   new XmlBranchInfo("1") },

                {new XmlBranchName(new string[] {"Information" }),
                   new XmlBranchInfo("2", new string[] {"Information" }) },

                {new XmlBranchName(new string[]{ Information.InputDateWithIndex.NameProp , "Value", Information.InputDateWithIndex.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[]{ Information.CriteriaReferenceWithRevision.NameProp, "Value", Information.CriteriaReferenceWithRevision.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[]{ Information.Responsible.NameProp, "Value", Information.Responsible.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[] {"SearchSettings" }),
                   new XmlBranchInfo("2") },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(sev_tags: MakeReapeatedTagDictionary(Information.Variables.NameProp,
                                                        Information.Variables.Text)),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[] {"Report" }),
                   new XmlBranchInfo("1") },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },
            };
        }

        private string[][] MakeReapeatedTagDictionary(string TagName, string TextInfo)
        {

            var variableRows = TextInfo.Split('\n', '\t').ToArray();

            var AllVars = variableRows.ToList().Select(row => row.Trim().Split(' ')).ToArray();

            return AllVars;

        }
    }
}
