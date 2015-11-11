using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{

    //Helper class that parse the page table entries and extracts the specified fields.
    public class CacheFieldParser
    {
        private static CacheFieldParser instance;
        private const int PHYSICAL_ADDR_WIDTH = 36;

        private CacheFieldParser() { }

        public static CacheFieldParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CacheFieldParser();
                }
                return instance;
            }
        }

        private static uint convertBytesToUint(uint b0, uint b1, uint b2, uint b3)
        {
            uint combinedBytes = (b0 << 24) | (b1 << 16) | (b2 << 8) | b3;
            return combinedBytes;
        }

        public static uint getTagFromPhysAddr(ulong Phys_Addr, int tagWidth)
        {
            int shift = PHYSICAL_ADDR_WIDTH - tagWidth;
            uint mask = (uint)((0x01<<tagWidth) -1);
            uint result = (uint)(Phys_Addr >> shift);
            result = (result & mask);
            return result;
        }

        public static ushort getSetIdxFromPhysAddr(ulong Phys_Addr, int SetIdxWidth)
        {
            ushort mask = (ushort)((0x01 << SetIdxWidth) - 1);
            ushort result = (ushort)(Phys_Addr >> 6);
            result = (ushort)((uint)result & (uint)mask);
            return result;
        }

        public static byte getBlockOffsetFromPhysAddr(ulong Phys_Addr)
        {
            return (byte)(((byte)Phys_Addr) & 0x03F);
        }

        public static uint getTagFromEntry(uint meta_data, int tagWidth)
        {
            return (meta_data & (uint)((1<<tagWidth)-1));
        }

        public static bool getValidBitFromEntry(uint meta_data, int tagWidth)
        {
            byte valid_bit = (byte)((meta_data >> (tagWidth + 1)) & 0x01);
            return (valid_bit == 1);
        }

        public static bool getDirtyBitFromEntry(uint meta_data, int tagWidth)
        {
            //uint combinedBytes = convertBytesToUint(b0, b1, b2, b3);
            return (((meta_data >> tagWidth) & 0x01) == 1);
        }

        public static uint generateMetaEntry(bool valid, bool dirty, uint Tag, int tagWidth)
        {
            uint retVal = (((uint)(Convert.ToInt32(valid) & 0x01)) << (tagWidth + 1)) | (((uint)(Convert.ToInt32(dirty) & 0x01)) << (tagWidth)) | ((uint)(Tag & ((1<<tagWidth)-1)));
            return retVal;
        }

    }
}
