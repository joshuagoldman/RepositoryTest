using System.Collections.Generic;
using Ericsson.LogAnalyzer;
using Ericsson.LogAnalyzer.Models;
using NSubstitute;
using NUnit.Framework;
using System.IO;
using Ericsson.LogAnalyzer.Definitions;
using TestUtilities;
using Ericsson.LogAnalyzer.BLL;
using Ericsson.LogAnalyzer.Models.Entity;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class PerformLogSearchLogicTest
    {
        // R-states aren't equal
        public readonly static ChooseCase UnEqualRStateCase = new ChooseCase(rstate: "ee");
        // Invalid R-states are equal
        public readonly static ChooseCase EqualExcludedRStateCase = new ChooseCase(excludedrstate: "RSA", rstate: null);
        // Invalid R-states aren't equal
        public readonly static ChooseCase UnEqualExcludedRStateCase = new ChooseCase(excludedrstate: "ee", rstate: null);
        // Product numbers aren't equal
        public readonly static ChooseCase UnEqualProdNumCase = new ChooseCase(productnumber: "KDU 127 925/31");
        // Date isn't in range of product manufacturing date
        public readonly static ChooseCase DateNotInRangeCase = new ChooseCase(datefrom: "20170201", dateto: "20170301");
        // Date is in range of product manufacturing date
        public readonly static ChooseCase DateInRangeCase = new ChooseCase(datefrom: "20171201", dateto: "20180301");
        // Station type is not an included station type
        public readonly static ChooseCase UnEqualStationTypeCase = 
                               new ChooseCase(includetesttypes: new List<StationTestType> { StationTestType.CloudLat });
        // Excluded station types are equal
        public readonly static ChooseCase EqualExcludedStationTypeCase = 
                               new ChooseCase(excludetesttypes: new List<StationTestType> { StationTestType.RcPrtt });
        // Excluded station types aren't equal
        public readonly static ChooseCase UnEqualExcludedStationTypeCase = 
                               new ChooseCase(excludetesttypes: new List<StationTestType> { StationTestType.CloudLat });

        ChooseCase CurrentCase { get; set; }

        private string HwLogCriteria { get; } = Path.Combine(FilePaths.RepoRoot, @"Datapackets\HWLogCriteria\documents\HwLogCriteria.xml");

        [Test]
        [TestCase("UnEqualRStateCase", ExpectedResult = false)]
        [TestCase("EqualExcludedRStateCase", ExpectedResult = false)]
        [TestCase("UnEqualExcludedRStateCase", ExpectedResult = true)]
        [TestCase("UnEqualProdNumCase", ExpectedResult = false)]
        [TestCase("DateNotInRangeCase", ExpectedResult = false)]
        [TestCase("DateInRangeCase", ExpectedResult = true)]
        [TestCase("UnEqualStationTypeCase", ExpectedResult = false)]
        [TestCase("EqualExcludedStationTypeCase", ExpectedResult = false)]
        [TestCase("UnEqualExcludedStationTypeCase", ExpectedResult = true)]
        public bool ShallSearchBePerformedTest(string Key)
        {
            //Arrange
            CurrentCase = GetObject(Key);
            ProductModel currentProduct = GetProduct("KDU 137 925/31");

            LogSearchLogic logic = new LogSearchLogic(DatabaseVariantType.Xml,
                                                      HwLogCriteria,
                                                      GetCustomFunctions(currentProduct));

            var key = logic.GetSearchKeyByName("CRAMC_error", "Baseband");

            key.SearchSettings.IncludeTestTypes = CurrentCase.IncludeTestTypes;
            key.SearchSettings.ExcludeTestTypes = CurrentCase.ExcludeTestTypes;

            key.SearchSettings.Products = GetProductEntityList();
            //Act
            var Result = key.ShallSearchBePerformed(currentProduct, CurrentCase.TestType,
                                                    CurrentCase.ServiceLoction);
            //Assert
            return Result;
        }

        private static ChooseCase GetObject(string key)
        {
            return key == "UnEqualRStateCase" ? UnEqualRStateCase :
                   key == "EqualExcludedRStateCase" ? EqualExcludedRStateCase :
                   key == "UnEqualExcludedRStateCase" ? UnEqualExcludedRStateCase :
                   key == "UnEqualProdNumCase" ? UnEqualProdNumCase :
                   key == "DateNotInRangeCase" ? DateNotInRangeCase :
                   key == "DateInRangeCase" ? DateInRangeCase :
                   key == "UnEqualStationTypeCase" ? UnEqualStationTypeCase :
                   key == "EqualExcludedStationTypeCase" ? EqualExcludedStationTypeCase :
                   key == "UnEqualExcludedStationTypeCase" ? UnEqualExcludedStationTypeCase :
                   null;
        }

        CustomFunctions GetCustomFunctions(ProductModel product)
        {
            return new CustomFunctions(null, product, Substitute.For<ILogger>());
        }

        private static ProductModel GetProduct(string productNumber)
        {
            ProductModel currentProduct = Substitute.For<ProductModel>();            
            currentProduct.ProductNumber = "KDU 137 925/31";
            currentProduct.RState = "RSA";
            currentProduct.ProductDate = "20180101";
            return currentProduct;
        }

        private List<ProductEntity> GetProductEntityList()
        {

            var ProductToCompare = Substitute.For<ProductModel>();
            {
                ProductToCompare.SerialNumber = CurrentCase.Serialnumber;
                ProductToCompare.ProductNumber = CurrentCase.ProductNumber;
                ProductToCompare.RState = CurrentCase.RState;
                ProductToCompare.ExcludedRState = CurrentCase.ExcludedRState;
                ProductToCompare.ProductDate = CurrentCase.ProductDate;
                ProductToCompare.DateFrom = CurrentCase.DateFrom;
                ProductToCompare.DateTo = CurrentCase.DateTo;
            }
            var ProductToCompareList = new List<ProductEntity>()
            {
                ProductToCompare
            };

            return ProductToCompareList;
        }


    }

    public class ChooseCase
    {

        public StationTestType TestType { get; set; }

        public List<StationTestType> IncludeTestTypes { get; set; }

        public List<StationTestType> ExcludeTestTypes { get; set; }

        public string ServiceLoction { get; set; }

        public string Serialnumber { get; set; }

        public string ProductNumber { get; set; }

        public string RState { get; set; }

        public string ExcludedRState { get; set; }

        public string ProductDate { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public ChooseCase(StationTestType testtype = StationTestType.RcPrtt,
                          List<StationTestType> includetesttypes = null,
                          List<StationTestType> excludetesttypes = null,
                          string serviceLocation = null,
                          string serialnumber = "*",
                          string productnumber = "KDU 137 925/31",
                          string rstate = "RSA",
                          string excludedrstate = null,
                          string productdate = null,
                          string datefrom = null,
                          string dateto = null)
        {
            TestType = testtype;
            IncludeTestTypes = includetesttypes ?? new List<StationTestType> { StationTestType.RcPrtt };
            ExcludeTestTypes = excludetesttypes ?? new List<StationTestType> { StationTestType.Undefined };
            ServiceLoction = serviceLocation;
            Serialnumber = serialnumber;
            ProductNumber = productnumber;
            RState = rstate;
            ExcludedRState = excludedrstate;
            ProductDate = productdate;
            DateFrom = datefrom;
            DateTo = dateto;
        }
        ~ChooseCase()
        {

        }
    }
}