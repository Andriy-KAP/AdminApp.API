using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.API.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action(item);
        }
    }
}