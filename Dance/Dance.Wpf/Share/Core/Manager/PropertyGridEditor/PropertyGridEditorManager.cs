using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Dance.Wpf
{
    /// <summary>
    /// 属性列表编辑器管理器
    /// </summary>
    [DanceSingleton(typeof(IPropertyGridEditorManager))]
    public class PropertyGridEditorManager : IPropertyGridEditorManager
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private readonly Dictionary<Type, List<EditorTemplateDefinition>> Cache = new();

        /// <summary>
        /// 获取自定义编辑器列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>自定义编辑器列表</returns>
        public List<EditorTemplateDefinition> GetEditorTemplateDefinitions(Type type)
        {
            if (this.Cache.TryGetValue(type, out List<EditorTemplateDefinition>? list))
                return list;

            lock (this.Cache)
            {
                list = this.CreateEditorTemplateDefinitions(type);
                this.Cache[type] = list;
            }

            return list;
        }

        /// <summary>
        /// 创建自定义编辑器列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>自定义编辑器列表</returns>
        private List<EditorTemplateDefinition> CreateEditorTemplateDefinitions(Type type)
        {
            List<EditorTemplateDefinition> list = new();

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                EditorAttribute? attribute = property.GetCustomAttribute<EditorAttribute>();
                if (attribute == null)
                    continue;

                EditorTemplateDefinition define = new();
                Type? editType = Type.GetType(attribute.EditorTypeName, false);
                if (editType == null)
                    continue;

                define.EditingTemplate = DanceXamlExpansion.CreateDataTemplate(editType);
                define.TargetProperties.Add(property.Name);

                list.Add(define);
            }

            return list;
        }
    }
}
