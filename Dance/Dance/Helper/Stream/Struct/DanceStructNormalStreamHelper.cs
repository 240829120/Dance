using CsvHelper;
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
    public class DanceStructNormalStreamHelper
    {
        /// <summary>
        /// 需要交换顺序的数据类型
        /// </summary>
        public readonly static List<Type> NEED_SWIP_TYPE_LIST = new()
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

        /// <summary>
        /// 标准化流辅助类
        /// </summary>
        /// <param name="type">数据结构类型</param>
        public DanceStructNormalStreamHelper(Type type)
        {
            List<Tuple<int, int>> infos = new();
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

            this.FixedLength = Marshal.SizeOf(type);
            this.Type = type;
            this.SwapInfos = infos;
        }

        /// <summary>
        /// 数据结构类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int FixedLength { get; private set; }

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
                for (int i = 0; i < (info.Item2 - info.Item1 + 1); i += 2)
                {
                    (buffer[info.Item1 + i], buffer[info.Item1 + i + 1]) = (buffer[info.Item1 + i + 1], buffer[info.Item1 + i]);
                }
            }
        }

        /// <summary>
        /// 转化为数据结构
        /// </summary>
        /// <typeparam name="T">数据结构</typeparam>
        /// <param name="buffer">二进制数据</param>
        /// <param name="isSwap">是否交换字节顺序</param>
        /// <returns>数据结构</returns>
        public T ConvertToStruct<T>(byte[] buffer, bool isSwap = false) where T : struct
        {
            if (isSwap)
            {
                this.Swap(buffer);
            }

            IntPtr ptr = Marshal.AllocHGlobal(this.FixedLength);
            Marshal.Copy(buffer, 0, ptr, this.FixedLength);
            T obj = Marshal.PtrToStructure<T>(ptr);
            Marshal.FreeHGlobal(ptr);

            return obj;
        }

        /// <summary>
        /// 转化为二进制数据
        /// </summary>
        /// <typeparam name="T">数据结构</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="isSwap">是否交换字节顺序</param>
        /// <returns>二进制数据</returns>
        public byte[] ConvertToByte<T>(T obj, bool isSwap = false) where T : struct
        {
            byte[] buffer = new byte[this.FixedLength];
            IntPtr ptr = Marshal.AllocHGlobal(this.FixedLength);
            Marshal.StructureToPtr(obj, ptr, false);
            Marshal.Copy(ptr, buffer, 0, this.FixedLength);
            Marshal.FreeHGlobal(ptr);

            if (isSwap)
            {
                this.Swap(buffer);
            }

            return buffer;
        }
    }
}