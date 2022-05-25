using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public class VectorClusterCentroid : VectorClusterPoint
    {
        ///
        /// Basic constructor
        ///
        /// Centroid x-coord
        /// Centroid y-coord
        public VectorClusterCentroid(Vector<double> vector) : base(vector)
        {
        }
    }
}
