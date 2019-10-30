using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindRepSearchKey
{
    public static class FindRepSearchKey
    {
        public static List<T1> FindMatchesInOther<T1>(
            IEnumerable<IEnumerable<T1>> sequenceOfSeqquence,
            string propertyName)
        {
            var ListOfList = new List<List<T1>>();

            foreach (var query in sequenceOfSeqquence)
            {
                ListOfList.Add(new List<T1>());
                foreach (var item in query)
                {
                    ListOfList.Last().Add(item);
                }
            }

            var FoundMoreThanOnce = new List<T1>();

            ListOfList.ForEach(list =>
            {
                var ListOfOtherList = ListOfList.Where(list_other => ListOfList.IndexOf(list_other) > ListOfList.IndexOf(list));
                ListOfOtherList.ToList().ForEach(list_other =>
                {
                    list_other.ForEach(item_other =>
                    {
                        if (list.Any(item => propertyNameEqual(item, item_other, propertyName) &&
                                             FoundMoreThanOnce.NotAlreadyInList(item)))
                        {
                            FoundMoreThanOnce.Add(list.FirstOrDefault(item => propertyNameEqual(item, item_other, propertyName)));
                        }
                    });
                });

            });
            return FoundMoreThanOnce;
        }

        public static bool propertyNameEqual<T1, T2>(
            T1 One,
            T2 Two,
            string propertyName)
        {

            var ItemOneName = (string)One.GetType().GetProperty(propertyName).GetValue(One);
            var ItemTwoName = (string)Two.GetType().GetProperty(propertyName).GetValue(Two);

            return ItemOneName == ItemTwoName;
        }

        public static bool NotAlreadyInList<T1, T2>(
            this IEnumerable<T1> Query,
            T2 item)
        {

            var ItemTwoName = (string)item.GetType().GetProperty("Name").GetValue(item);
            var NotInList = Query.AllWDef(item_comp => (string)item_comp.GetType().GetProperty("Name").GetValue(item_comp),
                                          str => str != ItemTwoName);

            return NotInList;
        }
    }
}
