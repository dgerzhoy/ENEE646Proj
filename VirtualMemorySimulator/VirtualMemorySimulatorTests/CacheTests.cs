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
    public class CacheTests
    {
        [TestMethod()]
        public void ReadBlockTest()
        {
            Cache L1 = new Cache(Constants.CACHE_TYPE.iL1Cache);

            int ENTRIES = L1.SETS * L1.BANKS * 2;
            Block retBlock = null;
            Block[] blocks = new Block[ENTRIES];
            Block inBlock;


            ushort cmpr_set;
            uint cmpr_tag;
            ushort ret_set;
            uint ret_tag;

            Tuple<uint, ushort>[] Phys36 = new Tuple<uint, ushort>[ENTRIES];
            Tuple<uint, ushort> inPhys;

            //Generate blocks and associated physical addresses
            for (int i = 0; i < ENTRIES; i++)
            {
                blocks[i] = new Block(i % L1.BANKS, (ushort)(i % L1.SETS), 0, (uint)(i));
                Phys36[i] = CacheFieldParser.generatePhysicalAddr36Pair(blocks[i], L1.TAG_WIDTH, L1.SET_IDX_WIDTH);
            }

            //Fill cache
            for (int i = 0; i < ENTRIES / 2; i++)
            {
                inBlock = blocks[i];
                retBlock = L1.ReplaceBlock(inBlock);
                Assert.AreEqual(null, retBlock);
            }

            //Read First half
            for (int i = 0; i < ENTRIES / 4; i++)
            {
                inPhys = Phys36[i];

                retBlock = L1.search(inPhys.Item1, inPhys.Item2);
                L1.ReadBlock(retBlock);
            }

            for (int i = ENTRIES / 2; i < (ENTRIES * 3 / 4); i++)
            {
                inBlock = blocks[i];

                retBlock = L1.ReplaceBlock(inBlock);
                Assert.AreEqual(null, retBlock);
            }

            for(int i = 0; i < ENTRIES / 4; i++)
            {
                inBlock = blocks[i];
                inPhys = Phys36[i];

                retBlock = L1.search(inPhys.Item1, inPhys.Item2);

                ret_tag = retBlock.tag;
                ret_set = retBlock.set;
                cmpr_tag = inBlock.tag;
                cmpr_set = inBlock.set;

                Assert.AreEqual(cmpr_tag, ret_tag);
                Assert.AreEqual(cmpr_set, ret_set);
            }


        }

        [TestMethod()]
        public void WriteBlockTest()
        {
            {
                Cache L1 = new Cache(Constants.CACHE_TYPE.iL1Cache);

                int ENTRIES = L1.SETS * L1.BANKS * 2;
                Block retBlock = null;
                Block[] blocks = new Block[ENTRIES];
                Block inBlock;
                Block repBlock;


                ushort cmpr_set;
                uint cmpr_tag;
                ushort ret_set;
                uint ret_tag;

                Tuple<uint, ushort>[] Phys36 = new Tuple<uint, ushort>[ENTRIES];
                Tuple<uint, ushort> inPhys;

                //Generate blocks and associated physical addresses
                for (int i = 0; i < ENTRIES; i++)
                {
                    blocks[i] = new Block(i % L1.BANKS, (ushort)(i % L1.SETS), 0, (uint)(i));
                    Phys36[i] = CacheFieldParser.generatePhysicalAddr36Pair(blocks[i], L1.TAG_WIDTH, L1.SET_IDX_WIDTH);
                }

                //Fill cache
                for (int i = 0; i < ENTRIES / 2; i++)
                {
                    inBlock = blocks[i];
                    retBlock = L1.ReplaceBlock(inBlock);
                    Assert.AreEqual(null, retBlock);
                }

                //Write whole cache (to set dirty flag)
                for (int i = 0; i < ENTRIES / 2; i++)
                {
                    inPhys = Phys36[i];

                    retBlock = L1.search(inPhys.Item1, inPhys.Item2);
                    L1.WriteBlock(retBlock);
                }

                //Replace full cache and check if first iteration is found
                for (int i = 0; i < ENTRIES/2; i++)
                {
                    repBlock = blocks[i+ENTRIES/2]; //2nd Gen blocks
                    inBlock = blocks[i]; //1st gen blocks

                    retBlock = L1.ReplaceBlock(repBlock);

                    ret_tag = retBlock.tag;
                    ret_set = retBlock.set;
                    cmpr_tag = inBlock.tag;
                    cmpr_set = inBlock.set;

                    Assert.AreEqual(cmpr_tag, ret_tag);
                    Assert.AreEqual(cmpr_set, ret_set);
                }


            }
        }

        [TestMethod()]
        public void ReplaceBlockTest()
        {
            Cache L1 = new Cache(Constants.CACHE_TYPE.iL1Cache);

            int ENTRIES = L1.SETS * L1.BANKS * 2;
            Block retBlock = null;
            Block[] blocks = new Block[ENTRIES];
            Block inBlock;
            

            ushort cmpr_set;
            uint cmpr_tag;
            ushort ret_set;
            uint ret_tag;

            Tuple<uint, ushort>[] Phys36 = new Tuple<uint, ushort>[ENTRIES];
            Tuple<uint, ushort> inPhys;

            for (int i = 0; i < ENTRIES; i++)
            {
                blocks[i] = new Block(i%L1.BANKS, (ushort)(i%L1.SETS), 0, (uint)(i));
                Phys36[i] = CacheFieldParser.generatePhysicalAddr36Pair(blocks[i], L1.TAG_WIDTH, L1.SET_IDX_WIDTH);
            }

            for (int i = 0; i < ENTRIES; i++)
            {
                inBlock = blocks[i];
                inPhys = Phys36[i];

                retBlock = L1.ReplaceBlock(inBlock);
                Assert.AreEqual(null, retBlock);

                retBlock = L1.search(inPhys.Item1, inPhys.Item2);

                ret_tag = retBlock.tag;
                ret_set = retBlock.set;
                cmpr_tag = inBlock.tag;
                cmpr_set = inBlock.set;

                Assert.AreEqual(cmpr_tag, ret_tag);
                Assert.AreEqual(cmpr_set, ret_set);
            }
        }

        [TestMethod()]
        public void UpdateBlockTest()
        {
            Cache L1 = new Cache(Constants.CACHE_TYPE.iL1Cache);

            int ENTRIES = L1.SETS * L1.BANKS * 2;
            Block retBlock = null;
            Block[] blocks = new Block[ENTRIES];
            Block inBlock;


            ushort cmpr_set;
            uint cmpr_tag;
            ushort ret_set;
            uint ret_tag;

            Tuple<uint, ushort>[] Phys36 = new Tuple<uint, ushort>[ENTRIES];
            Tuple<uint, ushort> inPhys;

            //Generate blocks and associated physical addresses
            for (int i = 0; i < ENTRIES; i++)
            {
                blocks[i] = new Block(i % L1.BANKS, (ushort)(i % L1.SETS), 0, (uint)(i));
                Phys36[i] = CacheFieldParser.generatePhysicalAddr36Pair(blocks[i], L1.TAG_WIDTH, L1.SET_IDX_WIDTH);
            }

            //Fill cache
            for (int i = 0; i < ENTRIES / 2; i++)
            {
                inBlock = blocks[i];
                retBlock = L1.ReplaceBlock(inBlock);
                Assert.AreEqual(null, retBlock);
            }

            //Read First half
            for (int i = 0; i < ENTRIES / 4; i++)
            {
                inBlock = blocks[i];

                L1.UpdateBlock(inBlock);
            }

            for (int i = ENTRIES / 2; i < (ENTRIES * 3 / 4); i++)
            {
                inBlock = blocks[i];

                retBlock = L1.ReplaceBlock(inBlock);
                Assert.AreEqual(null, retBlock);
            }

            for (int i = 0; i < ENTRIES / 4; i++)
            {
                inBlock = blocks[i];
                inPhys = Phys36[i];

                retBlock = L1.search(inPhys.Item1, inPhys.Item2);

                ret_tag = retBlock.tag;
                ret_set = retBlock.set;
                cmpr_tag = inBlock.tag;
                cmpr_set = inBlock.set;

                Assert.AreEqual(cmpr_tag, ret_tag);
                Assert.AreEqual(cmpr_set, ret_set);
            }


        }

    }
}