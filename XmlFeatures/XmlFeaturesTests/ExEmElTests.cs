﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlFeatures.XmlDoc.Tests
{
    [TestFixture]
    public class ExEmElTests
    {

        private static readonly TestModel TestCases = new TestModel(root: "Fruits",
                                                                    child: "Fruit,Value, Avocado",
                                                                    node: "Lacks,  Carbs",
                                                                    rep_or_new_tree: ExEmEl.ReplicateOrNewTree.DontRepl);

        private static readonly TestModel GetXmlInfoTestCase = new TestModel(info_to_find: "Fruit, Avocado");

        private static readonly TestModel TreeTest = new TestModel();

        string Key { get; set; }

        TestModel CurrentModel { get; set; }

        [Test, TestCase("TestCases")]
        public void WriteNodeToXmlTest(string key)
        {
            Key = key;
            Testcases();
            ExEmEl xml = new ExEmEl(
            @"C:\Users\DELL\Documents\GitRepoJosh\Data.xml",
            ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(fromroot: CurrentModel.Root,
                              child: CurrentModel.Child,
                              node: CurrentModel.Node,
                              replicatechoice: CurrentModel.RepOrNewTree,
                              writetreechoice: ExEmEl.ReplicateOrNewTree.NewTree,
                              instantiate_choice: ExEmEl.Instantiate.Both);
            xml.WriteNodeToXml();
        }


        [Test, TestCase("TestCases")]
        public void FindXLocationTest(string key)
        {
            Key = key;
            Testcases();
            //Arrange
            ExEmEl xml = new ExEmEl(@"C:\Users\DELL\Documents\GitRepoJosh\Countries.xml",
                                   ExEmEl.NewDocument.No);
            FindXLocation fxl = new FindXLocation
            {
                XDoc = XDocument.Load(@"C:\Users\DELL\Documents\GitRepoJosh\Countries.xml")
            };

            var ChildList = CurrentModel.Child.Split(',').ToList();
            //act
            fxl.FindByElement(ChildList);
            //Assert
            Assert.AreEqual(true, fxl.ChildParentElement.Parent.Name.ToString().Contains("Countries"));
        }

        [Test,TestCase("GetXmlInfoTestCase")]
        public void GetXmlInfoTest(string key)
        {
            Key = key;
            Testcases();

            ExEmEl xml = new ExEmEl(
            @"C:\Users\DELL\Documents\GitRepoJosh\Data.xml",
            ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(infotofind: CurrentModel.InfoToFind);
            var Result = xml.GetXmlInfo();

            Assert.AreEqual(Result, "Hummus");
        }

        [Test, TestCase("TreeTest")]
        public void XmlTreeTest(string key)
        {
            Key = key;
            Testcases();

            ExEmEl xml = new ExEmEl(
            @"C:\Users\DELL\Documents\GitRepoJosh\Data.xml",
            ExEmEl.NewDocument.No);

            var Dict = new Dictionary<string[], XmlBranchInfo>();
            {
                Dict.Add("a11,a11,a11".Split(',').ToArray(), new XmlBranchInfo("1"));
                Dict.Add("a12,a12,a12".Split(',').ToArray(), new XmlBranchInfo("1"));
                Dict.Add("a21,a21,a21".Split(',').ToArray(), new XmlBranchInfo("2", 
                    "a11,a11,a11".Split(',').ToArray() ));
                Dict.Add("a22,a22,a22".Split(',').ToArray(), new XmlBranchInfo("2", 
                    "a12,a12,a12".Split(',').ToArray()));
                Dict.Add("a31,a31,a31".Split(',').ToArray(), new XmlBranchInfo("3",
                    "a21,a21,a21".Split(',').ToArray()));
            }

            xml.XmlSearchInfo(tree_dict: Dict,
                              tree_root: "Document",
                              parent_above_child_no_attr: "SearchKeys",
                              instantiate_choice: ExEmEl.Instantiate.Both);
            var tree = new XmlTree()
            {
                TreeXDoc = xml.XDoc,
                TreeFind = xml.Find,
                TreeDict = xml.TreeDict,
                TreeRoot = xml.TreeRoot,
                FilePath = xml.FilePath
            };

            tree.GetTree();
        }

        public void Testcases()
        {
            CurrentModel = Key == "TestCases" ? TestCases :
                                     Key == "GetXmlInfoTestCase" ? GetXmlInfoTestCase : 
                                                                   TreeTest;
        }

        public class TestModel
        {
            public ExEmEl.ReplicateOrNewTree RepOrNewTree { get; set; }

            public string Root { get; set; }

            public string Child { get; set; }

            public string Node { get; set; }

            public string InfoToFind { get; set; }

            public TestModel(string root = null,
                             string child = null,
                             string node = null,
                             string info_to_find = null,
                             ExEmEl.ReplicateOrNewTree rep_or_new_tree = ExEmEl.ReplicateOrNewTree.DontRepl)
            {
                Root = root;
                Child = child;
                Node = node;
                InfoToFind = info_to_find;
                RepOrNewTree = rep_or_new_tree;
            }
        }
    }
}