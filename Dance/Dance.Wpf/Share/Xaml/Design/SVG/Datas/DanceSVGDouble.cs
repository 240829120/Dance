using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dance.Wpf
{
    /// <summary>
    /// Double类型
    /// </summary>
    public class DanceSVGDouble : DanceSVGData
    {
        /// <summary>
        /// 值
        /// </summary>
        public double Value;

        /// <summary>
        /// 单位
        /// </summary>
        public string? Unit;

        /// <summary>
        /// Double类型
        /// </summary>
        public DanceSVGDouble()
        {

        }

        /// <summary>
        /// Double类型
        /// </summary>
        /// <param name="value">值</param>
        public DanceSVGDouble(double value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Double类型
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="unit">单位</param>
        public DanceSVGDouble(double value, string unit)
        {
            this.Value = value;
            this.Unit = unit;
        }

        /// <summary>
        /// 零
        /// </summary>
        public static DanceSVGDouble Zero { get; } = new DanceSVGDouble(0);

        /// <summary>
        /// 一
        /// </summary>
        public static DanceSVGDouble One { get; } = new DanceSVGDouble(1);
    }
}
