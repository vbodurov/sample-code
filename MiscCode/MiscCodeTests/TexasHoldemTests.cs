using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class TexasHoldemTests
    {
        [Test]
        [TestCase(2, 1, 5, 4, 3, true)]
        [TestCase(2, 1, 7, 4, 3, false)]
        [TestCase(12, 11, 10, 1, 13, true)]
        [TestCase(12, 11, 9, 1, 13, false)]
        [TestCase(8, 7, 4, 6, 5, true)]
        [TestCase(11, 7, 4, 6, 5, false)]
        public void CanDetectIfIsStraight(int h1, int h2, int h3, int h4, int h5, bool expect)
        {
            var hand = new []{h1, h2, h3, h4, h5};

            var result = IsStraight(hand);

            Assert.That(result, Is.EqualTo(expect));
        }

        bool IsStraight(int[] hand)
        {

            SortWithCache(hand);// 2xN

            if (IsSequential(hand)) return true;// N
            if (hand[0] == 1) hand[0] = 14;

            ShiftValues(hand);//N

            return IsSequential(hand);//N

        }

        private void ShiftValues(int[] hand)
        {
            Array.Sort(hand);
        }

        void SortWithCache(int[] hand)
        {
            var temp = new bool[14];
            foreach (var t in hand)
            {
                temp[t] = true;
            }
            var j = 0;
            for (var i = 1; i < temp.Length; ++i)
            {
                var curr = temp[i];
                if (curr)
                {
                    hand[j++] = i;
                }
            }
        }
        bool IsSequential(int[] hand)
        {
            var prev = -1;
            foreach (var curr in hand)
            {
                if (prev > 0)
                {
                    if (curr - prev != 1)
                    {
                        return false;
                    }
                }
                prev = curr;
            }
            return true;
        }

    }
}