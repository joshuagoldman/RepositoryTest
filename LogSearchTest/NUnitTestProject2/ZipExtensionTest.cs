using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using LogSearch;


namespace LogSearchTest.Tests
{
    [TestFixture]
    public class ZipExtensionTest
    {
        [Test]
        [TestCase()]
        public void ZipTestin()
        {
            var ArrayOne = new string[] { "Joshua", "Mira", "Dafna", "Benedikte", "Igor" };

            var ArrayTwo = new string[] { "Goldman", "Andersson", "Stolarg", "Makrov", "Bjorklund" };

            var ArrayThree = new bool[] { true, true, false, false, false };

            var ArrayFour = new bool[] { true, true, false, false, false };

            var ArrayFive = new List<int> { 2, 12, 1, 2 };

            var Matrix = ZipExtension.ZipUnLim(x => new
            {
                Names = x[0],
                LastNames = x[1],
                Male = x[2],
                Swaggy = x[3],
                Nums = x[4]
            }, ArrayOne,
               ArrayTwo,
               ArrayThree,
               ArrayFour,
               ArrayFive);

            var s = ZipExtension.ZipUnLim(x => x[0] + x[1] , ArrayOne, ArrayTwo);
        }                            
    }
}
