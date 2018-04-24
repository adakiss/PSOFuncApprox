using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOFunctionApproximation
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionApproximationSolver solver = new FunctionApproximationSolver("FuncAppr1.txt");
            solver.SolveFunctionApproximation();
            Console.ReadLine();
        }
    }
}
