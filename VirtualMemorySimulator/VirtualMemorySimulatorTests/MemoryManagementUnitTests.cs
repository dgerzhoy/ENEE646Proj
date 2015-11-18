using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualMemorySimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator.Tests
{
    [TestClass()]
    public class MemoryManagementUnitTests
    {

        [TestMethod()]
        public void InstructionFetchTest_CacheOnly()
        {

            MemoryManagementUnit MMU = new MemoryManagementUnit();

            for(ulong i = 0; i < ((ulong)1<<48); i++)
            {
                MMU.InstructionFetch(i);
                MMU.OperandFetch(i);
                MMU.OperandStore(i);
            }
            


        }

        [TestMethod()]
        public void OperandFetchTest()
        {
            Assert.Fail();
        }
    }
}