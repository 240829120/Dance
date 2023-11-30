using HelixToolkit.SharpDX.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 路径转化器
    /// </summary>
    public class DanceTexturePathResolver : ITexturePathResolver
    {
        /// <summary>
        /// 转化路径
        /// </summary>
        /// <param name="modelPath">模型路径</param>
        /// <param name="texturePath">纹理路径</param>
        /// <returns>纹理完整路径</returns>
        public string Resolve(string modelPath, string texturePath)
        {
            string? dir = Path.GetDirectoryName(modelPath);
            if (string.IsNullOrWhiteSpace(dir))
                return string.Empty;

            Debug.WriteLine($"modelPath: {modelPath}  ||  texturePath: {texturePath}");

            if (Path.IsPathRooted(texturePath))
                return texturePath;

            string destPath = Path.GetFullPath(Path.Combine(dir, texturePath));

            return destPath;
        }
    }
}
