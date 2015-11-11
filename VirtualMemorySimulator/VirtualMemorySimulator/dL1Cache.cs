using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMemorySimulator
{
    public class dL1Cache
    {
        public const int CACHE_HIT_CLOCK_CYCLES = 4;
        private const int SETS = 64;
        private const int BANKS = 8;
        private const int BLOCK_SIZE = 64;
        private byte[][][] entries;

        public dL1Cache()
        {
            entries = new byte[BANKS][][];
            entries[0] = new byte[SETS][];
            entries[1] = new byte[SETS][];
            entries[2] = new byte[SETS][];
            entries[3] = new byte[SETS][];

            for (int i = 0; i < BANKS; i++)
            {
                for (int j = 0; j < SETS; j++)
                {
                    entries[i][j] = new byte[BLOCK_SIZE];
                }
            }
        }

        public ulong getEntry(int bank, int set, int block)
        {
            return entries[bank][set][block];
        }

        public void setEntry(int bank, int set, int block, byte value)
        {
            entries[bank][set][block] = value;
        }

        public OneTwentyEightBitInstruction search(ulong tag)
        {
            int bankCounter = 0;
            int setCounter = 0;
            int wordCounter = 0;
            uint firstFourBytes;
            uint secondFourBytes;
            uint thirdFourBytes;
            uint fourthFourBytes;
            OneTwentyEightBitInstruction instruction;
            /*
            for (bankCounter = 0; bankCounter < SETS; bankCounter++)
            {
                for(setCounter = 0; setCounter < SETS; setCounter++){
                    for (wordCounter = 0; wordCounter < 4; wordCounter++)
                    {
                        if (CacheFieldParser.getValidBitFromEntry(entries[bankCounter][setCounter][0 + 16 * wordCounter], entries[bankCounter][setCounter][1 + 16 * wordCounter], entries[bankCounter][setCounter][2 + 16 * wordCounter], entries[bankCounter][setCounter][3 + 16 * wordCounter]) == 1 &&
                            CacheFieldParser.get24ByteTagFromEntry(entries[bankCounter][setCounter][0 + 16 * wordCounter], entries[bankCounter][setCounter][1 + 16 * wordCounter], entries[bankCounter][setCounter][2 + 16 * wordCounter], entries[bankCounter][setCounter][3 + 16 * wordCounter]) == tag)
                        {
                            {
                                instruction = OneTwentyEightBitInstruction.BuildInstruction(
                                    entries[bankCounter][setCounter][0 + 16 * wordCounter], entries[bankCounter][setCounter][1 + 16 * wordCounter], entries[bankCounter][setCounter][2 + 16 * wordCounter], entries[bankCounter][setCounter][3 + 16 * wordCounter],
                                    entries[bankCounter][setCounter][4 + 16 * wordCounter], entries[bankCounter][setCounter][5 + 16 * wordCounter], entries[bankCounter][setCounter][6 + 16 * wordCounter], entries[bankCounter][setCounter][7 + 16 * wordCounter],
                                    entries[bankCounter][setCounter][8 + 16 * wordCounter], entries[bankCounter][setCounter][9 + 16 * wordCounter], entries[bankCounter][setCounter][10 + 16 * wordCounter], entries[bankCounter][setCounter][11 + 16 * wordCounter],
                                    entries[bankCounter][setCounter][12 + 16 * wordCounter], entries[bankCounter][setCounter][13 + 16 * wordCounter], entries[bankCounter][setCounter][14 + 16 * wordCounter], entries[bankCounter][setCounter][15 + 16 * wordCounter]);
                            }
                        }
                    }
                }
            }*/

            return null;

        }

    }
}
