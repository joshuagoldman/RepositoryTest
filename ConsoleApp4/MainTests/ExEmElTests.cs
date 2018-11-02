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
                              new List<string> { "Population", "Value", "1.3e+06" },
                              ExEmEl.Replicate.No,
                              "a" }
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
            Assert.Fail();
        }

        [TestCase("C:/Users/jogo/Documents/git_Test/Countries.xml")]
        public void ExEmElTest(string FilePath)
        {
            //Arrange
            //act
            ExEmEl xml = new ExEmEl(FilePath,
                                    ExEmEl.NewDocument.No);
            //Assert
        }

        private static readonly object[] _Data =
        {
             new object[] {new List<string> { "Country", "Value", "Sweden" }, "test" }
        };

        [Test, TestCaseSource(nameof(_Data))]
        public void FindXLocationTest(List<string> Child)
        {
            //Arrange
            ExEmEl xml = new ExEmEl("C:/Users/jogo/Documents/git_Test/Countries.xml",
                                   ExEmEl.NewDocument.No);
            FindXLocation fxl = new FindXLocation
            {
                Doc = xml.doc,
                XDoc = XDocument.Load("C:/Users/jogo/Documents/git_Test/Countries.xml")
            };
            //act
            fxl.FindByElement(Child);
            //Assert
            Assert.AreEqual("Countries", fxl.ChildParentElement.Parent.Value.Contains("Countries"));
        }
    }
}