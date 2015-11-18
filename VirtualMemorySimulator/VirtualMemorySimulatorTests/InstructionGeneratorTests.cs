using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualMemorySimulator;

namespace VirtualMemorySimulatorTests
{
    [TestClass()]
    public class InstructionGeneratorTests
    {

        [TestMethod()]
        public void TestGeneateInstructionParse()
        {
            Instruction instruction;

            instruction = InstructionGenerator.parseInstruction("(1,1,1000)");
            Assert.IsTrue(instruction.opcode == 1);
            Assert.IsTrue(instruction.numOperands == 1);
            Assert.IsTrue(instruction.branchAddress == 1000);
        }
    }
}
