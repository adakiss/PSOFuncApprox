using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATravelingSalesman
{
    class TravelingSalesmanProblem
    {
        private List<Town> towns;

        public TravelingSalesmanProblem()
        {
            towns = new List<Town>();
        }

        public List<Town> Towns { get { return towns; } }

        public void ReadTownsFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] splitLine = line.Split('\t');
                towns.Add(new Town() { X = int.Parse(splitLine[0]), Y = int.Parse(splitLine[1]) });
            }
        }

        public float Objective(List<Town> solution)
        {
            float sum_length = 0;
            for (int i = 0; i < solution.Count - 1; i++)
            {
                Town t1 = solution[i];
                Town t2 = solution[i + 1];
                sum_length += (float)Math.Sqrt(Math.Pow(t1.X - t2.X, 2) + Math.Pow(t1.Y - t2.Y, 2));
            }
            return sum_length;
        }
    }
}
