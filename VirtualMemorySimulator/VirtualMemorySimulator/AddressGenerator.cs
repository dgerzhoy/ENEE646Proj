using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class AddressGenerator
    {
        private static AddressGenerator instance;
        private static Random64 random;

        private AddressGenerator() { }

        public static AddressGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddressGenerator();
                    random = new Random64();
                }
                return instance;
            }
        }

        public static ulong GenerateVirtualAddress(ulong virtualAddressSize)
        {
            return random.Next(virtualAddressSize - 1);
        }

        public static ulong GeneratorSixtyBitVirtualPhysicalPair()
        {
            return random.Next(0xFFFFFFFFFFFFFFF - 1);
        }
    }
}
