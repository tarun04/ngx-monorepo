using Newtonsoft.Json;

namespace Ngx.Monorepo.Framework.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts any object into JSON. Using generic to avoid boxing of value types.
        /// </summary>
        /// <param name="obj">Object to convert to JSON.</param>
        /// <typeparam name="T">Generic type of object to convert to JSON.</typeparam>
        /// <returns>JSON of object.</returns>
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

    }
}
