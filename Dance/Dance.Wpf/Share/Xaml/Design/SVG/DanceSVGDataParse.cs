using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// 数据转化基类
    /// </summary>
    public abstract class DanceSVGDataParse<T> : IDanceSVGDataParse where T : DanceSVGData
    {
        private Type? type;

        /// <summary>
        /// 能够处理的数据类型
        /// </summary>
        public Type Type
        {
            get
            {
                this.type ??= typeof(T);

                return this.type;
            }
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <returns>是否转化成果</returns>
        public abstract bool Parse(DanceSVGAttribute attribute);
    }
}
