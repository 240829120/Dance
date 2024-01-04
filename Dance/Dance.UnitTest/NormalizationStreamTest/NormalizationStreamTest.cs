using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance.UnitTest
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StudentStruct
    {
        public byte Header1;

        public byte Header2;

        public int Value1;

        public int Value2;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StudentSingle
    {
        public UInt32 Value;
    }

    [TestClass]
    public class NormalizationStreamTest
    {
        [TestMethod]
        public void BufferTest()
        {
            DanceStructNormalStreamHelper helper = new(typeof(StudentStruct));

            DanceFixedNormalStream stream = new(10, [1, 1], 25);

            stream.Write([0, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2]);
            stream.Write([2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2]);
            stream.Write([1, 1, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2]);
            stream.Write([4, 4, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1]);
            stream.Write([1, 4, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5]);

            byte[]? buffer1 = stream.Read();
            byte[]? buffer2 = stream.Read();
            byte[]? buffer3 = stream.Read();
            byte[]? buffer4 = stream.Read();

            Assert.IsNotNull(buffer1);
            Assert.IsNotNull(buffer2);
            Assert.IsNotNull(buffer3);
            Assert.IsNull(buffer4);
        }

        [TestMethod]
        public void StrucTest()
        {
            DanceStructNormalStream<StudentStruct> stream = new([1, 1]);

            byte[] buffer = [1, 1, 254, 255, 255, 255, 1, 0, 0, 0];
            stream.Write(buffer);
            StudentStruct? student1 = stream.ReadStruct(false);

            DanceStructNormalStreamHelper helper = new(typeof(StudentStruct));
            helper.Swap(buffer);
            stream.Write(buffer);
            StudentStruct? student2 = stream.ReadStruct(true);

            helper.Swap(buffer);
            stream.Write(buffer);
            StudentStruct? student3 = stream.ReadStruct(false);

            Assert.IsNotNull(student1);
            Assert.IsNotNull(student2);
            Assert.IsNotNull(student3);

            Assert.AreEqual(student1.Value.Value1, student2.Value.Value1);
            Assert.AreEqual(student1.Value.Value1, student3.Value.Value1);
        }

        [TestMethod]
        public void LengthTest()
        {
            byte[] temp = [1, 2, 3, 4];
            var b = BitConverter.ToUInt32(temp);

            byte[] bytes = new byte[4];
            short[] buffer = [(short)U32HighWord(b), (short)U32LowWord(b)];
            Buffer.BlockCopy(buffer, 0, bytes, 0, 4);


        }

        [TestMethod]
        public void UdpTest()
        {
            UdpClient client1 = new(7777);
            UdpClient client2 = new();

            byte[]? receiveBuffer = null;
            Task tas1 = Task.Run(() =>
            {
                try
                {
                    receiveBuffer = client1.ReceiveAsync().Result.Buffer;
                }
                catch (Exception ex)
                {

                }
            });

            DanceStructNormalStreamHelper helper = new(typeof(StudentSingle));
            StudentSingle single = new()
            {
                Value = 1
            };
            byte[] bytes = helper.ConvertToByte(single, true);

            Task.Delay(2000).Wait();

            client2.Send(bytes, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777));

            Task.Delay(52000).Wait();

        }

        //将short类型数据的高低位进行交换
        private static ushort Swap(ushort x)
        {
            ushort highbit;
            ushort lowbit;
            ushort swap;
            highbit = (ushort)(x >> 8);
            lowbit = (ushort)((x & 0x00ff) << 8);
            swap = (ushort)(highbit | lowbit);
            return swap;
        }

        ///取Uint32的高１６位，并将Uint16的高８位和低８位交换
        private static ushort U32HighWord(uint x)
        {
            ushort highbit;

            highbit = (ushort)(x >> 16);
            highbit = Swap(highbit);

            return (highbit);
        }

        ///取Uint32的低１６位，并将Uint16的高８位和低８位交换
        private static ushort U32LowWord(uint x)
        {
            ushort lowbit;
            lowbit = (ushort)(x & 0x0000ffff);
            lowbit = Swap(lowbit);
            return (lowbit);
        }
    }
}
