using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;

namespace Dance
{
    /// <summary>
    /// HTTP辅助类
    /// </summary>
    public static class DanceHttpHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(DanceHttpHelper));

        /// <summary>
        /// 缓存字节大小
        /// </summary>
        private static readonly int BUFFER_SIZE = 5 * 1024 * 1024;

        /// <summary>
        /// 超时时间
        /// </summary>
        private static readonly int TIME_OUT = 10 * 1000;

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">文件路径</param>
        /// <param name="folder">下载的目录</param>
        /// <param name="fileName">文件名，如果不指定，那么采用远程文件名</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="callBack">回掉函数</param>
        public static async void Download(string url, string folder, string? fileName, CookieContainer? cookie, Action<DanceHttpHelperFileEventArgs>? callBack = null)
        {
            try
            {
                using HttpClientHandler handler = new();
                if (cookie != null)
                {
                    handler.UseCookies = true;
                    handler.CookieContainer = cookie;
                }

                using HttpClient client = new(handler);
                client.Timeout = TimeSpan.FromMilliseconds(TIME_OUT);
                using HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                long length = response.Content.Headers.ContentLength ?? 0;
                long position = 0;
                fileName ??= response.Content.Headers.ContentDisposition?.FileName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = Guid.NewGuid().ToString();
                }
                string fullPath = Path.Combine(folder, fileName);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                using FileStream fs = new(fullPath, FileMode.Create, FileAccess.Write);
                byte[] buffer = new byte[BUFFER_SIZE];
                int readCount = 0;
                while ((readCount = stream.Read(buffer, 0, BUFFER_SIZE)) > 0)
                {
                    fs.Write(buffer, 0, readCount);
                    if (callBack != null)
                    {
                        position += readCount;
                        DanceHttpHelperFileEventArgs e = new(position, length, fullPath, url);
                        callBack.Invoke(e);
                        if (e.IsCancel)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T">上传结果返回值</typeparam>
        /// <param name="url">上传路径</param>
        /// <param name="file">要上传的文件</param>
        /// <param name="cookie">Cookie</param>
        /// <returns>返回json对象</returns>
        public static async Task<T?> Upload<T>(string url, string file, CookieContainer? cookie) where T : class
        {
            try
            {
                using HttpClientHandler handler = new();
                if (cookie != null)
                {
                    handler.UseCookies = true;
                    handler.CookieContainer = cookie;
                }

                using HttpClient client = new(handler);
                client.Timeout = TimeSpan.FromMilliseconds(TIME_OUT);
                using FileStream fs = new(file, FileMode.Open, FileAccess.Read);
                using StreamContent content = new(fs);

                using HttpResponseMessage response = await client.PostAsync(url, content);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader sr = new(stream, Encoding.UTF8);
                string json = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求url</param>
        /// <param name="header">请求头</param>
        /// <param name="cookie">cookie</param>
        /// <returns>返回json对象</returns>
        public static async Task<T?> Get<T>(string url, Dictionary<string, string>? header, CookieContainer? cookie) where T : class
        {
            try
            {
                string? json = await Get(url, header, cookie);

                if (string.IsNullOrWhiteSpace(json))
                    return default;

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="header">请求头</param>
        /// <param name="cookie">cookie</param>
        /// <returns>数据</returns>
        public static async Task<string?> Get(string url, Dictionary<string, string>? header, CookieContainer? cookie)
        {
            try
            {
                using HttpClientHandler handler = new();
                if (cookie != null)
                {
                    handler.UseCookies = true;
                    handler.CookieContainer = cookie;
                }

                using HttpClient client = new(handler);
                if (header != null)
                {
                    foreach (var kv in header)
                    {
                        client.DefaultRequestHeaders.Add(kv.Key, kv.Value);
                    }
                }

                client.Timeout = TimeSpan.FromMilliseconds(TIME_OUT);
                using HttpResponseMessage response = await client.GetAsync(url);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader sr = new(stream, Encoding.UTF8);
                string json = sr.ReadToEnd();

                return json;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求url</param>
        /// <param name="data">请求内容</param>
        /// <param name="header">请求头</param>
        /// <param name="cookie">cookie</param>
        /// <returns>返回json对象</returns>
        public static async Task<T?> Post<T>(string url, object? data, Dictionary<string, string>? header, CookieContainer? cookie) where T : class
        {
            try
            {
                string? json = await Post(url, data, header, cookie);

                if (string.IsNullOrWhiteSpace(json))
                    return default;

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="data">请求内容</param>
        /// <param name="header">请求头</param>
        /// <param name="cookie">cookie</param>
        /// <returns>数据</returns>
        public static async Task<string?> Post(string url, object? data, Dictionary<string, string>? header, CookieContainer? cookie)
        {
            try
            {
                string postJson = data == null ? string.Empty : JsonConvert.SerializeObject(data);

                using HttpClientHandler handler = new();
                if (cookie != null)
                {
                    handler.UseCookies = true;
                    handler.CookieContainer = cookie;
                }

                using HttpClient client = new(handler);
                if (header != null)
                {
                    foreach (var kv in header)
                    {
                        client.DefaultRequestHeaders.Add(kv.Key, kv.Value);
                    }
                }

                client.Timeout = TimeSpan.FromMilliseconds(TIME_OUT);
                StringContent content = new(postJson, Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await client.PostAsync(url, content);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader sr = new(stream, Encoding.UTF8);
                string json = sr.ReadToEnd();

                return json;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }
    }
}