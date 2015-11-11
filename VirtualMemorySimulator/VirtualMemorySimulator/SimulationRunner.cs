using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class SimulationRunner
    {
        private dL1Cache dl1Cache;
        private Cache il1Cache;
        private L2Cache l2Cache;
        private VL3Cache vl3Cache;
        private iTLB itlb;
        private dTLB dtlb;
        private TLB tlb;
        private VirtualPageTable vpt;
        private String filePath;
        private List<Instruction> instructions;

        public SimulationRunner(String filePath)
        {
            dl1Cache = new dL1Cache();
            il1Cache = new Cache(Constants.CACHE_TYPE.iL1Cache);
            l2Cache = new L2Cache();
            vl3Cache = new VL3Cache();
            itlb = new iTLB();
            dtlb = new dTLB();
            tlb = new TLB();
            vpt = new VirtualPageTable();
            this.instructions = parseInstructions(filePath);
        }

        public void Run()
        {
            foreach (Instruction instruction in instructions){

            }
        }

        //parse the file with the instructions triple
        private List<Instruction> parseInstructions(String filePath)
        {
            return null;
        }
    }
}
