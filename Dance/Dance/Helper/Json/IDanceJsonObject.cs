using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// Json对象
    /// </summary>
    public interface IDanceJsonObject
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        string DanceJsonObjectType { get; }
    }
}
