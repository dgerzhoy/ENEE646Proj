using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class Cache
    {
        private int SETS;
        private int BANKS;
        private int BLOCK_SIZE;
        private int TAG_WIDTH;
        private int SET_IDX_WIDTH;
        private byte[][][] entries;
        private uint[][] meta;

        //Comment

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
                BANKS = 5;
                BLOCK_SIZE = 64;
                TAG_WIDTH = 21;
                SET_IDX_WIDTH = 9;
            } else
            {
                throw new Exception("Passed-in Value is not valid Cache_Type Enumeration. This should not be possible");
            }

            entries = new byte[BANKS][][];
            meta = new uint[BANKS][];

            for (int i = 0; i < BANKS; i++)
            {
                entries[i] = new byte[SETS][];
                meta[i] = new uint[SETS];
                for (int j = 0; j < SETS; j++)
                {
                    entries[i][j] = new byte[BLOCK_SIZE];
                }
            }

            LRU = new LRU_Manager(SETS, BANKS);
        }

        //Read numBytes bytes
        private byte[] ReadBytes(Tuple<int, ushort, byte, uint> BankSetBlockTag, int numBytes)
        {
            int bank = BankSetBlockTag.Item1;
            ushort set = BankSetBlockTag.Item2;
            byte block = BankSetBlockTag.Item3;

            byte[] out_bytes = new byte[numBytes];

            for (int i = 0; i < numBytes; i++)
            {
                out_bytes[i] = entries[bank][set][block + i];
            }

            LRU.toFront(bank, set);

            return out_bytes;
        }
        //Sets number of bytes in array (A Write, not a block replacement)
        private void WriteBytes(Tuple<int, ushort, byte, uint> BankSetBlockTag, byte[] value)
        {
            int bank = BankSetBlockTag.Item1;
            ushort set = BankSetBlockTag.Item2;
            byte block = BankSetBlockTag.Item3;
            uint Tag = BankSetBlockTag.Item4;

            for (int i = 0; i < value.Count(); i++)
            {
                entries[bank][set][block++] = value[i];
            }
            LRU.toFront(bank, set);

            //Set Dirty Bit
            meta[bank][set] = CacheFieldParser.generateMetaEntry(true, true, Tag, TAG_WIDTH);

        }


        //Read 1 Byte (8 bits)
        public byte ReadByte(Tuple<int, ushort, byte, uint> BankSetBlockTag)
        {
            int bank = BankSetBlockTag.Item1;
            ushort set = BankSetBlockTag.Item2;
            byte block = BankSetBlockTag.Item3;

            LRU.toFront(bank, set);

            return entries[bank][set][block];
        }

        //Read 4 bytes (32 bits)
        public byte[] Read32(Tuple<int, ushort, byte, uint> BankSetBlockTag)
        {
            return ReadBytes(BankSetBlockTag, 4);
        }

        //Read 8 bytes (64 bits)
        public byte[] Read64(Tuple<int, ushort, byte, uint> BankSetBlockTag)
        {
            return ReadBytes(BankSetBlockTag, 8);
        }

        //Read 16 bytes (128 bits)
        public byte[] Read128(Tuple<int, ushort, byte, uint> BankSetBlockTag)
        {
            return ReadBytes(BankSetBlockTag, 8);
        }

        //Read Entire Block (Used to copy data from cache to cache)
        //Also unsets the dirty bit
        public byte[] WriteBackBlock(Tuple<int, ushort, byte, uint> BankSetBlockTag)
        {
            int bank = BankSetBlockTag.Item1;
            ushort set = BankSetBlockTag.Item2;
            byte block = BankSetBlockTag.Item3;
            uint tag = BankSetBlockTag.Item4;

            //Set Dirty bit to zero for this block
            meta[bank][set] = CacheFieldParser.generateMetaEntry(true, false, tag, TAG_WIDTH);
            LRU.toFront(bank,set);

            return entries[bank][set];
        }


        //Returns the BankSetBlockTag tuple if the
        public Tuple<int, ushort, byte, uint> search(uint physical_addr_24, ushort page_offset_12)
        {
            uint block_tag;
            byte block_offset;
            ushort set_index;

            //Metadata content
            uint Meta_Curr;
            bool Meta_Valid;
            bool Meta_Dirty;
            uint Meta_Tag;

            ulong physical_addr_36 = (((ulong)(physical_addr_24 & 0x0FFFFFF)) << 13) | ((ulong)(((uint)page_offset_12) & 0x0FFF)); //23 bit physical address

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
                        return new Tuple<int, ushort, byte, uint>(b, set_index, block_offset, block_tag); 
                    }
                }
            }

            return null;

        }

    }
}
