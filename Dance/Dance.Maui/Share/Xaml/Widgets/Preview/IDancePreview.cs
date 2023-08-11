using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Maui
{
    /// <summary>
    /// 预览
    /// </summary>
    public interface IDancePreview
    {
        /// <summary>
        /// 填充方式
        /// </summary>
        Aspect Aspect { get; }

        /// <summary>
        /// 源
        /// </summary>
        string? Source { get; }

        /// <summary>
        /// 背景源
        /// </summary>
        string? BackgroundSource { get; }
    }
}
