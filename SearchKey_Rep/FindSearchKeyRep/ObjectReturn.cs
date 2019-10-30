
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindRepSearchKey
{
    public static class ObjectReturn
    {
        public static object ReturnObject<T1>(
            T1 Obj,
            Action<T1> Action)
        {
            Action(Obj);

            return Obj;
        }

        public static bool AllWDef<T1,T2>(
            this IEnumerable<T1> Query,
            Func<T1, T2> Obj,
            Func<T2,bool> CondFun)
        {

            var Res = true;

            foreach (var item in Query)
            {
                if (!CondFun(Obj(item)))
                {
                    Res = false;
                    break;
                }
            }
            return Res;
        }
    }
}
