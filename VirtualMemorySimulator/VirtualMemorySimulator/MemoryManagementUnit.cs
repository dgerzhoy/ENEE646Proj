using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class MemoryManagementUnit
    {

        public Cache iL1Cache = new Cache(Constants.CACHE_TYPE.iL1Cache);
        public Cache dL1Cache = new Cache(Constants.CACHE_TYPE.dL1Cache);
        public Cache L2Cache = new Cache(Constants.CACHE_TYPE.L2Cache);

        public iTLB iL1_TLB = new iTLB();
        public dTLB dL1_TLB = new dTLB();
        public TLB L2_TLB = new TLB();

        //VL3 Cache
        //Physical Memory
            //Page Table
        //Disk

        public MemoryManagementUnit()
        {

        }

        public void InstructionFetch(ulong VirtualAddress)
        {

            ulong resultTLB = Constants.NOT_FOUND;
            Block resultCache = null;
            uint PhysicalAddr24 = 0;
            ushort PageOffset = (ushort)(VirtualAddress & 0x0FFF);

            resultTLB = iL1_TLB.searchBanks(VirtualAddress);

            if(resultTLB == Constants.NOT_FOUND)
            {
                resultTLB = L2_TLB.searchBanks(VirtualAddress);
                if(resultTLB == Constants.NOT_FOUND)
                {
                    //ifnull Search PT
                        //ifnull pagefault
                }
            }

            PhysicalAddr24 = (uint)resultTLB;

            resultCache = iL1Cache.search(PhysicalAddr24, PageOffset);

            if(resultCache == null)
            {
                resultCache = L2Cache.search(PhysicalAddr24, PageOffset);

                if(resultCache == null)
                {
                    //search L3
                        //ifnull search main mem
                            //ifnull pagefault
                }

            }

            

        }

        public void OperandFetch(ulong VirtualAddress)
        {

            {

                ulong resultTLB = Constants.NOT_FOUND;
                Block resultCache = null;
                uint PhysicalAddr24 = 0;
                ushort PageOffset = (ushort)(VirtualAddress & 0x0FFF);

                resultTLB = dL1_TLB.searchBanks(VirtualAddress);

                if (resultTLB == Constants.NOT_FOUND)
                {
                    resultTLB = L2_TLB.searchBanks(VirtualAddress);
                    if (resultTLB == Constants.NOT_FOUND)
                    {
                        //ifnull Search PT
                        //ifnull pagefault
                    }
                }

                PhysicalAddr24 = (uint)resultTLB;

                resultCache = dL1Cache.search(PhysicalAddr24, PageOffset);

                if (resultCache == null)
                {
                    resultCache = L2Cache.search(PhysicalAddr24, PageOffset);

                    if (resultCache == null)
                    {
                        //search L3
                        //ifnull search main mem
                        //ifnull pagefault
                    }
                }
            }
        }

    }
}
