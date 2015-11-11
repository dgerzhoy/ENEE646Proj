using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualMemorySimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator.Tests
{
    [TestClass()]
    public class CacheFieldParserTests
    {
        [TestMethod()]
        public void getFromPhysAddrTest()
        {

            int tagWidth;
            int SetIdxWidth;
            uint Tag_Result;
            ushort setIdx_Result;
            byte blockOffset_Result;
            uint block_tag;
            ushort set_index;
            byte block_offset;

            ulong Phys_Addr = 0x0FDEADBEEF; //36 Bits

            //iL1Cache
            tagWidth = 23;
            SetIdxWidth = 7;
            block_tag = (uint)((Phys_Addr >> (36 - tagWidth)) & 0x07FFFFF);      //23 bit Block Tag
            set_index = (ushort)((Phys_Addr >> 6) & 0x07F);                     //7 bit set index
            block_offset = (byte)(Phys_Addr & 0x03F);                           //6 bit block offset

            Tag_Result = CacheFieldParser.getTagFromPhysAddr(Phys_Addr, tagWidth);
            Assert.AreEqual(block_tag, Tag_Result);
            setIdx_Result = CacheFieldParser.getSetIdxFromPhysAddr(Phys_Addr, SetIdxWidth);
            Assert.AreEqual(set_index, setIdx_Result);
            blockOffset_Result = CacheFieldParser.getBlockOffsetFromPhysAddr(Phys_Addr);
            Assert.AreEqual(block_offset, blockOffset_Result);

            //dL1Cache
            tagWidth = 24;
            SetIdxWidth = 6;
            block_tag = (uint)((Phys_Addr >> (36 - tagWidth)) & 0x0FFFFFF);  //24 bit Block Tag
            set_index = (ushort)((Phys_Addr >> 6) & 0x03F);                 //6 bit set index
            block_offset = (byte)(Phys_Addr & 0x03F);                       //6 bit block offset

            Tag_Result = CacheFieldParser.getTagFromPhysAddr(Phys_Addr, tagWidth);
            Assert.AreEqual(block_tag, Tag_Result);
            setIdx_Result = CacheFieldParser.getSetIdxFromPhysAddr(Phys_Addr, SetIdxWidth);
            Assert.AreEqual(set_index, setIdx_Result);
            blockOffset_Result = CacheFieldParser.getBlockOffsetFromPhysAddr(Phys_Addr);
            Assert.AreEqual(block_offset, blockOffset_Result);

            //L2Cache
            tagWidth = 21;
            SetIdxWidth = 9;
            block_tag = (uint)((Phys_Addr >> (36 - tagWidth)) & 0x01FFFFF);  //21 bit Block Tag
            set_index = (ushort)((Phys_Addr >> 6) & 0x01FF);                //9 bit set index
            block_offset = (byte)(Phys_Addr & 0x03F);                       //6 bit block offset

            Tag_Result = CacheFieldParser.getTagFromPhysAddr(Phys_Addr, tagWidth);
            Assert.AreEqual(block_tag, Tag_Result);
            setIdx_Result = CacheFieldParser.getSetIdxFromPhysAddr(Phys_Addr, SetIdxWidth);
            Assert.AreEqual(set_index, setIdx_Result);
            blockOffset_Result = CacheFieldParser.getBlockOffsetFromPhysAddr(Phys_Addr);
            Assert.AreEqual(block_offset, blockOffset_Result);


        }

        [TestMethod()]
        public void getFromEntryTest()
        {
            uint Tag_Result;
            bool Valid_Result;
            bool Dirty_Result;
            bool Valid_Cond;
            bool Dirty_Cond;

            uint[][] entry = new uint[3][];
            uint[] tag = new uint[3];

            for (int i = 0; i < 3; i++)
            {
                entry[i] = new uint[4];
            }
            tag[0] = 0xDEADBEEF & 0x03FFFFF; //23 bit iL1Cache tag
            tag[1] = 0xDEADBEEF & 0x0FFFFFF; //24 bit dL1Cache tag
            tag[2] = 0xDEADBEEF & 0x01FFFFF; //21 bit L2Cache tag

            //00
            //01
            //10
            //11
            for (uint i = 0; i < 4; i++)
            {
                entry[0][i] = ((i << 23) & 0x01800000) | (tag[0]);
                entry[1][i] = ((i << 24) & 0x02000000) | (tag[1]);
                entry[2][i] = ((i << 21) & 0x00600000) | (tag[2]);

            }



            for (uint i = 0; i < 4; i++)
            {
                //itlb
                Tag_Result = CacheFieldParser.getTagFromEntry(entry[0][i], 23);
                Assert.AreEqual(tag[0], Tag_Result);
                Valid_Result = CacheFieldParser.getValidBitFromEntry(entry[0][i], 23);
                Valid_Cond = (i == 2 | i == 3);
                Assert.AreEqual(Valid_Cond, Valid_Result);
                Dirty_Result = CacheFieldParser.getDirtyBitFromEntry(entry[0][i], 23);
                Dirty_Cond = (i == 1 | i == 3);
                Assert.AreEqual(Dirty_Cond, Dirty_Result);

            }
        }

        [TestMethod()]
        public void generateMetaEntryTest()
        {

            uint entry = CacheFieldParser.generateMetaEntry(true, true, 0x0FFFFF,23);
            uint expected = 0x018FFFFF;

            Assert.AreEqual(expected, entry);

            entry = CacheFieldParser.generateMetaEntry(true, true, 0x07FFFFF, 23);
            expected = 0x01FFFFFF;

            Assert.AreEqual(expected, entry);

            entry = CacheFieldParser.generateMetaEntry(false, false, 0x07FFFFF, 23);
            expected = 0x007FFFFF;

            Assert.AreEqual(expected, entry);

            entry = CacheFieldParser.generateMetaEntry(false, true, 0x07FFFFF, 23);
            expected = 0x00FFFFFF;

            Assert.AreEqual(expected, entry);

            entry = CacheFieldParser.generateMetaEntry(true, false, 0x07FFFFF, 23);
            expected = 0x0017FFFFF;

            Assert.AreEqual(expected, entry);

        }
    }
}