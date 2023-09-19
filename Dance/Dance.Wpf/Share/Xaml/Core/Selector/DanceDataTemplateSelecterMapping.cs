using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 数据模板选择器映射
    /// </summary>
    public class DanceDataTemplateSelecterMapping
    {
        /// <summary>
        /// 值
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// 数据模板
        /// </summary>
        public DataTemplate? DataTemplate { get; set; }
    }
}
