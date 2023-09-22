using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Ioc构建器扩展 -- 程序集
    /// </summary>
    public static class DanceIocBuilder_Assembly
    {
        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="builder">Ioc构建器</param>
        /// <param name="assemblyPrefix">程序集前缀</param>
        /// <returns>Ioc构建器</returns>
        public static DanceIocBuilder AddAssemblys(this DanceIocBuilder builder, string assemblyPrefix)
        {
            List<string> files = new();
            List<Assembly> assemblies = new();

            files.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(p => Path.GetFileName(p).StartsWith(assemblyPrefix)));
            files.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.exe").Where(p => Path.GetFileName(p).StartsWith(assemblyPrefix)));

            assemblies.AddRange(files.Select(p => Assembly.Load(AssemblyName.GetAssemblyName(p))));

            return builder.AddAssemblys(assemblies.ToArray());
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="builder">Ioc构建器</param>
        /// <param name="assemblies">程序集集合</param>
        /// <returns>Ioc构建器</returns>
        public static DanceIocBuilder AddAssemblys(this DanceIocBuilder builder, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    var singletons = type.GetCustomAttributes<DanceSingletonAttribute>(false);
                    foreach (var singleton in singletons)
                    {
                        builder.AddSingleton(singleton.Key, singleton.ServiceType ?? type, type);
                    }

                    var lifescopes = type.GetCustomAttributes<DanceLifeScopeAttribute>(false);
                    foreach (var lifescope in lifescopes)
                    {
                        builder.AddLifeScope(lifescope.Key, lifescope.ServiceType ?? type, type);
                    }
                }
            }

            return builder;
        }
    }
}
