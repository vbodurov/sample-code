using System;
using System.IO;
using NUnit.Framework;

// https://www.youtube.com/watch?v=9aHJ-FAzQaE
// http://www.quaetrix.com/Build2014.html
// For 2014 Microsoft Build Conference attendees
// April 2-4, 2014
// San Francisco, CA
//
// This is source for a C# console application.
// To compile you can 1.) create a new Visual Studio
// C# console app project named BuildNeuralNetworkDemo
// then zap away the template code and replace with this code,
// or 2.) copy this code into notepad, save as NeuralNetworkProgram.cs
// on your local machine, launch the special VS command shell
// (it knows where the csc.exe compiler is), cd-navigate to
// the directory containing the .cs file, type 'csc.exe
// NeuralNetworkProgram.cs' and hit enter, and then after 
// the compiler creates NeuralNetworkProgram.exe, you can
// run from the command line.
//
// This is an enhanced neural network. It is fully-connected
// and feed-forward. The training algorithm is back-propagation
// with momentum and weight decay. The input data is normalized
// so training is quite fast.
//
// You can use this code however you wish subject to the usual disclaimers
// (use at your own risk, etc.)

namespace MiscCodeTests.NeuralNets
{
    [TestFixture]
    public class BuildNeuralNetworkDemoSimplified
    {
        [Test]
        public void Run()
        {
//            SetupConsoleOut();
            Console.WriteLine();

#region data


            double[][] allData = new double[150][];
            allData[0] = new double[] {5.1, 3.5, 1.4, 0.2, 0, 0, 1}; // sepal length, width, petal length, width
            allData[1] = new double[] {4.9, 3.0, 1.4, 0.2, 0, 0, 1}; // Iris setosa = 0 0 1
            allData[2] = new double[] {4.7, 3.2, 1.3, 0.2, 0, 0, 1}; // Iris versicolor = 0 1 0
            allData[3] = new double[] {4.6, 3.1, 1.5, 0.2, 0, 0, 1}; // Iris virginica = 1 0 0
            allData[4] = new double[] {5.0, 3.6, 1.4, 0.2, 0, 0, 1};
            allData[5] = new double[] {5.4, 3.9, 1.7, 0.4, 0, 0, 1};
            allData[6] = new double[] {4.6, 3.4, 1.4, 0.3, 0, 0, 1};
            allData[7] = new double[] {5.0, 3.4, 1.5, 0.2, 0, 0, 1};
            allData[8] = new double[] {4.4, 2.9, 1.4, 0.2, 0, 0, 1};
            allData[9] = new double[] {4.9, 3.1, 1.5, 0.1, 0, 0, 1};

            allData[10] = new double[] {5.4, 3.7, 1.5, 0.2, 0, 0, 1};
            allData[11] = new double[] {4.8, 3.4, 1.6, 0.2, 0, 0, 1};
            allData[12] = new double[] {4.8, 3.0, 1.4, 0.1, 0, 0, 1};
            allData[13] = new double[] {4.3, 3.0, 1.1, 0.1, 0, 0, 1};
            allData[14] = new double[] {5.8, 4.0, 1.2, 0.2, 0, 0, 1};
            allData[15] = new double[] {5.7, 4.4, 1.5, 0.4, 0, 0, 1};
            allData[16] = new double[] {5.4, 3.9, 1.3, 0.4, 0, 0, 1};
            allData[17] = new double[] {5.1, 3.5, 1.4, 0.3, 0, 0, 1};
            allData[18] = new double[] {5.7, 3.8, 1.7, 0.3, 0, 0, 1};
            allData[19] = new double[] {5.1, 3.8, 1.5, 0.3, 0, 0, 1};

            allData[20] = new double[] {5.4, 3.4, 1.7, 0.2, 0, 0, 1};
            allData[21] = new double[] {5.1, 3.7, 1.5, 0.4, 0, 0, 1};
            allData[22] = new double[] {4.6, 3.6, 1.0, 0.2, 0, 0, 1};
            allData[23] = new double[] {5.1, 3.3, 1.7, 0.5, 0, 0, 1};
            allData[24] = new double[] {4.8, 3.4, 1.9, 0.2, 0, 0, 1};
            allData[25] = new double[] {5.0, 3.0, 1.6, 0.2, 0, 0, 1};
            allData[26] = new double[] {5.0, 3.4, 1.6, 0.4, 0, 0, 1};
            allData[27] = new double[] {5.2, 3.5, 1.5, 0.2, 0, 0, 1};
            allData[28] = new double[] {5.2, 3.4, 1.4, 0.2, 0, 0, 1};
            allData[29] = new double[] {4.7, 3.2, 1.6, 0.2, 0, 0, 1};

            allData[30] = new double[] {4.8, 3.1, 1.6, 0.2, 0, 0, 1};
            allData[31] = new double[] {5.4, 3.4, 1.5, 0.4, 0, 0, 1};
            allData[32] = new double[] {5.2, 4.1, 1.5, 0.1, 0, 0, 1};
            allData[33] = new double[] {5.5, 4.2, 1.4, 0.2, 0, 0, 1};
            allData[34] = new double[] {4.9, 3.1, 1.5, 0.1, 0, 0, 1};
            allData[35] = new double[] {5.0, 3.2, 1.2, 0.2, 0, 0, 1};
            allData[36] = new double[] {5.5, 3.5, 1.3, 0.2, 0, 0, 1};
            allData[37] = new double[] {4.9, 3.1, 1.5, 0.1, 0, 0, 1};
            allData[38] = new double[] {4.4, 3.0, 1.3, 0.2, 0, 0, 1};
            allData[39] = new double[] {5.1, 3.4, 1.5, 0.2, 0, 0, 1};

            allData[40] = new double[] {5.0, 3.5, 1.3, 0.3, 0, 0, 1};
            allData[41] = new double[] {4.5, 2.3, 1.3, 0.3, 0, 0, 1};
            allData[42] = new double[] {4.4, 3.2, 1.3, 0.2, 0, 0, 1};
            allData[43] = new double[] {5.0, 3.5, 1.6, 0.6, 0, 0, 1};
            allData[44] = new double[] {5.1, 3.8, 1.9, 0.4, 0, 0, 1};
            allData[45] = new double[] {4.8, 3.0, 1.4, 0.3, 0, 0, 1};
            allData[46] = new double[] {5.1, 3.8, 1.6, 0.2, 0, 0, 1};
            allData[47] = new double[] {4.6, 3.2, 1.4, 0.2, 0, 0, 1};
            allData[48] = new double[] {5.3, 3.7, 1.5, 0.2, 0, 0, 1};
            allData[49] = new double[] {5.0, 3.3, 1.4, 0.2, 0, 0, 1};

            allData[50] = new double[] {7.0, 3.2, 4.7, 1.4, 0, 1, 0};
            allData[51] = new double[] {6.4, 3.2, 4.5, 1.5, 0, 1, 0};
            allData[52] = new double[] {6.9, 3.1, 4.9, 1.5, 0, 1, 0};
            allData[53] = new double[] {5.5, 2.3, 4.0, 1.3, 0, 1, 0};
            allData[54] = new double[] {6.5, 2.8, 4.6, 1.5, 0, 1, 0};
            allData[55] = new double[] {5.7, 2.8, 4.5, 1.3, 0, 1, 0};
            allData[56] = new double[] {6.3, 3.3, 4.7, 1.6, 0, 1, 0};
            allData[57] = new double[] {4.9, 2.4, 3.3, 1.0, 0, 1, 0};
            allData[58] = new double[] {6.6, 2.9, 4.6, 1.3, 0, 1, 0};
            allData[59] = new double[] {5.2, 2.7, 3.9, 1.4, 0, 1, 0};

            allData[60] = new double[] {5.0, 2.0, 3.5, 1.0, 0, 1, 0};
            allData[61] = new double[] {5.9, 3.0, 4.2, 1.5, 0, 1, 0};
            allData[62] = new double[] {6.0, 2.2, 4.0, 1.0, 0, 1, 0};
            allData[63] = new double[] {6.1, 2.9, 4.7, 1.4, 0, 1, 0};
            allData[64] = new double[] {5.6, 2.9, 3.6, 1.3, 0, 1, 0};
            allData[65] = new double[] {6.7, 3.1, 4.4, 1.4, 0, 1, 0};
            allData[66] = new double[] {5.6, 3.0, 4.5, 1.5, 0, 1, 0};
            allData[67] = new double[] {5.8, 2.7, 4.1, 1.0, 0, 1, 0};
            allData[68] = new double[] {6.2, 2.2, 4.5, 1.5, 0, 1, 0};
            allData[69] = new double[] {5.6, 2.5, 3.9, 1.1, 0, 1, 0};

            allData[70] = new double[] {5.9, 3.2, 4.8, 1.8, 0, 1, 0};
            allData[71] = new double[] {6.1, 2.8, 4.0, 1.3, 0, 1, 0};
            allData[72] = new double[] {6.3, 2.5, 4.9, 1.5, 0, 1, 0};
            allData[73] = new double[] {6.1, 2.8, 4.7, 1.2, 0, 1, 0};
            allData[74] = new double[] {6.4, 2.9, 4.3, 1.3, 0, 1, 0};
            allData[75] = new double[] {6.6, 3.0, 4.4, 1.4, 0, 1, 0};
            allData[76] = new double[] {6.8, 2.8, 4.8, 1.4, 0, 1, 0};
            allData[77] = new double[] {6.7, 3.0, 5.0, 1.7, 0, 1, 0};
            allData[78] = new double[] {6.0, 2.9, 4.5, 1.5, 0, 1, 0};
            allData[79] = new double[] {5.7, 2.6, 3.5, 1.0, 0, 1, 0};

            allData[80] = new double[] {5.5, 2.4, 3.8, 1.1, 0, 1, 0};
            allData[81] = new double[] {5.5, 2.4, 3.7, 1.0, 0, 1, 0};
            allData[82] = new double[] {5.8, 2.7, 3.9, 1.2, 0, 1, 0};
            allData[83] = new double[] {6.0, 2.7, 5.1, 1.6, 0, 1, 0};
            allData[84] = new double[] {5.4, 3.0, 4.5, 1.5, 0, 1, 0};
            allData[85] = new double[] {6.0, 3.4, 4.5, 1.6, 0, 1, 0};
            allData[86] = new double[] {6.7, 3.1, 4.7, 1.5, 0, 1, 0};
            allData[87] = new double[] {6.3, 2.3, 4.4, 1.3, 0, 1, 0};
            allData[88] = new double[] {5.6, 3.0, 4.1, 1.3, 0, 1, 0};
            allData[89] = new double[] {5.5, 2.5, 4.0, 1.3, 0, 1, 0};

            allData[90] = new double[] {5.5, 2.6, 4.4, 1.2, 0, 1, 0};
            allData[91] = new double[] {6.1, 3.0, 4.6, 1.4, 0, 1, 0};
            allData[92] = new double[] {5.8, 2.6, 4.0, 1.2, 0, 1, 0};
            allData[93] = new double[] {5.0, 2.3, 3.3, 1.0, 0, 1, 0};
            allData[94] = new double[] {5.6, 2.7, 4.2, 1.3, 0, 1, 0};
            allData[95] = new double[] {5.7, 3.0, 4.2, 1.2, 0, 1, 0};
            allData[96] = new double[] {5.7, 2.9, 4.2, 1.3, 0, 1, 0};
            allData[97] = new double[] {6.2, 2.9, 4.3, 1.3, 0, 1, 0};
            allData[98] = new double[] {5.1, 2.5, 3.0, 1.1, 0, 1, 0};
            allData[99] = new double[] {5.7, 2.8, 4.1, 1.3, 0, 1, 0};

            allData[100] = new double[] {6.3, 3.3, 6.0, 2.5, 1, 0, 0};
            allData[101] = new double[] {5.8, 2.7, 5.1, 1.9, 1, 0, 0};
            allData[102] = new double[] {7.1, 3.0, 5.9, 2.1, 1, 0, 0};
            allData[103] = new double[] {6.3, 2.9, 5.6, 1.8, 1, 0, 0};
            allData[104] = new double[] {6.5, 3.0, 5.8, 2.2, 1, 0, 0};
            allData[105] = new double[] {7.6, 3.0, 6.6, 2.1, 1, 0, 0};
            allData[106] = new double[] {4.9, 2.5, 4.5, 1.7, 1, 0, 0};
            allData[107] = new double[] {7.3, 2.9, 6.3, 1.8, 1, 0, 0};
            allData[108] = new double[] {6.7, 2.5, 5.8, 1.8, 1, 0, 0};
            allData[109] = new double[] {7.2, 3.6, 6.1, 2.5, 1, 0, 0};

            allData[110] = new double[] {6.5, 3.2, 5.1, 2.0, 1, 0, 0};
            allData[111] = new double[] {6.4, 2.7, 5.3, 1.9, 1, 0, 0};
            allData[112] = new double[] {6.8, 3.0, 5.5, 2.1, 1, 0, 0};
            allData[113] = new double[] {5.7, 2.5, 5.0, 2.0, 1, 0, 0};
            allData[114] = new double[] {5.8, 2.8, 5.1, 2.4, 1, 0, 0};
            allData[115] = new double[] {6.4, 3.2, 5.3, 2.3, 1, 0, 0};
            allData[116] = new double[] {6.5, 3.0, 5.5, 1.8, 1, 0, 0};
            allData[117] = new double[] {7.7, 3.8, 6.7, 2.2, 1, 0, 0};
            allData[118] = new double[] {7.7, 2.6, 6.9, 2.3, 1, 0, 0};
            allData[119] = new double[] {6.0, 2.2, 5.0, 1.5, 1, 0, 0};

            allData[120] = new double[] {6.9, 3.2, 5.7, 2.3, 1, 0, 0};
            allData[121] = new double[] {5.6, 2.8, 4.9, 2.0, 1, 0, 0};
            allData[122] = new double[] {7.7, 2.8, 6.7, 2.0, 1, 0, 0};
            allData[123] = new double[] {6.3, 2.7, 4.9, 1.8, 1, 0, 0};
            allData[124] = new double[] {6.7, 3.3, 5.7, 2.1, 1, 0, 0};
            allData[125] = new double[] {7.2, 3.2, 6.0, 1.8, 1, 0, 0};
            allData[126] = new double[] {6.2, 2.8, 4.8, 1.8, 1, 0, 0};
            allData[127] = new double[] {6.1, 3.0, 4.9, 1.8, 1, 0, 0};
            allData[128] = new double[] {6.4, 2.8, 5.6, 2.1, 1, 0, 0};
            allData[129] = new double[] {7.2, 3.0, 5.8, 1.6, 1, 0, 0};

            allData[130] = new double[] {7.4, 2.8, 6.1, 1.9, 1, 0, 0};
            allData[131] = new double[] {7.9, 3.8, 6.4, 2.0, 1, 0, 0};
            allData[132] = new double[] {6.4, 2.8, 5.6, 2.2, 1, 0, 0};
            allData[133] = new double[] {6.3, 2.8, 5.1, 1.5, 1, 0, 0};
            allData[134] = new double[] {6.1, 2.6, 5.6, 1.4, 1, 0, 0};
            allData[135] = new double[] {7.7, 3.0, 6.1, 2.3, 1, 0, 0};
            allData[136] = new double[] {6.3, 3.4, 5.6, 2.4, 1, 0, 0};
            allData[137] = new double[] {6.4, 3.1, 5.5, 1.8, 1, 0, 0};
            allData[138] = new double[] {6.0, 3.0, 4.8, 1.8, 1, 0, 0};
            allData[139] = new double[] {6.9, 3.1, 5.4, 2.1, 1, 0, 0};

            allData[140] = new double[] {6.7, 3.1, 5.6, 2.4, 1, 0, 0};
            allData[141] = new double[] {6.9, 3.1, 5.1, 2.3, 1, 0, 0};
            allData[142] = new double[] {5.8, 2.7, 5.1, 1.9, 1, 0, 0};
            allData[143] = new double[] {6.8, 3.2, 5.9, 2.3, 1, 0, 0};
            allData[144] = new double[] {6.7, 3.3, 5.7, 2.5, 1, 0, 0};
            allData[145] = new double[] {6.7, 3.0, 5.2, 2.3, 1, 0, 0};
            allData[146] = new double[] {6.3, 2.5, 5.0, 1.9, 1, 0, 0};
            allData[147] = new double[] {6.5, 3.0, 5.2, 2.0, 1, 0, 0};
            allData[148] = new double[] {6.2, 3.4, 5.4, 2.3, 1, 0, 0};
            allData[149] = new double[] {5.9, 3.0, 5.1, 1.8, 1, 0, 0};

#endregion
            

            Console.WriteLine("Creating 80% training and 20% test data matrices");
            double[][] trainData = null;
            double[][] testData = null;
            MakeTrainTest(allData, out trainData, out testData);

            Normalize(trainData, new int[] {0, 1, 2, 3});
            Normalize(testData, new int[] {0, 1, 2, 3});

            Console.WriteLine("\nCreating a 4-input, 7-hidden, 3-output neural network");
            Console.Write("Hard-coded tanh function for input-to-hidden and softmax for ");
            Console.WriteLine("hidden-to-output activations");
            const int numInput = 4;
            const int numHidden = 7;
            const int numOutput = 3;
            var nn = new BuildNeuralNetworkSimplified(numInput, numHidden, numOutput);

            Console.WriteLine("\nInitializing weights and bias to small random values");
            nn.InitializeWeights();

            int maxEpochs = 400;
            double learnRate = 0.05;

            Console.WriteLine("\nBeginning training using incremental back-propagation\n");
            nn.Train(trainData, maxEpochs, learnRate);
            Console.WriteLine("Training complete");

            double trainAcc = nn.Accuracy(trainData);
            Console.WriteLine("\nAccuracy on training data = " + trainAcc.ToString("F4"));

            double testAcc = nn.Accuracy(testData);
            Console.WriteLine("\nAccuracy on test data = " + testAcc.ToString("F4"));

            Console.WriteLine("\nEnd Build 2013 neural network demo\n");
            Console.ReadLine();
        } // Main

        static void SetupConsoleOut()
        {
            const string fileName = "./__log.txt";

            StreamWriter writer;
//            TextWriter oldOut = Console.Out;
            try
            {
                if(File.Exists(fileName)) File.Delete(fileName);

                var ostrm = new FileStream (fileName, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter (ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine ("Cannot open Redirect.txt for writing");
                Console.WriteLine (e.Message);
                return;
            }
            Console.SetOut (writer);
        }

        static void MakeTrainTest(double[][] allData, out double[][] trainData, out double[][] testData)
        {
            // split allData into 80% trainData and 20% testData
            Random rnd = new Random(0);
            int totRows = allData.Length;
            int numCols = allData[0].Length;

            int trainRows = (int) (totRows*0.80); // hard-coded 80-20 split
            int testRows = totRows - trainRows;

            trainData = new double[trainRows][];
            testData = new double[testRows][];

            int[] sequence = new int[totRows]; // create a random sequence of indexes
            for (int i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }

            int si = 0; // index into sequence[]
            int j = 0; // index into trainData or testData

            for (; si < trainRows; ++si) // first rows to train data
            {
                trainData[j] = new double[numCols];
                int idx = sequence[si];
                Array.Copy(allData[idx], trainData[j], numCols);
                ++j;
            }

            j = 0; // reset to start of test data
            for (; si < totRows; ++si) // remainder to test data
            {
                testData[j] = new double[numCols];
                int idx = sequence[si];
                Array.Copy(allData[idx], testData[j], numCols);
                ++j;
            }
        } // MakeTrainTest

        static void Normalize(double[][] dataMatrix, int[] cols)
        {
            // normalize specified cols by computing (x - mean) / sd for each value
            foreach (int col in cols)
            {
                double sum = 0.0;
                for (int i = 0; i < dataMatrix.Length; ++i)
                    sum += dataMatrix[i][col];
                double mean = sum/dataMatrix.Length;
                sum = 0.0;
                for (int i = 0; i < dataMatrix.Length; ++i)
                    sum += (dataMatrix[i][col] - mean)*(dataMatrix[i][col] - mean);
                // thanks to Dr. W. Winfrey, Concord Univ., for catching bug in original code
                double sd = Math.Sqrt(sum/(dataMatrix.Length - 1));
                for (int i = 0; i < dataMatrix.Length; ++i)
                    dataMatrix[i][col] = (dataMatrix[i][col] - mean)/sd;
            }
        }
    } // class Program

    public class BuildNeuralNetworkSimplified
    {
        private static Random _rnd;

        private readonly int _numInput;
        private readonly int _numHidden;
        private readonly int _numOutput;

        private readonly double[] _inputs;

        private readonly double[][] _ihWeights; // input-hidden
        private readonly double[] _hidden;
        private readonly double[][] _hoWeights; // hidden-output
        private readonly double[] _outputs;



        public BuildNeuralNetworkSimplified(int numInput, int numHidden, int numOutput)
        {
            _rnd = new Random(0); // for InitializeWeights() and Shuffle()

            _numInput = numInput;
            _numHidden = numHidden;
            _numOutput = numOutput;

            _inputs = new double[numInput];

            _ihWeights = MakeMatrix(numInput, numHidden);
            _hidden = new double[numHidden];

            _hoWeights = MakeMatrix(numHidden, numOutput);

            _outputs = new double[numOutput];
        } // ctor

        private static double[][] MakeMatrix(int rows, int cols) // helper for ctor
        {
            double[][] result = new double[rows][];
            for (int r = 0; r < result.Length; ++r)
                result[r] = new double[cols];
            return result;
        }


        // ----------------------------------------------------------------------------------------

        public void SetWeights(double[] weights)
        {
            // copy weights and biases in weights[] array to i-h weights, i-h biases, h-o weights, h-o biases
            int numWeights = (_numInput*_numHidden) + (_numHidden*_numOutput);// + numHidden + numOutput;
            if (weights.Length != numWeights)
                throw new Exception("Bad weights array length: ");

            int k = 0; // points into weights param

            for (int i = 0; i < _numInput; ++i)
                for (int j = 0; j < _numHidden; ++j)
                    _ihWeights[i][j] = weights[k++];
            for (int i = 0; i < _numHidden; ++i)
                for (int j = 0; j < _numOutput; ++j)
                    _hoWeights[i][j] = weights[k++];
        }

        public void InitializeWeights()
        {
            // initialize weights and biases to small random values
            int numWeights = (_numInput*_numHidden) + (_numHidden*_numOutput);// + numHidden + numOutput;
            double[] initialWeights = new double[numWeights];
            double lo = -1;
            double hi = 1;
            for (int i = 0; i < initialWeights.Length; ++i)
                initialWeights[i] = (hi - lo)*_rnd.NextDouble() + lo;
            SetWeights(initialWeights);
        }
        

        // ----------------------------------------------------------------------------------------

        private double[] ComputeOutputs(double[] xValues)
        {
            if (xValues.Length != _numInput)
                throw new Exception("Bad xValues array length");

            double[] hSums = new double[_numHidden]; // hidden nodes sums scratch array
            double[] oSums = new double[_numOutput]; // output nodes sums

            for (int i = 0; i < xValues.Length; ++i) // copy x-values to inputs
                _inputs[i] = xValues[i];

            for (int h = 0; h < _numHidden; ++h) // compute i-h sum of weights * inputs
                for (int i = 0; i < _numInput; ++i)
                    hSums[h] += _inputs[i]*_ihWeights[i][h]; // note +=

            for (int h = 0; h < _numHidden; ++h) // apply activation
                _hidden[h] = Math.Tanh(hSums[h]); // hard-coded

            for (int o = 0; o < _numOutput; ++o) // compute h-o sum of weights * hOutputs
                for (int h = 0; h < _numHidden; ++h)
                    oSums[o] += _hidden[h]*_hoWeights[h][o];

            double[] softOut = Softmax(oSums); // softmax activation does all outputs at once for efficiency
            Array.Copy(softOut, _outputs, softOut.Length);

            double[] retResult = new double[_numOutput]; // could define a GetOutputs method instead
            Array.Copy(_outputs, retResult, retResult.Length);
            return retResult;
        } // ComputeOutputs


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

        // ----------------------------------------------------------------------------------------

        private void UpdateWeights(double[] tValues, double learnRate)
        {
            // update the weights and biases using back-propagation, with target values, eta (learning rate),
            // alpha (momentum).
            // assumes that SetWeights and ComputeOutputs have been called and so all the internal arrays
            // and matrices have values (other than 0.0)
            if (tValues.Length != _numOutput)
                throw new Exception("target values not same Length as output in UpdateWeights");

            var oGrads = new double[_numOutput];
            var hGrads = new double[_numHidden];

            // 1. compute output gradients
            for (int i = 0; i < oGrads.Length; ++i)
            {

                // derivative of softmax = (1 - y) * y (same as log-sigmoid)
                double derivative = (1 - _outputs[i])*_outputs[i];
                // 'mean squared error version' includes (1-y)(y) derivative
                oGrads[i] = derivative*(tValues[i] - _outputs[i]);
            }
            // 2. compute hidden gradients
            for (int i = 0; i < hGrads.Length; ++i)
            {
                // derivative of tanh = (1 - y) * (1 + y)
                double derivative = (1 - _hidden[i]) * (1 + _hidden[i]);
                double sum = 0.0;
                for (int j = 0; j < _numOutput; ++j) // each hidden delta is the sum of numOutput terms
                {
                    sum += oGrads[j]*_hoWeights[i][j];
                }
                hGrads[i] = derivative*sum;
            }

            // 3a. update hidden weights (gradients must be computed right-to-left but weights
            // can be updated in any order)
            for (int i = 0; i < _ihWeights.Length; ++i) // 0..2 (3)
            {
                for (int j = 0; j < _ihWeights[0].Length; ++j) // 0..3 (4)
                {
                    double delta = learnRate * hGrads[j] * _inputs[i]; // compute the new delta
                    _ihWeights[i][j] += delta; // update. note we use '+' instead of '-'. this can be very tricky.
                }
            }

            // 3. update hidden-output weights
            for (int i = 0; i < _hoWeights.Length; ++i)
            {
                for (int j = 0; j < _hoWeights[0].Length; ++j)
                {
                    // see above: hOutputs are inputs to the nn outputs
                    double delta = learnRate*oGrads[j]*_hidden[i];
                    _hoWeights[i][j] += delta;
                }
            }

        } // UpdateWeights

        // ----------------------------------------------------------------------------------------

        public void Train(double[][] trainData, int maxEpochs, double learnRate)
        {
            // train a back-prop style NN classifier using learning rate and momentum
            // weight decay reduces the magnitude of a weight value over time unless that value
            // is constantly increased
            int epoch = 0;
            double[] xValues = new double[_numInput]; // inputs
            double[] tValues = new double[_numOutput]; // target values


            while (epoch < maxEpochs)
            {
//                Shuffle(sequence); // visit each training data in random order
                for (int i = 0; i < trainData.Length; ++i)
                {
                    Array.Copy(trainData[i], xValues, _numInput);
                    Array.Copy(trainData[i], _numInput, tValues, 0, _numOutput);
                    ComputeOutputs(xValues); // copy xValues in, compute outputs (store them internally)
                    UpdateWeights(tValues, learnRate); // find better weights
                } // each training tuple
                ++epoch;
            }
        } // Train

        

        // ----------------------------------------------------------------------------------------

        public double Accuracy(double[][] testData)
        {
            // percentage correct using winner-takes all
            int numCorrect = 0;
            int numWrong = 0;
            double[] xValues = new double[_numInput]; // inputs
            double[] tValues = new double[_numOutput]; // targets
            double[] yValues; // computed Y

            for (int i = 0; i < testData.Length; ++i)
            {
                Array.Copy(testData[i], xValues, _numInput); // parse test data into x-values and t-values
                Array.Copy(testData[i], _numInput, tValues, 0, _numOutput);
                yValues = ComputeOutputs(xValues);
                int maxIndex = MaxIndex(yValues); // which cell in yValues has largest value?

                if (tValues[maxIndex] == 1.0) // ugly. consider AreEqual(double x, double y)
                    ++numCorrect;
                else
                    ++numWrong;
            }
            return (numCorrect*1.0)/(numCorrect + numWrong); // ugly 2 - check for divide by zero
        }

        private static int MaxIndex(double[] vector) // helper for Accuracy()
        {
            // index of largest value
            int bigIndex = 0;
            double biggestVal = vector[0];
            for (int i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i];
                    bigIndex = i;
                }
            }
            return bigIndex;
        }
    }
}