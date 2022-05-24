using System;
using System.Collections.Generic;

namespace ClassLibraryForData
{
    public class DataNormalize
    {
        /// <summary>
        /// Главный метод
        /// </summary>
        /// <param name="listfornormalize">список для преобразования</param>
        /// <param name="namemethod">название метода</param>
        /// <returns>преобразованный список</returns>
        public static List<string> Normalize(List<string> listfornormalize, string namemethod)
        {
            List<string> listresult = new List<string>();

            string checkDouble = string.Empty;

            foreach (string res in listfornormalize)
            {
                if (res != null)
                {
                    checkDouble = res;
                }
                else
                {
                    return null;
                }
            }

            if (double.TryParse(checkDouble, out var check))
            {
                if (namemethod == "sqrt")
                {
                    listresult = NormalizeDouble(listfornormalize);
                }
                else
                {
                    listresult = NormalizeMinMax(listfornormalize);
                }
            }
            else
            {
                return null;
            }

            return listresult;
        }

        /// <summary>
        /// Метод, который нормализирует данные к значениям от 0 до 1 (формула X = x/(Корень суммы квадратов))
        /// </summary>
        /// <param name="listfornormalize">лист для нормализации данных</param>
        /// <returns>лист с нормализированными данными</returns>
        private static List<string> NormalizeDouble(List<string> listfornormalize)
        {
            List<string> listforsearh = new List<string>();
            List<string> listforresult = new List<string>();
            listforsearh.AddRange(listfornormalize);
            listforsearh.Sort();
            double sum = 0.0;

            for(int i = 0; i < listforsearh.Count; i++)
            {
                sum += double.Parse(listforsearh[i]) * double.Parse(listforsearh[i]);
            }

            sum = Math.Sqrt(sum);

            for (int i = 0; i < listfornormalize.Count; i++)
            {
                double xnorm = double.Parse(listfornormalize[i]) / sum;
                listforresult.Add(xnorm.ToString("F8"));
            }

            return listforresult;
        }

        /// <summary>
        /// Метод, который нормализирует данные к значениям от 0 до 1 (формула X = (x-xmin)/(xmax-xmin))
        /// </summary>
        /// <param name="listfornormalize"></param>
        /// <returns></returns>
        private static List<string> NormalizeMinMax(List<string> listfornormalize)
        {
            var listforresult = new List<string>();
            var listsort = new List<string>();
            listsort.AddRange(listfornormalize);
            listsort.Sort();
            double min = double.Parse(listsort[0]);
            double max = double.Parse(listsort[listsort.Count - 1]);
            double X_std = 0.0;
            for (int i = 0; i < listfornormalize.Count; i++)
            {
                X_std = (double.Parse(listfornormalize[i]) - min) / (max - min);
                listforresult.Add(X_std.ToString());
            }

            return listforresult;
        }
        
    }
}
