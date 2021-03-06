﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class InstructionGenerator
    {

        public static Queue<Instruction> parseInstructionFile(String filename)
        {

            Queue<Instruction> instructions = new Queue<Instruction>();
            using (StreamReader sr = new StreamReader(filename))
            {
                while (sr.Peek() >= 0)
                {
                    instructions.Enqueue(parseInstruction(sr.ReadLine()));
                }
            }

            return instructions;
        }

        public static Instruction parseInstruction(String line)
        {
            line = line.Replace("(", "");
            line = line.Replace(")", "");

            var values = line.Split(',');
            
            return new Instruction(Byte.Parse(values[0]), Byte.Parse(values[1]), UInt64.Parse(values[2]));
            
        }

        public static void GenerateInstructions(ulong AddressSpaceSize, ulong numberOfInstructions,
            uint loadFrequency,
            uint storeFrequency,
            uint testBranchFrequency,
            uint otherFrequency,
            uint numberOfOperands,
            String filename)
        {

            uint loadStart = 0;
            uint loadEnd = loadFrequency;
            uint storeStart = loadEnd + 1;
            uint storeEnd = storeStart + storeFrequency;
            uint testBranchStart = storeEnd + 1;
            uint testBranchEnd = testBranchStart + testBranchFrequency;
            uint otherStart = testBranchEnd + 1;
            uint otherEnd = otherStart + otherFrequency;

            ulong address;

            ulong value;
            ulong operandNumberForInstruction;

            ulong counter = 0;
            Random64 random = new Random64();

            //make sure that test and branch instructions have a destination address with 13th
            //bit set = 0
            //conversely when you generate operand address 


            while (counter < numberOfInstructions)
            {
               /* while ((address = random.Next(0, AddressSpaceSize - 1)) % 64 != 0)
                    continue;*/

                address = random.Next(0, (AddressSpaceSize - 1)/64);
                address *= 64;

                if (address % 64 != 0)
                {
                    throw new Exception("Address is not 64 bit aligned");
                }

                operandNumberForInstruction = random.Next(1, numberOfOperands);

                value = random.Next(0, (ulong)(loadFrequency + storeFrequency + testBranchFrequency + otherFrequency));

                if (value >= loadStart && value <= loadEnd)
                {
                    FileWriter.WriteStringToFile(filename, "(0," + operandNumberForInstruction + "," + address + ")");
                }
                else if (value >= storeStart && value <= storeEnd)
                {
                    FileWriter.WriteStringToFile(filename, "(1," + operandNumberForInstruction + "," + address + ")");
                }
                else if (value >= testBranchStart && value <= testBranchEnd)
                {
                    address = address & 0xFFFFFFFFFFFFEFFF;
                    FileWriter.WriteStringToFile(filename, "(2," + operandNumberForInstruction + "," + address + ")");
                }
                else if (value >= otherStart && value <= otherEnd)
                {
                    FileWriter.WriteStringToFile(filename, "(3," + operandNumberForInstruction + "," + address + ")");
                }
                counter++;
            }
        }
    }
}
