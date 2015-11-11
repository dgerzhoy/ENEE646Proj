using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class TLBSearchUtility
    {
        private static TLBSearchUtility instance;

        private TLBSearchUtility() { }

        public static TLBSearchUtility Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TLBSearchUtility();

                }
                return instance;
            }
        }

        public static ulong SearchiTLB(VirtualPageTable vpt, iTLB itlb, TLB tlb, ulong virtualAddress){
            ulong result = itlb.searchBanks(virtualAddress);
            ulong generatedVirtualAndPhysicalAddressPair;

          
            if (result == Constants.NOT_FOUND)
            {
                //itlb miss
                result = tlb.searchBanks(virtualAddress);

                if (result != Constants.NOT_FOUND)
                {
                    //copy to itlb using LRU
                    return result;
                }
                else
                {

                    //signal tlb miss

                    generatedVirtualAndPhysicalAddressPair = AddressGenerator.GeneratorSixtyBitVirtualPhysicalPair();
                    result = vpt.search(generatedVirtualAndPhysicalAddressPair);

                    if (result == Constants.PAGE_FAULT)
                    {
                        //signal page fault
                        //copy page from disk
                    }
                }
            }
            else
            {

            }
            return 0;
        }
    }
}
