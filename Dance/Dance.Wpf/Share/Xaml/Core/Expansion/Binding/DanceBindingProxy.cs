using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 绑定代理
    /// </summary>
    public class DanceBindingProxy : Freezable
    {
        #region Source -- 源

        /// <summary>
        /// 源
        /// </summary>
        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// 源
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(DanceBindingProxy), new PropertyMetadata(null));

        #endregion

        protected override Freezable CreateInstanceCore() => new DanceBindingProxy();
    }
}
