using Newtonsoft.Json;

namespace Ngx.Monorepo.Framework.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a JSON string to the object of type T.
        /// </summary>
        /// <param name="string">JSON to convert to object.</param>
        /// <typeparam name="T">Generic type to convert JSON to.</typeparam>
        /// <returns>Object that was deserialized from JSON.</returns>
        public static T FromJson<T>(this string @string)
        {
            return JsonConvert.DeserializeObject<T>(@string);
        }
    }
}
