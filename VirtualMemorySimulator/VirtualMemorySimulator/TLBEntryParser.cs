using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{

    //Helper class that parse the page table entries and extracts the specified fields.
    public class TLBEntryParser
    {
        private static TLBEntryParser instance;

        private TLBEntryParser() { }

        public static TLBEntryParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TLBEntryParser();
                }
                return instance;
            }
        }

        public static ulong getPhyicalAddressFromPageTableEntry(ulong pageTableEntry)
        {
            return pageTableEntry & 0xFFFFFF;
        }

        public static ulong getPageAddressTagFromPageTableEntry(ulong pageTableEntry)
        {
            return (pageTableEntry >> 24) & 0x1FFFFFFF;
        }

        public static ulong getVBitFromPageTableEntry(ulong pageTableEntry)
        {
            return (pageTableEntry >> 53) & 0x1;
        }

        public static ulong getMemoryProtectionFromPageTableEntry(ulong pageTableEntry)
        {
            return (pageTableEntry >> 54) & 0xF;
        }

        public static ulong getPageOffsetFromVirtualAddress(ulong virtualAddress)
        {
            return virtualAddress & 0xFFF;
        }

        public static ulong getPageAddressTagFromVirtualAddress(ulong virtualAddress)
        {
            return (virtualAddress >> 12) & 0x1FFFFFFF;
        }

        public static ulong getPageIndexFromVirtualAddress(ulong virtualAddress)
        {
            return virtualAddress >> 41 & 0x1F;
        }

    }
}
