﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main.XmlDoc.Tests
{
    [TestFixture]
    public class ExEmElTests
    {
        static private object[] _sourceLists =
        {
                new object[] {"Animals",
                              "Animal,Elephant" ,
                              "Color,Grey",
                              ExEmEl.Replicate.No}
        };

        [Test, TestCaseSource("_sourceLists")]
        public void WriteNodeToXmlTest(string fromroot,
                                       string child,
                                       string node,
                                       ExEmEl.Replicate replicatechoice)
        {
            ExEmEl xml = new ExEmEl(
            @"C:\Users\DELL\Documents\GitRepoJosh\Data.xml",
            ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(fromroot: fromroot,
                              child: child,
                              node: node,
                              replicatechoice: replicatechoice);
            xml.WriteNodeToXml();
        }

        string Key { get; set; }

        [Test, TestCase("TestCases")]
        public void FindXLocationTest(string key)
        {
            Key = key;
            var Child = Testcases();
            //Arrange
            ExEmEl xml = new ExEmEl(@"C:\Users\DELL\Documents\GitRepoJosh\Countries.xml",
                                   ExEmEl.NewDocument.No);
            FindXLocation fxl = new FindXLocation
            {
                Doc = xml.doc,
                XDoc = XDocument.Load(@"C:\Users\DELL\Documents\GitRepoJosh\Countries.xml")
            };
            //act
            fxl.FindByElement(Child);
            //Assert
            Assert.AreEqual(true, fxl.ChildParentElement.Parent.Name.ToString().Contains("Countries"));
        }

        [Test,TestCase("GetXmlInfoTestCase")]
        public void GetXmlInfoTest(string key)
        {
            Key = key;
            var InfoToFind = GetXmlInfoTestCase();

            ExEmEl xml = new ExEmEl(
            @"C:\Users\DELL\Documents\GitRepoJosh\Data.xml",
            ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(infotofind: InfoToFind);
            var Result = xml.GetXmlInfo();

            Assert.AreEqual(Result, "Hummus");
        }

        public List<string> Testcases()
        {
            var TestCases = new List<string> { "Country", "Country", "Sweden" };
            return Key == "TestCases" ? TestCases : null;
        }

        public string GetXmlInfoTestCase()
        {
            var GetXmlInfoTestCase = "Country,Israel,National Food";
            return Key == "GetXmlInfoTestCase" ? GetXmlInfoTestCase : null;
        }
    }
}