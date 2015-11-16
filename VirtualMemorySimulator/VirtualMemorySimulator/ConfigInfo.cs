using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class ConfigInfo
    {
        private static ConfigInfo instance;

        public static uint LOAD_INSTRUCTION = 0;
        public static uint STORE_INSTRUCTION = 1;
        public static uint TEST_BRANCH_INSTRUCTION = 2;
        public static uint OTHER_INSTRUCTION =3;
        public static uint ITLB_MISS = 4;
        public static uint DTLB_MISS = 5;
        public static uint TLB_MISS = 6;
        public static uint IL1CACHE_MISS = 7;
        public static uint DL1CACHE_MISS = 8;
        public static uint L2CACHE_MISS = 9;
        public static uint L3CACHE_MISS = 10;
        public static uint PAGE_FAULT = 11;


        public static ulong VirtualAddressSpaceSize;
        public static ulong NumberOfInstructions;
        public static uint LoadInstructionFrequency;
        public static uint StoreInstructionFrequency;
        public static uint TestBranchFrequency;
        public static uint OtherInstructionFrequency;
        public static uint NumberOfOperands;
        public static String InstructionFilePath;
        public static String LogFilePath;
        public static Queue<Instruction> instructions;

        private ConfigInfo() { }

        public static ConfigInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigInfo();


                }
                return instance;
            }
        }

    }
}
