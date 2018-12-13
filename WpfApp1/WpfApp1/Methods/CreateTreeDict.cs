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
        public Dictionary<XmlBranchName,XmlBranchInfo> GetTreeDict()
        {
            var InfoBaseMain = new string[][][]
                {
                    new string[][]{
                                    new string[]{Information.SearchKey.NameProp, "Name", Information.SearchKey.Text },
                                    new string[]{ "1"}
                    },
                    new string[][]{
                                    new string[]{"Information" },
                                    new string[]{"2" },
                                    new string[]{ Information.SearchKey.NameProp, "Name", Information.SearchKey.Text }
                    },
                    new string[][]{
                                    new string[]{ Information.InputDateWithIndex.NameProp , "Value", Information.InputDateWithIndex.Text},
                                    new string[]{ "3"},
                                    new string[]{ "Information" }
                    },
                    new string[][]{
                                    new string[]{Information.CriteriaReferenceWithRevision.NameProp, "Value", Information.CriteriaReferenceWithRevision.Text },
                                    new string[]{ "3"},
                                    new string[]{ "Information" }
                    },
                    new string[][]{
                                    new string[]{Information.Responsible.NameProp, "Value", Information.Responsible.Text  },
                                    new string[]{"3" },
                                    new string[]{ "Information" }
                    },
                    new string[][]{
                                    new string[]{  Information.Reason.NameProp, "Value", Information.Reason.Text},
                                    new string[]{"3" },
                                    new string[]{ "Information" }
                    },
                    new string[][]{
                                    new string[]{Information.SearchKey.NameProp, "Name", Information.SearchKey.Text },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{Information.ExcludeTestTypes.NameProp, "Value", Information.ExcludeTestTypes.Text },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{Information.SerialNumber.NameProp, "Value", Information.SerialNumber.Text },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{ "Products"},
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{  Information.Expression.NameProp, "Equation", Information.Expression.Text },
                                    new string[]{"3"},
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{Information.SearchPathOption.NameProp, "Value", Information.SearchPathOption.Text },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{ Information.RegexOptions.NameProp, "Value", Information.RegexOptions.Text },
                                    new string[]{"3" },
                                    new string[]{"SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{Information.IncludeFiles.NameProp, "Value", Information.IncludeFiles.Text },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{ Information.ExcludeFiles.NameProp, "Value", Information.ExcludeFiles.Text},
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{ Information.ScreeningAllowed.NameProp, "Value", Information.ScreeningAllowed.Text},
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{ Information.IncludedInTest.NameProp, "Value", Information.IncludedInTest.Text },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{"Report" },
                                    new string[]{"2" },
                                    new string[]{ Information.SearchKey.NameProp, "Name", Information.SearchKey.Text }
                    },
                    new string[][]{
                                    new string[]{ Information.ReasonReport.NameProp, "Value", Information.ReasonReport.Text},
                                    new string[]{"3" },
                                    new string[]{ "Report" }
                    },
                    new string[][]{
                                    new string[]{  Information.InfotextScreening.NameProp, "Value", Information.InfotextScreening.Text },
                                    new string[]{"3" },
                                    new string[]{"Report" }
                    },
                    new string[][]{
                                    new string[]{Information.Infotext.NameProp, "Value", Information.Infotext.Text },
                                    new string[]{"3" },
                                    new string[]{ "Report" }
                    },
                    new string[][]{
                                    new string[]{Information.InfotextExtended.NameProp, "Value", Information.InfotextExtended.Text  },
                                    new string[]{"3" },
                                    new string[]{ "Report" }
                    },
                };


            var InfoBaseRepeatingNodes = new string[]
                {
                    string.Join("Variable",Information.Variable.Text),
                    string.Join("Product",Information.Product.Text),
                    CreateRepeatingTag("Filters", Information.Variable.Text),
                    string.Join("SearchFilesFilter",Information.SearchFilesFilter.Text)
                };

            var InfoBaseRepeatingNodesParents = new string[]
                {
                    string.Join(Information.Expression.NameProp,Information.Variable.Text),
                    CreateRepeatingTagNoAttr("Products",Information.Product.Text),
                    string.Join("Variable",Information.Variable.Text),
                    string.Join("Variable",Information.Variable.Text)
                };

            var InfoBaseRepeatingNodesGenerations = "4,4,5,6";

            var InfoBaseAll = RepeatedTagInfoUnits(InfoBaseRepeatingNodes, InfoBaseRepeatingNodesGenerations, InfoBaseRepeatingNodesParents);

            InfoBaseAll.ToList().Add(InfoBaseMain);

            return MakeDict(InfoBaseAll);
        }

        private string CreateRepeatingTagWAttr(string PacketName, string PacketAttr)
        {
            var PacketNodeNmeRep = CreateRepeatingTagNoAttr(PacketName, PacketAttr).Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
            var PacketAttrRep = PacketAttr.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
            var BothStringArrays = PacketNodeNmeRep.Zip(PacketAttrRep, (x, y) => new { node_name = x, node_attr = y });
            BothStringArrays

        }

        private string CreateRepeatingTagNoAttr(string Str, string CountStr)
        {
            return string.Concat(Enumerable.Repeat(Str + "\n", MakeReapeatedTag(CountStr)[0].Count - 1)) + Str;

        }

        private List<List<string>> MakeReapeatedTag(string TextInfo)
        {

            var variableRows = TextInfo.Split(new string[] { "\n" }, StringSplitOptions.None).ToArray();

            var Node = variableRows.ToList().
                Select(row => row.Split(new string[] { "NEXT" }, StringSplitOptions.None).
                Select(attr_part => attr_part.Trim()).ToList()).ToList();

            return Node;

        }

        private string[][][][] RepeatedTagInfoUnits(string[] NodeUnitsInfo, string Gen, string[] ParentUnitsInfo)
        {
            var NodeUnits = NodeUnitsInfo.ToList().Select(node_unit_info => MakeReapeatedTag(node_unit_info)).ToList();

            var ParentUnits = ParentUnitsInfo.ToList().Select(parent_unit_info => MakeReapeatedTag(parent_unit_info)).ToList();

            var NodesNParentsUnits = NodeUnits.Zip(ParentUnits.ToList(), (x, y) => new { node_packets = x, parent_packets = y });

            var AllGens = Gen.Split(new string[] { "," }, StringSplitOptions.None).
                Select(gen => gen.Trim());

            var InfoUnits= new List<List<List<List<string>>>>();

            AllGens.ToList().ForEach(gen => NodesNParentsUnits.ToList().
                ForEach(node_n_parent_Unit =>
                {
                    var NodeNParentPackets = node_n_parent_Unit.node_packets.Zip(node_n_parent_Unit.parent_packets, (x, y) => new { node_packet = x, parent_packet = y });

                    var TempUnit = new List<List<List<string>>>();

                    NodeNParentPackets.ToList().ForEach(node_n_parent_packet =>
                                                {
                                                    TempUnit.Add(new List<List<string>>
                                                    {
                                                        node_n_parent_packet.node_packet,
                                                        new List<string> {gen },
                                                        node_n_parent_packet.parent_packet

                                                    });

                                                });

                    InfoUnits.Add(TempUnit);
                }
                ));

            return InfoUnits.Select(info_unit => info_unit.Select(info_packet => info_packet.Select(info_list => info_list.ToArray()).ToArray()).ToArray()).ToArray();
        }

        private Dictionary<XmlBranchName, XmlBranchInfo> MakeDict(params string[][][][] InfoPackageUnits)
        {
            var TreeDict = new Dictionary<XmlBranchName, XmlBranchInfo>();
            InfoPackageUnits.ToList().
                ForEach(info_package_unit => info_package_unit.ToList().
                    ForEach(info_package =>
                    {
                        TreeDict.Add(new XmlBranchName(info_package[0]),
                                new XmlBranchInfo(info_package[1].ToString(), info_package[2]));
                    }));

            return TreeDict;

    }
    }
}
