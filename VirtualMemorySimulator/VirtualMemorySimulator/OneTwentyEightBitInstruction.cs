using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class OneTwentyEightBitInstruction
    {
        public uint u0;
        public uint u1;
        public uint u2;
        public uint u3;

        public OneTwentyEightBitInstruction(uint u0, uint u1, uint u2, uint u3)
        {
            this.u0 = u0;
            this.u1 = u1;
            this.u2 = u2;
            this.u3 = u3;
        }

        public static OneTwentyEightBitInstruction BuildInstruction(uint b0,
                                                   uint b1,
                                                   uint b2,
                                                   uint b3,
                                                   uint b4,
                                                   uint b5,
                                                   uint b6,
                                                   uint b7,
                                                   uint b8,
                                                   uint b9,
                                                   uint b10,
                                                   uint b11,
                                                   uint b12,
                                                   uint b13,
                                                   uint b14,
                                                   uint b15){
            uint firstFour = (b0 << 24) | (b1 << 16) | (b2 << 8) | b3;
            uint secondFour = (b4 << 24) | (b5 << 16) | (b6 << 8) | b7;
            uint thirdFour = (b8 << 24) | (b9 << 16) | (b10 << 8) | b11;
            uint fourthFour = (b12 << 24) | (b13 << 16) | (b14 << 8) | b15;

            return new OneTwentyEightBitInstruction(firstFour, secondFour, thirdFour, fourthFour);
        }
    }
}
