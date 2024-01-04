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
    [DanceSingleton(typeof(IDanceServiceManager))]
    public class DanceServiceManager : IDanceServiceManager
    {
        /// <summary>
        /// 服务名后缀
        /// </summary>
        public const string SERVICE_NAME_SUFFIX = "Service";

        /// <summary>
        /// 根节点
        /// </summary>
        private readonly List<DanceServiceRouteNode> Roots = [];

        /// <summary>
        /// 构建服务
        /// </summary>
        /// <param name="assemblies">程序集</param>
        public void Build(params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    DanceServiceRouteAttribute? attr = type.GetCustomAttribute<DanceServiceRouteAttribute>();
                    if (attr == null)
                        continue;

                    ConstructorInfo? constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                        continue;

                    object instance = constructor.Invoke(null);
                    this.AddService(instance);
                }
            }
        }

        /// <summary>
        /// 构建服务
        /// </summary>
        /// <param name="assemblyPrefix">程序集前缀</param>
        public void Build(string assemblyPrefix)
        {
            List<string> files = [];
            List<Assembly> assemblies = [];

            files.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(p => Path.GetFileName(p).StartsWith(assemblyPrefix)));

            assemblies.AddRange(files.Select(p => Assembly.Load(AssemblyName.GetAssemblyName(p))));

            this.Build(assemblies.ToArray());
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="instance">服务实例</param>
        public void AddService(object instance)
        {
            ArgumentNullException.ThrowIfNull(instance);

            Type classType = instance.GetType();
            DanceServiceRouteAttribute? classRouteAttr = classType.GetCustomAttribute<DanceServiceRouteAttribute>();
            if (classRouteAttr == null)
                return;

            string? classRoute = !string.IsNullOrWhiteSpace(classRouteAttr.Route) ? classRouteAttr.Route :
                                        (classType.Name.EndsWith(SERVICE_NAME_SUFFIX) ? classType.Name[..^SERVICE_NAME_SUFFIX.Length] : classType.Name);

            if (string.IsNullOrWhiteSpace(classRoute))
                throw new Exception("class route is null.");

            classRoute = classRoute.Replace("\\", "/");
            List<string> classRoutes = [.. classRoute.Split('/', StringSplitOptions.RemoveEmptyEntries)];

            foreach (MethodInfo method in classType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                DanceServiceRouteAttribute? methodRouteAtrr = method.GetCustomAttribute<DanceServiceRouteAttribute>();
                if (methodRouteAtrr == null)
                    continue;

                string methodRoute = !string.IsNullOrWhiteSpace(methodRouteAtrr.Route) ? methodRouteAtrr.Route : method.Name;
                methodRoute = methodRoute.Replace("\\", "/");
                List<string> methodRoutes = [.. methodRoute.Split('/', StringSplitOptions.RemoveEmptyEntries)];

                List<string> routes = [.. classRoutes, .. methodRoutes];

                CreateRouteNode(routes, this.Roots, instance, method);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        public object? Invoke(string route, params object?[] args)
        {
            List<string> parts = [.. route.Split('/', StringSplitOptions.RemoveEmptyEntries)];
            DanceServiceRouteNode? node = FindRouteNode(parts, this.Roots);
            if (node == null)
                return null;

            return node.Invoke(args);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        public string? InvokeJson(string route, string?[] args)
        {
            List<string> parts = [.. route.Split('/', StringSplitOptions.RemoveEmptyEntries)];
            DanceServiceRouteNode? node = FindRouteNode(parts, this.Roots);
            if (node == null)
                return null;

            return node.Invoke(args);
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="routes">路由</param>
        /// <param name="nodes">路由节点</param>
        /// <param name="instance">实例</param>
        /// <param name="method">方法</param>
        /// <returns></returns>
        private static void CreateRouteNode(List<string> routes, List<DanceServiceRouteNode> nodes, object instance, MethodInfo method)
        {
            if (routes.Count == 0)
                return;

            string? route = routes.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(route))
                return;

            DanceServiceRouteNode? next = nodes.FirstOrDefault(p => p.Route == route);
            if (next == null)
            {
                next = routes.Count == 1 ? new(route, instance, method) : new(route);
                nodes.Add(next);
            }

            routes.RemoveAt(0);

            CreateRouteNode(routes, next.Items, instance, method);
        }

        /// <summary>
        /// 查找路由节点
        /// </summary>
        /// <param name="route">路由</param>
        /// <returns>服务实例</returns>
        private static DanceServiceRouteNode? FindRouteNode(List<string> routes, List<DanceServiceRouteNode> nodes)
        {
            if (routes.Count == 0)
                return null;

            string? findNode = routes.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(findNode))
                return null;

            DanceServiceRouteNode? next = nodes.FirstOrDefault(p => p.Route == findNode);
            if (next == null)
                return null;

            if (routes.Count == 1)
                return next;

            routes.RemoveAt(0);

            return FindRouteNode(routes, next.Items);
        }
    }
}