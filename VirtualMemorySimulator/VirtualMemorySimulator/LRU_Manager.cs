using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    class LRU_Manager
    {
        private Queue<int>[] LRU;//Contains Bank address of each TLB Entry in LRU order (FIFO) for each entry separately
        private int BANKS;
        private int ENTRIES;

        public LRU_Manager(int Entries, int Banks)
        {
            BANKS = Banks;
            ENTRIES = Entries;

            LRU = new Queue<int>[ENTRIES];
            for(int i = 0; i < ENTRIES; i++)
            {
                LRU[i] = new Queue<int>();
            }

            for (int j = ENTRIES-1; j >= 0; j--)
            {
                for (int i = BANKS-1; i >= 0; i--)
                {
                    LRU[j].Enqueue(i);
                }
            }
        }

        //Set least recently used entry at specified row(index) to value
        public void Set_LRU(ulong index, ref ulong[][] entries, ulong value)
        {
            int count_init = LRU[index].Count;
            int front = LRU[index].Dequeue(); //Pop LRU element for this entry (which bank was LRU)
            int count_after = LRU[index].Count;

            if(count_init != count_after + 1)
            {
                throw new Exception("Too many Bank entries in FIFO, something is wrong with the code. This should never happen");
            }

            LRU[index].Enqueue(front); //Put that bank at the front (newest)
            entries[front][index] = value; //Set the entry
        }

        //Set least recently used entry at specified row(index) to value
        public void Set_LRU(ulong index, ref byte[][][] entries, byte[] value)
        {
            int count_init = LRU[index].Count;
            int front = LRU[index].Dequeue(); //Pop LRU element for this entry (which bank was LRU)
            int count_after = LRU[index].Count;

            if (count_init != count_after + 1)
            {
                throw new Exception("Too many Bank entries in FIFO, something is wrong with the code. This should never happen");
            }

            LRU[index].Enqueue(front); //Put that bank at the front (newest)
            entries[front][index] = value; //Set the entry
        }

        //Move Entry from position in Queue to front
        public void toFront(int bank, ulong index)
        {
            int count = LRU[index].Count(); //Content Size of FIFO
            int hold = -1;
            int b;

            if (count > BANKS)
            {
                throw new Exception("Too many Bank entries in FIFO, something is wrong with the code. This should never happen");
            }

            for (int i = 0; i < count; i++)
            {
                b = LRU[index].Dequeue();
                if (b == bank)
                {
                    hold = b;
                }
                else
                {
                    LRU[index].Enqueue(b);
                }
            }
            if (hold == -1)
            {
                throw new Exception("Bank not found in QUEUE, something is wrong with the code. This should never happen");
            }

            LRU[index].Enqueue(hold);
            return;

        }

    }
}
