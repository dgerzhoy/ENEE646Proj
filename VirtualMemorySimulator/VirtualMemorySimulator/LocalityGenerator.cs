using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class LocalityGenerator
    {
        private static LocalityGenerator instance;

        private LocalityGenerator() { }

        public static LocalityGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocalityGenerator();
                }
                return instance;
            }
        }

        public static Queue<ulong> GenerateLocality(ulong address, ulong addressSpaceSize, ulong distance, ulong numberOfAddresses)
        {
            Queue<ulong> queue = new Queue<ulong>();
            Random64 random = new Random64();
            ulong addressToAdd;
            ulong upperBound = (address + distance) < addressSpaceSize ? address + distance : addressSpaceSize;
            ulong lowerBound = (address - distance) > 0 ? address - distance : 0;


            queue.Enqueue(address);

            for (ulong i = 0; i < numberOfAddresses-1; i++)
            {
                if (i % 2 == 0)
                {
                    
                    while((addressToAdd = random.Next(address, upperBound)) % 64 != 0){
                        
                    }
                    queue.Enqueue(addressToAdd);
                }
                else
                {
                    while ((addressToAdd = random.Next(lowerBound, address)) % 64 != 0)
                    {

                    }
                    queue.Enqueue(addressToAdd);

                }
            }


            return queue;
        }
    }
}
