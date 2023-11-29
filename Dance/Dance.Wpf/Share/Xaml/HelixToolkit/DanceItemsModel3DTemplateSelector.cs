using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Wpf
{
    /// <summary>
    /// 3D模型模板选择器
    /// </summary>
    public class DanceItemsModel3DTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// 选择模板
        /// </summary>
        /// <param name="item">数据源</param>
        /// <param name="container">容器</param>
        /// <returns>数据模板</returns>
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is not IDanceModel3D model)
                return null;

            return model.DataTemplate;
        }
    }
}
