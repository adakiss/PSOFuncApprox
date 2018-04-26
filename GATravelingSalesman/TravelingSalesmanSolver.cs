using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATravelingSalesman
{
    class TravelingSalesmanSolver
    {
        private const int POPULATION_SIZE = 150;
        private const int ELITISM = 10;
        private const int MATINGPOOL_SIZE = 50;
        private static Random rnd = new Random();
        private TravelingSalesmanProblem problem;

        public TravelingSalesmanSolver(TravelingSalesmanProblem problem, string inputPath)
        {
            this.problem = problem;
            this.problem.ReadTownsFromFile(inputPath);

        }

        public Solution SolveTravelingSalesmanProblem()
        {
            List<Solution> population = InitializePopulation();
            Evaluation(ref population);
            Solution bestSolution = population[0];
            for (int i = 0; i < 10000; i++)
            {
                List<Solution> matinPool = GetMatingPool(population);
                List<Solution> nextGen = new List<Solution>();
                nextGen.AddRange(population.GetRange(0, ELITISM));
                while (nextGen.Count < POPULATION_SIZE)
                {
                    Solution[] parents = GetParents(matinPool);
                    Solution child = Crossover(parents);
                    Mutate(child);
                    nextGen.Add(child);
                }
                population = nextGen;
                Evaluation(ref population);
                if (bestSolution.Fitness > population[0].Fitness)
                {
                    bestSolution = population[0];
                    Console.Clear();
                    Console.WriteLine("Actual best solution: " + bestSolution.Fitness);
                    Console.WriteLine("Number of cities: " + bestSolution.Route.Count);
                    WriteRouteToConsole(bestSolution.Route);
                }
            }
            Console.WriteLine("GA has been terminated");
            return bestSolution;

        }

        private List<Solution> InitializePopulation()
        {
            List<Solution> population = new List<Solution>();
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                population.Add(new Solution() { Route = problem.Towns });
            }
            Console.WriteLine("Init population - Size: " + population.Count);
            return population;
        }

        private void Evaluation(ref List<Solution> population)
        {
            foreach (Solution solution in population)
            {
                solution.Fitness = problem.Objective(solution.Route);
            }
            population = population.OrderBy(sol => sol.Fitness).ToList();
        }

        private List<Solution> GetMatingPool(List<Solution> population)
        {
            List<Solution> matingPool = new List<Solution>();
            matingPool.AddRange(population.GetRange(ELITISM, MATINGPOOL_SIZE));
            return matingPool;
        }

        private Solution[] GetParents(List<Solution> matingPool)
        {
            Solution[] parent = new Solution[2];
            parent[0] = matingPool[rnd.Next(0, MATINGPOOL_SIZE)];
            parent[1] = matingPool[rnd.Next(0, MATINGPOOL_SIZE)];
            return parent;
        }

        private Solution Crossover(Solution[] parents)
        {
            Solution child = new Solution() { Route = new List<Town>() };
            int splitIndex = rnd.Next(0, parents[0].Route.Count);
            child.Route.AddRange(parents[0].Route.GetRange(0, splitIndex));
            for (int i = 0; child.Route.Count < parents[0].Route.Count; i++)
            {
                if (!child.Route.Contains(parents[1].Route[i]))
                {
                    child.Route.Add(parents[1].Route[i]);
                }
            }
            return child;
        }

        private void Mutate(Solution childToMutate)
        {
            int switchCount = rnd.Next(0, 3);
            for (int i = 0; i < switchCount; i++)
            {
                int index1 = rnd.Next(0, childToMutate.Route.Count);
                int index2 = rnd.Next(0, childToMutate.Route.Count);
                Town t = childToMutate.Route[index1];
                childToMutate.Route[index1] = childToMutate.Route[index2];
                childToMutate.Route[index2] = t;
            }
        }

        private void WriteRouteToConsole(List<Town> towns)
        {
            foreach(Town town in towns)
            {
                Console.Write(town.X + "-" + town.Y + "     ");
            }
        }
    }
}

