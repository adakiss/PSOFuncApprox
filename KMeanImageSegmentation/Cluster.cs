using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeanImageSegmentation
{
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Cluster
    {
        public int Centroid { get; set; }
        private List<Point> points;
        
        public Cluster()
        {
            points = new List<Point>();
        }

        public void ClearPoints()
        {
            points.Clear();
        }

        public void AddPoint(Point p)
        {
            points.Add(p);
        }

        public List<Point> Points
        {
            get
            {
                return points;
            }
        }
    }
}
