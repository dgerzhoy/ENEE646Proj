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
        public static uint itlbHits = 0;
        public static uint dtlbHits = 0;
        public static uint tlbHits = 0;
        public static uint iL1CacheHits = 0;
        public static uint dL1CacheHits = 0;
        public static uint L2CacheHits = 0;
        public static uint L3CacheHits = 0;
        public static uint MemoryAccess = 0;
        public static uint DiskAccess = 0;
        public static ulong Cycles = 0;
        public static ulong Instructions = 0;

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
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteiTLBMiss(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordDTLBMisses()
        {
            dtlbMisses++;
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WritedTLBMiss(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordTLBMisses()
        {
            tlbMisses++;
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteTLBMiss(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordIL1CacheTLBMisses()
        {
            iL1CacheMisses++;
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteiL1CacheMiss(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordDL1CacheTLBMisses()
        {
            dL1CacheMisses++;
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WritedL1CacheMiss(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordL2CacheMissess()
        {
            L2CacheMisses++;
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteL2CacheMiss(ConfigInfo.LogFilePath);
            }
        }
        public static void RecordL3CacheMissess()
        {
            L3CacheMisses++;
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteL3CacheMiss(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordITLBHit()
        {
            itlbHits++;
        }

        public static void RecordDTLBHit()
        {
            dtlbHits++;
        }

        public static void RecordTLBMHit()
        {
            tlbHits++;
        }

        public static void RecordIL1CacheTLBHits()
        {
            iL1CacheHits++;
        }

        public static void RecordDL1CacheTLBHits()
        {
            dL1CacheHits++;
        }

        public static void RecordL2CacheHits()
        {
            L2CacheHits++;
        }
        public static void RecordL3CacheHits()
        {
            L3CacheHits++;
        }

        public static void RecordPageFaults()
        {
            PageFaults++;
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.PageFault(ConfigInfo.LogFilePath);
            }
        }

        public static void RecorditlbAccesses()
        {
            itlbAccesses++;
            RecordCycle(4);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteiTLBAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecorddtlbAccesses()
        {
            dtlbAccesses++;
            RecordCycle(4);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WritedTLBAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordtlbAccesses()
        {
            tlbAccesses++;
            RecordCycle(8);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteTLBAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordiL1CacheAccesses()
        {
            iL1CacheAccesses++;
            RecordCycle(2); //This is 2 because the Virtual Address translation will occure while this cache is doing set indexing. Saving time.
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WritedL1CacheAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecorddL1CacheAccesses()
        {
            dL1CacheAccesses++;
            RecordCycle(4);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WritedL1CacheAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordL2CacheAccessess()
        {
            L2CacheAccesses++;
            RecordCycle(8);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteL2CacheAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordL3CacheAccesses()
        {
            L3CacheAccesses++;
            RecordCycle(16);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteL3CacheAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordMemoryAccess()
        {
            MemoryAccess++;
            RecordCycle(100);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteMMAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordDiskAccess()
        {
            DiskAccess++;
            RecordCycle(100000);
            if (ConfigInfo.Log_Accesses)
            {
                LogTrace.WriteDISKAccess(ConfigInfo.LogFilePath);
            }
        }

        public static void RecordCycle(ulong cycles)
        {
            Cycles += cycles;
        }

        public static void RecordInstructions(ulong instructions)
        {
            Instructions += instructions;
        }

        public static void ResetStatistics()
        {
            itlbMisses = 0;
            dtlbMisses = 0;
            tlbMisses = 0;
            iL1CacheMisses = 0;
            dL1CacheMisses = 0;
            L2CacheMisses = 0;
            L3CacheMisses = 0;
            PageFaults = 0;
            itlbAccesses = 0;
            dtlbAccesses = 0;
            tlbAccesses = 0;
            iL1CacheAccesses = 0;
            dL1CacheAccesses = 0;
            L2CacheAccesses = 0;
            L3CacheAccesses = 0;
            itlbHits = 0;
            dtlbHits = 0;
            tlbHits = 0;
            iL1CacheHits = 0;
            dL1CacheHits = 0;
            L2CacheHits = 0;
            L3CacheHits = 0;
            MemoryAccess = 0;
            DiskAccess = 0;
            Cycles = 0;
            Instructions = 0;
        }
    }
}
