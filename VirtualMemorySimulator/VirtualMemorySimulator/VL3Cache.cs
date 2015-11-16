using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class VL3Cache
    {
        private const int ENTRIES = 1024;
        //private ulong[] entries;
        List<Block> entries;
        public const int CACHE_HIT_CLOCK_CYCLES = 16;

        public int TAG_WIDTH = 30;
        public int SET_IDX_WIDTH = 0;

        public VL3Cache()
        {
            //entries = new ulong[ENTRIES];
            entries = new List<Block>();
        }
        
        /*
        public ulong getEntry(int entry)
        {
            return entries[entry];
        }

        public void setEntry(int entry, ulong value)
        {
            entries[entry] = value;
        }*/

        public Block search(uint PhysicalAddr24, ushort PageOffset)
        {
            Random random = new Random();
            int rVL3CacheMiss = random.Next(1);
            int rMainMemoryMiss;
            ulong PhysicalAddr36 = CacheFieldParser.generatePhysAddr36(PhysicalAddr24, PageOffset);

            uint tag = CacheFieldParser.getTagFromPhysAddr(PhysicalAddr36, TAG_WIDTH);
            ushort set = CacheFieldParser.getSetIdxFromPhysAddr(PhysicalAddr36, SET_IDX_WIDTH);
            byte Block_Offset = CacheFieldParser.getBlockOffsetFromPhysAddr(PhysicalAddr36);


            Block retblock = new Block(0, set, Block_Offset, tag);

            if (entries.Contains(retblock))
            {
                return retblock;
            }

            if (rVL3CacheMiss == 1)
            {

                //Record V3 Cache Miss
                rMainMemoryMiss = random.Next(1);
                if(rMainMemoryMiss == 1)
                {
                    //Page Fault
                } else
                {
                    //Main Memory Miss
                }

            }
            else
            {
                //Record V3 Hit
                entries.Add(retblock);
                
            }

            return retblock;
        }
    }
}
