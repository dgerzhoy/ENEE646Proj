using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class Constants
    {
        public const ulong NOT_FOUND = 0xFFFFFFFFFFFFFFFF;
        public const ulong FOUND = 0xFFFFFFFFFFFFFFFE;
        public const ulong PAGE_FAULT = 0xFFFFFFFFFFFFFFFD;
        public const ulong VALID = 0x1;

        public enum CACHE_TYPE
        {
            iL1Cache,
            dL1Cache,
            L2Cache
        }
    }
}
