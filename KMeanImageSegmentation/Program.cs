using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeanImageSegmentation
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageSegmentationProblem problem = new ImageSegmentationProblem();
            problem.ReadFromFile("slide_sample.bmp");
        }
    }
}
