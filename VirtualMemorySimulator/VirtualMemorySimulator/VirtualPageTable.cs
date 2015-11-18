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
        //private ulong[] entries;
        //private List<Tuple<ulong,ushort>> entries;
        private Dictionary<ulong, uint > entries;

        public VirtualPageTable()
        {
            //entries = new ulong[ENTRIES];
            entries = new Dictionary<ulong, uint>();
        }

        /*public ulong getEntry(int bank, int entry)
        {
            return entries[entry];
        }

        public void setEntry(int entry, ulong value)
        {
            entries[entry] = value;
        }
        */

        public uint search(ulong VirtualAddr36)
        {
            Random random = new Random();
            int rVPMiss = random.Next(1);

            //Memory Access
            StatisticsGatherer.RecordMemoryAccess();

            if (entries.ContainsKey(VirtualAddr36))
            {
                return entries[VirtualAddr36];
            }

            entries.Add(VirtualAddr36, (uint)random.Next(((1 << 24) - 1)));

            if (rVPMiss == 1)
            {
                //Page fault
                StatisticsGatherer.RecordPageFaults();

            }

            
            return entries[VirtualAddr36];
        }

    }
}
