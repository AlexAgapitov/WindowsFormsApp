using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public sealed class VectorCluster : IEquatable<VectorCluster>
    {
        public VectorCluster() => Points = new List<VectorClusterPoint>();

        public VectorCluster(VectorClusterCentroid centroid)
        {
            Points = new List<VectorClusterPoint>();
            Centroid = centroid;
        }

        public IList<VectorClusterPoint> Points { get; }
        public VectorClusterCentroid Centroid { get; set; }

        public void CalculateCentroid()
        {
            if (Points.Count == 0)
                return;

            int vectorSize = (from point in Points select point.Vector.Count).Min();
            double[] centroid = new double[vectorSize];

            for (int i = 0; i < vectorSize; i++)
            {
                double curSum = Convert.ToDouble(Vector<double>.Build.DenseOfEnumerable(from point in Points select point.Vector[i]).Sum());
                centroid[i] = curSum / Points.Count;
            }

            Centroid = new VectorClusterCentroid(Vector<double>.Build.DenseOfArray(centroid));
        }

        public override int GetHashCode()
        {
            int result = 37;
            foreach (var point in Points)
            {
                result += (13 * result) + (point == null ? 0 : point.GetHashCode());
            }
            return result;
        }

        public override bool Equals(object obj) => this.Equals(obj as VectorCluster);

        public bool Equals(VectorCluster other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Points.SequenceEqual(other.Points);
        }
    }
}