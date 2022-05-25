using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization.Distances
{
    public class MinkowskiDistance<T> : IDistanceMeasure<T> where T : struct, IEquatable<T>, IFormattable
    {
        public MinkowskiDistance() : this(2.0d)
        {
        }

        public MinkowskiDistance(double p)
        {
            this.P = p;
        }

        public double P { get; }

        public double Compute(Vector<T> a, Vector<T> b)
        {
            return Distance.Minkowski(this.P, a, b);
        }
    }
}