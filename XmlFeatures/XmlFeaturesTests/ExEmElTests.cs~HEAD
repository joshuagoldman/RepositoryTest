using NUnit.Framework;
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
                new object[] {"Country, Country, Sweden",
                              "Language,  Swedish",
                               ExEmEl.ReplicateOrNewTree.DontRepl}
        };

        [Test, TestCaseSource("_sourceLists")]
        public void WriteNodeToXmlTest(string child,
                                       string node,
                                       ExEmEl.ReplicateOrNewTree replicatechoice)
        {
            ExEmEl xml = new ExEmEl(
            @"C:\Users\jogo\Documents\git_Test\Data.xml",
            ExEmEl.NewDocument.No);
            xml.XmlSearchInfo(fromroot: "Countries,Value,All",
                              parent_above_child_no_attr: "Extra",
                              child: child,
                              node: node,
                              replicatechoice: replicatechoice,
                              writetreechoice: ExEmEl.ReplicateOrNewTree.NewTree);
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