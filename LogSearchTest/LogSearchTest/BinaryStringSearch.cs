using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogSearch
{
    public static class BinaryStringSearch
    {
        public static IEnumerable<int> Searchh<T1, T2>(
            this IEnumerable<T1> Content,
            IEnumerable<T2> Elements)
        {
            var ContentSorted = Content.QuickSortString();

            return null;
        }
    }
}
