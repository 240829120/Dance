using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Http扩展
    /// </summary>
    public static class DanceHttpExpansion
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly static ILog log = DanceLogManager.GetLogger(typeof(DanceHttpExpansion));

        /// <summary>
        /// 默认超时时间
        /// </summary>
        private readonly static TimeSpan DEFAULT_TIME_OUT = TimeSpan.FromSeconds(10);

        /// <summary>
        /// 请求
        /// </summary>
        public static event EventHandler<DanceHttpRequestEventArgs>? Request;

        /// <summary>
        /// 返回
        /// </summary>
        public static event EventHandler<DanceHttpResponseEventArgs>? Response;

        /// <summary>
        /// 错误
        /// </summary>
        public static event EventHandler<DanceHttpErrorEventArgs>? Error;

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">文件路径</param>
        /// <param name="folder">下载的目录</param>
        /// <param name="fileName">文件名，如果不指定，那么采用远程文件名</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="timeout">超时时间</param>
        public static async void Download(string url, string folder, string? fileName, CookieContainer? cookie, TimeSpan? timeout)
        {
            DanceHttpExpansionContext context = new(url);

            try
            {
                Request?.Invoke(null, context.CreateRequestEventArgs(null));

                using HttpClientHandler handler = new();
                if (cookie != null)
                {
                    handler.UseCookies = true;
                    handler.CookieContainer = cookie;
                }

                using HttpClient client = new(handler);
                client.Timeout = timeout ?? DEFAULT_TIME_OUT;
                using HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                using Stream stream = await response.Content.ReadAsStreamAsync();

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
                byte[] buffer = new byte[10240];
                int readCount = 0;
                while ((readCount = stream.Read(buffer, 0, 10240)) > 0)
                {
                    fs.Write(buffer, 0, readCount);
                }

                Response?.Invoke(null, context.CreateResponseEventArgs(null));
            }
            catch (Exception ex)
            {
                log.Error(ex);

                Error?.Invoke(null, context.CreateErrorEventArgs(ex));
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T">上传结果返回值</typeparam>
        /// <param name="url">上传路径</param>
        /// <param name="file">要上传的文件</param>
        /// <param name="cookie">Cookie</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>返回json对象</returns>
        public static async Task<T?> Upload<T>(string url, string file, CookieContainer? cookie, TimeSpan? timeout) where T : class
        {
            DanceHttpExpansionContext context = new(url);

            try
            {
                Request?.Invoke(null, context.CreateRequestEventArgs(null));

                using HttpClientHandler handler = new();
                if (cookie != null)
                {
                    handler.UseCookies = true;
                    handler.CookieContainer = cookie;
                }

                using HttpClient client = new(handler);
                client.Timeout = timeout ?? DEFAULT_TIME_OUT;
                using FileStream fs = new(file, FileMode.Open, FileAccess.Read);
                using StreamContent content = new(fs);

                using HttpResponseMessage response = await client.PostAsync(url, content);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader sr = new(stream, Encoding.UTF8);
                string json = sr.ReadToEnd();

                Response?.Invoke(null, context.CreateResponseEventArgs(json));

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Error?.Invoke(null, context.CreateErrorEventArgs(ex));

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
        /// <param name="timeout">超时时间</param>
        /// <returns>返回json对象</returns>
        public static async Task<T?> Get<T>(string url, Dictionary<string, string>? header, CookieContainer? cookie, TimeSpan? timeout) where T : class
        {
            DanceHttpExpansionContext context = new(url);

            try
            {
                Request?.Invoke(null, context.CreateRequestEventArgs(null));

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

                client.Timeout = timeout ?? DEFAULT_TIME_OUT;
                using HttpResponseMessage response = await client.GetAsync(url);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader sr = new(stream, Encoding.UTF8);
                string json = sr.ReadToEnd();

                Response?.Invoke(null, context.CreateResponseEventArgs(json));

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Error?.Invoke(null, context.CreateErrorEventArgs(ex));

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
        /// <param name="timeout">超时时间</param>
        /// <returns>返回json对象</returns>
        public static async Task<T?> Post<T>(string url, object? data, Dictionary<string, string>? header, CookieContainer? cookie, TimeSpan? timeout) where T : class
        {
            DanceHttpExpansionContext context = new(url);

            try
            {
                string postJson = data == null ? string.Empty : JsonConvert.SerializeObject(data);

                Request?.Invoke(null, context.CreateRequestEventArgs(postJson));

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

                client.Timeout = timeout ?? DEFAULT_TIME_OUT;
                StringContent content = new(postJson, Encoding.UTF8, "application/json");
                using HttpResponseMessage response = await client.PostAsync(url, content);
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using StreamReader sr = new(stream, Encoding.UTF8);
                string json = sr.ReadToEnd();

                Response?.Invoke(null, context.CreateResponseEventArgs(json));

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Error?.Invoke(null, context.CreateErrorEventArgs(ex));

                return null;
            }
        }
    }
}