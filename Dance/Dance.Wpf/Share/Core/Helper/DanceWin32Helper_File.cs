using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dance.Wpf
{    /// <summary>
     /// 文件默认名称选项
     /// </summary>
    public enum FileDefaultNameOption
    {
        /// <summary>
        /// 空格括号和数字
        /// </summary>
        SpaceAndParenthesesAndNumber,

        /// <summary>
        /// 只有数字
        /// </summary>
        OnelyNumber
    }
    public static partial class DanceWin32Helper
    {

        /// <summary>
        /// 在floder文件夹下创建文件夹
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="defaultName">默认名称</param>
        /// <returns>是否成功创建</returns>
        public static bool CreateNewFloderIn(string folder, string defaultName)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, defaultName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }

            string[] folders = Directory.GetDirectories(folder);
            List<string> names = folders.Select(p => Path.GetFileName(p)).ToList();

            string name = GetNewDefaultName(defaultName, names, FileDefaultNameOption.SpaceAndParenthesesAndNumber);
            if (string.IsNullOrWhiteSpace(name))
                return false;

            Directory.CreateDirectory(Path.Combine(folder, name));
            return true;
        }

        /// <summary>
        /// 获取新的默认名字
        /// </summary>
        /// <param name="defaultName">默认名称</param>
        /// <param name="names">已经包含的名称集合</param>
        /// <param name="option">默认名称选项</param>
        /// <returns>新的默认名称</returns>
        public static string GetNewDefaultName(string defaultName, IEnumerable<string?> names, FileDefaultNameOption option)
        {
            List<int> ids = new();
            Regex? regex = option switch
            {
                FileDefaultNameOption.SpaceAndParenthesesAndNumber => new($"{defaultName} [(]([0-9]+)[)]"),
                FileDefaultNameOption.OnelyNumber => new($"{defaultName}([0-9]+)"),
                _ => new($"{defaultName}"),
            };
            foreach (string? name in names)
            {
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                Match match = regex.Match(name);
                if (!match.Success)
                    continue;

                if (!int.TryParse(match.Groups[1].Value, out int id))
                    continue;

                if (id <= 0)
                    continue;

                ids.Add(id);
            }

            if (ids.Count == 0 && !names.Contains(defaultName))
            {
                return defaultName;
            }

            ids.Sort();
            int max = ids.MaxOrDefault();

            for (int i = 1; i <= max + 1; i++)
            {
                if (ids.Contains(i))
                    continue;

                switch (option)
                {
                    case FileDefaultNameOption.SpaceAndParenthesesAndNumber: return $"{defaultName} ({i})";
                    case FileDefaultNameOption.OnelyNumber: return $"{defaultName}{i}";
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 拷贝文件或文件夹
        /// </summary>
        /// <param name="files">文件集合</param>
        /// <param name="dstFloder">目标文件夹</param>
        /// <param name="isRename">是否重命名</param>
        /// <returns>是否成功拷贝</returns>
        public static bool Copy(IList<string> files, string dstFloder, bool isRename)
        {
            SHFILEOPSTRUCT op = new()
            {
                hwnd = IntPtr.Zero,
                wFunc = FileFuncFlags.FO_COPY,
                pFrom = $"{string.Join("\0", files)}\0",// 需要注意，最后需要加入"\0"表示字符串结束，如果需要拷贝多个文件，则 file1 + "\0" + file2 + "\0"...
                pTo = dstFloder + "\0",// 需要注意，最后需要加入"\0"表示字符串结束
                hNameMappings = IntPtr.Zero,
                fFlags = isRename ? FILEOP_FLAGS.FOF_NOCONFIRMMKDIR | FILEOP_FLAGS.FOF_RENAMEONCOLLISION : FILEOP_FLAGS.FOF_NOCONFIRMMKDIR,
                fAnyOperationsAborted = false
            };

            int ret = SHFileOperation(ref op);
            return ret == 0;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="files">文件集合</param>
        /// <returns>是否成功操作</returns>
        public static bool Delete(IList<string> files)
        {
            SHFILEOPSTRUCT op = new()
            {
                hwnd = IntPtr.Zero,
                wFunc = FileFuncFlags.FO_DELETE,
                pFrom = $"{string.Join("\0", files)}\0",// 需要注意，最后需要加入"\0"表示字符串结束，如果需要拷贝多个文件，则 file1 + "\0" + file2 + "\0"...
                pTo = "\0",// 需要注意，最后需要加入"\0"表示字符串结束
                hNameMappings = IntPtr.Zero,
                fFlags = FILEOP_FLAGS.FOF_NOERRORUI | FILEOP_FLAGS.FOF_ALLOWUNDO,
                fAnyOperationsAborted = false
            };

            int ret = SHFileOperation(ref op);
            return ret == 0;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="dstFolder"></param>
        /// <returns></returns>
        public static bool Move(IList<string> files, string dstFolder)
        {
            SHFILEOPSTRUCT op = new()
            {
                hwnd = IntPtr.Zero,
                wFunc = FileFuncFlags.FO_MOVE,
                pFrom = $"{string.Join("\0", files)}\0",// 需要注意，最后需要加入"\0"表示字符串结束，如果需要拷贝多个文件，则 file1 + "\0" + file2 + "\0"...
                pTo = $"{dstFolder}\0",// 需要注意，最后需要加入"\0"表示字符串结束
                hNameMappings = IntPtr.Zero,
                fFlags = FILEOP_FLAGS.FOF_NOCONFIRMMKDIR,
                fAnyOperationsAborted = false
            };

            int ret = SHFileOperation(ref op);
            return ret == 0;
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="oldFile">原始文件</param>
        /// <param name="newFile">新文件</param>
        /// <returns>是否成功</returns>
        public static bool Rename(string oldFile, string newFile)
        {
            SHFILEOPSTRUCT op = new()
            {
                hwnd = IntPtr.Zero,
                wFunc = FileFuncFlags.FO_RENAME,
                pFrom = $"{oldFile}\0",// 需要注意，最后需要加入"\0"表示字符串结束，如果需要拷贝多个文件，则 file1 + "\0" + file2 + "\0"...
                pTo = $"{newFile}\0",// 需要注意，最后需要加入"\0"表示字符串结束
                hNameMappings = IntPtr.Zero,
                fFlags = FILEOP_FLAGS.FOF_NOCONFIRMMKDIR,
                fAnyOperationsAborted = false
            };

            int ret = SHFileOperation(ref op);
            return ret == 0;
        }

        private enum FileFuncFlags : uint
        {
            FO_MOVE = 0x1,
            FO_COPY = 0x2,
            FO_DELETE = 0x3,
            FO_RENAME = 0x4
        }

        [Flags]
        private enum FILEOP_FLAGS : ushort
        {
            FOF_MULTIDESTFILES = 0x1,
            FOF_CONFIRMMOUSE = 0x2,
            FOF_SILENT = 0x4,
            FOF_RENAMEONCOLLISION = 0x8,
            FOF_NOCONFIRMATION = 0x10,
            FOF_WANTMAPPINGHANDLE = 0x20,
            FOF_ALLOWUNDO = 0x40,
            FOF_FILESONLY = 0x80,
            FOF_SIMPLEPROGRESS = 0x100,
            FOF_NOCONFIRMMKDIR = 0x200,
            FOF_NOERRORUI = 0x400,
            FOF_NOCOPYSECURITYATTRIBS = 0x800,
            FOF_NORECURSION = 0x1000,
            FOF_NO_CONNECTED_ELEMENTS = 0x2000,
            FOF_WANTNUKEWARNING = 0x4000,
            FOF_NORECURSEREPARSE = 0x8000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public FileFuncFlags wFunc;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pFrom;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pTo;
            public FILEOP_FLAGS fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHFileOperation([In] ref SHFILEOPSTRUCT lpFileOp);
    }
}
