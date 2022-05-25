using System.Collections.Generic;
using AlgorithmsLibrary.Clusterization.Distances;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public abstract class VectorClusterer
    {
        public IDistanceMeasure<double> DistanceMeasure { get; }
        protected VectorClusterer(IDistanceMeasure<double> measure) => DistanceMeasure = measure;

        public abstract IEnumerable<VectorCluster> Cluster(IEnumerable<VectorClusterPoint> points);

        protected double Distance(Vector<double> a, Vector<double> b)
        {
            return DistanceMeasure.Compute(a, b);
        }
    }
}