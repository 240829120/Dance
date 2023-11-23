using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Json对象转化器
    /// </summary>
    public class DanceJsonObjectConverter : JsonConverter
    {
        /// <summary>
        /// 是否可以转化
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableTo(typeof(IDanceJsonObject));
        }

        /// <summary>
        /// 读取
        /// </summary>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            JProperty? property = jObject.Property(nameof(IDanceJsonObject.PART_DanceObjectType));
            if (property == null || property.Value.Value<string>() is not string value || string.IsNullOrWhiteSpace(value))
                return null;

            if (Type.GetType(value) is not Type type || string.IsNullOrWhiteSpace(type.FullName))
                return null;

            object? target = type.Assembly.CreateInstance(type.FullName);
            if (target == null)
                return null;

            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {

        }

        /// <summary>
        /// 拷贝对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="src">源</param>
        /// <returns>拷贝对象</returns>
        public static T? Copy<T>(IDanceJsonObject? src) where T : IDanceJsonObject
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
