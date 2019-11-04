using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.ExtensionMoethods
{
    public static class AreSameValueExtension
    {
        public static bool AreSameValue<T, TResult>(this IEnumerable<T> source, Func<T,TResult> selector)
        {
            bool valueAssigned = false;
            TResult lastValue = default;
            foreach (var item in source)
            {
                var currentValue = selector(item);
                if (!valueAssigned)
                {
                    valueAssigned = true;
                }
                else if (!currentValue.Equals(lastValue))
                {
                    return false;
                }
                lastValue = currentValue;              
            }
            return true;
        }
    }
}
