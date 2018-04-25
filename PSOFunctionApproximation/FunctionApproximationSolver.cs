using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOFunctionApproximation
{
    class FunctionApproximationSolver
    {
        private const int POPULATION_SIZE = 150;
        private static Random RND = new Random();
        private FunctionApproxmationProblem problem;
        private double[] globalOptimum;
        private int lastGlobalOptimumIndex;

        public FunctionApproximationSolver(string path)
        {
            problem = new FunctionApproxmationProblem();
            problem.LoadKnownValuesFromFile(path);
            globalOptimum = new double[FunctionApproxmationProblem.Dimension];
            for(int i = 0; i < globalOptimum.Length; i++)
            {
                globalOptimum[i] = 10000;
            }
            lastGlobalOptimumIndex = 0;
        }

        public double[] SolveFunctionApproximation()
        {
            WriteActualGlobalOptToConsole(-1);
            List<PSOItem> population = InitializePopulation();
            Evaulation(population);
            for(int i = 0; i-lastGlobalOptimumIndex < 10000; i++)
            {
                double prevGlobalOptFitness = problem.Objective(globalOptimum);
                CalculateVelocity(population);
                MovePSOItems(population);
                Evaulation(population);
                if(problem.Objective(globalOptimum) < prevGlobalOptFitness)
                {
                    lastGlobalOptimumIndex = i;
                    WriteActualGlobalOptToConsole(i);
                }
            }
            Console.WriteLine("PSO function terminated");
            return globalOptimum;
        }

        private List<PSOItem> InitializePopulation()
        {
            List<PSOItem> result = new List<PSOItem>();
            for(int i = 0; i < POPULATION_SIZE; i++)
            {
                double[] randomValues = new double[FunctionApproxmationProblem.Dimension];
                for(int j = 0; j < randomValues.Length; j++)
                {
                    randomValues[j] = RND.Next(-10, 11);
                }
                result.Add(new PSOItem() { Position = (double[])randomValues.Clone(), LocalOptimum = (double[])randomValues.Clone(), Velocity = (double[])randomValues.Clone() });
            }
            return result;
        }

        private void Evaulation(List<PSOItem> population)
        {
            foreach(PSOItem item in population)
            {
                double actFitness = problem.Objective(item.Position);
                if (actFitness <= problem.Objective(item.LocalOptimum))
                {
                    item.LocalOptimum = (double[])item.Position.Clone();
                    if(actFitness < problem.Objective(globalOptimum))
                    {
                        globalOptimum = (double[])item.Position.Clone();
                    }
                }
            }
        }

        private void CalculateVelocity(List<PSOItem> population)
        {
            double omega = 0.1;
            double fiP = 0.7;
            double fiG = 0.9;
            foreach(PSOItem item in population)
            {
                for(int i = 0; i < item.Velocity.Length; i++)
                {
                    double rndP = randomSign(RND.NextDouble());
                    double rndG = randomSign(RND.NextDouble());
                    double value = omega * item.Velocity[i] + fiP * rndP * (item.LocalOptimum[i] - item.Position[i]) + fiG * rndG * (globalOptimum[i] - item.Position[i]);
                    item.Velocity[i] = value;
                }

            }
        }

        private double randomSign(double value)
        {
            double result = value;
            if (RND.Next(0, 2) == 1)
            {
                result = result * -1;
            }
            return result;
        }


        private void MovePSOItems(List<PSOItem> population)
        {
            foreach(PSOItem item in population)
            {
                for(int i = 0; i < item.Position.Length; i++)
                {
                    item.Position[i] += item.Velocity[i];
                }
            }
        }

        private void WriteActualGlobalOptToConsole(int iteration)
        {
            Console.Clear();
            Console.WriteLine("Values in iteration {0}: ", iteration);
            for(int i = 0; i < globalOptimum.Length; i++)
            {
                Console.WriteLine(globalOptimum[i]);
            }
            Console.WriteLine("Global optimum fitness: {0}", problem.Objective(globalOptimum));
        }
    }
}
