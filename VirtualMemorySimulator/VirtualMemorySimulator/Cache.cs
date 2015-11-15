using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class Cache
    {
        public int SETS;
        public int BANKS;
        public int BLOCK_SIZE;
        public int TAG_WIDTH;
        public int SET_IDX_WIDTH;
        //private byte[][][] entries;
        private uint[][] meta;

        //Comment 1

        private LRU_Manager LRU;

        public Cache(Constants.CACHE_TYPE CacheType)
        {
            if (CacheType.Equals(Constants.CACHE_TYPE.iL1Cache))
            {
                SETS = 128;
                BANKS = 4;
                BLOCK_SIZE = 64;
                TAG_WIDTH = 23;
                SET_IDX_WIDTH = 7;
            }
            else if(CacheType.Equals(Constants.CACHE_TYPE.dL1Cache))
            {
                SETS = 64;
                BANKS = 8;
                BLOCK_SIZE = 64;
                TAG_WIDTH = 24;
                SET_IDX_WIDTH = 6;
            } else if (CacheType.Equals(Constants.CACHE_TYPE.L2Cache))
            {
                SETS = 512;
                BANKS = 8;
                BLOCK_SIZE = 64;
                TAG_WIDTH = 21;
                SET_IDX_WIDTH = 9;
            } else
            {
                throw new Exception("Passed-in Value is not valid Cache_Type Enumeration. This should not be possible");
            }

            //entries = new byte[BANKS][][];
            meta = new uint[BANKS][];

            for (int i = 0; i < BANKS; i++)
            {
                meta[i] = new uint[SETS];
             
                for (int j = 0; j < SETS; j++)
                {
                    meta[i][j] = 0;
                }
            }

            LRU = new LRU_Manager(SETS, BANKS);
        }

        //Read numBytes bytes
        public void ReadBlock (Block BankSetBlockTag)
        {
            int bank = BankSetBlockTag.bank;
            ushort set = BankSetBlockTag.set;

            LRU.toFront(bank, set);

        }
        //Sets number of bytes in array (A Write, not a block replacement)
        public void WriteBlock(Block BankSetBlockTag)
        {
            int bank = BankSetBlockTag.bank;
            ushort set = BankSetBlockTag.set;
            uint Tag = BankSetBlockTag.tag;

            LRU.toFront(bank, set);

            //Set Dirty Bit
            meta[bank][set] = CacheFieldParser.generateMetaEntry(true, true, Tag, TAG_WIDTH);

        }

        //Replaces the LRU block in the set with the block given.
        //Returns evicted block if dirty 
        public Block ReplaceBlock(Block BankSetBlockTag)
        {
            int bank = BankSetBlockTag.bank;   //Bank in cache above
            ushort set = BankSetBlockTag.set;
            uint tag = BankSetBlockTag.tag;

            Block Evicted = null;
            int Evicted_Bank = LRU.Get_LRU(set);
            uint Evicted_Tag;

            if (CacheFieldParser.getDirtyBitFromEntry(meta[Evicted_Bank][set],TAG_WIDTH))
            {
                Evicted_Tag = CacheFieldParser.getTagFromEntry(meta[Evicted_Bank][set],TAG_WIDTH);

                //LRU
                Evicted = new Block(Evicted_Bank, set, 0, Evicted_Tag);
            }
            //Replace old meta data with old bit to zero for this block
            meta[Evicted_Bank][set] = CacheFieldParser.generateMetaEntry(true, false, tag, TAG_WIDTH);
            LRU.toFront(Evicted_Bank, set); //Put block at back of Queue

            return Evicted;
        }

        /// <summary>
        /// Updates block content from lower level block (doesn't actually, because we do not track data, but it would! Instead moves to MRU.
        /// Must find block with tag anb set first, since data comes from another cache
        /// </summary>
        public void UpdateBlock(Block BankSetBlockTag)
        {
            ushort set = BankSetBlockTag.set;
            uint tag = BankSetBlockTag.tag;

            uint Meta_Curr;
            bool Meta_Valid;
            uint Meta_Tag;

            //Find it in this cache
            for (int b = 0; b < BANKS; b++) //Bank Iterator
            {
                Meta_Curr = meta[b][set];
                //Search Metadata at set index
                Meta_Tag = CacheFieldParser.getTagFromEntry(Meta_Curr, TAG_WIDTH);
                if (Meta_Tag == tag)
                {
                    Meta_Valid = CacheFieldParser.getValidBitFromEntry(Meta_Curr, TAG_WIDTH);
                    if (Meta_Valid)
                    {
                        LRU.toFront(b,set);
                        return;
                    }
                }
            }
            //If it gets to this point the cache block being updated is not in this cache, which should not happen. This function can only be used on the superset cache of a dirty cache block
            throw new Exception("UpdateBlock Function: block not found - This function can only be used on the superset cache of a dirty cache block");
        }


        //Returns the Bank Set Block and Tag for the found block. Null if not found.
        public Block search(uint physical_addr_24, ushort page_offset_12)
        {
            uint block_tag;
            byte block_offset;
            ushort set_index;

            //Metadata content
            uint Meta_Curr;
            bool Meta_Valid;
            uint Meta_Tag;

            ulong phys24_tmp = (((ulong)(physical_addr_24 & 0x0FFFFFF)) << 12);
            ulong page_offs12_tmp = ((ulong)(((uint)page_offset_12) & 0x0FFF));
            ulong physical_addr_36 = phys24_tmp | page_offs12_tmp; //23 bit physical address

            block_tag = CacheFieldParser.getTagFromPhysAddr(physical_addr_36, TAG_WIDTH);
            set_index = CacheFieldParser.getSetIdxFromPhysAddr(physical_addr_36, SET_IDX_WIDTH);
            block_offset = CacheFieldParser.getBlockOffsetFromPhysAddr(physical_addr_36);

            for (int b = 0; b < BANKS; b++) //Bank Iterator
            {
                Meta_Curr = meta[b][set_index];
                //Search Metadata at set index
                Meta_Tag = CacheFieldParser.getTagFromEntry(Meta_Curr, TAG_WIDTH);
                if (Meta_Tag == block_tag)
                {
                    Meta_Valid = CacheFieldParser.getValidBitFromEntry(Meta_Curr, TAG_WIDTH);
                    if (Meta_Valid)
                    {
                        //Return bank and all relevant variables to control logic, to be used for read or write
                        return new Block (b, set_index, block_offset, block_tag); 
                    }
                }
            }

            return null;

        }

    }
}
