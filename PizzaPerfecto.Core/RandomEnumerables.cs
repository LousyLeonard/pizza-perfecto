using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPerfecto.Core
{
    public static class RandomEnumerables
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var rand = new Random();
            int index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    }
}
