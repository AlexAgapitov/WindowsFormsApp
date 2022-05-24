using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public class VectorClusterPoint
    {
        ///
        /// Gets or sets Vector of the point
        ///
        public Vector<double> Vector { get; set; }

        ///
        /// Gets or sets some additional data for point
        ///
        public object Tag { get; set; }

        ///
        /// Gets or sets cluster index
        ///
        public double ClusterIndex { get; set; }

        ///
        /// Basic constructor
        ///
        /// X-coord
        /// Y-coord
        public VectorClusterPoint(Vector<double> vector)
        {
            this.Vector = vector;
            this.ClusterIndex = -1;
        }

        ///
        /// Basic constructor
        ///
        /// X-coord
        /// Y-coord
        public VectorClusterPoint(Vector<double> vector, object tag)
        {
            this.Vector = vector;
            this.Tag = tag;
            this.ClusterIndex = -1;
        }

        public override int GetHashCode()
        {
            int result = 37;
            foreach (var coordinate in Vector)
            {
                result += (13 * result) + coordinate.GetHashCode();
            }
            return result;
        }

        public override bool Equals(object obj) => this.Equals(obj as VectorClusterPoint);

        public bool Equals(VectorClusterPoint other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Vector.SequenceEqual(other.Vector);
        }
    }
}
