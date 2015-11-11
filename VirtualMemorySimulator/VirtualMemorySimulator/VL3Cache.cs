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
        private ulong[] entries;
        public const int CACHE_HIT_CLOCK_CYCLES = 16;

        public VL3Cache()
        {
            entries = new ulong[ENTRIES];
        }

        public ulong getEntry(int entry)
        {
            return entries[entry];
        }

        public void setEntry(int entry, ulong value)
        {
            entries[entry] = value;
        }

        public bool search()
        {
            Random random = new Random();
            int rVL3CacheMiss = random.Next(1);

            if (rVL3CacheMiss == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
