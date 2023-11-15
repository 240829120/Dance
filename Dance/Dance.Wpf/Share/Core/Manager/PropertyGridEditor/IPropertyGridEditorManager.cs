using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Dance.Wpf
{
    /// <summary>
    /// 属性列表编辑器管理器
    /// </summary>
    public interface IPropertyGridEditorManager
    {
        /// <summary>
        /// 获取自定义编辑器列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>自定义编辑器列表</returns>
        List<EditorTemplateDefinition> GetEditorTemplateDefinitions(Type type);
    }
}
