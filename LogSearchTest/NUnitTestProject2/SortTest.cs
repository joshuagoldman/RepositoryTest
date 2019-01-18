using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;


namespace LogSearchTest.Tests
{
    [TestFixture]
    class SortTest
    {
        [Test]
        [TestCase()]
        public void LogSearchTest()
        {
            var Array = new string[]
                {
                "ael",
                "gkjlfj",
                "rie",
                "asdljf",
                "rsölglk",
                "dfjk",
                "tkjg"
                };

            var SortedArr = Array.QuickSortString().ToList();

            Assert.IsTrue(SortedArr.TakeWhile(el => SortedArr.IndexOf(el) < SortedArr.Count - 2).All(el => string.Compare(el, SortedArr.ElementAt(SortedArr.IndexOf(el) + 1)) == -1));
        }
    }
}
