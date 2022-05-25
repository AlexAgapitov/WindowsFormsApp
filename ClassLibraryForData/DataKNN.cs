using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibraryForData
{
    public class Point2D
    {
        public double X;
        public double Y;
        public int Class;

        public Point2D(double x, double y, int Class)
        {
            X = x;
            Y = y;
            this.Class = Class;
        }
    }

    public class Point3D : Point2D
    {
        public double Z;

        public Point3D(double x, double y, double z, int Class) : base(x, y, Class)
        {
            Z = z;
        }
    }

    public class DataKNN
    {
        public static List<string> seachKNN(List<string> list, int k, int countparam)
        {
            var result = new List<string>();
            int count = (list.Count / (countparam + 1));

            if (countparam == 2)
            {
                int step = 0;
                var temp = new List<Point2D>();
                var searchlist = new List<Point2D>();
                var normlist = new List<Point2D>();
                var list2d = new List<Point2D>();
                for (int i = 0; i < count; i++)
                {
                    list2d.Add(new Point2D(double.Parse(list[step]), double.Parse(list[step + 1]), int.Parse(list[step + 2])));
                    step += countparam + 1;
                }
                temp.AddRange(list2d);
                list2d.Clear();
                foreach (Point2D p in temp)
                {
                    if (p.Class == 0)
                    {
                        searchlist.Add(p);
                    }
                    else
                    {
                        normlist.Add(p);
                    }
                }

                var evkl = new Dictionary<int, double>();
                var kmeans = new List<int>();

                for (int i = 0; i < searchlist.Count; i++)
                {
                    for (int j = 0; j < normlist.Count; j++)
                    {
                        evkl.Add(j, EvklidCalculate(new double[] { normlist[j].X, normlist[j].Y }, new double[] { searchlist[i].X, searchlist[i].Y }));
                    }
                    evkl = evkl.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                    foreach (var item in evkl.Take(k))
                    {
                        kmeans.Add(normlist[item.Key].Class);
                    }
                    kmeans.Sort();

                    int tempval = 0;
                    double tempclass = 0.0;
                    foreach (int val in kmeans.Distinct())
                    {
                        if (kmeans.Count(x => x == val) > tempclass)
                        {
                            tempclass = kmeans.Count(x => x == val);
                            tempval = val;
                        }
                    }
                    //вставить этот класс tempval в список temp
                    searchlist[i].Class = tempval;
                    var add2d = new Point2D(searchlist[i].X, searchlist[i].Y, tempval);
                    normlist.Add(add2d);
                    evkl.Clear();
                    kmeans.Clear();
                }
                foreach (Point2D p in temp)
                {
                    foreach (Point2D point in searchlist)
                    {
                        if (p.X == point.X && p.Y == point.Y)
                            p.Class = point.Class;
                    }
                    result.Add(p.Class.ToString());
                }
            }
            else
            {
                int step = 0;
                var temp = new List<Point3D>();
                var searchlist = new List<Point3D>();
                var normlist = new List<Point3D>();
                var list3d = new List<Point3D>();
                for (int i = 0; i < count; i++)
                {
                    list3d.Add(new Point3D(double.Parse(list[step]), double.Parse(list[step + 1]), double.Parse(list[step + 2]), int.Parse(list[step + 3])));
                    step += countparam + 1;
                }
                temp.AddRange(list3d);
                list3d.Clear();
                foreach (Point3D p in temp)
                {
                    if (p.Class == 0)
                    {
                        searchlist.Add(p);
                    }
                    else
                    {
                        normlist.Add(p);
                    }
                }

                var evkl = new Dictionary<int, double>();
                var kmeans = new List<int>();

                for (int i = 0; i < searchlist.Count; i++)
                {
                    for (int j = 0; j < normlist.Count; j++)
                    {
                        evkl.Add(j, EvklidCalculate(new double[] { normlist[j].X, normlist[j].Y, normlist[j].Z}, new double[] { searchlist[i].X, searchlist[i].Y, searchlist[i].Z }));
                    }
                    evkl = evkl.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                    foreach (var item in evkl.Take(k))
                    {
                        kmeans.Add(normlist[item.Key].Class);
                    }
                    kmeans.Sort();

                    int tempval = 0;
                    double tempclass = 0.0;
                    foreach (int val in kmeans.Distinct())
                    {
                        if (kmeans.Count(x => x == val) > tempclass)
                        {
                            tempclass = kmeans.Count(x => x == val);
                            tempval = val;
                        }
                    }
                    //вставить этот класс tempval в список temp
                    searchlist[i].Class = tempval;
                    evkl.Clear();
                    kmeans.Clear();
                }
                foreach (Point3D p in temp)
                {
                    foreach (Point3D point in searchlist)
                    {
                        if (p.X == point.X && p.Y == point.Y && p.Z == point.Z)
                            p.Class = point.Class;
                    }
                    result.Add(p.Class.ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// Евклидово выражение
        /// </summary>
        /// <param name="v1">x,y или x,y,z</param>
        /// <param name="v2">x,y или x,y,z</param>
        /// <returns>расстрояние</returns>
        private static double EvklidCalculate(double[] v1, double[] v2) ///{x1,y1} {x2,y2} 
        {
            double result = 0.0;
            for (int i = 0; i < v1.Length; i++)
            {
                result += (v1[i] - v2[i]) * (v1[i] - v2[i]);
            }
            return Math.Sqrt(result);
        }
    }
}
