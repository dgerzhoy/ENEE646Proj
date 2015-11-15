using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class StatisticsGatherer
    {
        private static StatisticsGatherer instance;

        public static uint itlbMisses = 0;
        public static uint dtlbMisses = 0;
        public static uint tlbMisses = 0;
        public static uint iL1CacheMisses = 0;
        public static uint dL1CacheMisses = 0;
        public static uint L2CacheMisses = 0;
        public static uint L3CacheMisses = 0;
        public static uint PageFaults = 0;
        public static uint itlbAccesses = 0;
        public static uint dtlbAccesses = 0;
        public static uint tlbAccesses = 0;
        public static uint iL1CacheAccesses = 0;
        public static uint dL1CacheAccesses = 0;
        public static uint L2CacheAccesses = 0;
        public static uint L3CacheAccesses = 0;
        public static uint MemoryAccess = 0;
        public static uint DiskAccess = 0;

        private StatisticsGatherer() { }

        public static StatisticsGatherer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatisticsGatherer();
                }
                return instance;
            }
        }

        public static void RecordITLBMiss()
        {
            itlbMisses++;
        }

        public static void RecordDTLBMisses()
        {
            dtlbMisses++;
        }

        public static void RecordTLBMisses()
        {
            tlbMisses++;
        }

        public static void RecordIL1CacheTLBMisses()
        {
            iL1CacheMisses++;
        }

        public static void RecordDL1CacheTLBMisses()
        {
            dL1CacheMisses++;
        }

        public static void RecordL2CacheMissess()
        {
            L2CacheMisses++;
        }
        public static void RecordL3CacheMissess()
        {
            L3CacheMisses++;
        }

        public static void RecordPageFaults()
        {
            PageFaults++;
        }

        public static void RecorditlbAccesses()
        {
            itlbAccesses++;
        }

        public static void RecorddtlbAccesses()
        {
            dtlbAccesses++;
        }

        public static void RecordtlbAccesses()
        {
            tlbAccesses++;
        }

        public static void RecordiL1CacheAccesses()
        {
            iL1CacheAccesses++;
        }

        public static void RecorddL1CacheAccesses()
        {
            dL1CacheAccesses++;
        }

        public static void RecordL2CacheAccessess()
        {
            L2CacheAccesses++;
        }

        public static void RecordL3CacheAccesses()
        {
            L3CacheAccesses++;
        }

        public static void RecordMemoryAccess()
        {
            MemoryAccess++;
        }

        public static void RecordDiskAccess()
        {
            DiskAccess++;
        }
    }
}
