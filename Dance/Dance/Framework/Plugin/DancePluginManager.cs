using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    [DanceSingleton(typeof(IDancePluginManager))]
    public class DancePluginManager : DanceObject, IDancePluginManager
    {
        #region PluginDomains -- 插件领域集合

        private readonly List<DancePluginDomain> pluginDomains = [];
        /// <summary>
        /// 插件领域集合
        /// </summary>
        public IReadOnlyList<DancePluginDomain> PluginDomains
        {
            get { return pluginDomains; }
        }

        #endregion

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="assemblyPrefix">程序集前缀</param>
        public void LoadPlugin(string assemblyPrefix)
        {
            List<string> files = [];
            List<Assembly> assemblies = [];

            files.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(p => Path.GetFileName(p).StartsWith(assemblyPrefix)));

            assemblies.AddRange(files.Select(p => Assembly.Load(AssemblyName.GetAssemblyName(p))));

            this.LoadPlugin(assemblies.ToArray());
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="assemblys">程序集</param>
        public void LoadPlugin(params Assembly[] assemblys)
        {
            foreach (Assembly assembly in assemblys)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    try
                    {
                        if (!type.IsClass || !type.IsAssignableTo(typeof(IDancePluginLifescope)) || string.IsNullOrWhiteSpace(type.FullName))
                            continue;

                        if (type.Assembly.CreateInstance(type.FullName) is not IDancePluginLifescope lifescpoe)
                            continue;

                        this.LoadPlugin(lifescpoe);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
            }
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="lifescopes">插件生命周期</param>
        public void LoadPlugin(params IDancePluginLifescope[] lifescopes)
        {
#if DEBUG
            Stopwatch stopwatch = new();

            foreach (IDancePluginLifescope lifescope in lifescopes)
            {
                try
                {
                    stopwatch.Restart();
                    IDancePluginInfo info = lifescope.Register();
                    DancePluginDomain domain = new(lifescope, info);
                    this.pluginDomains.Add(domain);

                    Debug.WriteLine($"注册插件 ID: {info.ID}, Name: {info.Name}".PadRight(20) + $"  -- {stopwatch.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
#else
            foreach (IDancePluginLifescope lifescope in lifescopes)
            {
                try
                {
                    IDancePluginInfo info = lifescope.Register();

                    DancePluginDomain domain = new(lifescope, info);
                    this.pluginDomains.Add(domain);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
#endif
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        /// <param name="pluginIds">插件ID集合</param>
        public void InitializePlugin(params string[] pluginIds)
        {
#if DEBUG
            Stopwatch stopwatch = new();

            foreach (string pluginId in pluginIds)
            {
                try
                {
                    DancePluginDomain? domain = this.PluginDomains.FirstOrDefault(p => p.PluginInfo.ID == pluginId);
                    if (domain == null || domain.IsInitialized)
                        continue;

                    stopwatch.Restart();
                    domain.Lifescope.Initialize();
                    domain.IsInitialized = true;

                    Debug.WriteLine($"初始化插件 ID: {domain.PluginInfo.ID}, Name: {domain.PluginInfo.Name}".PadRight(20) + $"  -- {stopwatch.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
#else
            foreach (string pluginId in pluginIds)
            {
                try
                {
                    DancePluginDomain? domain = this.PluginDomains.FirstOrDefault(p => p.PluginInfo.ID == pluginId);
                    if (domain == null || domain.IsInitialized)
                        continue;

                    domain.Lifescope.Initialize();
                    domain.IsInitialized = true;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
#endif
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
#if DEBUG
            Stopwatch stopwatch = new();

            foreach (DancePluginDomain domain in this.PluginDomains)
            {
                stopwatch.Restart();
                domain.Dispose();
                Debug.WriteLine($"注册插件 ID: {domain.PluginInfo.ID}, Name: {domain.PluginInfo.Name}".PadRight(20) + $"  -- {stopwatch.ElapsedMilliseconds}ms");
            }
#else
            foreach (DancePluginDomain domain in this.PluginDomains)
            {
                domain.Dispose();
            }
#endif
        }
    }
}