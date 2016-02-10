using System;
using System.Linq;
using NUnit.Framework;

namespace MiscCodeTests
{
    public static class ShortGuid
    {
        private static readonly long BaseDate = new DateTime(2016,1,1).Ticks;
        private const double TicksPerSecond = 1.0/TimeSpan.TicksPerSecond;
        private const int BitsAll = 64;
        private const int BitsHalf = BitsAll/2;
        private const int BitsTime = 27;
        private const int BitsRandom = 22;
        private const int BitsCount = BitsAll - (BitsTime + BitsRandom);
        private static readonly int MaxTime = (int) Math.Pow(2, BitsTime);// 27 bits = 134,217,728 seconds ~= 4.36 years
        private static readonly int MaxRandom = (int) Math.Pow(2, BitsRandom);// 22 bits = 4,194,304
        private static readonly int MaxCount = (int) Math.Pow(2, BitsCount);// 15 bits = 32,768
        private static readonly Random _Random = new Random();
        private const int BitsMoveTimeInRaw = BitsAll - BitsTime;
        private const int BitsMoveRandomInRaw = BitsMoveTimeInRaw - BitsCount;
        private static long _count = DateTime.UtcNow.Ticks%MaxCount;

        public static ulong New()
        {
            // time 27 bits (each second for 4.36 years)
            var time = (long)((DateTime.UtcNow.Ticks - BaseDate) * TicksPerSecond) % MaxTime;
            var rand = (long)(_Random.NextDouble()*MaxRandom);
            _count = (_count + 1)%MaxCount;
            long raw = (time << BitsMoveTimeInRaw) | (rand << BitsMoveRandomInRaw) | _count;
            uint a = (uint) raw;
            uint b = (uint) (raw >> BitsHalf);
            ulong val = 0;
            var i = 0;
            var t = 0;
            var r = 0;
            for (; i < BitsAll; ++i)
            {
                ulong curr = (ulong)1 << i;
                if (i%2 == 0)
                {
                    if ((a & (1 << r)) > 0)
                    {
                        val |= curr;
                    }
                    ++r;
                }
                else
                {
                    if ((b & (1 << t)) > 0)
                    {
                        val |= curr;
                    }
                    ++t;
                }
            }
            return val;
        }

    }

    public class ShortGuidTests
    {
        [Test]
        public void CanCreateShortGuid()
        {
            foreach (var i in Enumerable.Range(0,10))
            {
                Console.WriteLine("0x"+ShortGuid.New().ToString("X"));
            }
        }
    }
}