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

            if (A.Count() < 2)
            {
                return A;
            }

            var PossibPivots = new string[] { A[0], A[A.Count() / 2], A[A.Count() - 1] };

            var Pivot = Array.IndexOf(A, PossibPivots.FirstOrDefault(el => PossibPivots.Where(el_other => el_other != el).
                            Any(el_other => string.Compare(el_other, el) == 1) &&
                            PossibPivots.Where(el_other => el_other != el).
                            Any(el_other => string.Compare(el_other, el) == -1)));

            A = Swap(A.ToArray(), Pivot, A.Count() - 1);

            var NotPivot = A.TakeWhile(el => Array.IndexOf(A, el) < A.Count() - 1).ToArray();

            var TempLeft = 0;
            var TempRight = NotPivot.Count() - 1;

            NotPivot.DoWhile(el => Math.Abs(Array.IndexOf(NotPivot, NotPivot[TempLeft]) - Array.IndexOf(NotPivot, NotPivot[TempRight])) > 1,
                                el =>
                                {
                                    NotPivot.DoWhile(new_el => string.Compare(NotPivot[TempLeft], A[Pivot]) == -1,
                                                        new_el => TempLeft++);

                                    NotPivot.DoWhile(new_el => string.Compare(NotPivot[TempRight], A[Pivot]) == 1,
                                                        new_el => TempRight--);

                                    NotPivot = Swap(NotPivot.ToArray(), TempLeft, TempRight);
                                });

            var NotPivotList = NotPivot.ToList();
            NotPivotList.Add(A[A.Count() - 1]);
            NotPivot = Swap(NotPivotList.ToArray(), NotPivotList.Count() - 1, TempRight);
            A = NotPivot.ToArray();

            var partitionFirst = A.TakeWhile(el => Array.IndexOf(A, el) < TempRight);
            var partitionSec = A.SkipWhile(el => Array.IndexOf(A, el) <= TempRight);
            var AFirst = partitionFirst.QuickSortString().ToList();
            var ASec = partitionSec.QuickSortString().ToList();

            AFirst.Add(A[TempRight]);
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

        public static void DoWhile<T>(
            this IEnumerable<T> elementsEnum,
            Func<T , bool> condition,
            Action<T> Action
            )
        {
            var elements = elementsEnum.ToArray();
            var el = elements[0];

            while (condition(el))
            {
                Action(el);
                el = elements[Array.IndexOf(elements, el) + 1];
            }
        }
    }
}
