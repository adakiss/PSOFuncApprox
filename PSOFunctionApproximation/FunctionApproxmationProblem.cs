using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOFunctionApproximation
{
    class FunctionApproxmationProblem
    {
        private List<ValuePair> knownValues;
        public static int Dimension = 5;

        public FunctionApproxmationProblem()
        {
            knownValues = new List<ValuePair>();
        }

        public void LoadKnownValuesFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach(string line in lines)
            {
                string[] splittedLine = line.Split('\t');
                knownValues.Add(new ValuePair() { X = float.Parse(splittedLine[0]), Y = float.Parse(splittedLine[1]) });
            }
        }

        public double Objective(double[] coefficients)
        {
            double sum_diff = 0;
            foreach(ValuePair valuePair in knownValues)
            {
                double x = valuePair.X;
                double y = coefficients[0] * Math.Pow(x - coefficients[1], 3) + coefficients[2] * Math.Pow(x - coefficients[3], 2) + coefficients[4] * x;
                double diff = Math.Pow(y - valuePair.Y, 2);
                sum_diff += diff;
            }
            return sum_diff;
        }
    }
}
