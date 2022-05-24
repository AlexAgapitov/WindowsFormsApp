using System;
using System.Collections.Generic;
using AlgorithmsLibrary.Clusterization.Distances;

namespace AlgorithmsLibrary.Clusterization
{
    public class VectorDBScanClusterer : VectorClusterer
    {
        private enum PointStatus
        {
            Noise,
            PartOfCluster,
        }

        public double Epsilon { get; private set; }

        public int MinPts { get; private set; }

        public VectorDBScanClusterer(double epsilon, int minPts) : this(epsilon, minPts, new EuclideanDistance<double>())
        {
        }

        public VectorDBScanClusterer(double epsilon, int minPts, IDistanceMeasure<double> measure) : base(measure)
        {
            if (epsilon < 0.0d)
            {
                throw new ArgumentOutOfRangeException("epsilon", epsilon, "Argument must be greather than 0.0");
            }

            if (minPts < 0)
            {
                throw new ArgumentOutOfRangeException("minPts", minPts, "Argument must be greather than 0.0");
            }

            this.Epsilon = epsilon;
            this.MinPts = minPts;
        }

        public override IEnumerable<VectorCluster> Cluster(IEnumerable<VectorClusterPoint> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }

            var clusters = new List<VectorCluster>();
            var visited = new Dictionary<VectorClusterPoint, PointStatus>();

            foreach (var point in points)
            {
                if (visited.ContainsKey(point))
                {
                    continue;
                }

                var neighbors = this.GetNeighbors(point, points);
                if (neighbors.Count >= this.MinPts)
                {
                    var cluster = new VectorCluster();
                    clusters.Add(this.ExpandCluster(cluster, point, neighbors, points, visited));
                }
                else
                {
                    visited[point] = PointStatus.Noise;
                }
            }

            return clusters;
        }

        private VectorCluster ExpandCluster(
            VectorCluster cluster,
            VectorClusterPoint point,
            IList<VectorClusterPoint> neighbors,
            IEnumerable<VectorClusterPoint> points,
            IDictionary<VectorClusterPoint, PointStatus> visited)
        {
            cluster.Points.Add(point);
            visited[point] = PointStatus.PartOfCluster;

            IList<VectorClusterPoint> seeds = new List<VectorClusterPoint>(neighbors);
            var index = 0;
            while (index < seeds.Count)
            {
                var current = seeds[index];
                var status = PointStatus.Noise;

                if (!visited.ContainsKey(current))
                {
                    var currentNeghbors = this.GetNeighbors(current, points);
                    if (currentNeghbors.Count >= this.MinPts)
                    {
                        seeds = this.Merge(seeds, currentNeghbors);
                    }
                }

                if (status != PointStatus.PartOfCluster)
                {
                    visited[current] = PointStatus.PartOfCluster;
                    cluster.Points.Add(current);
                }

                index++;
            }

            return cluster;
        }

        private IList<VectorClusterPoint> GetNeighbors(VectorClusterPoint point, IEnumerable<VectorClusterPoint> points)
        {
            var neighbors = new List<VectorClusterPoint>();
            foreach (var neighbor in points)
            {
                if (!point.Equals(neighbor) && this.Distance(point.Vector, neighbor.Vector) <= this.Epsilon)
                {
                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }

        private IList<VectorClusterPoint> Merge(IList<VectorClusterPoint> one, IList<VectorClusterPoint> two)
        {
            var setOne = new HashSet<VectorClusterPoint>(one);
            var setTwo = new HashSet<VectorClusterPoint>(two);

            setOne.UnionWith(setTwo);
            return new List<VectorClusterPoint>(setOne);
        }
    }
}
