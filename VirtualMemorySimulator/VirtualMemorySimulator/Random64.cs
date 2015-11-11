using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//class borrow from http://www.experts-exchange.com/Programming/Languages/.NET/Q_26103149.html

namespace VirtualMemorySimulator
{
    public class Random64
    {
        private Random _random;

        public Random64()
        {
            _random = new Random();
        }

        public ulong Next()
        {
            return Next(UInt64.MaxValue);
        }

        public ulong Next(ulong maxValue)
        {
            return Next(0, maxValue);
        }

        public ulong Next(ulong minValue, ulong maxValue)
        {
            if (maxValue < minValue)
                throw new ArgumentException();

            return (ulong)(_random.NextDouble() * (maxValue - minValue)) + minValue;
        }
    }
}
