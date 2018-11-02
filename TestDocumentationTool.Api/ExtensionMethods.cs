using System;
using System.Collections.Generic;

namespace TestDocumentationTool.Api
{
    public static class ExtensionMethods
    {
        public static void ForEach<T>(this IReadOnlyCollection<T> items, Action<T> action)
        {
            if(items == null) return;

            foreach (var item in items)
            {
                action(item);
            }
        }

        public static void AddIfNotNull<T>(this List<T> items, T item)
        {
            if (item == null) return;

            items?.Add(item);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}