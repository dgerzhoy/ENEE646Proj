using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class Block
    {
        //Tuple<int, ushort, byte, uint>
        public int bank;
        public ushort set;
        public byte block_offset;
        public uint tag;

        public Block(int bank, ushort set, byte block_offset, uint tag)
        {

            this.bank = bank;
            this.set = set;
            this.block_offset = block_offset;
            this.tag = tag;

        }

    }
}
