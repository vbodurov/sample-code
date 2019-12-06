using System;
using NUnit.Framework;
using Random = System.Random;

namespace MiscCodeTests.NeuralNets
{
    //https://stevenmiller888.github.io/mind-how-to-build-a-neural-network/
    [TestFixture]
    public class CustomNeuralNetDemo
    {
        [Test]
        public void Run()
        {
//            var input = new double[100][];
//            var output = new double[100];
//
//            var i = 0;
//            for(var a = 1; a <= 10; ++a)
//            {
//                for (var b = 1; b <= 10; ++b)
//                {
//                    input[i] = new double[] { a, b };
//                    output[i] = (a * b);
//                    ++i;
//                }
//            }
//
//
//            ICustomNeuralNet cnn = new CustomNeuralNet(2, 4, 1);
//
//
//            var sw = Stopwatch.StartNew();
//            cnn.Train(input, output);
//            sw.Stop();
//
//
//            i = 0;
//            foreach (var arr in input)
//            {
//                var y = cnn.ComputeOutput(arr);
//
//                
//
//Console.WriteLine(arr[0] + "*" + arr[1] + "=" + output[i]+" ("+y+")");
//                ++i;
//            }


        }
        [Test]
        public  void Temp()
        {
            Console.WriteLine(SigmoidDerivative(1.235)*-0.77);
        }
        private static double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }
        private static double SigmoidDerivative(double x)
        {
            return Sigmoid(x) * (1.0 - Sigmoid(x)); 
        }
        private static double Tanh(double x)
        {
            return Math.Tanh(x);
        }
        private static double TanhDerivative(double x)
        {
            return 1.0 - Tanh(x) * Tanh(x);
        }
    }

    public interface ICustomNeuralNet
    {
        void Train(double[][] inputs, double[] expectedOutputs);
        double ComputeOutput(double[] inputs);
    }
    public sealed class CustomNeuralNet : ICustomNeuralNet
    {
        private readonly ICustomNeuralNet _cnn;
        private readonly Random _rnd;
        private readonly int _numInput;
        private readonly int _numHidden;
        private readonly int _numOutput;
        private readonly double[] _inputs;
        private readonly double[,] _ihWeights;
        private readonly double[] _hidden;
        private readonly double[,] _hoWeights;
        private readonly double[] _outputs;

        public CustomNeuralNet(int numInput, int numHidden, int numOutput)
        {
            _cnn = this;
            _rnd = new Random(0); // for InitializeWeights() and Shuffle()

            _numInput = numInput;
            _numHidden = numHidden;
            _numOutput = numOutput;

            _inputs = new double[numInput];
            _ihWeights = new double[numInput, numHidden];
            _hidden = new double[numHidden];
            _hoWeights = new double[numHidden, numOutput];
            _outputs = new double[numOutput];

            InitializeWeights();

        }
        void ICustomNeuralNet.Train(double[][] inputs, double[] expectedOutputs)
        {
            var numRows = inputs.GetLength(0);
            
            const float maxEpochs = 10000;
            for (var epoch = 0; epoch  < maxEpochs; ++epoch)
            {
                for(var r = 0; r < numRows; ++r)
                {

                    _cnn.ComputeOutput(inputs[r]);
                    UpdateWeightsOfSingleRow(expectedOutputs[r]);
                }
            }
        }
        double ICustomNeuralNet.ComputeOutput(double[] inputs)
        {
            double[] hSums = new double[_numHidden]; // hidden nodes sums scratch array
            double[] oSums = new double[_numOutput]; // output nodes sums

            for (var c = 0; c < _inputs.Length; ++c) // copy x-values to inputs
                _inputs[c] = inputs[c];

            for (int j = 0; j < _numHidden; ++j) // compute i-h sum of weights * inputs
                for (int i = 0; i < _numInput; ++i)
                    hSums[j] += _inputs[i]*_ihWeights[i,j]; // note +=

            for (int i = 0; i < _numHidden; ++i) // apply activation
                _hidden[i] = Math.Tanh(hSums[i]);

            for (int j = 0; j < _numOutput; ++j) // compute h-o sum of weights * hOutputs
                for (int i = 0; i < _numHidden; ++i)
                    oSums[j] += _hidden[i]*_hoWeights[i,j];

            for (int i = 0; i < _numOutput; ++i) // apply activation
                _outputs[i] = oSums[i];

            return _outputs[0];
        }

        

        private void InitializeWeights()
        {
            for (int i = 0; i < _numInput; ++i)
                for (int h = 0; h < _numHidden; ++h)
                    _ihWeights[i,h] = _rnd.NextDouble()*(2) - 1;
            for (int h = 0; h < _numHidden; ++h)
                for (int o = 0; o < _numOutput; ++o)
                    _hoWeights[h,o] = _rnd.NextDouble()*(2) - 1;
        }

        private void UpdateWeightsOfSingleRow(double expectedOutput)
        {
            var oGrads = new double[_numOutput];
            var hGrads = new double[_numHidden];

            // 1. compute output gradients
            for (int i = 0; i < oGrads.Length; ++i)
            {

                // derivative of softmax = (1 - y) * y (same as log-sigmoid)
//                double derivative = _outputs[i] > 0.5 ? 0.05 : -0.05;
//                double derivative = (1 - _outputs[i])*_outputs[i];
                double derivative = (1 - _outputs[i]) * (1 + _outputs[i]);
//                double derivative = Math.Sqrt(1 - Math.Pow(_outputs[i] * 2 - 1, 2));
                // 'mean squared error version' includes (1-y)(y) derivative
                oGrads[i] = derivative*(expectedOutput - _outputs[i]);
if (double.IsNaN(oGrads[i])) throw new ApplicationException("nan |" + oGrads[i] + "|" + i);
            }
            // 2. compute hidden gradients
            for (int i = 0; i < hGrads.Length; ++i)
            {
                // derivative of tanh = (1 - y) * (1 + y)
                double derivative = (1 - _hidden[i]) * (1 + _hidden[i]);
                double sum = 0.0;
                for (int j = 0; j < _numOutput; ++j) // each hidden delta is the sum of numOutput terms
                {
                    sum += oGrads[j]*_hoWeights[i,j];
                }
                hGrads[i] = derivative*sum;
            }

            // 3a. update hidden weights (gradients must be computed right-to-left but weights
            // can be updated in any order)
            var ihNumRows = _ihWeights.GetLength(0);
            var ihNumCols = _ihWeights.GetLength(1);
            for (var r = 0; r < ihNumRows; ++r) // 0..2 (3)
            {
                for (var c = 0; c < ihNumCols; ++c) // 0..3 (4)
                {
                    double delta = hGrads[c] * _inputs[r]; // compute the new delta
                    _ihWeights[r,c] += delta; // update. note we use '+' instead of '-'. this can be very tricky.
                }
            }

            // 3. update hidden-output weights
            var hoNumRows = _hoWeights.GetLength(0);
            var hoNumCols = _hoWeights.GetLength(1);
            for (var r = 0; r < hoNumRows; ++r)
            {
                for (var c = 0; c < hoNumCols; ++c)
                {
                    // see above: hOutputs are inputs to the nn outputs
                    double delta = oGrads[c]*_hidden[r];
                    _hoWeights[r,c] += delta;
                }
            }

        } // UpdateWeights
        private static double[] Softmax(double[] input)
        {
            var result = new double[input.Length];
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                double u = Math.Exp(input[i]);
                result[i] = u;
                sum += u;
            }

            if (Math.Abs(sum) > 0.000001)
            {
                for (int i = 0; i < result.Length; i++)
                    result[i] /= sum;
            }

            return result;
        }
    }

}