using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class FileWriter
    {

        private static FileWriter instance;

        private FileWriter() { }

        public static FileWriter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileWriter();
                }
                return instance;
            }
        }

        public static void WriteStringToFile(String filename, String writeString)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, true))
            {
                file.WriteLine(writeString);
            }
        }
    }
}
