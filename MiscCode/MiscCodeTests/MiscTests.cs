using System;
using MiscCodeTests.Extensions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void Test1()
        {
            var s = new double[4,3];

            const int copyRowIndex = 2;

            s[0,0] =    1;s[0,1] =    2;s[0,2] =    3;
            s[1,0] =   10;s[1,1] =   20;s[1,2] =   30;
            s[2,0] =  100;s[2,1] =  200;s[2,2] =  300;
            s[3,0] = 1000;s[3,1] = 2000;s[3,2] = 3000;


            var d = s.GetRow(copyRowIndex);

            for(var i = 0; i < d.Length ; ++i)
            {
                Console.WriteLine(i + " = " + d[i]);
            }
        }
        [Test]
        public void Test2()
        {
            Console.WriteLine("Test 2");
        }
        [Test]
        public void Test3()
        {
            
        }
        [Test]
        public void Test4()
        {

        }
        [Test]
        public void Test5()
        {
            var output = Softmax(0, 13,25,99.98,99.99,100);
            foreach (var i in output)
            {
                Console.WriteLine(i.ToString("N24"));
            }
        }
        private static double[] Softmax2(params double[] oSums)
        {
            // determine max output sum
            // does all output nodes at once so scale doesn't have to be re-computed each time
            double max = oSums[0];
            for (int i = 0; i < oSums.Length; ++i)
                if (oSums[i] > max) max = oSums[i];

            // determine scaling factor -- sum of exp(each val - max)
            double scale = 0.0;
            for (int i = 0; i < oSums.Length; ++i)
                scale += Math.Exp(oSums[i] - max);

            double[] result = new double[oSums.Length];
            for (int i = 0; i < oSums.Length; ++i)
                result[i] = Math.Exp(oSums[i] - max)/scale;

            return result; // now scaled so that xi sum to 1.0
        }
        public static double[] Softmax(params double[] input)
        {
            return Softmax(input, new double[input.Length]);
        }
        public static double[] Softmax(double[] input, double[] result)
        {
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                double u = Math.Exp(input[i]);
                result[i] = u;
                sum += u;
            }

            if (sum != 0)
            {
                for (int i = 0; i < result.Length; i++)
                    result[i] /= sum;
            }

            return result;
        }
    }
}

