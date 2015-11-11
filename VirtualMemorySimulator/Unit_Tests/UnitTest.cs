using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;



namespace Unit_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            dTLB TLB = new dTLB();

            //PTE Contains
            //4 Unused Bits
            //4 bit protection
            //1 Valid Bit
            //31 bit page address tag
            //24 bit physical addr
            ulong[] Valid_Entry = new ulong[128];
                for (int i = 0; i< 128; i++)
                {
                    Valid_Entry[i] =
                        (0x0 << 60) |       //4 Unused Bits
                        (0x0F << 56) |       //4 bit protection
                        (0x01 << 55) |       //1 Valid Bit
                        ((ulong)(0x07FFFFFFF & i) << 24) |       //31 bit page address tag
                        (0x0FFFFFF);                                //24 bit physical addr
                }
        //Virtual Address Contains
        //5 bit page index
        //31 bit page address tag
        //12 bit page offset
        ulong[] Valid_Virtual_Addr = new ulong[128];
                for (int i = 0; i< 128; i++)
                {
                    Valid_Virtual_Addr[i] =
                        ((ulong)(0x01F & i % 64) << 43) |       //5 bit page index
                        ((ulong)(0x07FFFFFFF & i) << 12) |       //31 bit page address tag
                        (0x0FFFFFF);                                //12 bit page offset

                }

                for (int i = 0; i< 128; i++)
                {
                    TLB.setEntry_LRU(Valid_Virtual_Addr[i], Valid_Entry[i]);
                    Assert.NotEqual(TLB.searchBanks(Valid_Virtual_Addr[i]), Constants.NOT_FOUND);
                }
        
        }
    }
}
