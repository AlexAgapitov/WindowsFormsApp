using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization.Distances
{
    public class ChebyshevDistance<T> : IDistanceMeasure<T> where T : struct, IEquatable<T>, IFormattable
    {
        public double Compute(Vector<T> a, Vector<T> b)
        {
            return Distance.Chebyshev(a, b);
        }
    }
}