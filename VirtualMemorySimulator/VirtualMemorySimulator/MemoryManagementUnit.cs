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
        public VL3Cache vL3Cache = new VL3Cache();
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
            Block transferBlock = null;
            Block DirtyBlock = null;
            uint PhysicalAddr24 = 0;
            ushort PageOffset = (ushort)(VirtualAddress & 0x0FFF);
            ulong PhysicalAddr36 = 0;
            Tuple<uint, ushort> DirtyAddress;

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

            //PhysicalAddr24 = (uint)resultTLB;
            //PhysicalAddr36 = (((ulong)PhysicalAddr24) << 12) | PageOffset);

            PhysicalAddr36 = CacheFieldParser.generatePhysAddr36(PhysicalAddr24, PageOffset);

            resultCache = iL1Cache.search(PhysicalAddr24, PageOffset);

            if (resultCache == null)
            {
                resultCache = L2Cache.search(PhysicalAddr24, PageOffset);

                if (resultCache == null)
                {
                    //search L3
                    //vL3Cache.search(CacheFieldParser.getTagFromPhysAddr(PhysicalAddr24, PageOffset));
                    //ifnull search main mem
                    //ifnull pagefault
                }
                else
                {
                    //Replace block in into iL1 and Writeback evicted block if dirty
                    transferBlock = CacheFieldParser.translateCacheBlock(resultCache, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH, iL1Cache.TAG_WIDTH, iL1Cache.SET_IDX_WIDTH);
                    DirtyBlock = iL1Cache.ReplaceBlock(transferBlock);

                    if (DirtyBlock != null)
                    {
                        transferBlock = CacheFieldParser.translateCacheBlock(DirtyBlock, iL1Cache.TAG_WIDTH, iL1Cache.SET_IDX_WIDTH, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                        DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(DirtyBlock, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                        DirtyBlock = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);

                        if (DirtyBlock == null)
                        {
                            //Find it in L3 and replace in L2

                            //Search again
                            DirtyBlock = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                            if (DirtyBlock == null)
                            {
                                throw new Exception("L2 Cache is still missing dirty block after miss-service");
                            }
                        }

                        L2Cache.UpdateBlock(DirtyBlock);


                    }

                    resultCache = iL1Cache.search(PhysicalAddr24, PageOffset);
                    if (resultCache == null)
                    {
                        throw new Exception("L1 Cache is still missing data after miss-service");
                    }

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

        public void WriteBack(Block DirtyBlock, Cache StartCache)
        {
            Block transferBlock = null;
            Tuple<uint, ushort> DirtyAddress;

            if (DirtyBlock == null)
            {
                return;
            }

            transferBlock = CacheFieldParser.translateCacheBlock(DirtyBlock, iL1Cache.TAG_WIDTH, iL1Cache.SET_IDX_WIDTH, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

            DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(DirtyBlock, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

            DirtyBlock = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);

            if (DirtyBlock == null)
            {
                //Find it in L3 and replace in L2

                //Search again
                DirtyBlock = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                if (DirtyBlock == null)
                {
                    throw new Exception("L2 Cache is still missing dirty block after miss-service");
                }
            }
            else
            {
                //Found in L3 Replace in L2 now
                transferBlock = CacheFieldParser.translateCacheBlock(DirtyBlock, vL3Cache.TAG_WIDTH, vL3Cache.SET_IDX_WIDTH, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);
                DirtyBlock = iL1Cache.ReplaceBlock(transferBlock);
            }

            L2Cache.UpdateBlock(DirtyBlock);

        }

    }
}
