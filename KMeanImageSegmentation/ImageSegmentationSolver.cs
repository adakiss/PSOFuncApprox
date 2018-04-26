using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeanImageSegmentation
{
    class ImageSegmentationSolver
    {
        private ImageSegmentationProblem problem;
        private static Random rnd = new Random();
        private const int CLUSTER_CNT = 2;

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
            int iterator = 0;
            do
            {
                centorids = (int[])nextCentroids.Clone();
                SetCentroidsForClusters(clusters, centorids);
                ClearClusters(clusters);
                for(int i = 0; i < problem.GrayScale.Width;i++)
                {
                    for(int j = 0; j < problem.GrayScale.Height; j++)
                    {
                        int nearestCentroidIndex = GetNearestCentroidIndex(centorids, i, j);
                        clusters[nearestCentroidIndex].AddPoint(new Point() { X = i, Y = j });
                    }
                }
                for(int l = 0; l < CLUSTER_CNT; l++)
                {
                    nextCentroids[l] = GetNextCentroidValue(clusters[l]);
                }
                Console.WriteLine("Iteration: {0}", iterator++);
            } while (!CompareCentroids(centorids, nextCentroids));
            AssignColorsToClusters(clusters);
            problem.Result.Save("result.bmp");
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

        private int GetNearestCentroidIndex(int[] centroids, int x, int y)
        {
            int minDistance = int.MaxValue;
            int nearestCentroidIndex = -1;
            for (int k = 0; k < CLUSTER_CNT; k++)
            {
                int distance = Math.Abs(centroids[k] - problem.GrayScale.GetPixel(x, y).G);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestCentroidIndex = k;
                }
            }
            return nearestCentroidIndex;
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

        private void AssignColorsToClusters(Cluster[] clusters)
        {
            for (int i = 0; i < clusters.Length; i++)
            {
                foreach(Point pixel in clusters[i].Points)
                {
                    switch(i)
                    {
                        case 0:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.Blue);
                            break;
                        case 1:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.Red);
                            break;
                        case 2:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.Green);
                            break;
                        case 3:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.Yellow);
                            break;
                        case 4:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.Brown);
                            break;
                        case 5:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.MistyRose);
                            break;
                        case 6:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.AliceBlue);
                            break;
                        case 8:
                            problem.Result.SetPixel(pixel.X, pixel.Y, Color.Khaki);
                            break;
                    }
                }
            }
        }
    }
}
