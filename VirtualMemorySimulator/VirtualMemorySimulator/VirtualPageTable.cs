using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class VirtualPageTable
    {
        private const int ENTRIES = 1024;
        private const int BANKS = 4;
        private ulong[] entries;

        public VirtualPageTable()
        {
            entries = new ulong[ENTRIES];
        }

        public ulong getEntry(int bank, int entry)
        {
            return entries[entry];
        }

        public void setEntry(int entry, ulong value)
        {
            entries[entry] = value;
        }

        public ulong search(ulong virtualAndPhysicalAddressPair)
        {
            Random random = new Random();
            int rVPMiss = random.Next(1);

            if (rVPMiss == 1)
            {
                return Constants.PAGE_FAULT;
            }
            else
            {
                return Constants.FOUND;
            }

        }

    }
}
