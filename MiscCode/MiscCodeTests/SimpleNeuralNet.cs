using System;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class SimpleNeuralNet
    {
        //https://github.com/stmorgan/pythonNNexample/blob/master/PythonNNExampleFromSirajology.py
        [Test]
        public void Run()
        {
            var input = new double[,]
            {
                {0,0,1},
                {0,1,1},
                {1,0,1},
                {1,1,1},
            };

            var output = new double[,]
            {
                {0},{1},{1},{0}
            };

//            var rand = new Random(1);
//            var syn0 = new double[3,4].PopulateWithRandom(rand);
//            var syn1 = new double[4,1].PopulateWithRandom(rand);
            var syn0 = new [,]
            {
                {-0.16595599,  0.44064899, -0.99977125, -0.39533485},
                {-0.70648822, -0.81532281, -0.62747958, -0.30887855},
                {-0.20646505,  0.07763347, -0.16161097,  0.370439  }
            };
            var syn1 = new [,]
            {
                {-0.5910955 },
                {0.75623487},
                {-0.94522481},
                {0.34093502}
            };

            foreach (var times in Enumerable.Range(0,60000))
            {
                var l0 = input;
                var l1 = l0.DotProductWith(syn0).ApplyForEach(Func);
                var l2 = l1.DotProductWith(syn1).ApplyForEach(Func);

                var errorsL2 = output.SubstractEach(l2);
                if(times%10000==0)
                    Console.WriteLine("Error:"+errorsL2.AggregateEach(0.0, (n,e) => n + e) / (errorsL2.Columns()*errorsL2.Rows()));

                var deltaL2 = errorsL2.MultiplyEach(l2.ApplyForEach(FuncDerivative));
                var errorL1 = deltaL2.DotProductWith(syn1.Transpose());
                var deltaL1 = errorL1.MultiplyEach(l1.ApplyForEach(FuncDerivative));

                syn1 = syn1.AddEach(l1.Transpose().DotProductWith(deltaL2));
                syn0 = syn0.AddEach(l0.Transpose().DotProductWith(deltaL1));
            }
        }
        private static double Func(double x)
        {
            return 1.0/(1.0 + Math.Exp(-x)); // sigmoid
        }
        private static double FuncDerivative(double x)
        {
            return x * (1.0 - x); // sigmoid derivative
        }

    }
    internal static class SimpleNeuralNetExtensions
    {
        internal static T AggregateEach<T>(this double[,] a, T aggregator, Func<T,double,T> func)
        {
            for(var r = 0; r < a.GetLength(0); r++)//row
                for(var c = 0; c < a.GetLength(1); c++)//column
                    aggregator = func(aggregator,a[r,c]);
            return aggregator;
        }
        internal static double[,] SubstractEach(this double[,] a, double[,] b)
        {
            var result = new double[a.GetLength(0),a.GetLength(1)];
            for(var r = 0; r < a.GetLength(0); r++)//row
                for(var c = 0; c < a.GetLength(1); c++)//column
                    result[r,c] = a[r,c] - b[r,c];
            return result;
        }
        internal static double[,] AddEach(this double[,] a, double[,] b)
        {
            var result = new double[a.GetLength(0),a.GetLength(1)];
            for(var r = 0; r < a.GetLength(0); r++)//row
                for(var c = 0; c < a.GetLength(1); c++)//column
                    result[r,c] = a[r,c] + b[r,c];
            return result;
        }
        internal static double[,] MultiplyEach(this double[,] a, double[,] b)
        {
            var result = new double[a.GetLength(0),a.GetLength(1)];
            for(var r = 0; r < a.GetLength(0); r++)//row
                for(var c = 0; c < a.GetLength(1); c++)//column
                    result[r,c] = a[r,c] * b[r,c];
            return result;
        }
        internal static double[,] PopulateWithRandom(this double[,] matrix, Random rand)
        {
            for(var r = 0; r < matrix.GetLength(0); r++)//row
                for(var c = 0; c < matrix.GetLength(1); c++)//column
                    matrix[r,c] = rand.NextDouble()*2.0-1.0;
            return matrix;
        }
        internal static double[,] DotProductWith(this double[,] a, double[,] b)
        {
            var result = new double[a.Rows(), b.Columns()];
            int N = result.Rows();
            int K = a.Columns();
            int M = result.Columns();
            int stride = b.Columns();

            var t = new double[K];

            unsafe
            {
                fixed (double* A = a)
                fixed (double* B = b)
                fixed (double* T = t)
                fixed (double* R = result)
                {
                    for (int j = 0; j < M; j++)
                    {
                        double* pb = B + j;
                        for (int k = 0; k < K; k++)
                        {
                            T[k] = *pb;
                            pb += stride;
                        }

                        double* pa = A;
                        double* pr = R + j;
                        for (int i = 0; i < N; i++)
                        {
                            double s = (double)0;
                            for (int k = 0; k < K; k++)
                                s += (double)((double)pa[k] * (double)T[k]);
                            *pr = (double)s;
                            pa += K;
                            pr += M;
                        }
                    }
                }
            }
            return result;
        }
        internal static int Rows<T>(this T[,] matrix)
        {
            return matrix.GetLength(0);
        }
        internal static int Columns<T>(this T[,] matrix)
        {
            return matrix.GetLength(1);
        }
        internal static double[,] ApplyForEach(this double[,] matrix, Func<double,double> func)
        {
            for(var r = 0; r < matrix.Rows(); r++)//row
                for(var c = 0; c < matrix.Columns(); c++)//column
                    matrix[r,c] = func(matrix[r,c]);
            return matrix;
        }
        internal static T[,] Transpose<T>(this T[,] matrix)
        {
            return Transpose(matrix, false);
        }
        internal static T[,] Transpose<T>(this T[,] matrix, bool inPlace)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (inPlace)
            {
                if (rows != cols)
                    throw new ArgumentException("Only square matrices can be transposed in place.", "matrix");

                for (int i = 0; i < rows; i++)
                {
                    for (int j = i; j < cols; j++)
                    {
                        T element = matrix[j, i];
                        matrix[j, i] = matrix[i, j];
                        matrix[i, j] = element;
                    }
                }

                return matrix;
            }
            else
            {
                T[,] result = new T[cols, rows];
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        result[j, i] = matrix[i, j];

                return result;
            }
        }
    }
}