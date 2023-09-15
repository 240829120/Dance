using System;
using System.Collections.Generic;
using System.Linq;
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

    [TestClass]
    public class NormalizationStreamTest
    {
        [TestMethod]
        public void BufferTest()
        {
            DanceStructNormalStreamHelper<StudentStruct> helper = new();

            DanceFixedNormalStream stream = new(10, new byte[] { 1, 1 }, 25);

            stream.Write(new byte[] { 0, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 });
            stream.Write(new byte[] { 1, 1, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 });
            stream.Write(new byte[] { 4, 4, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 });
            stream.Write(new byte[] { 1, 4, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5 });

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
            DanceStructNormalStream<StudentStruct> stream = new(new byte[] { 1, 1 });

            byte[] buffer = new byte[] { 1, 1, 254, 255, 255, 255, 1, 0, 0, 0 };
            stream.Write(buffer);
            StudentStruct? student1 = stream.ReadStruct(false);

            DanceStructNormalStreamHelper<StudentStruct> helper = new();
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
            Single s1 = 1;
            var b = BitConverter.GetBytes(s1);
        }
    }
}
