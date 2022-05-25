using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public sealed class VectorGustafsonKesselFCM
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
        public VectorGustafsonKesselFCM(List<VectorClusterPoint> points, List<VectorClusterCentroid> clusters, float fuzzy)
        {
            this.Points = points ?? throw new ArgumentNullException("points");
            this.Clusters = clusters ?? throw new ArgumentNullException("clusters");

            this.Fuzzyness = fuzzy;
            U = new double[this.Points.Count, this.Clusters.Count];

            InitData();
        }

        public VectorGustafsonKesselFCM(List<VectorClusterPoint> points, int clustersCount, float fuzzy)
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
        private VectorGustafsonKesselFCM()
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
                    double top = CalculateEulerDistance(Points[h], Clusters[c], c);
                    if (top < 1.0) top = Eps;

                    // Bottom is the sum of distances from this data point to all clusters.
                    double sumTerms = 0.0;
                    for (int ck = 0; ck < Clusters.Count; ck++)
                    {
                        double thisDistance = CalculateEulerDistance(Points[h], Clusters[ck], ck);
                        if (thisDistance < 1.0) thisDistance = Eps;
                        sumTerms += Math.Pow(top / thisDistance, 2.0 / (this.Fuzzyness - 1.0));
                    }

                    // Then the MF can be calculated as...
                    U[h, c] = 1.0 / sumTerms;
                }
            }

            this.RecalculateClusterIndexes();
        }

        /// <summary>
        /// Perform one step of the algorithm.
        /// </summary>
        public void StepGK()
        {
            for (int c = 0; c < Clusters.Count; c++)
            {
                for (int h = 0; h < Points.Count; h++)
                {
                    double top = CalculateEulerDistance(Points[h], Clusters[c], c);
                    if (top < 1.0)
                        top = Eps;

                    // Bottom is the sum of distances from this data point to all clusters.
                    double sumTerms = 0.0;
                    for (int ck = 0; ck < Clusters.Count; ck++)
                    {
                        double thisDistance = CalculateEulerDistance(Points[h], Clusters[ck], ck);
                        if (thisDistance < 1.0)
                            thisDistance = Eps;

                        sumTerms += Math.Pow(top / thisDistance, 1.0 / (this.Fuzzyness - 1.0));
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
        private double CalculateEulerDistance(VectorClusterPoint p, VectorClusterCentroid c, int clusterInd)
        {
            double sum = 0.0;
            List<VectorClusterPoint> covMatrix = CalculateCovMatrix(c, clusterInd);
            double[] diffVector = GetDiffVector(p, c);// i, j
            double[] newMatrix = new double[p.Vector.Count];

            for (int k = 0; k < newMatrix.Length; k++)
            {
                for (int l = 0; l < covMatrix.Count; l++)
                {
                    newMatrix[k] += diffVector[k] * covMatrix[l].Vector[k];
                }
                sum += newMatrix[k] * diffVector[k];
            }

            return sum;
        }

        /// <summary>
        /// Compute the covariance matrix of cluster k.
        /// </summary>
        /// <returns></returns>
        public List<VectorClusterPoint> CalculateCovMatrix(VectorClusterCentroid c, int clusterInd)
        {
            int dataSize = U.GetLength(0);
            int dataDimension = c.Vector.Count;

            double denominator;
            List<VectorClusterPoint> covMatrix = new List<VectorClusterPoint>();
            for (int i = 0; i < dataDimension; i++)
            {
                covMatrix.Add(new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[dataDimension])));
            }

            //Compute the powers of degree-of-membership
            double[,] t = new double[dataSize, Clusters.Count];

            for (int i = 0; i < dataSize; i++)
            {
                for (int j = 0; j < Clusters.Count; j++)
                {
                    t[i, j] = Math.Pow(U[i, j], Fuzzyness);
                }
            }

            //Calculate the fuzzy covariance matrix*
            denominator = 0;
            for (int i = 0; i < dataSize; i++)
            {
                List<VectorClusterPoint> productMatrix = GetProduct(Points[i], c);
                //multiply t[i][j] with product matrix
                for (int p = 0; p < dataDimension; p++)
                {
                    for (int q = 0; q < dataDimension; q++)
                    {
                        covMatrix[p].Vector[q] += t[i, clusterInd] * productMatrix[p].Vector[q];
                    }
                }
                denominator += t[i, clusterInd];
            }

            //covariance matrix*
            for (int p = 0; p < covMatrix.Count; p++)
            {
                for (int q = 0; q < dataDimension; q++)
                {
                    covMatrix[p].Vector[q] /= denominator;
                }
            }

            return covMatrix;
        }

        /// <summary>
        /// Get the difference vector of data i with cluster center j
        /// </summary>
        /// <returns></returns>
        public double[] GetDiffVector(VectorClusterPoint p, VectorClusterCentroid c)
        {
            int dataDimension = c.Vector.Count;
            double[] diffVector = new double[dataDimension];
            for (int k = 0; k < dataDimension; k++)
            {
                diffVector[k] = p.Vector[k] - c.Vector[k];
            }
            return diffVector;
        }

        /// <summary>
        /// Calculate the product of (Xi-Cj)*Transpose-of(Xi-Cj)
        /// </summary>
        /// <returns></returns>
        public List<VectorClusterPoint> GetProduct(VectorClusterPoint p, VectorClusterCentroid c)
        {
            int dataDimension = p.Vector.Count;
            List<VectorClusterPoint> productMatrix = new List<VectorClusterPoint>();
            for (int i = 0; i < dataDimension; i++)
            {
                productMatrix.Add(new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[dataDimension])));
            }

            double[] diffVector = GetDiffVector(p, c);
            for (int k = 0; k < productMatrix.Count; k++)
            {
                for (int l = 0; l < dataDimension; l++)
                {
                    productMatrix[k].Vector[l] = diffVector[k] * diffVector[l];
                }
            }
            return productMatrix;
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
                    Jk += Math.Pow(U[i, j], this.Fuzzyness) * Math.Pow(this.CalculateEulerDistance(Points[i], Clusters[j], j), 2);
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
                this.StepGK();
                double Jnew = this.CalculateObjectiveFunction();
                if (Math.Abs(this.J - Jnew) < accuracy) break;
            }
            while (maxIterations > i);
            return i;
        }
    }
}
