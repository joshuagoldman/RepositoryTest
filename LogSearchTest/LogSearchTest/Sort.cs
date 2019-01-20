using System;
using System.Linq;
using System.Collections.Generic;

namespace LogSearch
{

    public class QuickSort
    {
       public Dictionary<int, int> QuickDict { get; set; }

        public string[] Array { get; set; }
    }

    public static class Sort
    {
        public static QuickSort QuickSortString<T1>(
            this IEnumerable<T1> Enum,
            QuickSort Q = null,
            int FormerPivot = 0
            )
        {

            Q = Q ?? new QuickSort()
            {
                Array = Enum.Select(el => el.ToString()).ToArray(),
                QuickDict = Enum.CreatePosDict()

            };

            if (Q.Array.AllEqual())
            {
                return Q;
            }

            if (Q.Array.Count() < 2)
            {
                return Q;
            }

            var PossibPivots = new string[] { Q.Array[0], Q.Array[Q.Array.Count() / 2], Q.Array[Q.Array.Count() - 1] };

            if (Q.Array.Count() == 2)
            {
                return string.Compare(Q.Array[0], Q.Array[1]) == -1 ? Q : Swap(Q, 0, 1, FormerPivotPos: FormerPivot);

            }

            var Pivot = Array.IndexOf(Q.Array, PossibPivots.FirstOrDefault(el => PossibPivots.Where(el_other => el_other != el).
                            Any(el_other => string.Compare(el_other, el) == 1) &&
                            PossibPivots.Where(el_other => el_other != el).
                            Any(el_other => string.Compare(el_other, el) == -1)));

            if (Pivot == -1)
            {
                Pivot = 0;
            }

            Q = Swap(Q, Pivot, Q.Array.Count() - 1, FormerPivotPos: FormerPivot);

            var QNotPivot = new QuickSort()
            {
                Array = Q.Array.Take(Q.Array.Count() - 1).ToArray(),
                QuickDict = Q.QuickDict
            };

            var TempLeft = 0;
            var TempRight = QNotPivot.Array.Count() - 1;
            var SwapWPivot = 0;

            while (true)
            {
                if (string.Compare(QNotPivot.Array[TempLeft], Q.Array[Q.Array.Count() - 1]) != -1 && string.Compare(QNotPivot.Array[TempRight], Q.Array[Q.Array.Count() - 1]) != 1)
                {
                    QNotPivot = Swap(QNotPivot, TempLeft, TempRight, FormerPivotPos: FormerPivot);
                }
                if (string.Compare(QNotPivot.Array[TempLeft], Q.Array[Q.Array.Count() - 1]) == -1 ||
                    string.Compare(QNotPivot.Array[TempLeft], Q.Array[Q.Array.Count() - 1]) == 0)
                {
                    TempLeft++;
                }
                if (string.Compare(QNotPivot.Array[TempRight], Q.Array[Q.Array.Count() - 1]) == 1 ||
                    string.Compare(QNotPivot.Array[TempRight], Q.Array[Q.Array.Count() - 1]) == 0)
                {
                    TempRight--;
                }
                if (TempRight - TempLeft < 1)
                {
                    if (TempLeft == TempRight)
                    {
                        SwapWPivot = string.Compare(QNotPivot.Array[TempLeft], Q.Array[Q.Array.Count() - 1]) == 1 ? TempLeft : TempLeft + 1;
                        break;
                    }
                    SwapWPivot = Math.Max(TempLeft, TempRight);
                    break;
                }
            }

            var NotPivotList = QNotPivot.Array.ToList();
            NotPivotList.Add(Q.Array[Q.Array.Count() - 1]);

            QNotPivot.Array = NotPivotList.ToArray();

            Q = Swap(QNotPivot, QNotPivot.Array.Count() - 1, SwapWPivot, FormerPivotPos: FormerPivot);

            var QFirst = new QuickSort()
            {
                Array = Q.Array.Take(SwapWPivot).ToArray(),
                QuickDict = Q.QuickDict.Where(pos_pair => pos_pair.Value < SwapWPivot + FormerPivot).
                              ToDictionary(pos_pair => pos_pair.Key, pos_pair => pos_pair.Value)
            };

            var QSec = new QuickSort()
            {
                Array = Q.Array.Skip(SwapWPivot + 1).ToArray(),
                QuickDict = Q.QuickDict.Where(pos_pair => pos_pair.Value > SwapWPivot + FormerPivot).
                              ToDictionary(pos_pair => pos_pair.Key, pos_pair => pos_pair.Value)
            };

            QFirst = QFirst.Array.QuickSortString(QFirst, FormerPivot);
            QSec = QSec.Array.QuickSortString(QSec, FormerPivot: SwapWPivot + FormerPivot + 1);

            var ArrayTemp = QFirst.Array.ToList();
            ArrayTemp.Add(Q.Array[SwapWPivot]);
            ArrayTemp.AddRange(QSec.Array.ToList());

            QFirst.Array = ArrayTemp.ToArray();

            QFirst.QuickDict.Add(Q.QuickDict.FirstOrDefault(pos_pair => pos_pair.Value == SwapWPivot + FormerPivot).Key, SwapWPivot + FormerPivot);

            QSec.QuickDict.ToList().ForEach(pos_pair =>
                                    {
                                        QFirst.QuickDict.Add(pos_pair.Key, pos_pair.Value);
                                    });

            return QFirst;
        }

        private static QuickSort Swap(QuickSort Q, int leftPos, int rightPos, int FormerPivotPos = 0)
        {
            string tmp = Q.Array[leftPos];
            Q.Array[leftPos] = Q.Array[rightPos];
            Q.Array[rightPos] = tmp;

            if (Q.QuickDict != null)
            {
                var LeftKey = Q.QuickDict.FirstOrDefault(x => x.Value == leftPos + FormerPivotPos).Key;
                var RightKey = Q.QuickDict.FirstOrDefault(x => x.Value == rightPos + FormerPivotPos).Key;
                Q.QuickDict[LeftKey] = rightPos + FormerPivotPos;
                Q.QuickDict[RightKey] = leftPos + FormerPivotPos;
            }

            return Q;
        }

        private static Dictionary<int, int> CreatePosDict<T>(
            this IEnumerable<T> A)
        {
            var Dict = new Dictionary<int, int>();

            for (int i = 0; i < A.Count(); i++)
            {
                Dict.Add(i, i);
            }

            return Dict;
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
