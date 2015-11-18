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

        public static uint getPhyicalAddressFromPageTableEntry(ulong pageTableEntry)
        {
            return (uint)pageTableEntry & 0x0FFFFFF;
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

        public static ulong generatePTE(ulong VirtualAddress, uint Phys24)
        {
            ulong VAddr36 = VirtualAddress >> 12;
            ulong PTE;
            ulong valid = ((ulong)1 << 53);
            VAddr36 &= 0x01FFFFFFF;//29 bit tag

            PTE = valid | VAddr36 << 24 | ((ulong)Phys24 & 0x0FFFFFF);

            return PTE;
        }


    }
}
