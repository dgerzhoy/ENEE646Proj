using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class Instruction
    {
        public byte opcode;
        public byte numOperands;
        public ulong branchAddress;

        public Instruction(byte opcode, byte numOperands, ulong branchAddress)
        {
            this.opcode =opcode;
            this.numOperands = numOperands;
            this.branchAddress = branchAddress;
        }
    }
}
