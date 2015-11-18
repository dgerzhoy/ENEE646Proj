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

        //Generates a 24 bit physical address and 12 bit page offset from a block value
        public static Tuple<uint,ushort> generatePhysicalAddr36Pair(Block block, int tagWidth, int SetIdxWidth)
        {
            uint block_offs_tmp = block.block_offset;
            uint set_tmp = ((uint)block.set << 6);
            uint block_mask = (uint)((1 << (tagWidth)) - 1);
            ulong block_tag_tmp = (ulong)((ulong)(block.tag & block_mask) << (6 + SetIdxWidth));

            ulong retVal36 = block_tag_tmp | set_tmp | block_offs_tmp;
            uint retVal24 = (uint)(retVal36 >> 12) & 0x0FFFFFF; 
            ushort retVal12 = (ushort)(retVal36 & 0x0FFF);

            return new Tuple<uint,ushort>(retVal24, retVal12);
        }

        //Combines 24 bit physical address from tlb and 12 bit page offset from virtual address into 36 bit physical address
        public static ulong generatePhysAddr36(uint physical_addr_24, ushort page_offset_12)
        {
            //uint combinedBytes = convertBytesToUint(b0, b1, b2, b3);

            ulong phys24_tmp = (((ulong)(physical_addr_24 & 0x0FFFFFF)) << 12);
            ulong page_offs12_tmp = ((ulong)(((uint)page_offset_12) & 0x0FFF));
            ulong physical_addr_36 = phys24_tmp | page_offs12_tmp; //23 bit physical address

            return physical_addr_36;
        }

        public static Block translateCacheBlock(Block inBlock, Cache Source, Cache Target)
        {
            int SourceTagWidth = Source.TAG_WIDTH;
            int SourceSetIdxWidth = Source.SET_IDX_WIDTH;
            int TargetTagWidth = Target.TAG_WIDTH;
            int TargetSetIdxWidth = Target.SET_IDX_WIDTH;

            Tuple<uint, ushort> Phys36_Pair = generatePhysicalAddr36Pair(inBlock, SourceTagWidth, SourceSetIdxWidth);
            ulong Phys36 = generatePhysAddr36(Phys36_Pair.Item1, Phys36_Pair.Item2);

            ushort SetIdx = getSetIdxFromPhysAddr(Phys36, TargetSetIdxWidth);
            byte BlockOffset = getBlockOffsetFromPhysAddr(Phys36);
            uint Tag = getTagFromPhysAddr(Phys36,TargetTagWidth);

            Block retBlock = new Block(0, SetIdx, BlockOffset, Tag);

            return retBlock;
        }

        public static Block translateCacheBlock(Block inBlock, VL3Cache Source, Cache Target)
        {
            int SourceTagWidth = Source.TAG_WIDTH;
            int SourceSetIdxWidth = Source.SET_IDX_WIDTH;
            int TargetTagWidth = Target.TAG_WIDTH;
            int TargetSetIdxWidth = Target.SET_IDX_WIDTH;

            Tuple<uint, ushort> Phys36_Pair = generatePhysicalAddr36Pair(inBlock, SourceTagWidth, SourceSetIdxWidth);
            ulong Phys36 = generatePhysAddr36(Phys36_Pair.Item1, Phys36_Pair.Item2);

            ushort SetIdx = getSetIdxFromPhysAddr(Phys36, TargetSetIdxWidth);
            byte BlockOffset = getBlockOffsetFromPhysAddr(Phys36);
            uint Tag = getTagFromPhysAddr(Phys36, TargetTagWidth);

            Block retBlock = new Block(0, SetIdx, BlockOffset, Tag);

            return retBlock;
        }

    }
}
