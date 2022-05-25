namespace AlgorithmsLibrary.Clusterization.Tests
{
    using System;
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra;
    using Xunit;

    public class VectorFuzzyTests
    {
        [Fact]
        public void ClusterTest()
        {
            var points = new List<VectorClusterPoint>
            {
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 73.53189604333602, 34.896145021310076 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 73.28498173551634, 33.96860806993209 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 73.45828098873608, 33.92584423092194 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 73.9657889183145, 35.73191006924026 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 74.0074097183533, 36.81735596177168 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 73.41247541410848, 34.27314856695011 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 73.9156256353017, 36.83206791547127 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 74.81499205809087, 37.15682749846019 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 74.03144880081527, 37.57399178552441 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 74.51870941207744, 38.674258946906775 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 74.50754595105536, 35.58903978415765 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 74.51322752749547, 36.030572259100154 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 59.27900996617973, 46.41091720294207 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 59.73744793841615, 46.20015558367595 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 58.81134076672606, 45.71150126331486 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 58.52225539437495, 47.416083617601544 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 58.218626647023484, 47.36228902172297 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 60.27139669447206, 46.606106348801404 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 60.894962462363765, 46.976924697402865 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 62.29048673878424, 47.66970563563518 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 61.03857608977705, 46.212924720020965 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 60.16916214139201, 45.18193661351688 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 59.90036905976012, 47.555364347063005 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 62.33003634144552, 47.83941489877179 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 57.86035536718555, 47.31117930193432 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 58.13715479685925, 48.985960494028404 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 56.131923963548616, 46.8508904252667 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 55.976329887053, 47.46384037658572 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 56.23245975235477, 47.940035191131756 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 58.51687048212625, 46.622885352699086 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 57.85411081905477, 45.95394361577928 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 56.445776311447844, 45.162093662656844 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 57.36691949656233, 47.50097194337286 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 58.243626387557015, 46.114052729681134 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 56.27224595635198, 44.799080066150054 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 57.606924816500396, 46.94291057763621 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.18714230041951, 13.877149710431695 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.449448810657486, 13.490778346545994 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.295018390286714, 13.264889000216499 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.160201832884923, 11.89278262341395 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 31.341509791789576, 15.282655921997502 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 31.68601630325429, 14.756873246748 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 29.325963742565364, 12.097849250072613 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 29.54820742388256, 13.613295356975868 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 28.79359608888626, 10.36352064087987 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 31.01284597092308, 12.788479208014905 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 27.58509216737002, 11.47570110601373 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 28.593799561727792, 10.780998203903437 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 31.356105766724795, 15.080316198524088 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 31.25948503636755, 13.674329151166603 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 32.31590076372959, 14.95261758659035 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.460413702763617, 15.88402809202671 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 32.56178203062154, 14.586076852632686 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 32.76138648530468, 16.239837325178087 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.1829453331884, 14.709592407103628 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 29.55088173528202, 15.0651247180067 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 29.004155302187428, 14.089665298582986 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 29.339624439831823, 13.29096065578051 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.997460327576846, 14.551914158277214 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new[] { 30.66784126125276, 16.269703107886016 })),
            };

            VectorCMeansAlgorithm alg = new VectorCMeansAlgorithm(points, 3, 2);
            int iterations = alg.Run(Math.Pow(10, -5));

            Assert.Equal(3, alg.Clusters.Count);
        }

        [Fact]
        public void KMeansPlusPlusButerflyTest()
        {
            var points = new List<VectorClusterPoint>()
            {
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 0, 4 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 0, 2 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 0, 0 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 1, 3 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 1, 2 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 1, 1 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 2, 2 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 3, 2 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 4, 2 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 5, 3 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 5, 2 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 5, 1 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 6, 4 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 6, 2 })),
                 new VectorClusterPoint(Vector<double>.Build.DenseOfArray(new double[] { 6, 0 })),
            };

            List<VectorClusterCentroid> centroids = new List<VectorClusterCentroid>();

            centroids.Add(new VectorClusterCentroid(Vector<double>.Build.DenseOfArray(new double[] { 0, 2 })));
            centroids.Add(new VectorClusterCentroid(Vector<double>.Build.DenseOfArray(new double[] { 6, 2 })));

            VectorCMeansAlgorithm alg = new VectorCMeansAlgorithm(points, 2, 2);
            int iterations = alg.Run(Math.Pow(10, -5));

            Assert.Equal(2, alg.Clusters.Count);
        }

        [Fact]
        public void GaussianNoiseTest()
        {
            Random rand = new Random();
            List<VectorClusterPoint> points = new List<VectorClusterPoint>(400);
            int centroidsCnt = 4;
            Point centroid1 = new Point(4, 4);
            Point centroid2 = new Point(-4, -4);
            Point centroid3 = new Point(-4, 4);
            Point centroid4 = new Point(4, -4);

            double mean = 0;
            double stdDev = 1.4;
            for (int i = 0; i < 100; i++)
            {
                points.Add(new VectorClusterPoint(Vector<double>.Build.DenseOfArray(
                    new[] { centroid1.X + GaussianNoise(rand, mean, stdDev), centroid1.Y + GaussianNoise(rand, mean, stdDev) })));
                points.Add(new VectorClusterPoint(Vector<double>.Build.DenseOfArray(
                    new[] { centroid2.X + GaussianNoise(rand, mean, stdDev), centroid2.Y + GaussianNoise(rand, mean, stdDev) })));
                points.Add(new VectorClusterPoint(Vector<double>.Build.DenseOfArray(
                    new[] { centroid3.X + GaussianNoise(rand, mean, stdDev), centroid3.Y + GaussianNoise(rand, mean, stdDev) })));
                points.Add(new VectorClusterPoint(Vector<double>.Build.DenseOfArray(
                    new[] { centroid4.X + GaussianNoise(rand, mean, stdDev), centroid4.Y + GaussianNoise(rand, mean, stdDev) })));
            }

            VectorCMeansAlgorithm alg = new VectorCMeansAlgorithm(points, centroidsCnt, 2);
            int iterations = alg.Run(Math.Pow(10, -5));

            Assert.Equal(4, alg.Clusters.Count);
        }

        private static double GaussianNoise(Random rand, double mean, double stdDev)
        {
            double u = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u)) * Math.Sin(2.0 * Math.PI * u);
            double randNormal = mean + (stdDev * randStdNormal); // random normal(mean, stdDev^2)
            return randNormal;
        }
    }
}
