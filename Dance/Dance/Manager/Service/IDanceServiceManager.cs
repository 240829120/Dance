using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art
{
    /// <summary>
    /// 服务管理器
    /// </summary>
    public interface IDanceServiceManager
    {
        /// <summary>
        /// 构建服务
        /// </summary>
        /// <param name="assemblies">程序集</param>
        void Build(params Assembly[] assemblies);

        /// <summary>
        /// 构建服务
        /// </summary>
        /// <param name="assemblyPrefix">程序集前缀</param>
        void Build(string assemblyPrefix);

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="instance">服务实例</param>
        void AddService(object instance);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        object? Invoke(string route, params object?[] args);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        string? InvokeJson(string route, string?[] args);
    }
}
