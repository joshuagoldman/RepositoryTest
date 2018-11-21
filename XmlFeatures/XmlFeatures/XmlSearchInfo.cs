﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main.XmlDoc
{
    public partial class ExEmEl
    {
        public enum ReplicateOrNewTree { Repl, DontRepl, NewTree }

        private enum SearchFor { Element, Attribute }

        private List<string> Child { get; set; }

        private List<string> Node { get; set; }

        private ReplicateOrNewTree ReplicateChoice;

        private ReplicateOrNewTree WriteTreeChoice;

        private string NodeNoValue { get; set; }

        public List<string> FromRoot { get; set; }

        public List<string> InfoToFind { get; set; }

        public void XmlSearchInfo(string child = null,
                                  string node = null,
                                  ReplicateOrNewTree replicatechoice = ReplicateOrNewTree.DontRepl,
                                  ReplicateOrNewTree writetreechoice = ReplicateOrNewTree.DontRepl,
                                  string nodenovalue = null,
                                  string fromroot = null,
                                  string infotofind = null)        
        {
            Child= child?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            Node =  node?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            InfoToFind = infotofind?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            ReplicateChoice = replicatechoice;
            WriteTreeChoice = writetreechoice;
            NodeNoValue = nodenovalue;
            var FromRootTemp = fromroot?.Trim() + ",All";
            FromRoot = FromRootTemp?.Split(',').Select(x => x.Trim().Replace(' ', '_')).ToList();
            EnumConditions();
        }        
    }
}
