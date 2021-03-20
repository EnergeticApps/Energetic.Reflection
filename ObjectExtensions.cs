using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class ObjectExtensions
    {
        public static TAttribute? GetAttribute<TAttribute>(this object instance, bool throwIfNotFound = true)
            where TAttribute : Attribute
        {
            return instance.GetType().GetAttribute<TAttribute>(throwIfNotFound);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this object instance)
        where TAttribute : Attribute
        {
            return instance.GetType().GetAttributes<TAttribute>();
        }
    }
}
