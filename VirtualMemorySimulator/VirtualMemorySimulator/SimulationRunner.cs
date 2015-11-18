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

        public SimulationRunner(String filePath, ulong addressSpaceSize, ulong numInstructions, uint loadInstructionFrequency, uint storeInstructionFrequency,
            uint testBranchFrequency,
            uint otherInstructionFrequency,
            String logFilePath,
            uint numberOfOperands,
            uint percentBranchTaken,
            ulong distanceFromInstruction)
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
            ConfigInfo.PercentBranchTaken = percentBranchTaken;
            ConfigInfo.DistanceFromInstruction = distanceFromInstruction;
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
            Queue<ulong> operandAddresses;
            ulong operandAddress;
            Random random = new Random();
            ulong instructionAddress;
            uint penalty;
            uint result;
            ulong PC=0;
            bool branchTaken=false;

            while (ConfigInfo.instructions.Count > 0)
            {

                branchTaken=false;

                operandAddress = AddressGenerator.GenerateVirtualAddress(ConfigInfo.VirtualAddressSpaceSize) | 0x1000;

                for (int i = 0; i < 2 && ConfigInfo.instructions.Count > 0; i++)
                {
                    instruction = ConfigInfo.instructions.Dequeue();

                    if (instruction.opcode == ConfigInfo.LOAD_INSTRUCTION)
                    {
                       mmu.InstructionFetch(PC);

                        operandAddresses = LocalityGenerator.GenerateLocality(operandAddress,
                            ConfigInfo.VirtualAddressSpaceSize,
                            ConfigInfo.DistanceFromInstruction,
                            instruction.numOperands);

                        while (operandAddresses.Count > 0)
                        {
                             mmu.OperandFetch(operandAddresses.Dequeue());
                            operandAddresses.Dequeue();
                            if (operandAddresses.Count > 0)
                                mmu.OperandFetch(operandAddresses.Dequeue());
                                //operandAddresses.Dequeue();

                            if (operandAddresses.Count > 0)
                                mmu.OperandFetch(operandAddresses.Dequeue());
                                //operandAddresses.Dequeue();
                        }
                    }
                    else if (instruction.opcode == ConfigInfo.STORE_INSTRUCTION)
                    {
                        mmu.InstructionFetch(PC);

                        operandAddresses = LocalityGenerator.GenerateLocality(operandAddress,
                        ConfigInfo.VirtualAddressSpaceSize,
                        ConfigInfo.DistanceFromInstruction,
                        instruction.numOperands);

                        while (operandAddresses.Count > 0)
                        {
                            mmu.OperandFetch(operandAddresses.Dequeue());
                           // operandAddresses.Dequeue();
                        }
                        
                        //Fetch instructions
                        //Fetch operands
                        //make the block dirty (in mmu)

                    }
                    else if (instruction.opcode == ConfigInfo.TEST_BRANCH_INSTRUCTION)
                    {

                        if (random.Next(0, 100) <= ConfigInfo.PercentBranchTaken)
                        {
                            //branch taken
                            PC = instruction.branchAddress;
                            branchTaken = true;
                            break;
                        }
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

                if (branchTaken == true)
                {
                    continue;
                }
                  

                PC += 16;
                if (((PC & 0x1000) >> 12) == 0x1)
                {
                    PC += 0x1000;
                }
            }
        }
    }
}
