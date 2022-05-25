using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmsLibrary.Clusterization.Distances;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public class VectorKMeansPlusPlus : VectorClusterer
    {
        public int ClustersCount { get; }
        private IEnumerable<VectorCluster> Clusters { get; set; }

        private readonly Random _rnd = new Random();

        public VectorKMeansPlusPlus(IEnumerable<VectorClusterPoint> points, int clustersCount) : this(points, clustersCount, new EuclideanDistance<double>()) { }

        public VectorKMeansPlusPlus(IEnumerable<VectorClusterPoint> points, int clustersCount, IDistanceMeasure<double> measure) : base(measure)
        {
            this.ClustersCount = clustersCount;

            Clusters = new List<VectorCluster>(InitializeClustersCentroids(points));
        }

        public VectorKMeansPlusPlus(IList<VectorClusterCentroid> centroids) : this(centroids, new EuclideanDistance<double>()) { }

        public VectorKMeansPlusPlus(IList<VectorClusterCentroid> centroids, IDistanceMeasure<double> measure) : base(measure)
        {
            this.ClustersCount = centroids.Count;

            List<VectorCluster> clusters = new List<VectorCluster>();
            for (int i = 0; i < centroids.Count; i++)
            {
                clusters.Add(new VectorCluster(centroids[i]));
            }
            Clusters = new List<VectorCluster>(clusters);
        }

        public override IEnumerable<VectorCluster> Cluster(IEnumerable<VectorClusterPoint> points)
        {
            IEnumerable<VectorCluster> clusters = new List<VectorCluster>(Clusters);

            IList<VectorClusterCentroid> oldCentroids = new List<VectorClusterCentroid>();
            IList<VectorClusterCentroid> curCentroids = new List<VectorClusterCentroid>();

            do
            {
                oldCentroids = new List<VectorClusterCentroid>(curCentroids);
                RefillClusters(points, clusters);
                UpdateCentroids(clusters);
                curCentroids = (from cluster in clusters select cluster.Centroid).ToList();

                // Условие останова - отсутствие изменений в центройдах
            } while (!oldCentroids.SequenceEqual(curCentroids));

            return clusters;
        }

        IEnumerable<VectorCluster> InitializeClustersCentroids(IEnumerable<VectorClusterPoint> points)
        {
            // Выбрать первый центроид случайным образом (среди всех точек)
            VectorClusterPoint firstCentroid = points.ElementAt(_rnd.Next(0, points.Count()));

            IList<VectorCluster> clusters = new List<VectorCluster>();
            clusters.Add(new VectorCluster(new VectorClusterCentroid(firstCentroid.Vector)));

            // Найти все остальные центройды
            while (clusters.Count() < ClustersCount)
            {
                clusters.Add(new VectorCluster(new VectorClusterCentroid(GetNextCentroid(clusters, points))));
            }

            return clusters;
        }

        Vector<double> GetNextCentroid(IEnumerable<VectorCluster> centroids, IEnumerable<VectorClusterPoint> points)
        {
            double sumDistances = 0;

            // Для каждой точки найти расстояние до ближайшего центроида и подсчитать сумму этих расстояний.
            foreach (var point in points)
            {
                sumDistances += GetDistanceToNearestNeighbor(point, centroids);
            }

            /*
             * Выбрать следующий центроид так, чтобы вероятность выбора точки
             * была пропорциональна вычисленному для неё квадрату расстояния.
             * Это можно сделать следующим образом. После накопления суммы найти значение Rnd=random(0.0,1.0)*Sum.
             * Rnd случайным образом укажет на число из интервала [0; Sum), и нам остаётся только определить,
             * какой точке это соответствует. Для этого нужно снова начать подсчитывать сумму 
             * до тех пор, пока сумма не превысит Rnd. Как только это случится, суммирование останавливается,
             * и мы можем взять текущую точку в качестве центроида. При выборе каждого следующего центроида
             * специально следить за тем, чтобы он не совпал с одной из уже выбранных в качестве центроидов
             * точек, не нужно, так как вероятность повторного выбора некоторой точки равна 0.
             */
            double nextCentroidValue = _rnd.NextDouble() * sumDistances;
            sumDistances = 0;

            foreach (var point in points)
            {
                sumDistances += GetDistanceToNearestNeighbor(point, centroids);
                if (sumDistances > nextCentroidValue)
                {
                    return point.Vector;
                }
            }

            return points.ElementAt(_rnd.Next(0, points.Count())).Vector;
        }

        double GetDistanceToNearestNeighbor(VectorClusterPoint point, IEnumerable<VectorCluster> neighbors)
        {
            double minDistance = double.MaxValue;

            foreach (var neighbor in neighbors)
            {
                double curDistance = this.Distance(point.Vector, neighbor.Centroid.Vector);
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                }
            }
            return minDistance;
        }

        VectorCluster GetNearestCluster(VectorClusterPoint point, IEnumerable<VectorCluster> clusters)
        {
            double minDistance = double.MaxValue;
            VectorCluster nearestCluster = null;

            foreach (var cluster in clusters)
            {
                double curDistance = this.Distance(point.Vector, cluster.Centroid.Vector);
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    nearestCluster = cluster;
                }
            }
            return nearestCluster;
        }

        /// <summary>
        /// Перезаполняет кластеры по их центройдам.
        /// </summary>
        /// <param name="points">Точки</param>
        /// <param name="clusters">Кластеры</param>
        /// <returns></returns>
        void RefillClusters(IEnumerable<VectorClusterPoint> points, IEnumerable<VectorCluster> clusters)
        {
            foreach (var cluster in clusters)
            {
                cluster.Points.Clear();
            }

            foreach (var point in points)
            {
                GetNearestCluster(point, clusters).Points.Add(point);
            }
        }

        void UpdateCentroids(IEnumerable<VectorCluster> clusters)
        {
            foreach (var cluster in clusters)
            {
                cluster.CalculateCentroid();
            }
        }
    }
}
