using System;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization.Distances
{
    public interface IDistanceMeasure<T> where T : struct, IEquatable<T>, IFormattable
    {
        double Compute(Vector<T> a, Vector<T> b);
    }
}