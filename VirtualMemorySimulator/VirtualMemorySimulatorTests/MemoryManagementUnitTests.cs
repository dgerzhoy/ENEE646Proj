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

            MMU.InstructionFetch(0);


        }

        [TestMethod()]
        public void OperandFetchTest()
        {
            Assert.Fail();
        }
    }
}