using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class LogisticGradient
    {
        //https://msdn.microsoft.com/en-us/magazine/dn913188.aspx
        [Test]
        public void Run()
        {
            Console.WriteLine("\nBegin Logistic Regression (binary) Classification demo");
            Console.WriteLine("Goal is to demonstrate training using gradient descent");

            int numFeatures = 8; // synthetic data
            int numRows = 10000;
            int seed = 1;

            Console.WriteLine("\nGenerating " + numRows + " artificial data items with " + numFeatures + " features");
            double[][] allData = MakeAllData(numFeatures, numRows, seed);

            Console.WriteLine("Creating train (80%) and test (20%) matrices");
            double[][] trainData;
            double[][] testData;
            MakeTrainTest(allData, 0, out trainData, out testData);
            Console.WriteLine("Done");

 
            Console.WriteLine("\nTraining data: \n");
            ShowData(trainData, 3, 2, true);

            Console.WriteLine("\nTest data: \n");
            ShowData(testData, 3, 2, true);


            Console.WriteLine("Creating LR binary classifier");
            LogisticClassifier lc = new LogisticClassifier(numFeatures); //

            int maxEpochs = 1000; // 
            Console.WriteLine("Setting maxEpochs = " + maxEpochs);
            double alpha = 0.01;
            Console.WriteLine("Setting learning rate = " + alpha.ToString("F2"));

            Console.WriteLine("\nStarting training using (stochastic) gradient descent");
            double[] weights = lc.Train(trainData, maxEpochs, alpha);
            Console.WriteLine("Training complete");

            Console.WriteLine("\nBest weights found:");
            ShowVector(weights, 4, true);

            double trainAcc = lc.Accuracy(trainData, weights);
            Console.WriteLine("Prediction accuracy on training data = " +
            trainAcc.ToString("F4"));

            double testAcc = lc.Accuracy(testData, weights);
            Console.WriteLine("Prediction accuracy on test data = " +
            testAcc.ToString("F4"));

            Console.WriteLine("\nEnd LR binary classification demo\n");
            Console.ReadLine();
        }

    static double[][] MakeAllData(int numFeatures, int numRows, int seed)
    {
      Random rnd = new Random(seed);
      double[] weights = new double[numFeatures + 1]; // inc. b0
      for (int i = 0; i < weights.Length; ++i)
        weights[i] = 20.0 * rnd.NextDouble() - 10.0; // [-10.0 to +10.0]

      double[][] result = new double[numRows][]; // allocate matrix
      for (int i = 0; i < numRows; ++i)
        result[i] = new double[numFeatures + 1]; // Y in last column

      for (int i = 0; i < numRows; ++i) // for each row
      {
        double z = weights[0]; // the b0 
        for (int j = 0; j < numFeatures; ++j) // each feature / column except last
        {
          double x = 20.0 * rnd.NextDouble() - 10.0; // random X in [10.0, +10.0]
          result[i][j] = x; // store x
          double wx = x * weights[j + 1]; // weight * x 
          z += wx; // accumulate to get Y
        }
        double y = 1.0 / (1.0 + Math.Exp(-z));
        if (y > 0.55)  // slight bias towards 0
          result[i][numFeatures] = 1.0; // store y in last column
        else
          result[i][numFeatures] = 0.0;
      }
      Console.WriteLine("Data generation weights:");
      ShowVector(weights, 4, true);

      return result;
    } // MakeAllData

    static void MakeTrainTest(double[][] allData, int seed,
      out double[][] trainData, out double[][] testData)
    {
      Random rnd = new Random(seed);
      int totRows = allData.Length;
      int numTrainRows = (int)(totRows * 0.80); // 80% hard-coded
      int numTestRows = totRows - numTrainRows;
      trainData = new double[numTrainRows][];
      testData = new double[numTestRows][];

      double[][] copy = new double[allData.Length][]; // ref copy of all data
      for (int i = 0; i < copy.Length; ++i)
        copy[i] = allData[i];

      for (int i = 0; i < copy.Length; ++i) // scramble order
      {
        int r = rnd.Next(i, copy.Length); // use Fisher-Yates
        double[] tmp = copy[r];
        copy[r] = copy[i];
        copy[i] = tmp;
      }
      for (int i = 0; i < numTrainRows; ++i)
        trainData[i] = copy[i];

      for (int i = 0; i < numTestRows; ++i)
        testData[i] = copy[i + numTrainRows];
    } // MakeTrainTest


    public static void ShowData(double[][] data, int numRows,
      int decimals, bool indices)
    {
      int len = data.Length.ToString().Length;
      for (int i = 0; i < numRows; ++i)
      {
        if (indices == true)
          Console.Write("[" + i.ToString().PadLeft(len) + "]  ");
        for (int j = 0; j < data[i].Length; ++j)
        {
          double v = data[i][j];
          if (v >= 0.0)
            Console.Write(" "); // '+'
          Console.Write(v.ToString("F" + decimals) + "  ");
        }
        Console.WriteLine("");
      }
      Console.WriteLine(". . .");
      int lastRow = data.Length - 1;
      if (indices == true)
        Console.Write("[" + lastRow.ToString().PadLeft(len) + "]  ");
      for (int j = 0; j < data[lastRow].Length; ++j)
      {
        double v = data[lastRow][j];
        if (v >= 0.0)
          Console.Write(" "); // '+'
        Console.Write(v.ToString("F" + decimals) + "  ");
      }
      Console.WriteLine("\n");
    }

    static void ShowVector(double[] vector, int decimals, bool newLine)
    {
      for (int i = 0; i < vector.Length; ++i)
        Console.Write(vector[i].ToString("F" + decimals) + " ");
      Console.WriteLine("");
      if (newLine == true)
        Console.WriteLine("");
    }

  } // Program

  public class LogisticClassifier
  {
    private int numFeatures; // number of x variables aka features
    private double[] weights; // b0 = constant
    private Random rnd;

    public LogisticClassifier(int numFeatures)
    {
      this.numFeatures = numFeatures; 
      this.weights = new double[numFeatures + 1]; // [0] = b0 constant
      this.rnd = new Random(0);
      //for (int i = 0; i < weights.Length; ++i) // not necessary
      //  weights[i] = 0.01 * rnd.NextDouble(); // [0.00, 0.01)
    }

    public double[] Train(double[][] trainData, int maxEpochs, double alpha)
    {
      // alpha is the learning rate
      int epoch = 0;
      int[] sequence = new int[trainData.Length]; // random order
      for (int i = 0; i < sequence.Length; ++i)
        sequence[i] = i;

      while (epoch < maxEpochs)
      {
        ++epoch;

        if (epoch % 100 == 0 && epoch != maxEpochs)
        {
          double mse = Error(trainData, weights);
          Console.Write("epoch = " + epoch);
          Console.WriteLine("  error = " + mse.ToString("F4"));
        }

        Shuffle(sequence); // process data in random order

        // stochastic/online/incremental approach
        for (int ti = 0; ti < trainData.Length; ++ti)
        {
          int i = sequence[ti];
          double computed = ComputeOutput(trainData[i], weights);
          int targetIndex = trainData[i].Length - 1;
          double target = trainData[i][targetIndex];

          weights[0] += alpha * (target - computed) * 1; // the b0 weight has a dummy 1 input
          //weights[0] += alpha * (target - computed) * computed * (1 -computed) * 1; // alt. form
          for (int j = 1; j < weights.Length; ++j)
            weights[j] += alpha * (target - computed) * trainData[i][j - 1];
          //weights[j] += alpha * (target - computed) * computed * (1 - computed) * trainData[i][j - 1]; // alt. form
        }

        // batch/offline approach
        //double[] accumulatedGradients = new double[weights.Length]; // one acc for each weight

        //for (int i = 0; i < trainData.Length; ++i)  // accumulate
        //{
        //  double computed = ComputeOutput(trainData[i], weights); // no need to shuffle order
        //  int targetIndex = trainData[i].Length - 1;
        //  double target = trainData[i][targetIndex];
        //  accumulatedGradients[0] += (target - computed) * 1; // for b0
        //  for (int j = 1; j < weights.Length; ++j)
        //    accumulatedGradients[j] += (target - computed) * trainData[i][j - 1];
        //}

        //for (int j = 0; j < weights.Length; ++j) // update
        //  weights[j] += alpha * accumulatedGradients[j];
 
      } // while
      return this.weights; // by ref is somewhat risky
    } // Train

    private void Shuffle(int[] sequence)
    {
      for (int i = 0; i < sequence.Length; ++i)
      {
        int r = rnd.Next(i, sequence.Length);
        int tmp = sequence[r];
        sequence[r] = sequence[i];
        sequence[i] = tmp;
      }
    }

    private double Error(double[][] trainData, double[] weights)
    {
      // mean squared error using supplied weights
      int yIndex = trainData[0].Length - 1; // y-value (0/1) is last column
      double sumSquaredError = 0.0;
      for (int i = 0; i < trainData.Length; ++i) // each data
      {
        double computed = ComputeOutput(trainData[i], weights);
        double desired = trainData[i][yIndex]; // ex: 0.0 or 1.0
        sumSquaredError += (computed - desired) * (computed - desired);
      }
      return sumSquaredError / trainData.Length;
    }

    private double ComputeOutput(double[] dataItem, double[] weights)
    {
      double z = 0.0;
      z += weights[0]; // the b0 constant
      for (int i = 0; i < weights.Length - 1; ++i) // data might include Y
        z += (weights[i + 1] * dataItem[i]); // skip first weight
      return 1.0 / (1.0 + Math.Exp(-z));
    }

    private int ComputeDependent(double[] dataItem, double[] weights)
    {
      double y = ComputeOutput(dataItem, weights); // 0.0 to 1.0
      if (y <= 0.5)
        return 0;
      else
        return 1;
    }

    public double Accuracy(double[][] trainData, double[] weights)
    {
      int numCorrect = 0;
      int numWrong = 0;
      int yIndex = trainData[0].Length - 1;
      for (int i = 0; i < trainData.Length; ++i)
      {
        int computed = ComputeDependent(trainData[i], weights); 
        int target = (int)trainData[i][yIndex]; // risky?

        if (computed == target)
          ++numCorrect;
        else
          ++numWrong;
      }
      return (numCorrect * 1.0) / (numWrong + numCorrect);
    }
    }
}