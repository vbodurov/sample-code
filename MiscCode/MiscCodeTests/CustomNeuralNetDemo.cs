using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using UnityEngine;
using Random = System.Random;

namespace MiscCodeTests
{
    [TestFixture]
    public class CustomNeuralNetDemo
    {
        [Test]
        public void Run()
        {
            JsonConvert.DefaultSettings = () => { var settings = new JsonSerializerSettings(); settings.Converters.Add(new StringEnumConverter { CamelCaseText = true }); return settings; };
//            Console.WriteLine(JsonConvert.SerializeObject(each));

            IIncomeParser inPar = new IncomeParser();
            IIncomeNormalizer inNor = new IncomeNormalizer();
            var data = inPar.Read("../../Files/Income/adult.data");
            var trainInputs = inNor.Normalize(data);
            var test = inPar.Read("../../Files/Income/adult.test");
            var testInputs = inNor.Normalize(test);

            ICustomNeuralNet cnn = new CustomNeuralNet(IncomeData.NumberInputColumns,IncomeData.NumberInputColumns,1);
//            cnn.Train(trainInputs);//
            var outputs = cnn.ComputeOutputs(testInputs);
//
//            IIncomePredictionAccuracyComputer inAcc = new IncomePredictionAccuracyComputer();
//            var accuracy = inAcc.ComputeAccuracy(outputs, testInputs);
//
//            Console.WriteLine("Accuracy:"+accuracy);
        }
    }

    public interface ICustomNeuralNet
    {
        void Train(double[,] inputs);
        double[] ComputeOutputs(double[,] inputs);
    }
    public sealed class CustomNeuralNet : ICustomNeuralNet
    {
        private readonly Random _rnd;
        private readonly int _numInput;
        private readonly int _numHidden;
        private readonly int _numOutput;
        private readonly double[] _inputs;
        private readonly double[,] _ihWeights;
        private readonly double[] _hBiases;
        private readonly double[] _hidden;
        private readonly double[,] _hoWeights;
        private readonly double[] _oBiases;
        private readonly double[] _outputs;

        private readonly double[] _hGrads;
        private readonly double[] _oGrads;

        private readonly double[,] _ihPrevWeightsDelta;
        private readonly double[] _hPrevBiasesDelta;
        private readonly double[,] _hoPrevWeightsDelta;
        private readonly double[] _oPrevBiasesDelta;

        public CustomNeuralNet(int numInput, int numHidden, int numOutput)
        {
            _rnd = new System.Random(0); // for InitializeWeights() and Shuffle()

            _numInput = numInput;
            _numHidden = numHidden;
            _numOutput = numOutput;

            _inputs = new double[numInput];

            _ihWeights = new double[numInput, numHidden];
            _hBiases = new double[numHidden];
            _hidden = new double[numHidden];

            _hoWeights = new double[numHidden, numOutput];
            _oBiases = new double[numOutput];
            _outputs = new double[numOutput];

            // back-prop related arrays below
            _hGrads = new double[numHidden];
            _oGrads = new double[numOutput];

            _ihPrevWeightsDelta = new double[numInput, numHidden];
            _hPrevBiasesDelta = new double[numHidden];
            _hoPrevWeightsDelta = new double[numHidden, numOutput];
            _oPrevBiasesDelta = new double[numOutput];
        } // ctor
        void ICustomNeuralNet.Train(double[,] inputs)
        {
            throw new NotImplementedException();
        }
        double[] ICustomNeuralNet.ComputeOutputs(double[,] inputs)
        {
            throw new NotImplementedException();
        }
    }
    public interface IIncomeParser { IncomeData[] Read(string fileLocation); }
    public sealed class IncomeParser : IIncomeParser
    {
        IncomeData[] IIncomeParser.Read(string fileLocation)
        {
            var data = new List<IncomeData>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var line in File.ReadLines(fileLocation))
            {
                if(line.Trim() == "") continue;
                var a = line.Trim().Split(',').Select(s => s.Trim()).ToArray();
                if(a.Contains("?")) continue;
                if(a.Length < 10) continue;
               
                var index = 0;
                // ReSharper disable PossibleMultipleEnumeration
                data.Add(new IncomeData
                {
                    Age = ParseInt(a[index++]),
                    WorkClass = ParseEnum<WorkClass>(a[index++]),
                    FinalSamplingWeight = ParseInt(a[index++]),
                    EducLevel = ParseEnum<EducLevel>(a[index++]),
                    EducYears = ParseInt(a[index++]),
                    MaritalStatus = ParseEnum<MaritalStatus>(a[index++]),
                    OccupationType = ParseEnum<OccupationType>(a[index++]),
                    RelationshipType = ParseEnum<RelationshipType>(a[index++]),
                    RaceKind = ParseEnum<RaceKind>(a[index++]),
                    Sex = ParseEnum<Sex>(a[index++]),
                    CapitalGain = ParseInt(a[index++]),
                    CapitalLoss = ParseInt(a[index++]),
                    HoursPerWeek = ParseInt(a[index++]),
                    NativeCountry = ParseEnum<NativeCountry>(a[index++]),
                    HasIncomeAbove50K = a[index] == ">50K",
                });
            }
            return data.ToArray();
        }
        private int ParseInt(string str) { return int.Parse(str); }
        private T ParseEnum<T>(string val)
        {
            val = val.Trim();
            var capitlize = false;
            var sb = new StringBuilder();
            foreach (char ch in val)
            {
                if(sb.Length == 0 && char.IsDigit(ch)) sb.Append("G");

                if(ch == '-' || ch == '&')
                {
                    capitlize = true;
                    continue;
                }
                if(ch == '(') break;
                sb.Append(capitlize ? char.ToUpperInvariant(ch) : ch);
            }
            return (T)Enum.Parse(typeof(T), sb.ToString(), true);
        }
    }
    public interface IIncomeNormalizer
    {
        double[,] Normalize(IncomeData[] data);
    }
    public sealed class IncomeNormalizer : IIncomeNormalizer
    {
        double[,] IIncomeNormalizer.Normalize(IncomeData[] data)
        {
            var matrix = new double[data.Length, IncomeData.NumberInputColumns + IncomeData.NumberOutputColumns];
            var nonEnumColumns = new List<int>();
            for(var r = 0; r < data.Length; ++r)
            {
                var cols = ExtractColumns(data[r], nonEnumColumns);
                for(var c = 0; c < cols.Length; ++c)
                {
                    matrix[r, c] = cols[c];
                }
            }
            var matrixRows = matrix.GetLength(0);
//            var matrixCols = matrix.GetLength(1);
            foreach (var col in nonEnumColumns)
            {
                var sum = 0.0;
                for (var i = 0; i < matrixRows; ++i)
                    sum += matrix[i,col];
                var mean = sum/matrixRows;
                sum = 0.0;
                for (var i = 0; i < matrixRows; ++i)
                    sum += (matrix[i,col] - mean) * (matrix[i,col] - mean);
                var sd = Math.Sqrt(sum/(matrixRows - 1));
                for (var i = 0; i < matrixRows; ++i)
                    matrix[i,col] = (matrix[i,col] - mean)/sd;
            }
            return matrix;
        }

        private static double[] ExtractColumns(IncomeData data, List<int> nonEnumColumns)
        {
            var isFirstCall = nonEnumColumns.Count == 0;
            var cols = new double[IncomeData.NumberInputColumns + IncomeData.NumberOutputColumns];
            var i = 0;
            i = PopulateNumeric(cols, i, data.Age, isFirstCall, nonEnumColumns);
            i = PopulateEnum(cols, i, 8, (int)data.WorkClass);
            i = PopulateNumeric(cols, i, data.FinalSamplingWeight, isFirstCall, nonEnumColumns);
            i = PopulateEnum(cols, i, 16, (int)data.EducLevel);
            i = PopulateNumeric(cols, i, data.EducYears, isFirstCall, nonEnumColumns);
            i = PopulateEnum(cols, i, 7, (int)data.MaritalStatus);
            i = PopulateEnum(cols, i, 14, (int)data.OccupationType);
            i = PopulateEnum(cols, i, 6, (int)data.RelationshipType);
            i = PopulateEnum(cols, i, 5, (int)data.RaceKind);
            i = PopulateEnum(cols, i, 2, (int)data.Sex);
            i = PopulateNumeric(cols, i, data.CapitalGain, isFirstCall, nonEnumColumns);
            i = PopulateNumeric(cols, i, data.CapitalLoss, isFirstCall, nonEnumColumns);
            i = PopulateNumeric(cols, i, data.HoursPerWeek, isFirstCall, nonEnumColumns);
            i = PopulateEnum(cols, i, 41, (int)data.NativeCountry);
            PopulateNumeric(cols, i, data.HasIncomeAbove50K?1:0, isFirstCall, nonEnumColumns);
            return cols;
        }
        private static int PopulateNumeric(double[] cols, int i, int value, bool isFirstCall, List<int> nonEnumColumns)
        {
            cols[i++] = value;
            if(isFirstCall) nonEnumColumns.Add(i - 1);
            return i;
        }
        private static int PopulateEnum(double[] cols, int i, int max, int value)
        {
            for(var n = 1; n <= max; ++n)
            {
                cols[i++] = value == n ? 1 : 0;
            }
            return i;
        }
    }
    public interface IIncomePredictionAccuracyComputer
    {
        double ComputeAccuracy(double[] outputs, double[,] testInputs);
    }
    public sealed class IncomePredictionAccuracyComputer : IIncomePredictionAccuracyComputer
    {
        double IIncomePredictionAccuracyComputer.ComputeAccuracy(double[] outputs, double[,] testInputs)
        {
            throw new NotImplementedException();
        }
    }
    public class IncomeData
    {
        public const int NumberInputColumns = 105;//1+8+1+16+1+7+14+6+5+2+1+1+1+41
        public const int NumberOutputColumns = 1;
        public int Age { get; set; }//1
        public WorkClass WorkClass { get; set; }//8
        public int FinalSamplingWeight { get; set; }//1
        public EducLevel EducLevel { get; set; }//16
        public int EducYears { get; set; }//1
        public MaritalStatus MaritalStatus { get; set; }//7
        public OccupationType OccupationType { get; set; }//14
        public RelationshipType RelationshipType { get; set; }//6
        public RaceKind RaceKind { get; set; }//5
        public Sex Sex { get; set; }//2
        public int CapitalGain { get; set; }//1
        public int CapitalLoss { get; set; }//1
        public int HoursPerWeek { get; set; }//1
        public NativeCountry NativeCountry { get; set; }//41
        public bool HasIncomeAbove50K { get; set; }//1 (has=1 not=0)
    }
    public enum WorkClass { Private = 1, SelfEmpNotInc, SelfEmpInc, FederalGov, LocalGov, StateGov, WithoutPay, NeverWorked }//8
    public enum EducLevel { Bachelors = 1, SomeCollege, G11th, HSgrad, ProfSchool, AssocAcdm, AssocVoc, G9th, G7th8th, G12th, Masters, G1st4th, G10th, Doctorate, G5th6th, Preschool }//16
    public enum MaritalStatus { NeverMarried = 1, Separated, Widowed, Divorced, MarriedSpouseAbsent, MarriedCivSpouse, MarriedAFSpouse }//7
    public enum OccupationType { TechSupport = 1, CraftRepair, OtherService, Sales, ExecManagerial, ProfSpecialty, HandlersCleaners, MachineOpInspct, AdmClerical, FarmingFishing, TransportMoving, PrivHouseServ, ProtectiveServ, ArmedForces  }//14
    public enum RelationshipType { Wife = 1, Husband, OwnChild, OtherRelative, Unmarried, NotInFamily }//6
    public enum RaceKind { White = 1, AsianPacIslander, AmerIndianEskimo, Other, Black }//5
    public enum Sex { Female = 1, Male }//2
    public enum NativeCountry { UnitedStates = 1, Cambodia, England, PuertoRico, Canada, Germany, OutlyingUS, India, Japan, Greece, South, China, Cuba, Iran, Honduras, Philippines, Italy, Poland, Jamaica, Vietnam, Mexico, Portugal, Ireland, France, DominicanRepublic, Laos, Ecuador, Taiwan, Haiti, Columbia, Hungary, Guatemala, Nicaragua, Scotland, Thailand, Yugoslavia, ElSalvador, TrinadadTobago, Peru, Hong, HolandNetherlands }//41
}