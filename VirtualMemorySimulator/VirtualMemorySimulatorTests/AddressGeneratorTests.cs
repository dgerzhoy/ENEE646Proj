using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMemorySimulator;

namespace VirtualMemorySimulatorTests
{
    [TestClass()]
    public class AddressGeneratorTests
    {

        [TestMethod()]
        public void TestGeneateAddress()
        {
            ulong addressSpaceSize = 10000;
            ulong address;
            for (int i = 0; i < 50; i++)
            {
                address = AddressGenerator.GenerateVirtualAddress(addressSpaceSize);
                if (address < 0 || address >= addressSpaceSize)
                {
                    Assert.IsFalse(true);
                }
            }
        }
    }
}
