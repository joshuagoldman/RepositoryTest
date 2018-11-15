using System.Collections.Generic;
using Ericsson.LogAnalyzer;
using Ericsson.LogAnalyzer.Models;
using NSubstitute;
using NUnit.Framework;
using System.IO;
using Ericsson.LogAnalyzer.Definitions;
using TestUtilities;
using Ericsson.LogAnalyzer.BLL;
using System.Linq;
using System;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class RunSearchTest
    {
        public readonly static TestModel TestCase = new TestModel(ProductNumber: "KRC 118*",
                                                                  searchkeyname: "Linearization_problems, 2/-7, Rev C",
                                                                  RState: "*",
                                                                  SerialNumber: "*",
                                                                  hitsexpected: "1",
                                                                  // STRING OCCURRENCE PATTERN:
                                                                  // string_1, string_2, ..., String_N <NEXTFILE> string_1, string_2, ..., string_N
                                                                  stringoccurrence: "0,0,0,0,0,0,0,0 <NEXTFILE> 0,0,0,0,0,0,0,0",                                                                                                                                    
                                                                  searchkeygroup: "Radio");

        TestModel CurrentTestModel { get; set; }

        LogSearchLogic Logic { get; set; }

        private string HwLogCriteria { get; } = Path.Combine(FilePaths.RepoRoot,
                                                @"Datapackets\HWLogCriteria\documents\HwLogCriteria.xml");

        private string LogPath { get; } = Path.Combine(FilePaths.RepoRoot,
                                          @"CXP9015115-RBS6000\Test\LogAnalyzerTest\LogFiles");

        List<LogSearchExpressionVariableModel> Variables { get; set; }

        readonly string HitsMessage = "Expected number of hits does not match the actual";

        /// <summary>
        /// A custom number and types of strings, corresponding to a particular searchcase,
        /// are written within created corresponding logflies, and expected result is tested. 
        /// </summary>
        [Test]
        [TestCase()]
        public void RunSearchSTest()
        {
            //Arrange            

            CurrentTestModel = TestCase;
            Logic = new LogSearchLogic(DatabaseVariantType.Xml,
                                       HwLogCriteria,
                                       GetCustomFunctions(CurrentTestModel.CurrentProduct));
            var ExpectedSearchKeyHits = Int32.Parse(CurrentTestModel.HitsExpected);
            GetLogFilesAndStrings();
            WriteStringsToFile();
            //Act
            var HitsActual = GetActualHits();
            CurrentTestModel.LogFiles.ForEach(x => File.Delete(LogPath + "\\" + x));            
            //Assert
            Assert.AreEqual(ExpectedSearchKeyHits, HitsActual,  HitsMessage);
        }

        CustomFunctions GetCustomFunctions(ProductModel product)
        {
            return new CustomFunctions(null, product, Substitute.For<ILogger>());
        }

        private void GetLogFilesAndStrings()
        {
            var ResultModel = Logic.RunSearch(LogPath,
                    CurrentTestModel.CurrentProduct,
                    CurrentTestModel.SearchKeyGroup,
                    CurrentTestModel.TestType,
                    CurrentTestModel.ServiceLocation);

            CurrentTestModel.LogFiles = ResultModel.LogSearchKeys.
                Where(x => x.Name == CurrentTestModel.SearchKeyName).First().SearchSettings.IncludedFiles;
            var AllStrings = ResultModel.LogSearchKeys.
                Where(x => x.Name == CurrentTestModel.SearchKeyName).First().SearchSettings.Expression.Variables.Select(x => x.Value).ToList();
            CurrentTestModel.AllStrings = AllStrings;
        }

        private void WriteStringsToFile()
        {
            var AllStrings = CurrentTestModel.AllStrings;
            var LogFiles = CurrentTestModel.LogFiles;
            var OccurencesAllFiles = CurrentTestModel.StringOccurrence.Split(new string[] { " <NEXTFILE> " }, StringSplitOptions.None);
            var col = LogFiles.Zip(OccurencesAllFiles, (x, y) => new { LogFile = x, OccurrencesString = y });
            foreach (var Entry in col)
            {
                if (!File.Exists(LogPath + "\\" + Entry.LogFile))
                {
                    var CurrentFile = File.CreateText(LogPath + "\\" + Entry.LogFile);
                    CurrentFile.Close();
                }                
                StreamWriter outputFile = new StreamWriter(Path.Combine(LogPath, Entry.LogFile),true);
                var Occurrences = Entry.OccurrencesString.Split(',').Select(x => Int32.Parse(x)).ToList();
                var col2 = AllStrings.Zip(Occurrences, (x, y) => new { Line = x, Occurrence = y });
                col2.Where(x => x.Occurrence != 0).ToList().
                     ForEach(x => outputFile.WriteLine(string.Concat(Enumerable.Repeat($"{x.Line}\r\n", x.Occurrence))));
                outputFile.Close();
            }
        }
        private int GetActualHits()
        { 
            var ResultModel = Logic.RunSearch(LogPath,
                                CurrentTestModel.CurrentProduct,
                                CurrentTestModel.SearchKeyGroup,
                                CurrentTestModel.TestType,
                                CurrentTestModel.ServiceLocation);
            int Hits = ResultModel.LogSearchKeysHits.Where(x => x.Name == CurrentTestModel.SearchKeyName).Count() > 0 ?
                       ResultModel.LogSearchKeysHits.Count : 0;
            return Hits;
        }
    }

    public class TestModel
    {
        public ProductModel CurrentProduct { get; set; }

        public string SearchKeyGroup { get; set; }

        public string SearchKeyName { get; set; }

        public string StringOccurrence { get; set; }

        public List<string> LogFiles { get; set; }

        public List<string> AllStrings { get; set; }

        public string HitsExpected { get; set; }

        public string ServiceLocation { get; set; }

        public StationTestType TestType { get; set; }

        public TestModel(string ProductNumber,
                         string searchkeygroup,
                         string searchkeyname,
                         string stringoccurrence,                         
                         string RState = null,
                         string hitsexpected = null,
                         string SerialNumber = null,
                         string DateFrom = null,
                         string DateTo = null,
                         string ExcludedRState = null,
                         string ProductDate = null,
                         string servicelocation = null,
                         StationTestType testtype = StationTestType.All)
        {
            CurrentProduct = Substitute.For<ProductModel>();
            CurrentProduct.ProductNumber = ProductNumber;
            CurrentProduct.RState = RState;
            CurrentProduct.SerialNumber = SerialNumber;
            CurrentProduct.DateFrom = DateFrom;
            CurrentProduct.DateTo = DateTo;
            CurrentProduct.ProductDate = ProductDate;
            CurrentProduct.ExcludedRState = ExcludedRState;            
            SearchKeyGroup = searchkeygroup;
            SearchKeyName = searchkeyname;
            StringOccurrence = stringoccurrence;
            HitsExpected = hitsexpected;
            TestType = testtype;
            ServiceLocation = servicelocation;
        }
        ~TestModel()
        {

        }
    }
}
