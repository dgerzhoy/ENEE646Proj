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
    public class iTLBTests
    {
        [TestMethod()]
        public void iTLBTest_All_Valid()
        {
            const int ENTRIES = 256;

            iTLB TLB = new iTLB();

            //Test Vars
            ulong Curr_Entry;
            ulong Curr_Addr;
            ulong Result;
            ulong Expected_Result;

            //PTE Contains
            //4 Unused Bits
            //4 bit protection
            //1 Valid Bit
            //31 bit page address tag
            //24 bit physical addr
            ulong[] Entries = new ulong[ENTRIES];
            for (int i = 0; i < ENTRIES; i++)
            {
                ulong unused = 0x0;             //4 Unused Bits
                ulong prot = ((ulong)0xF) << 56;         //4 bit protection
                ulong valid = ((ulong)0x1) << 55;        //1 Valid Bit
                ulong tag = (((ulong)(i & 0x07FFFFFFF)) << 24);   //31 bit page address tag
                ulong phys_addr = 0xFFFFFF;    //24 bit physical addr

                Entries[i] = unused | prot | valid | tag | phys_addr;

            }
            //Virtual Address Contains
            //5 bit page index
            //31 bit page address tag
            //12 bit page offset
            ulong[] Virtual_Addrs = new ulong[ENTRIES];
            for (int i = 0; i < ENTRIES; i++)
            {
                Virtual_Addrs[i] =
                    ((ulong)(0x001F & (i % iTLB.ENTRIES)) << 43) |       //5 bit page index (leading bit of 0)
                    ((ulong)(0x07FFFFFFF & i) << 12) |       //31 bit page address tag
                    (0x0FFF);                                //12 bit page offset

            }

            for (int i = 0; i < ENTRIES; i++)
            {
                Curr_Entry = Entries[i];
                Curr_Addr = Virtual_Addrs[i];
                Expected_Result = id_TLBParser.getPhyicalAddressFromPageTableEntry(Curr_Entry);

                TLB.setEntry_LRU(Curr_Addr, Curr_Entry);
                Result = TLB.searchBanks(Curr_Addr);
                Assert.AreEqual(Expected_Result, Result);
            }
        }

        [TestMethod()]
        public void iTLBTest_Invalid()
        {
            const int ENTRIES = 256;

            iTLB TLB = new iTLB();

            //Test Vars
            ulong Curr_Entry;
            ulong Curr_Addr;
            ulong Result;
            ulong Expected_Result;

            //PTE Contains
            //4 Unused Bits
            //4 bit protection
            //1 Valid Bit
            //31 bit page address tag
            //24 bit physical addr
            ulong[] Entries = new ulong[ENTRIES];
            //Evens invalid
            for (int i = 0; i < ENTRIES; i++)
            {
                ulong unused = 0x0;             //4 Unused Bits
                ulong prot = ((ulong)0xF) << 56;         //4 bit protection
                ulong valid = ((ulong)(0x1 & (i % 2))) << 55;        //1 Valid Bit
                ulong tag = (((ulong)(i & 0x07FFFFFFF)) << 24);   //31 bit page address tag
                ulong phys_addr = 0xFFFFFF;    //24 bit physical addr

                Entries[i] = unused | prot | valid | tag | phys_addr;
            }
            //Virtual Address Contains
            //5 bit page index
            //31 bit page address tag
            //12 bit page offset
            ulong[] Virtual_Addrs = new ulong[ENTRIES];
            for (int i = 0; i < ENTRIES; i++)
            {
                Virtual_Addrs[i] =
                    ((ulong)(0x01F & (i % iTLB.ENTRIES)) << 43) |           //5 bit page index (leading bit of 0)
                    ((ulong)(0x07FFFFFFF & i) << 12) |          //31 bit page address tag
                    (0x0FFF);                                //12 bit page offset

            }

            for (int i = 0; i < ENTRIES; i++)
            {
                Curr_Entry = Entries[i];
                Curr_Addr = Virtual_Addrs[i];
                Expected_Result = id_TLBParser.getPhyicalAddressFromPageTableEntry(Curr_Entry);

                TLB.setEntry_LRU(Curr_Addr, Curr_Entry);
                Result = TLB.searchBanks(Curr_Addr);
                if (i % 2 == 0)
                {
                    Assert.AreEqual(Constants.NOT_FOUND, Result);
                }
                else
                {
                    Assert.AreEqual(Expected_Result, Result);
                }
            }
        }

        [TestMethod()]
        public void iTLBTest_LRU()
        {
            const int ENTRIES = 256;

            iTLB TLB = new iTLB();

            //Test Vars
            ulong Curr_Entry;
            ulong Curr_Addr;
            ulong Prev_Addr;
            ulong Result;
            ulong Expected_Result;

            //PTE Contains
            //4 Unused Bits
            //4 bit protection
            //1 Valid Bit
            //31 bit page address tag
            //24 bit physical addr
            ulong[] Entries = new ulong[ENTRIES];
            for (int i = 0; i < ENTRIES; i++)
            {
                ulong unused = 0x0;                         //4 Unused Bits
                ulong prot = ((ulong)0xF) << 56;         //4 bit protection
                ulong valid = ((ulong)0x1) << 55;        //1 Valid Bit
                ulong tag = (((ulong)(i & 0x07FFFFFFF)) << 24);   //31 bit page address tag
                ulong phys_addr = 0xFFFFFF;    //24 bit physical addr

                Entries[i] = unused | prot | valid | tag | phys_addr;

            }
            //Virtual Address Contains
            //5 bit page index
            //31 bit page address tag
            //12 bit page offset
            ulong[] Virtual_Addrs = new ulong[ENTRIES];
            for (int i = 0; i < ENTRIES; i++)
            {
                Virtual_Addrs[i] =
                    ((ulong)(0x01F & (i % iTLB.ENTRIES)) << 43) |       //5 bit page index (leading bit of 0)
                    ((ulong)(0x07FFFFFFF & i) << 12) |       //31 bit page address tag
                    (0x0FFF);                                //12 bit page offset

            }

            for (int i = 0; i < ENTRIES; i++)
            {
                Curr_Entry = Entries[i];
                Curr_Addr = Virtual_Addrs[i];
                if (i < (ENTRIES / 2))
                {
                    Expected_Result = id_TLBParser.getPhyicalAddressFromPageTableEntry(Curr_Entry);

                    TLB.setEntry_LRU(Curr_Addr, Curr_Entry);
                    Result = TLB.searchBanks(Curr_Addr);
                    Assert.AreEqual(Expected_Result, Result);
                }
                else
                {
                    Prev_Addr = Virtual_Addrs[i - (ENTRIES / 2)];
                    Expected_Result = Constants.NOT_FOUND;

                    TLB.setEntry_LRU(Curr_Addr, Curr_Entry);
                    Result = TLB.searchBanks(Prev_Addr);

                    Assert.AreEqual(Expected_Result, Result);
                }
            }
        }
    }
}