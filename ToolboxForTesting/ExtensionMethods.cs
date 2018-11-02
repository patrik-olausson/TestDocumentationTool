using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ToolboxForTesting
{
    public static class ExtensionMethods
    {
        public static bool EqualsIgnoreCase(this string value, string value2)
        {
            return string.Equals(value, value2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Helper method that serializes an object to a string
        /// </summary>
        public static string ToJsonString(this object objectToSerialize, bool indented = true)
        {
            return JsonConvert.SerializeObject(
                objectToSerialize,
                indented ? Formatting.Indented : Formatting.None);
        }

        /// <summary>
        /// Helper method that deserializes an object from a string containing json
        /// </summary>
        public static T FromJsonString<T>(this string jsonContent)
        {
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this T entity)
        {
            return new[] { entity };
        }

        public static string ToDelimitedString(this IReadOnlyList<string> values, string delimiter = ";")
        {
            if (values == null)
                return string.Empty;

            if (values.Count == 0)
                return string.Empty;

            if (values.Count == 1)
                return values[0];

            var sb = new StringBuilder();
            for (var i = 0; i < values.Count; i++)
            {
                sb.Append(values[i]);
                if (i < values.Count - 1)
                    sb.Append(delimiter);
            }

            return sb.ToString();
        }
    }
}