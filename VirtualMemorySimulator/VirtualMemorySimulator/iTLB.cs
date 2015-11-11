using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{

    public class iTLB
    {
        public const int ENTRIES = 32;
        public const int BANKS = 4;
        private ulong[][] entries;
        private LRU_Manager LRU;

        public iTLB(){
            entries = new ulong[BANKS][];
            entries[0] = new ulong[ENTRIES];
            entries[1] = new ulong[ENTRIES];
            entries[2] = new ulong[ENTRIES];
            entries[3] = new ulong[ENTRIES];

            LRU = new LRU_Manager(ENTRIES, BANKS);
        }

        //gets entry in specifed bank and entry (row)
        public ulong getEntry(int bank, int entry)
        {
            return entries[bank][entry];
        }

        //sets entry in specifed bank and entry (row)
        public void setEntry(int bank, int entry, ulong value)
        {
            entries[bank][entry] = value;
        }

        //sets Least Recently Used Entry to Value
        public void setEntry_LRU(ulong virtualAddress, ulong value)
        {

            ulong index = id_TLBParser.getPageIndexFromVirtualAddress(virtualAddress); //Get index

            LRU.Set_LRU(index, ref entries, value);

        }


        //looks for the virtual address in all banks per the project description
        //if the the page address tag is not found or it is found but the valid bit is not set
        //return the constant NOT_FOUND
        public ulong searchBanks(ulong virtualAddress)
        {
            ulong index = id_TLBParser.getPageIndexFromVirtualAddress(virtualAddress);
            ulong pageAddressTag = id_TLBParser.getPageAddressTagFromVirtualAddress(virtualAddress);
            ulong pageAddressTagFromPTE = Constants.NOT_FOUND;
            ulong PTE_Tag;
            ulong valid;

            for (int b = 0; b < BANKS; b++)
            {
                valid = id_TLBParser.getVBitFromPageTableEntry(entries[b][index]);
                PTE_Tag = id_TLBParser.getPageAddressTagFromPageTableEntry(entries[b][index]);
                if (valid == Constants.VALID && pageAddressTag == PTE_Tag)
                {
                    pageAddressTagFromPTE = id_TLBParser.getPhyicalAddressFromPageTableEntry(entries[b][index]);

                    //Find entry in queue and move to front
                    LRU.toFront(b, index);

                    break;
                }
            }

            return pageAddressTagFromPTE;
        }
    }
}
