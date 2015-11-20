using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class LogTrace
    {
        private static LogTrace instance;

        private LogTrace() { }

        public static LogTrace Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogTrace();
                }
                return instance;
            }
        }

        public static void WritePC(String filename, String PC)
        {
            FileWriter.WriteStringToFile(filename, "PC: " + PC);
        }

        public static void WriteCurrentInstruction(String filename, String instruction)
        {
            FileWriter.WriteStringToFile(filename, "Instruction: " + instruction);
        }

        public static void WriteCurrentInstruction(String filename, Instruction instruction)
        {
            if (instruction.opcode == ConfigInfo.LOAD_INSTRUCTION)
            {
                FileWriter.WriteStringToFile(filename, "Load Instruction");
            }
            else if (instruction.opcode == ConfigInfo.STORE_INSTRUCTION)
            {
                FileWriter.WriteStringToFile(filename, "Store Instruction");
            }
            else if (instruction.opcode == ConfigInfo.TEST_BRANCH_INSTRUCTION)
            {
                FileWriter.WriteStringToFile(filename, "Test and Branch Instruction");
            }
            else if (instruction.opcode == ConfigInfo.OTHER_INSTRUCTION)
            {
                FileWriter.WriteStringToFile(filename, "Other Instruction");
            }
        }

        public static void WriteiTLBMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "iTLB Miss! ");
        }

        public static void WriteiTLBAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "iTLB Access! ");
        }

        public static void WritedTLBMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "dTLB Miss! ");
        }

        public static void WritedTLBAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "dTLB Access! ");
        }

        public static void WriteTLBMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "TLB Miss! ");
        }

        public static void WriteTLBAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "TLB Access! ");
        }

        public static void WritePTMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Page Table Miss! ");
        }

        public static void WritePTAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Page Table Access! ");
        }

        public static void WriteL2CacheMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "L2Cache Miss! ");
        }

        public static void WriteL2CacheAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "L2Cache Access! ");
        }

        public static void WritedL1CacheMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "dL1Cache Miss! ");
        }

        public static void WritedL1CacheAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "dL1Cache Access! ");
        }

        public static void WriteiL1CacheMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "iL1Cache Miss! ");
        }

        public static void WriteiL1CacheAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "iL1Cache Access! ");
        }

        public static void WriteL3CacheAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "L3Cache Access! ");
        }

        public static void WriteL3CacheMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "L3Cache Miss! ");
        }

        public static void WriteMMAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Main Memory Access! ");
        }

        public static void WriteMMMiss(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Main Memory Miss! ");
        }

        public static void WriteDISKAccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Main Memory Access! ");
        }

        public static void WriteBranchSuccess(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Branch Success! ");
        }

        public static void WriteBranchUnSuccessful(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Branch Not Successful! ");
        }

        public static void PageFault(String filename)
        {
            FileWriter.WriteStringToFile(filename, "Page Fault! ");
        }

        public static void WriteStatus(String filename)
        {

        }

    }
}
