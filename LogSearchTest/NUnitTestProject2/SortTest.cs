using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using LogSearch;


namespace LogSearchTest.Tests
{
    [TestFixture]
    class SortTest
    {
        [Test]
        [TestCase()]
        public void LogSearchTest()
        {
            var Arr = new string[]
                {
                "ael",
                "efl",
                "poo",
                "dlkdjf",
                "zske",
                "hhh",  
                "eldfkjf",
                "skdlk",
                "xjk",
                "ael",
                "ael",
                "ael"
                };

            var SortedArr = Arr.QuickSortString().Array;

           Assert.IsTrue(SortedArr.TakeWhile(el => Array.IndexOf(SortedArr, el) < SortedArr.Count() - 2).All(el => string.Compare(el, SortedArr.ElementAt(Array.IndexOf(SortedArr, el) + 1)) == -1 ||
                                                                                                          string.Compare(el, SortedArr.ElementAt(Array.IndexOf(SortedArr, el) + 1)) == 0));
        }
    }
}
