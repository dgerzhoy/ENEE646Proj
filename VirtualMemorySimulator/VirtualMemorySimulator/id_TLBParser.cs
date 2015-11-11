﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class id_TLBParser
    {
        private static id_TLBParser instance;

        private id_TLBParser() { }

        public static id_TLBParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new id_TLBParser();
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
            return (pageTableEntry >> 24 ) & 0x7FFFFFFF;
        }

        public static ulong getVBitFromPageTableEntry(ulong pageTableEntry)
        {
            return (pageTableEntry >> 55 )& 0x1;
        }

        public static ulong getMemoryProtectionFromPageTableEntry(ulong pageTableEntry)
        {
            return (pageTableEntry >> 56) & 0xF;
        }

        public static ulong getPageOffsetFromVirtualAddress(ulong virtualAddress)
        {
            return virtualAddress & 0xFFF;
        }

        public static ulong getPageAddressTagFromVirtualAddress(ulong virtualAddress)
        {
            return (virtualAddress >> 12) & 0x7FFFFFFF;
        }

        public static ulong getPageIndexFromVirtualAddress(ulong virtualAddress)
        {
            return virtualAddress >> 43 & 0x1F;
        }

    }
}