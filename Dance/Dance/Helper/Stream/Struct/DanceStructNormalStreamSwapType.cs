using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 标准化流字节交换类型
    /// </summary>
    public enum DanceStructNormalStreamSwapType
    {
        /// <summary>
        /// 不交换
        /// </summary>
        None,

        /// <summary>
        /// 倒序 [ 1 2 3 4 ==> 4 3 2 1 ]
        /// </summary>
        Reverse,

        /// <summary>
        /// 每2个字节倒序 [ 1 2 3 4 ==> 2 1 4 3 ]
        /// </summary>
        EveryTwoByteReverse
    }
}
