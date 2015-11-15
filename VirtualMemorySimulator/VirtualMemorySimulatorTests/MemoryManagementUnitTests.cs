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
    public class MemoryManagementUnitTests
    {

        [TestMethod()]
        public void InstructionFetchTest_CacheOnly()
        {

            MemoryManagementUnit MMU = new MemoryManagementUnit();

            int L2_ENTRIES = TLB.ENTRIES * TLB.BANKS;

            ulong[] VirtualAddrs = new ulong[L2_ENTRIES];
            Block[] blocks = new Block[L2_ENTRIES];
            Tuple<uint, ushort>[] Phys36 = new Tuple<uint, ushort>[L2_ENTRIES];

            int bank;
            int set;
            ulong PTE;

            //Fill L2 TLB and partially fill L2 Cache
            for (int i = 0; i < L2_ENTRIES; i++)
            {
                //7-bit Page index
                //29-bit Page Tag
                //12-bit page offset
                bank = L2_ENTRIES % MMU.L2Cache.BANKS;
                set = L2_ENTRIES % MMU.L2Cache.SETS;
                //PTE = ;

                //MMU.iL1_TLB.setEntry(bank, set, PTE)
                //Fill with 24bit physical addr


                //blocks[i] = new Block();
                //Phys36[i] = CacheFieldParser.generatePhysicalAddr36Pair(blocks[i], L1.TAG_WIDTH, L1.SET_IDX_WIDTH);
            }


            Assert.Fail();
        }

        [TestMethod()]
        public void OperandFetchTest()
        {
            Assert.Fail();
        }
    }
}