
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LogSearch
{
    public static class ZipExtension
    {
        /// <summary>
        /// Works like a zip function, but for more than two sets of data.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TResult> ZipUnLim<TResult>(
            Func<List<dynamic>, TResult> func,
            params object[] second)
        {

            var options = new List<Options>();

            foreach (var item in second)
            {
                    options.Add(

                    (item as IEnumerable<string>) != null ? new Options { Str = item as IEnumerable<string>,
                                                                          Len = (item as IEnumerable<string>).Count()} :

                    (item as IEnumerable<bool>) != null ? new Options { Boolean = item as IEnumerable<bool>,
                                                                        Len = (item as IEnumerable<bool>).Count()} :

                    (item as IEnumerable<int>) != null ? new Options { Integer = item as IEnumerable<int>,
                                                                        Len = (item as IEnumerable<int>).Count()} :

                    (item as IEnumerable<char>) != null ? new Options { Character = item as IEnumerable<char>,
                                                                         Len = (item as IEnumerable<char>).Count()} :

                    (item as IEnumerable<float>) != null ? new Options { Flt = item as IEnumerable<float>,
                                                                         Len = (item as IEnumerable<float>).Count() } :
                                                           null);
                if(options.Last() == null)
                    throw new Exception("Only items of type string, bool, int, char and decimal are allowed");
            }

            var minCount = options.Select(option => option.Len).Min();

            for (int i = 0; i < minCount; i++)
            {
                var TempList = new List<object>();
                foreach (var option in options)
                {
                    if (option.Str != null)
                        TempList.Add(option.Str.ElementAt(i));
                    else if (option.Boolean != null )
                        TempList.Add(option.Boolean.ElementAt(i));
                    else if (option.Integer!= null)
                        TempList.Add(option.Integer.ElementAt(i));
                    else if (option.Character != null)
                        TempList.Add(option.Character.ElementAt(i));
                    else if (option.Flt != null)
                        TempList.Add(option.Flt.ElementAt(i));
                }

                yield return func(TempList);
            }
        }

        private class Options
        {
            public IEnumerable<string> Str { get; set; }

            public IEnumerable<bool> Boolean { get; set; }

            public IEnumerable<int> Integer { get; set; }

            public IEnumerable<char> Character { get; set; }

            public IEnumerable<float> Flt { get; set; }

            public int Len { get; set; }
        }
    }
}
