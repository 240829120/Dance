using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 文件辅助类
    /// </summary>
    public static class DanceFileHelper
    {
        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="src">源目录</param>
        /// <param name="dst">模板目录</param>
        public static void CopyDirectory(string src, string dst)
        {
            if (!Directory.Exists(src))
                return;

            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }

            foreach (string file in Directory.GetFiles(src))
            {
                File.Copy(file, Path.Combine(dst, Path.GetFileName(file)), true);
            }

            foreach (string dir in Directory.GetDirectories(src))
            {
                CopyDirectory(dir, Path.Combine(dst, Path.GetFileName(dir)));
            }
        }

        /// <summary>
        /// 写入Json文件
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="path">文件路径</param>
        public static void WriteJson(object obj, string path)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            using StreamWriter sw = new(path, false, Encoding.UTF8);
            sw.Write(json);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        /// <summary>
        /// 读取Json文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>对象</returns>
        public static T? ReadJson<T>(string path) where T : class
        {
            using StreamReader sr = new(path, Encoding.UTF8);
            string json = sr.ReadToEnd();
            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
