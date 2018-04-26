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
            ImageSegmentationSolver solver = new ImageSegmentationSolver("slide_sample.bmp");
            solver.SegmentImage();
        }
    }
}
