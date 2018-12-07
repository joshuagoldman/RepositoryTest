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
                {new XmlBranchName(new string[] {Information.SearchKey.NameProp, "Name", Information.SearchKey.Text }),
                   new XmlBranchInfo("1") },

                {new XmlBranchName(new string[] {"Information" }),
                   new XmlBranchInfo("2", new string[] {Information.SearchKey.NameProp, "Name", Information.SearchKey.Text }) },

                {new XmlBranchName(new string[]{ Information.InputDateWithIndex.NameProp , "Value", Information.InputDateWithIndex.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[]{ Information.CriteriaReferenceWithRevision.NameProp, "Value", Information.CriteriaReferenceWithRevision.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[]{ Information.Responsible.NameProp, "Value", Information.Responsible.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[]{ Information.Reason.NameProp, "Value", Information.Reason.Text }),
                   new XmlBranchInfo("3", new string[] {"Information" }) },

                {new XmlBranchName(new string[] {"SearchSettings" }),
                   new XmlBranchInfo("2",new string[] {Information.SearchKey.NameProp, "Name", Information.SearchKey.Text }) },

                {new XmlBranchName(new string[]{ Information.ExcludeTestTypes.NameProp, "Value", Information.ExcludeTestTypes.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.SerialNumber.NameProp, "Value", Information.SerialNumber.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[] {"Products" }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(sev_tags: MakeReapeatedTag(Information.Product.NameProp,
                                                        Information.Product.Text)),
                   new XmlBranchInfo("4", new string[] { "Products" }) },

                {new XmlBranchName(new string[]{ Information.Expression.NameProp, "Value", Information.Expression.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(sev_tags: MakeReapeatedTag(Information.Variable.NameProp,
                                                        Information.Variable.Text)),
                   new XmlBranchInfo("4", new string[]{ Information.Expression.NameProp, "Value", Information.Expression.Text }) },

                {new XmlBranchName(new string[]{ Information.SearchPathOption.NameProp, "Value", Information.SearchPathOption.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.RegexOptions.NameProp, "Value", Information.RegexOptions.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.IncludeFiles.NameProp, "Value", Information.IncludeFiles.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.ExcludeFiles.NameProp, "Value", Information.ExcludeFiles.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.ScreeningAllowed.NameProp, "Value", Information.ScreeningAllowed.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[]{ Information.IncludedInTest.NameProp, "Value", Information.IncludedInTest.Text }),
                   new XmlBranchInfo("3", new string[] {"SearchSettings" }) },

                {new XmlBranchName(new string[] {"Report" }),
                   new XmlBranchInfo("2", new string[] {Information.SearchKey.NameProp, "Name", Information.SearchKey.Text }) },

                {new XmlBranchName(new string[]{ Information.ReasonReport.NameProp, "Value", Information.ReasonReport.Text }),
                   new XmlBranchInfo("3", new string[] {"Report" }) },

                {new XmlBranchName(new string[]{ Information.InfoTextScreening.NameProp, "Value", Information.InfoTextScreening.Text }),
                   new XmlBranchInfo("3", new string[] {"Report" }) },

                {new XmlBranchName(new string[]{ Information.InfoText.NameProp, "Value", Information.InfoText.Text }),
                   new XmlBranchInfo("3", new string[] {"Report" }) },

                {new XmlBranchName(new string[]{ Information.InfoTextExtended.NameProp, "Value", Information.InfoTextExtended.Text }),
                   new XmlBranchInfo("3", new string[] {"Report" }) },
            };
        }

        private string[][] MakeReapeatedTag(string TagName, string TextInfo)
        {

            var variableRows = TextInfo.Split(new string[] { "\n" }, StringSplitOptions.None).ToArray();

            var AllAttr = variableRows.ToList().
                Select(row => row.Split(new string[] { "NEXT" }, StringSplitOptions.None).
                Select(attr_part => attr_part.Trim()));

            var ListWTagNAttr = new List<List<string>>();

            AllAttr.ToList().ForEach(row => ListWTagNAttr.Add(new List<string> { TagName }));

            var AllVarsMatrix = AllAttr.Zip(ListWTagNAttr, (x, y) => new { attr = x, tag = y });

            AllVarsMatrix.ToList().ForEach(row => row.tag.AddRange(row.attr.ToList()));

            var AllVars = AllVarsMatrix.Select(row => row.tag.ToArray()).ToArray();

            return AllVars;

        }
    }
}
