using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeanImageSegmentation
{
    class ImageSegmentationSolver
    {
        private ImageSegmentationProblem problem;
        private static Random rnd = new Random();
        private const int CLUSTER_CNT = 3;

        public ImageSegmentationSolver(string path)
        {
            problem = new ImageSegmentationProblem();
            problem.ReadFromFile(path);
        }

        public void SegmentImage()
        {
            int[] centorids = new int[CLUSTER_CNT];
            Cluster[] clusters = InitializeClusters();
            int[] nextCentroids = InitializeCentroids();
            do
            {
                centorids = (int[])nextCentroids.Clone();
                SetCentroidsForClusters(clusters, centorids);
                ClearClusters(clusters);
                for(int i = 0; i < problem.GrayScale.Width;i++)
                {
                    for(int j = 0; j < problem.GrayScale.Height; j++)
                    {
                        int minDistance = int.MaxValue;
                        int nearestCentroidIndex = -1;
                        for(int k = 0; k < CLUSTER_CNT; k++)
                        {
                            int distance = Math.Abs(centorids[i] - problem.GrayScale.GetPixel(i, j).G);
                            if (distance < minDistance)
                            {
                                minDistance = distance;
                                nearestCentroidIndex = k;
                            }
                        }
                        clusters[nearestCentroidIndex].AddPoint(new Point() { X = i, Y = j });
                    }
                }

                for(int l = 0; l < CLUSTER_CNT; l++)
                {
                    nextCentroids[l] = GetNextCentroidValue(clusters[l]);
                }

            } while (CompareCentroids(centorids, nextCentroids));
        }

        private int[] InitializeCentroids()
        {
            int[] result = new int[CLUSTER_CNT];
            for(int i = 0; i < CLUSTER_CNT; i++)
            {
                result[i] = rnd.Next(0, 256);
            }
            return result;
        }

        private Cluster[] InitializeClusters()
        {
            Cluster[] result = new Cluster[CLUSTER_CNT];
            for(int i= 0; i< CLUSTER_CNT;i++)
            {
                result[i] = new Cluster();
            }
            return result;
        }

        private bool CompareCentroids(int[] centroids, int[] nextCentorids)
        {
            for(int i= 0; i < CLUSTER_CNT; i++)
            {
                if (centroids[i] != nextCentorids[i])
                    return false;
            }
            return true;
        }

        private void ClearClusters(Cluster[] clusters)
        {
            foreach(Cluster cluster in clusters)
            {
                cluster.ClearPoints();
            }
        }

        private void SetCentroidsForClusters(Cluster[] clusters, int[] centroids)
        {
            for(int i = 0; i < CLUSTER_CNT; i++)
            {
                clusters[i].Centroid = centroids[i];
            }
        }

        private int GetNextCentroidValue(Cluster cluster)
        {
            int sum = 0;
            foreach(Point point in cluster.Points)
            {
                sum += problem.GrayScale.GetPixel(point.X, point.Y).B;
            }
            return sum / cluster.Points.Count;
        }
    }
}
