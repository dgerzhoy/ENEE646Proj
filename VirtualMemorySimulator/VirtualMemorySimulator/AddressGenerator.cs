using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class AddressGenerator
    {
        public static ulong GenerateVirtualAddress(ulong virtualAddressSize)
        {
            Random64 random = new Random64();
            return random.Next(virtualAddressSize - 1);
        }

        public static ulong GeneratorSixtyBitVirtualPhysicalPair()
        {
            Random64 random = new Random64();
            return random.Next(0xFFFFFFFFFFFFFFF - 1);
        }
    }
}
