using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class OneDimensionalNeuralNet
    {
        [Test]
        public void Run()
        {
            const double input = 0.5;
            const double expectedOutput = 10.0;

            var odnn = new OneDimNeuralNet();
            var ouput = odnn.Compute(input);
            Console.WriteLine("random="+ouput.ToString("F9"));

            for(var i = 0; i < 100; ++i)
            {
                ouput = odnn.Compute(input);
                Console.WriteLine(i+"="+ouput.ToString("F9"));
                odnn.Train(input, expectedOutput);
            }

            ouput = odnn.Compute(input);
            Console.WriteLine(ouput.ToString("F9"));
            

        }

        public class OneDimNeuralNet
        {
            private double _i;
            private double _ih = 0.5;
            private double _h;
            private double _ho = 0.5;
            private double _o;

            public double Compute(double input)
            {
                _i = input;
                var iih = _i * _ih;
                _h = Math.Tanh(iih);
                var hho = _h * _ho;
                _o = hho;
                return _o;
            }
            public void Train(double input, double expectedOutput)
            {
                _i = input;

                // compute output gradient
                var oDerivative =  1;// (1 - _o) * _o;
//                var oDerivative = (1 - _h) * (1 + _h);// (1 - _o) * _o;
                var oGrad = oDerivative * (expectedOutput - _o);

                // compute hidden gradient
                var hDerivative =  (1 - _h) * (1 + _h);
                var hGrad = oGrad * _ho * hDerivative;

                _ih += hGrad * _i;

                _ho += oGrad * _h;

            }
        }
    }
}