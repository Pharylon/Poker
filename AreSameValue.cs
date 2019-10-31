using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public static class AreSameValueExtension
    {
        public static bool AreSameValue<T, TResult>(this IEnumerable<T> source, Func<T,TResult> selector)
        {
            TResult lastValue = default;
            foreach (var item in source)
            {
                var currentValue = selector(item);
                if (lastValue.Equals(default(TResult)))
                {
                    lastValue = currentValue;
                }
                else
                {
                    if (!currentValue.Equals(lastValue))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
