using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public sealed class VectorCMeansAlgorithm
    {
        private readonly Random _rnd = new Random();

        ///
        /// Array containing all points used by the algorithm
        ///
        private List<VectorClusterPoint> Points;

        ///
        /// Array containing all clusters handled by the algorithm
        ///
        public List<VectorClusterCentroid> Clusters { get; private set; }

        ///
        /// Gets or sets membership matrix
        ///
        public double[,] U;

        ///
        /// Gets or sets the current fuzzyness factor
        ///
        private double Fuzzyness;

        ///
        /// Algorithm precision
        ///
        private double Eps = Math.Pow(10, -5);

        ///
        /// Gets or sets objective function
        ///
        private double J { get; set; }

        ///
        /// Gets or sets log message
        ///
        public string Log { get; set; }

        ///
        /// Initialize the algorithm with points and initial clusters
        ///
        /// Points
        /// Clusters
        /// The fuzzyness factor to be used
        public VectorCMeansAlgorithm(List<VectorClusterPoint> points, List<VectorClusterCentroid> clusters, float fuzzy)
        {
            this.Points = points ?? throw new ArgumentNullException("points");
            this.Clusters = clusters ?? throw new ArgumentNullException("clusters");

            this.Fuzzyness = fuzzy;
            U = new double[this.Points.Count, this.Clusters.Count];

            InitData();
        }

        public VectorCMeansAlgorithm(List<VectorClusterPoint> points, int clustersCount, float fuzzy)
        {
            this.Points = points ?? throw new ArgumentNullException("points");
            this.Clusters = new List<VectorClusterCentroid>(clustersCount);

            this.Fuzzyness = fuzzy;
            U = new double[this.Points.Count, clustersCount];

            InitializeClustersCentroids(clustersCount);
            InitData();
        }

        private void InitializeClustersCentroids(int clustersCount)
        {
            List<VectorClusterPoint> pointsTmp = new List<VectorClusterPoint>(this.Points);

            for (int i = 0; i < clustersCount; i++)
            {
                int index = _rnd.Next(0, pointsTmp.Count);
                VectorClusterPoint point = pointsTmp.ElementAt(index);
                pointsTmp.RemoveAt(index);
                VectorClusterCentroid centroid = new VectorClusterCentroid(point.Vector.Clone());
                Clusters.Add(centroid);
            }
        }

        public void InitData()
        {
            // Iterate through all points to create initial U matrix
            for (int i = 0; i < this.Points.Count; i++)
            {
                VectorClusterPoint p = this.Points[i];
                double sum = 0.0;

                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    VectorClusterCentroid c = this.Clusters[j];
                    var diff = Distance.Euclidean(p.Vector, c.Vector);
                    U[i, j] = (diff == 0) ? Eps : diff;
                    sum += U[i, j];
                }

                double sum2 = 0.0;
                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    U[i, j] = 1.0 / Math.Pow(U[i, j] / sum, 2.0 / (Fuzzyness - 1.0));
                    sum2 += U[i, j];
                }

                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    U[i, j] /= sum2;
                }
            }

            this.RecalculateClusterIndexes();
        }

        ///
        /// Private constructor
        ///
        private VectorCMeansAlgorithm()
        {
        }

        ///
        /// Recalculates cluster indexes
        ///
        private void RecalculateClusterIndexes()
        {
            for (int i = 0; i < this.Points.Count; i++)
            {
                double max = -1.0;
                var p = this.Points[i];

                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    if (max < U[i, j])
                    {
                        max = U[i, j];
                        p.ClusterIndex = (max == 0.5) ? 0.5 : j;
                    }
                }
            }
        }

        ///
        /// Perform one step of the algorithm
        ///
        public void Step()
        {
            for (int c = 0; c < Clusters.Count; c++)
            {
                for (int h = 0; h < Points.Count; h++)
                {
                    double top = CalculateEulerDistance(Points[h], Clusters[c]);
                    if (top < 1.0) top = Eps;

                    // Bottom is the sum of distances from this data point to all clusters.
                    double sumTerms = 0.0;
                    for (int ck = 0; ck < Clusters.Count; ck++)
                    {
                        double thisDistance = CalculateEulerDistance(Points[h], Clusters[ck]);
                        if (thisDistance < 1.0) thisDistance = Eps;
                        sumTerms += Math.Pow(top / thisDistance, 2.0 / (this.Fuzzyness - 1.0));
                    }

                    // Then the MF can be calculated as...
                    U[h, c] = 1.0 / sumTerms;
                }
            }

            this.RecalculateClusterIndexes();
        }

        ///
        /// Calculates Euler's distance between point and centroid
        ///
        /// Point
        /// Centroid
        /// Calculated distance
        private double CalculateEulerDistance(VectorClusterPoint p, VectorClusterCentroid c)
        {
            return Distance.Euclidean(p.Vector, c.Vector);
        }

        ///
        /// Calculate the objective function
        ///
        /// The objective function as double value
        private double CalculateObjectiveFunction()
        {
            double Jk = 0;

            for (int i = 0; i < this.Points.Count; i++)
            {
                for (int j = 0; j < this.Clusters.Count; j++)
                {
                    Jk += Math.Pow(U[i, j], this.Fuzzyness) * Math.Pow(this.CalculateEulerDistance(Points[i], Clusters[j]), 2);
                }
            }
            return Jk;
        }

        ///
        /// Calculates the centroids of the clusters
        ///
        private void CalculateClusterCenters()
        {
            for (int j = 0; j < this.Clusters.Count; j++)
            {
                VectorClusterCentroid c = this.Clusters[j];
                double[] u = new double[c.Vector.Count];
                double l = 0.0;

                for (int i = 0; i < this.Points.Count; i++)
                {
                    double uu = Math.Pow(U[i, j], this.Fuzzyness);

                    for (int k = 0; k < u.Length; k++)
                    {
                        u[k] += uu * c.Vector[k];
                    }
                    l += uu;
                }

                for (int k = 0; k < u.Length; k++)
                {
                    if (l == 0)
                        u[k] = 0;
                    else
                        u[k] /= l;
                }

                c.Vector = Vector<double>.Build.DenseOfArray(u);

                this.Log += string.Format("Cluster Centroid: ({0})" + Environment.NewLine, c.Vector.ToString());
            }
        }

        ///
        /// Perform a complete run of the algorithm until the desired accuracy is achieved.
        /// For demonstration issues, the maximum Iteration counter is set to 20.
        ///
        /// Algorithm accuracy
        /// The number of steps the algorithm needed to complete
        public int Run(double accuracy)
        {
            int i = 0;
            int maxIterations = 40;
            do
            {
                i++;
                this.J = this.CalculateObjectiveFunction();
                this.CalculateClusterCenters();
                this.Step();
                double Jnew = this.CalculateObjectiveFunction();
                if (Math.Abs(this.J - Jnew) < accuracy) break;
            }
            while (maxIterations > i);
            return i;
        }
    }
}
