using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class VirtualPageTableParser
    {
        private static VirtualPageTableParser instance;

        private VirtualPageTableParser() { }

        public static VirtualPageTableParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VirtualPageTableParser();
                }
                return instance;
            }
        }

        public static ulong getVirutalAddressFromEntry(ulong pageTableEntry)
        {
            return (pageTableEntry >> 24) & 0x1FFFFFFF;
        }

        public static ulong getPhysicalAddressFromEntry(ulong pageTableEntry)
        {
            return pageTableEntry & 0xFFFFFF;
        }

    }
}
