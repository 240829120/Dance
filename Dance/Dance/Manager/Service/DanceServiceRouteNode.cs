using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 服务路由节点
    /// </summary>
    public class DanceServiceRouteNode
    {
        /// <summary>
        /// 服务路由节点
        /// </summary>
        /// <param name="route">路由</param>
        public DanceServiceRouteNode(string route)
        {
            this.Route = route;
        }

        /// <summary>
        /// 服务路由节点
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="instance">实例</param>
        /// <param name="method">方法</param>
        public DanceServiceRouteNode(string route, object instance, MethodInfo method)
        {
            this.Route = route;
            this.Instance = instance;
            this.Method = method;
            this.Parameters = method.GetParameters();
        }

        /// <summary>
        /// 子项集合
        /// </summary>
        public List<DanceServiceRouteNode> Items { get; private set; } = [];

        /// <summary>
        /// 节点值
        /// </summary>
        public string Route { get; private set; }

        /// <summary>
        /// 实例
        /// </summary>
        public object? Instance { get; private set; }

        /// <summary>
        /// 方法
        /// </summary>
        public MethodInfo? Method { get; private set; }

        /// <summary>
        /// 参数信息
        /// </summary>
        public ParameterInfo[]? Parameters { get; private set; }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        public object? Invoke(object?[] args)
        {
            if (this.Instance == null || this.Method == null || this.Parameters == null)
                return null;

            if (this.Parameters.Length != args.Length)
                return null;

            object? result = this.Method.Invoke(this.Instance, args);

            return result;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        public string? Invoke(string?[] args)
        {
            if (this.Instance == null || this.Method == null || this.Parameters == null)
                return null;

            if (this.Parameters.Length != args.Length)
                return null;

            object?[] parameters = new object[args.Length];
            for (int i = 0; i < this.Parameters.Length; i++)
            {
                string? arg = args[i];

                if (arg == null)
                {
                    continue;
                }

                parameters[i] = Newtonsoft.Json.JsonConvert.DeserializeObject(arg, this.Parameters[i].ParameterType);
            }

            object? result = this.Method.Invoke(this.Instance, parameters);

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}
