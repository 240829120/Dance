using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Wpf
{
    /// <summary>
    /// 属性定义
    /// </summary>
    public class DancePropertyMetadata : PropertyMetadata
    {
        /// <summary>
        /// 属性定义
        /// </summary>
        /// <param name="defaultValueCreator">默认值创建器</param>
        public DancePropertyMetadata(Func<object> defaultValueCreator)
        {
            this.DefaultValueCreator = defaultValueCreator;
        }

        /// <summary>
        /// 属性定义
        /// </summary>
        /// <param name="defaultValueCreator">默认值创建器</param>
        /// <param name="propertyChangedCallback">属性改变回调</param>
        public DancePropertyMetadata(Func<object> defaultValueCreator, PropertyChangedCallback propertyChangedCallback) : base(propertyChangedCallback)
        {
            this.DefaultValueCreator = defaultValueCreator;
        }

        /// <summary>
        /// 属性定义
        /// </summary>
        /// <param name="defaultValueCreator">默认值创建器</param>
        /// <param name="propertyChangedCallback">属性改变回调</param>
        /// <param name="coerceValueCallback">强制转化值回调</param>
        public DancePropertyMetadata(Func<object> defaultValueCreator, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) : base(null, propertyChangedCallback, coerceValueCallback)
        {
            this.DefaultValueCreator = defaultValueCreator;
        }

        /// <summary>
        /// 默认值创建器
        /// </summary>
        public Func<object> DefaultValueCreator { get; private set; }

        public new object DefaultValue
        {
            get
            {
                object value = base.DefaultValue;
                if (value != null)
                {
                    return value;
                }

                value = this.DefaultValueCreator.Invoke();
                this.DefaultValue = value;

                return value;
            }

            set
            {
                base.DefaultValue = value;
            }
        }
    }
}
