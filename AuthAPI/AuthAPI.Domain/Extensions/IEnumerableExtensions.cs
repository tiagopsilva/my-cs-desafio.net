using System.Collections.Generic;
using System.Linq;

namespace AuthAPI.Domain.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Any() == false;
        }
    }
}