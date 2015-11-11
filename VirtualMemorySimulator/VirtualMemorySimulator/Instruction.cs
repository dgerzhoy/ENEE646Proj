using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class Instruction
    {
        public uint opcode;
        public uint numOperands;
        public ulong branchAddress;

        public Instruction(String instruction)
        {
            parseInstruction(instruction);
        }

        //parse the instruction
        private void parseInstruction(String instruction)
        {
            opcode = 0;
            numOperands = 0;
            branchAddress = 0;
        }
    }
}
