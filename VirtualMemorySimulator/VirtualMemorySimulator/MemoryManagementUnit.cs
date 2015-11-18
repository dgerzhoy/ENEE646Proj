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

        public VirtualPageTable vPT = new VirtualPageTable();

        public MemoryManagementUnit()
        {

        }

        public void InstructionFetch(ulong VirtualAddress)
        {

            Block resultCache = null;
            Block transferBlock = null;
            Block DirtyBlock_L1 = null;
            Block DirtyBlock_L2 = null;
            uint PhysicalAddr24 = 0;
            ushort PageOffset = (ushort)(VirtualAddress & 0x0FFF);
            ulong PhysicalAddr36 = 0;
            Tuple<uint, ushort> DirtyAddress;


            #region TLB_TRANSLATION
            /*
            resultTLB = iL1_TLB.searchBanks(VirtualAddress);

            if(resultTLB == Constants.NOT_FOUND) //L1TLB Miss
            {
                resultTLB = L2_TLB.searchBanks(VirtualAddress);
                if(resultTLB == Constants.NOT_FOUND) //L2 TLB Miss
                {
                    //Search Virtual Page Table. This will hit (but simulate pagefaulting)
                    resultvPT = vPT.search(VirtualAddress);

                    resultTLB = TLBEntryParser.generatePTE(VirtualAddress, resultvPT);

                    L2_TLB.setEntry_LRU(VirtualAddress, resultTLB);
                    resultTLB = L2_TLB.searchBanks(VirtualAddress);
                    if (resultTLB == Constants.NOT_FOUND)
                    {
                        throw new Exception("L2 TLB entry Still not found after service");
                    }
                }
                resultTLB = id_TLBParser.generatePTE(VirtualAddress, TLBEntryParser.getPhyicalAddressFromPageTableEntry(resultTLB));
                iL1_TLB.setEntry_LRU(VirtualAddress, resultTLB);
                resultTLB = iL1_TLB.searchBanks(VirtualAddress);
                if(resultTLB == Constants.NOT_FOUND)
                {
                    throw new Exception("L1 TLB entry Still not found after service");
                }
            }
            */
            #endregion

            //Virtual to Physical Translation
            PhysicalAddr24 = iTLB_Translation(VirtualAddress);
            PhysicalAddr36 = CacheFieldParser.generatePhysAddr36(PhysicalAddr24, PageOffset);

            //L1 Search
            resultCache = iL1Cache.search(PhysicalAddr24, PageOffset);

            if (resultCache == null) //L1 Miss
            {
                StatisticsGatherer.RecordIL1CacheTLBMisses();
                //L2 Search
                resultCache = L2Cache.search(PhysicalAddr24, PageOffset);

                if (resultCache == null) //L2 Miss
                {
                    StatisticsGatherer.RecordL2CacheMissess();
                    //search L3 (and Mem/Disk)
                    //Might need to indicate TLB pagefault here.
                    resultCache = vL3Cache.search(PhysicalAddr24, PageOffset);

                    //Replace in L2
                    transferBlock = CacheFieldParser.translateCacheBlock(resultCache, vL3Cache, L2Cache);
                    //Enforce L2 Superset
                    Enforce_L2_Superset(transferBlock);

                    DirtyBlock_L2 = L2Cache.ReplaceBlock(transferBlock);

                    if (DirtyBlock_L2 != null) //Evicted Block From L2 is dirty
                    {
                        DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(DirtyBlock_L2, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                        //Search L3 to simulate an update to L3, will be a hit
                        vL3Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                    }
                    resultCache = transferBlock;
                } else
                {
                    StatisticsGatherer.RecordL2CacheHits();
                }
                
                //Replace block in into iL1 and Writeback evicted block if dirty
                transferBlock = CacheFieldParser.translateCacheBlock(resultCache, L2Cache, iL1Cache);
                DirtyBlock_L1 = iL1Cache.ReplaceBlock(transferBlock);

                if (DirtyBlock_L1 != null) //Evicted block from L1 is dirty
                {
                    transferBlock = CacheFieldParser.translateCacheBlock(DirtyBlock_L1, iL1Cache, L2Cache);
                    DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(transferBlock, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                    //DirtyBlock_L1 = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);

                    if (DirtyBlock_L1 == null) //Dirty Block Missing From L2
                    {
                        throw new Exception("Dirty Block from L1 missing from L2, this cannot happen");
                        /*//Find it in L3 and replace in L2
                        DirtyBlock_L1 = vL3Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                        DirtyBlock_L2 = L2Cache.ReplaceBlock(DirtyBlock_L1);

                        //Search L2 Again
                        DirtyBlock_L1 = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                        if (DirtyBlock_L1 == null)
                        {
                            throw new Exception("L2 Cache is still missing dirty block after miss-service");
                        }

                        if (DirtyBlock_L2 != null) //Evicted Block From L2 is dirty
                        {
                            DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(DirtyBlock_L2, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                            //Search L3 to simulate an update to L3, will be a hit
                            vL3Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                        }*/
                    }
                    L2Cache.UpdateBlock(DirtyBlock_L1);
                }

                resultCache = iL1Cache.search(PhysicalAddr24, PageOffset);
                if (resultCache == null)
                {
                    throw new Exception("L1 Cache is still missing data after miss-service");
                }

                //Push Block to Front of LRU
                iL1Cache.ReadBlock(resultCache);
            } else
            {
                StatisticsGatherer.RecordIL1CacheTLBHits();
            }
        }

        public void OperandFetch(ulong VirtualAddress)
        {
            Block resultCache = null;
            Block transferBlock = null;
            Block DirtyBlock_L1 = null;
            Block DirtyBlock_L2 = null;
            uint PhysicalAddr24 = 0;
            ushort PageOffset = (ushort)(VirtualAddress & 0x0FFF);
            ulong PhysicalAddr36 = 0;
            Tuple<uint, ushort> DirtyAddress;

            //Virtual to Physical Translation
            PhysicalAddr24 = iTLB_Translation(VirtualAddress);
            PhysicalAddr36 = CacheFieldParser.generatePhysAddr36(PhysicalAddr24, PageOffset);

            //L1 Search
            resultCache = dL1Cache.search(PhysicalAddr24, PageOffset);

            if (resultCache == null) //L1 Miss
            {
                StatisticsGatherer.RecordDL1CacheTLBMisses();
                //L2 Search
                resultCache = L2Cache.search(PhysicalAddr24, PageOffset);

                if (resultCache == null) //L2 Miss
                {
                    StatisticsGatherer.RecordL2CacheMissess();
                    //search L3 (and Mem/Disk)
                    //Might need to indicate TLB pagefault here.
                    resultCache = vL3Cache.search(PhysicalAddr24, PageOffset);

                    //Replace in L2
                    transferBlock = CacheFieldParser.translateCacheBlock(resultCache, vL3Cache, L2Cache);
                    //Enforce L2 Superset
                    Enforce_L2_Superset(transferBlock);

                    DirtyBlock_L2 = L2Cache.ReplaceBlock(transferBlock);

                    if (DirtyBlock_L2 != null) //Evicted Block From L2 is dirty
                    {
                        DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(DirtyBlock_L2, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                        //Search L3 to simulate an update to L3, will be a hit
                        vL3Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                    }
                    resultCache = transferBlock;
                } else
                {
                    StatisticsGatherer.RecordL2CacheHits();
                }

                //Replace block in into dL1 and Writeback evicted block if dirty
                transferBlock = CacheFieldParser.translateCacheBlock(resultCache, L2Cache, dL1Cache);
                DirtyBlock_L1 = dL1Cache.ReplaceBlock(transferBlock);

                if (DirtyBlock_L1 != null) //Evicted block from L1 is dirty
                {
                    transferBlock = CacheFieldParser.translateCacheBlock(DirtyBlock_L1, dL1Cache, L2Cache);
                    DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(transferBlock, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                    //DirtyBlock_L1 = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);

                    if (DirtyBlock_L1 == null) //Dirty Block Missing From L2
                    {
                        throw new Exception("Dirty Block from L1 missing from L2, this cannot happen");
                    }
                    L2Cache.UpdateBlock(DirtyBlock_L1);
                }

                resultCache = dL1Cache.search(PhysicalAddr24, PageOffset);
                if (resultCache == null)
                {
                    throw new Exception("L1 Cache is still missing data after miss-service");
                }

                //Push Block to Front of LRU
                dL1Cache.ReadBlock(resultCache);
            } else
            {
                StatisticsGatherer.RecordDL1CacheTLBHits();
            }


        }

        public void OperandStore(ulong VirtualAddress)
        {
            Block resultCache = null;
            Block transferBlock = null;
            Block DirtyBlock_L1 = null;
            Block DirtyBlock_L2 = null;
            uint PhysicalAddr24 = 0;
            ushort PageOffset = (ushort)(VirtualAddress & 0x0FFF);
            ulong PhysicalAddr36 = 0;
            Tuple<uint, ushort> DirtyAddress;

            //Virtual to Physical Translation
            PhysicalAddr24 = iTLB_Translation(VirtualAddress);
            PhysicalAddr36 = CacheFieldParser.generatePhysAddr36(PhysicalAddr24, PageOffset);

            //L1 Search
            resultCache = dL1Cache.search(PhysicalAddr24, PageOffset);

            if (resultCache == null) //L1 Miss
            {
                StatisticsGatherer.RecordDL1CacheTLBMisses();
                //L2 Search
                resultCache = L2Cache.search(PhysicalAddr24, PageOffset);

                if (resultCache == null) //L2 Miss
                {
                    StatisticsGatherer.RecordL2CacheMissess();
                    //search L3 (and Mem/Disk)
                    //Might need to indicate TLB pagefault here.
                    resultCache = vL3Cache.search(PhysicalAddr24, PageOffset);

                    //Replace in L2
                    transferBlock = CacheFieldParser.translateCacheBlock(resultCache, vL3Cache, L2Cache);
                    //Enforce L2 Superset
                    Enforce_L2_Superset(transferBlock);

                    DirtyBlock_L2 = L2Cache.ReplaceBlock(transferBlock);

                    if (DirtyBlock_L2 != null) //Evicted Block From L2 is dirty
                    {
                        DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(DirtyBlock_L2, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                        //Search L3 to simulate an update to L3, will be a hit
                        vL3Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);
                    }
                    resultCache = transferBlock;
                } else
                {
                    StatisticsGatherer.RecordL2CacheHits();
                }

                //Replace block in into dL1 and Writeback evicted block if dirty
                transferBlock = CacheFieldParser.translateCacheBlock(resultCache, L2Cache, dL1Cache);
                DirtyBlock_L1 = dL1Cache.ReplaceBlock(transferBlock);

                if (DirtyBlock_L1 != null) //Evicted block from L1 is dirty
                {
                    transferBlock = CacheFieldParser.translateCacheBlock(DirtyBlock_L1, dL1Cache, L2Cache);
                    DirtyAddress = CacheFieldParser.generatePhysicalAddr36Pair(transferBlock, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                    //DirtyBlock_L1 = L2Cache.search(DirtyAddress.Item1, DirtyAddress.Item2);

                    if (DirtyBlock_L1 == null) //Dirty Block Missing From L2
                    {
                        throw new Exception("Dirty Block from L1 missing from L2, this cannot happen");
                    }
                    L2Cache.UpdateBlock(DirtyBlock_L1);
                }

                resultCache = dL1Cache.search(PhysicalAddr24, PageOffset);
                if (resultCache == null)
                {
                    throw new Exception("L1 Cache is still missing data after miss-service");
                }

                //Push Block to Front of LRU and set dirty
                dL1Cache.WriteBlock(resultCache);
            } else
            {
                StatisticsGatherer.RecordDL1CacheTLBHits();
            }


        }

        private uint iTLB_Translation(ulong VirtualAddress)
        {
            ulong resultTLB;
            uint resultvPT;

            resultTLB = iL1_TLB.searchBanks(VirtualAddress);

            if (resultTLB == Constants.NOT_FOUND) //L1TLB Miss
            {
                StatisticsGatherer.RecordITLBMiss();
                resultTLB = L2_TLB.searchBanks(VirtualAddress);
                if (resultTLB == Constants.NOT_FOUND) //L2 TLB Miss
                {
                    StatisticsGatherer.RecordITLBMiss();

                    //Search Virtual Page Table. This will hit (but simulate pagefaulting)
                    resultvPT = vPT.search(VirtualAddress);

                    resultTLB = TLBEntryParser.generatePTE(VirtualAddress, resultvPT);

                    L2_TLB.setEntry_LRU(VirtualAddress, resultTLB);
                    resultTLB = L2_TLB.searchBanks(VirtualAddress);
                    if (resultTLB == Constants.NOT_FOUND)
                    {
                        throw new Exception("L2 TLB entry Still not found after service");
                    }
                }  else
                {
                    StatisticsGatherer.RecordITLBHit();
                }
                resultTLB = id_TLBParser.generatePTE(VirtualAddress, TLBEntryParser.getPhyicalAddressFromPageTableEntry(resultTLB));
                iL1_TLB.setEntry_LRU(VirtualAddress, resultTLB);
                resultTLB = iL1_TLB.searchBanks(VirtualAddress);
                if (resultTLB == Constants.NOT_FOUND)
                {
                    throw new Exception("L1 TLB entry Still not found after service");
                }
            } else
            {
                StatisticsGatherer.RecordITLBHit();
            }

            return (uint)resultTLB;


        }

        private uint dTLB_Translation(ulong VirtualAddress)
        {
            ulong resultTLB;
            uint resultvPT;

            resultTLB = dL1_TLB.searchBanks(VirtualAddress);

            if (resultTLB == Constants.NOT_FOUND) //L1TLB Miss
            {
                StatisticsGatherer.RecordDTLBMisses();
                resultTLB = L2_TLB.searchBanks(VirtualAddress);
                if (resultTLB == Constants.NOT_FOUND) //L2 TLB Miss
                {
                    StatisticsGatherer.RecordITLBMiss();
                    //Search Virtual Page Table. This will hit (but simulate pagefaulting)
                    resultvPT = vPT.search(VirtualAddress);

                    resultTLB = TLBEntryParser.generatePTE(VirtualAddress, resultvPT);

                    L2_TLB.setEntry_LRU(VirtualAddress, resultTLB);
                    resultTLB = L2_TLB.searchBanks(VirtualAddress);
                    if (resultTLB == Constants.NOT_FOUND)
                    {
                        throw new Exception("L2 TLB entry Still not found after service");
                    }
                } else
                {
                    StatisticsGatherer.RecordITLBHit();
                }
                resultTLB = id_TLBParser.generatePTE(VirtualAddress, TLBEntryParser.getPhyicalAddressFromPageTableEntry(resultTLB));
                dL1_TLB.setEntry_LRU(VirtualAddress, resultTLB);
                resultTLB = dL1_TLB.searchBanks(VirtualAddress);
                if (resultTLB == Constants.NOT_FOUND)
                {
                    throw new Exception("L1 TLB entry Still not found after service");
                }
            } else
            {
                StatisticsGatherer.RecordDTLBHit();
            }

            return (uint)resultTLB;


        }

        //Takes an L2 Block thats going into L2 from L3. Ensures no block leaving L2 is in either L1 Cache
        private void Enforce_L2_Superset(Block inBlock) {

            Block LRU_Block;
            Tuple<uint, ushort> Phys36Pair;
            Block iL1Block;
            Block dL1Block;

            bool done = false;

            while (!done)
            {
                LRU_Block = L2Cache.getLRU(inBlock.set);

                Phys36Pair = CacheFieldParser.generatePhysicalAddr36Pair(LRU_Block, L2Cache.TAG_WIDTH, L2Cache.SET_IDX_WIDTH);

                iL1Block = iL1Cache.search(Phys36Pair.Item1, Phys36Pair.Item2);
                dL1Block = dL1Cache.search(Phys36Pair.Item1, Phys36Pair.Item2);

                if (dL1Block != null | iL1Block != null)
                {
                    L2Cache.ReadBlock(LRU_Block);
                }
                else
                {
                    done = true;
                }
            }
        }

    }
}
