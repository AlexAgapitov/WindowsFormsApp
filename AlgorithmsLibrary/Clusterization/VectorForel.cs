using System;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AlgorithmsLibrary.Clusterization
{
    public class VectorForel
    {
        /// <summary>
        /// Разбивает список точек на несколько кластеров.
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <param name="radius">Радиус поиска локальных сгущений</param>
        /// <param name="epsilon">Точность</param>
        /// <returns>Список кластеров (список списков точек)</returns>
        public static List<VectorCluster> Recluster(List<VectorClusterPoint> points, double radius, double epsilon)
        {
            Random rand = new Random();

            List<VectorCluster> result = new List<VectorCluster>();
            List<VectorClusterPoint> unclustered = new List<VectorClusterPoint>(points);

            while (unclustered.Count > 0)
            {
                VectorClusterCentroid newCenter = new VectorClusterCentroid(unclustered[rand.Next(unclustered.Count)].Vector);
                VectorClusterCentroid center;

                do
                {
                    // Получаем новый центр тяжести
                    center = newCenter;
                    double[] value = new double[center.Vector.Count];
                    int cnt = 0;
                    foreach (VectorClusterPoint point in unclustered)
                        if (Dist(center, point) < radius)
                        {
                            for (int i = 0; i < value.Length; i++)
                            {
                                value[i] += point.Vector[i];
                            }
                            cnt++;
                        }
                    for (int i = 0; i < value.Length; i++)
                    {
                        value[i] /= cnt;
                    }
                    newCenter = new VectorClusterCentroid(Vector<double>.Build.DenseOfArray(value));
                }
                while (!PointsAreEqual(center, newCenter, epsilon));

                //Переносим точки в новый кластер
                VectorCluster cluster = new VectorCluster(newCenter);
                for (int i = unclustered.Count - 1; i >= 0; i--)
                    if (Dist(center, unclustered[i]) < radius)
                    {
                        cluster.Points.Add(unclustered[i]);
                        unclustered.RemoveAt(i);
                    }
                result.Add(cluster);
            }
            return result;
        }

        /// <summary>
        /// Расстояние между точками.
        /// </summary>
        static double Dist(VectorClusterPoint point1, VectorClusterPoint point2)
        {
            return Distance.Euclidean(point1.Vector, point2.Vector);
        }

        /// <summary>
        /// Проверяет две точки на равнество с точностью epsilon.
        /// </summary>
        static bool PointsAreEqual(VectorClusterPoint point1, VectorClusterPoint point2, double epsilon)
        {
            bool flag = true;
            for (int i = 0; i < point1.Vector.Count; i++)
            {
                flag &= Math.Abs(point1.Vector[i] - point2.Vector[i]) < epsilon;
            }
            return flag;
        }
    }
}
