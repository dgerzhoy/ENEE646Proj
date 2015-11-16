using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class SimulationRunner
    {
        private MemoryManagementUnit mmu;

        public SimulationRunner(String filePath, ulong addressSpaceSize, ulong numInstructions, uint loadInstructionFrequency, uint storeInstructionFrequency, uint testBranchFrequency, uint otherInstructionFrequency, String logFilePath, uint numberOfOperands)
        {

            ConfigInfo.VirtualAddressSpaceSize = addressSpaceSize;
            ConfigInfo.LoadInstructionFrequency = loadInstructionFrequency;
            ConfigInfo.NumberOfInstructions = numInstructions;
            ConfigInfo.StoreInstructionFrequency = storeInstructionFrequency;
            ConfigInfo.TestBranchFrequency = testBranchFrequency;
            ConfigInfo.OtherInstructionFrequency = otherInstructionFrequency;
            ConfigInfo.LogFilePath = logFilePath;
            ConfigInfo.NumberOfOperands = numberOfOperands;
            ConfigInfo.InstructionFilePath = filePath;
            mmu = new MemoryManagementUnit();
        }

        public void Run()
        {

            InstructionGenerator.GenerateInstructions(
                ConfigInfo.VirtualAddressSpaceSize,
                ConfigInfo.NumberOfInstructions,
                ConfigInfo.LoadInstructionFrequency,
                ConfigInfo.StoreInstructionFrequency,
                ConfigInfo.TestBranchFrequency,
                ConfigInfo.OtherInstructionFrequency,
                ConfigInfo.NumberOfOperands,
                ConfigInfo.InstructionFilePath);

            ConfigInfo.instructions = InstructionGenerator.parseInstructionFile(ConfigInfo.InstructionFilePath);

            Instruction instruction;
            ulong instructionAddress;
            uint penalty;
            uint result;

            while (ConfigInfo.instructions.Count > 0)
            {
                instruction = ConfigInfo.instructions.Dequeue();
                instructionAddress = AddressGenerator.GenerateVirtualAddress(ConfigInfo.VirtualAddressSpaceSize);

                //dequeue twice
                //make sure instructions addresses have 13th bit set to 0
                // have a PC that increments by 16

                //have an outer loop here

                if (instruction.opcode == ConfigInfo.LOAD_INSTRUCTION)
                {
                    
                    //Fetch Instruction
                    //Fetch operands
                    //for load instructions...use the locality thing for their virtual addresses
                }
                else if (instruction.opcode == ConfigInfo.STORE_INSTRUCTION)
                {
                    //Fetch instructions
                    //Fetch operands
                    //make the block dirty (in mmu)

                }
                else if (instruction.opcode == ConfigInfo.TEST_BRANCH_INSTRUCTION)
                {
                    //No fetch operand
                    //generate new program counter
                    //if branch taken....update PC to braddress
                    //create tweakable random "should we take this branch?"
                    //if branch taken and is first of two, flush second instruction
                }
                else if (instruction.opcode == ConfigInfo.OTHER_INSTRUCTION)
                {
                    //Do nothing

                }
            }
        }
    }
}
