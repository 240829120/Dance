using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 数据结构标准化流辅助类
    /// </summary>
    public class DanceStructNormalStreamHelper<T> where T : struct
    {
        /// <summary>
        /// 需要交换顺序的数据类型
        /// </summary>
        private readonly static List<Type> NEED_SWIP_TYPE_LIST = new()
        {
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(UInt16),
            typeof(UInt32),
            typeof(UInt64),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        public DanceStructNormalStreamHelper()
        {
            List<Tuple<int, int>> infos = new();

            Type type = typeof(T);
            int index = 0;

            foreach (var item in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                int size = Marshal.SizeOf(item.FieldType);

                if (NEED_SWIP_TYPE_LIST.Contains(item.FieldType))
                {
                    infos.Add(new Tuple<int, int>(index, index + size - 1));
                }

                index += size;
            }

            this.SwapInfos = infos;
        }

        /// <summary>
        /// 交换信息 [ 开始索引, 结束索引 ]
        /// </summary>
        public IReadOnlyList<Tuple<int, int>>? SwapInfos { get; private set; }

        /// <summary>
        /// 交换
        /// </summary>
        /// <param name="buffer">数据</param>
        public void Swap(byte[] buffer)
        {
            if (this.SwapInfos == null || this.SwapInfos.Count == 0)
                return;

            foreach (var info in this.SwapInfos)
            {
                for (int i = 0; i < (info.Item2 - info.Item1 + 1) / 2; ++i)
                {
                    (buffer[info.Item2 - i], buffer[info.Item1 + i]) = (buffer[info.Item1 + i], buffer[info.Item2 - i]);
                }
            }
        }
    }
}