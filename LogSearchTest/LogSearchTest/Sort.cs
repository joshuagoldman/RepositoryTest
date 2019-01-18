using System;
using System.Linq;
using System.Collections.Generic;

namespace LogSearchTest
{

    public static class TestClass
    {

    }

    public static class Sort
    {
        public static string[] QuickSortString<T1>(
            this IEnumerable<T1> Enum)
        {

            var A = Enum.Select(el => el.ToString()).ToArray();

            if (A.AllEqual())
            {
                return A;
            }

            if (A.Count() < 2)
            {
                return A;
            }

            var PossibPivots = new string[] { A[0], A[A.Count() / 2], A[A.Count() - 1] };

            if (A.Count() == 2)
            {
                return string.Compare(A[0], A[1]) == -1 ? A : Swap(A, 0, 1);
            }

            var Pivot = Array.IndexOf(A, PossibPivots.FirstOrDefault(el => PossibPivots.Where(el_other => el_other != el).
                            Any(el_other => string.Compare(el_other, el) == 1) &&
                            PossibPivots.Where(el_other => el_other != el).
                            Any(el_other => string.Compare(el_other, el) == -1)));

            if (Pivot == -1)
            {
                Pivot = 0;
            }

            A = Swap(A.ToArray(), Pivot, A.Count() - 1);

            var NotPivot = A.Take(A.Count() - 1).ToArray();

            var TempLeft = 0;
            var TempRight = NotPivot.Count() - 1;
            var SwapWPivot = 0;

            while (true)
            {
                if (string.Compare(NotPivot[TempLeft], A[A.Count() - 1]) != -1 && string.Compare(NotPivot[TempRight], A[A.Count() - 1]) != 1)
                {
                    NotPivot = Swap(NotPivot.ToArray(), TempLeft, TempRight);
                }
                if (string.Compare(NotPivot[TempLeft], A[A.Count() - 1]) == -1)
                {
                    TempLeft++;
                }
                if (string.Compare(NotPivot[TempRight], A[A.Count() - 1]) == 1)
                {
                    TempRight--;
                }
                if (TempRight - TempLeft < 1)
                {
                    if (TempLeft == TempRight)
                    {
                        SwapWPivot = string.Compare(NotPivot[TempLeft], A[A.Count() - 1]) == 1 ? TempLeft : TempLeft + 1;
                        break;
                    }
                    SwapWPivot = Math.Max(TempLeft, TempRight);
                    break;
                }
            }

            var NotPivotList = NotPivot.ToList();
            NotPivotList.Add(A[A.Count() - 1]);
            NotPivot = Swap(NotPivotList.ToArray(), NotPivotList.Count() - 1, SwapWPivot);
            A = NotPivot.ToArray();

            var partitionFirst = A.Take(SwapWPivot);
            var partitionSec = A.Skip(SwapWPivot + 1);
            var AFirst = partitionFirst.QuickSortString().ToList();
            var ASec = partitionSec.QuickSortString().ToList();

            AFirst.Add(A[SwapWPivot]);
            AFirst.AddRange(ASec);
            return AFirst.ToArray();
        }

        private static string[] Swap(string[] A, int leftPos, int rightPos)
        {
            string tmp = A[leftPos];
            A[leftPos] = A[rightPos];
            A[rightPos] = tmp;

            return A;
        }

        public static bool AllEqual<T>(this IEnumerable<T> elementsEnum)
        {
            var A = elementsEnum.Select(el => el.ToString()).ToArray();

            for (int i = 0; i < A.Count() - 1; i++)
            {
                if (A[i] != A[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
