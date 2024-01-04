using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Dance.Wpf
{
    /// <summary>
    /// 数据模板选择器
    /// </summary>
    [ContentProperty(nameof(Mappings))]
    public class DanceDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// 项集合
        /// </summary>
        public List<DanceDataTemplateSelecterMapping> Mappings { get; private set; } = [];

        /// <summary>
        /// 属性名
        /// </summary>
        public string? PropertyName { get; set; }

        /// <summary>
        /// 选择模板
        /// </summary>
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            object? itemValue = item;
            if (!string.IsNullOrWhiteSpace(this.PropertyName))
            {
                PropertyInfo? property = item.GetType()?.GetProperty(this.PropertyName, BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                    return null;

                itemValue = property.GetValue(item);
            }

            DanceDataTemplateSelecterMapping? mapping = this.Mappings.FirstOrDefault(p => object.Equals(p.Value, itemValue));
            return mapping?.DataTemplate;
        }
    }
}
