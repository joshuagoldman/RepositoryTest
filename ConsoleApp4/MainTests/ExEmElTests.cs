using Main.XmlDoc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Main.XmlDoc.Tests
{
    [TestFixture]
    public class ExEmElTests
    {
        static private object[] _sourceLists =
        {
                new object[] {new List<string> { "Country", "Value", "Sweden" },
                              new List<string> { "Population", "1.3e+06" },
                              ExEmEl.Replicate.No,
                              ""}
        };

        [Test, TestCaseSource("_sourceLists")]
        public void WriteNodeToXmlTest(List<string> child,
                                       List<string> node,
                                       ExEmEl.Replicate replicatechoice,
                                       string NodeNoValue)
        {
            ExEmEl xml = new ExEmEl(
            @"C:\Users\DELL\Documents\GitRepoJosh\Countries.xml",
            ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(child, node, replicatechoice);
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
            @"C:\Users\DELL\Documents\GitRepoJosh\Countries.xml",
            ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(infotofind: InfoToFind);
            var Result = xml.GetXmlInfo();

            Assert.AreEqual(Result, "1.3e+06");
        }

        public List<string> Testcases()
        {
            var TestCases = new List<string> { "Country", "Value", "Sweden" };
            return Key == "TestCases" ? TestCases : null;
        }

        public string GetXmlInfoTestCase()
        {
            var GetXmlInfoTestCase = "Country,Sweden,Population";
            return Key == "GetXmlInfoTestCase" ? GetXmlInfoTestCase : null;
        }
    }
}