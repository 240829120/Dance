using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 文件扩展
    /// </summary>
    public static class FileExpansion
    {
        /// <summary>
        /// 写入Json
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">对象</param>
        public static void WriteJson(string path, object? obj)
        {
            WriteJson(path, obj, Encoding.UTF8);
        }

        /// <summary>
        /// 写入Json
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">对象</param>
        /// <param name="encoding">编码</param>
        public static void WriteJson(string path, object? obj, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            string json = obj == null ? string.Empty : JsonConvert.SerializeObject(obj);

            using StreamWriter sw = new(path, false, encoding);
            sw.Write(json);
        }

        /// <summary>
        /// 读取Json对象
        /// </summary>
        /// <typeparam name="T">Json对象类型</typeparam>
        /// <param name="path">路径</param>
        /// <returns>对象</returns>
        public static T? ReadJson<T>(string path)
        {
            return ReadJson<T>(path, Encoding.UTF8);
        }

        /// <summary>
        /// 读取Json对象
        /// </summary>
        /// <typeparam name="T">Json对象类型</typeparam>
        /// <param name="path">路径</param>
        /// <param name="encoding">编码</param>
        /// <returns>对象</returns>
        public static T? ReadJson<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            using StreamReader sr = new(path, encoding);

            string str = sr.ReadToEnd();

            if (string.IsNullOrWhiteSpace(str))
                return default;

            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="document">文档</param>
        public static void WriteTxt(string path, string document)
        {
            WriteTxt(path, document, Encoding.UTF8);
        }

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="document">文档</param>
        /// <param name="encoding">编码</param>
        public static void WriteTxt(string path, string document, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            using StreamWriter sw = new(path, false, encoding);
            sw.Write(document);
        }

        /// <summary>
        /// 读取文档
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文档</returns>
        public static string ReadTxt(string path)
        {
            return ReadTxt(path, Encoding.UTF8);
        }

        /// <summary>
        /// 读取文档
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="encoding">编码</param>
        /// <returns>文档</returns>
        public static string ReadTxt(string path, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            using StreamReader sr = new(path, encoding);
            string str = sr.ReadToEnd();

            return str;
        }
    }
}
