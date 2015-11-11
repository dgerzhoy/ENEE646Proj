using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class BlockTagGenerator
    {
        private static BlockTagGenerator instance;

        private BlockTagGenerator() { }

        public static BlockTagGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BlockTagGenerator();

                }
                return instance;
            }
        }

        public static ulong generateBlockTag()
        {
            return 0;
        }
    }
}
