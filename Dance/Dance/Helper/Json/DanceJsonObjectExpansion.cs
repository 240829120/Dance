using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Json对象扩展
    /// </summary>
    public static class DanceJsonObjectExpansion
    {
        /// <summary>
        /// JsonObject对象拷贝
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="src">源</param>
        /// <returns>拷贝对象</returns>
        public static T? JsonObjectCopy<T>(this IDanceJsonObject? src) where T : IDanceJsonObject
        {
            if (src == null)
                return default;

            string json = JsonConvert.SerializeObject(src);
            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonConvert.DeserializeObject<T>(json, new DanceJsonObjectConverter());
        }
    }
}
