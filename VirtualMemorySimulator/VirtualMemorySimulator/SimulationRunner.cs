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

                FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "PC: " + PC.ToString());

                for (int i = 0; i < 2 && ConfigInfo.instructions.Count > 0; i++)
                {
                    instruction = ConfigInfo.instructions.Dequeue();

                    if (instruction.opcode == ConfigInfo.LOAD_INSTRUCTION)
                    {
                        
                       FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Fetching Instruction: " + instruction.ToString()); 
                       mmu.InstructionFetch(PC);
                      

                        operandAddresses = LocalityGenerator.GenerateLocality(instruction.branchAddress,
                            ConfigInfo.VirtualAddressSpaceSize,
                            ConfigInfo.DistanceFromInstruction,
                            instruction.numOperands);

                        while (operandAddresses.Count > 0)
                        {
                            var operand = operandAddresses.Dequeue();

                            FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Fetching Operand: " + operand);
                            mmu.OperandFetch(operand);

                            if (operandAddresses.Count > 0)
                            {
                                operand = operandAddresses.Dequeue();

                                FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Fetching Operand: " + operand);
                                mmu.OperandFetch(operand);
                            }  

                            if (operandAddresses.Count > 0)
                            {
                                operand = operandAddresses.Dequeue();

                                FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Fetching Operand: " + operand);
                                mmu.OperandFetch(operand);
                            }
                        }
                    }
                    else if (instruction.opcode == ConfigInfo.STORE_INSTRUCTION)
                    {
                        FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Fetching Instruction: " + instruction.ToString()); 
                        mmu.InstructionFetch(PC);

                        operandAddresses = LocalityGenerator.GenerateLocality(instruction.branchAddress,
                        ConfigInfo.VirtualAddressSpaceSize,
                        ConfigInfo.DistanceFromInstruction,
                        instruction.numOperands);

                        while (operandAddresses.Count > 0)
                        {
                            var operand = operandAddresses.Dequeue();
                            FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Fetching Operand: " + operand);
                            mmu.OperandFetch(operand);
                        }
                    }
                    else if (instruction.opcode == ConfigInfo.TEST_BRANCH_INSTRUCTION)
                    {

                        if (random.Next(0, 100) <= ConfigInfo.PercentBranchTaken)
                        {
                            //branch taken
                            PC = instruction.branchAddress;
                            FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Branch Taken, PC: " + PC);
                            branchTaken = true;
                            break;
                        }

                        FileWriter.WriteStringToFile(ConfigInfo.LogFilePath, "Branch Not Taken, PC: " + PC);
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
