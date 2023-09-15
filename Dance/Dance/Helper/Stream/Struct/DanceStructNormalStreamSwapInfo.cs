using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 交换信息
    /// </summary>
    public class DanceStructNormalStreamSwapInfo
    {
        /// <summary>
        /// 交换信息
        /// </summary>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束索引</param>
        public DanceStructNormalStreamSwapInfo(int startIndex, int endIndex)
        {
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
        }

        /// <summary>
        /// 开始索引
        /// </summary>
        public int StartIndex { get; private set; }

        /// <summary>
        /// 结束索引
        /// </summary>
        public int EndIndex { get; private set; }
    }
}
