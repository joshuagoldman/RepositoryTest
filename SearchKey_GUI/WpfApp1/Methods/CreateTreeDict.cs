﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchKey_GUI.Models;
using XmlFeatures;

namespace SearchKey_GUI.Methods
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
                                    new string[]{Information.IncludeServiceLocations.NameProp, "Value", Information.IncludeServiceLocations.Text  },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{Information.IncludeTestTypes.NameProp, "Value", Information.IncludeTestTypes.Text  },
                                    new string[]{"3" },
                                    new string[]{ "SearchSettings" }
                    },
                    new string[][]{
                                    new string[]{"SearchSettings" },
                                    new string[]{"2" },
                                    new string[]{ Information.SearchKey.NameProp, "Name", Information.SearchKey.Text }
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
                    CreateRepeatingElementWAttr("Variable", Information.Variable.Text),
                    CreateRepeatingElementWAttr("Product", Information.Product.Text),
                    CreateRepeatingElementWAttr("Filters", string.IsNullOrEmpty(Information.SearchFilesFilter.Text) ? "" :
                                                       CreateRepeatingElementWAttr("Variable", Information.Variable.Text)),
                    CreateRepeatingElementWAttr("SearchFilesFilter", Information.SearchFilesFilter.Text)
                };

            var InfoBaseRepeatingNodesParents = new string[]
                {
                    CreateRepeatingElementNoAttr(Information.Expression.NameProp + "NEXTEquationNEXT" + Information.Expression.Text, Information.Variable.Text),
                    CreateRepeatingElementNoAttr("Products", Information.Product.Text),
                    CreateRepeatingElementWAttr("Variable", Information.Variable.Text),
                    CreateRepeatingElementWAttr("Filters", CreateRepeatingElementWAttr("Variable", Information.Variable.Text))
                };

            var InfoBaseRepeatingNodesGenerations = "4,4,5,6";

            var InfoBaseAll = RepeatedTagInfoUnits(InfoBaseRepeatingNodes, InfoBaseRepeatingNodesGenerations, InfoBaseRepeatingNodesParents).ToList();

            InfoBaseAll.Add(InfoBaseMain);

            return MakeDict(InfoBaseAll.ToArray());
        }

        private string RemoveNodeWNoAttr(string UnitStr)
        {
            var StrPackets = MakeReapeatedTag(UnitStr);

            StrPackets.ForEach(row => { if (row.Count == 1) row[0] = ""; });
            return string.Join(",", string.Join(",", StrPackets).Replace(",", "")).Replace(",", "");

        }

        private string CreateRepeatingElementWAttr(string PacketName, string PacketAttr)
        {
            var PacketsAttributes = PacketAttr.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
            var BothStringsPackets = new List<string>();
            PacketsAttributes.ToList().ForEach(packet_attributes => BothStringsPackets.Add(PacketName + "NEXT" + packet_attributes + "\n"));
            BothStringsPackets[BothStringsPackets.Count - 1] = BothStringsPackets[BothStringsPackets.Count - 1].Replace("\n", ""); 
            return string.Join(",", BothStringsPackets.ToArray()).Replace(",","");
        }

        private string CreateRepeatingElementNoAttr(string Str, string CountStr)
        {
            return string.Concat(Enumerable.Repeat(Str + "\n", MakeReapeatedTag(CountStr).Count - 1)) + Str;
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

            var GensNNodesNParentsUnits = AllGens.Zip(NodesNParentsUnits.ToList(), (x, y) => new { gen = x, node_n_parent_unit = y });

            GensNNodesNParentsUnits.ToList().ForEach(gens_n_nodes_n_parents_unit =>
                {
                    var NodeNParentPackets = 
                    gens_n_nodes_n_parents_unit.node_n_parent_unit.node_packets.
                    Zip(gens_n_nodes_n_parents_unit.node_n_parent_unit.parent_packets, (x, y) => new { node_packet = x, parent_packet = y });

                    var TempUnit = new List<List<List<string>>>();

                    NodeNParentPackets.ToList().ForEach(node_n_parent_packet =>
                                                {
                                                    TempUnit.Add(new List<List<string>>
                                                    {
                                                        node_n_parent_packet.node_packet,
                                                        new List<string> {gens_n_nodes_n_parents_unit.gen },
                                                        node_n_parent_packet.parent_packet

                                                    });
                                                });

                    InfoUnits.Add(TempUnit);
                }
                );

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
                                     info_package.Count() < 3 ?
                                     new XmlBranchInfo(info_package[1][0]) :
                                     new XmlBranchInfo(info_package[1][0], info_package[2]));
                    }));

            return TreeDict;

        }
    }
}
