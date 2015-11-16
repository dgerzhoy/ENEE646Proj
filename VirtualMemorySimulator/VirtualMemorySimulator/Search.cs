using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class Search
    {
        private static Search instance;

        private Search() { }

        public static Search Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Search();

                }
                return instance;
            }
        }

        
        
        public static ulong searchCaches(Cache dL1Cache, Cache il1Cache, Cache l2Cache, VL3Cache vl3Cache, uint PhysicalAddr24, ushort PageOffset)
        {
            OneTwentyEightBitInstruction result = OneTwentyEightBitInstruction.BuildInstruction(0,1,2,3,4,5,6,7,8,9,1,2,3,4,5,6);
            
            il1Cache.search(PhysicalAddr24, PageOffset);

            //check if it is found
            if(result == null){
                //result = l2Cache.search(blockTag);

                if(result == null) 
                {
                    //not found
                    //search L3
                    if(false) {//vl3Cache.search()){
                        //put 64 bytes in L2cache
                        //put it in il1cache
                        //copy to instruction register
                    }
                    else
                    {
                        //cache miss
                        //check vpt?
                    }

                }
                else
                {
                    //found
                    //copy instruction to il1cache
                    //copy instruction to instruction register
                    

                }
            }
            else // found
            {
                //copy instruction to instruction register
            }

            return 0;
                
        }

        public static ulong searchDCaches(dL1Cache dl1Cache, Cache il1Cache, L2Cache l2Cache, VL3Cache vl3Cache, ulong blockTag)
        {
            OneTwentyEightBitInstruction result = dl1Cache.search(blockTag);

            //check if it is found
            if(result == null){
                result = l2Cache.search(blockTag);

                if(result == null) 
                {
                    //not found
                    //search L3
                    if(false) {//vl3Cache.search()){
                        //put 64 bytes in L2cache
                        //put it in il1cache
                        //copy to instruction register
                    }
                    else
                    {
                        //cache miss
                    }

                }
                else
                {
                    //found
                    //copy instruction to dl1cache
                    //copy instruction to instruction register
                    

                }
            }
            else // found
            {
                //copy instruction to instruction register
            }

            return 0;
        }
    }
}
