using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForData
{
    public class DataNormalize
    {
        /// <summary>
        /// Главный метод
        /// </summary>
        /// <param name="listfornormalize">лист для нормализации данных</param>
        /// <returns>лист с нормализированными данными</returns>
        public static List<string> Normalize(List<string> listfornormalize)
        {
            List<string> listresult = new List<string>();

            string checkDouble = string.Empty;

            foreach (string res in listfornormalize)
            {
                if (res != null)
                {
                    checkDouble = res;
                }
            }

            if (double.TryParse(checkDouble, out var check))
            {
                listresult = NormalizeDouble(listfornormalize);
            }
            else
            {
                return null;
            }

            return listresult;
        }

        /// <summary>
        /// Метод, который нормализирует данные к значениям от 0 до 1
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
                sum += int.Parse(listforsearh[i]) * int.Parse(listforsearh[i]);
            }

            sum = Math.Sqrt(sum);

            for (int i = 0; i < listfornormalize.Count; i++)
            {
                double xnorm = double.Parse(listfornormalize[i]) / sum;
                listforresult.Add(xnorm.ToString("F8"));
            }

            return listforresult;
        }

        
    }
}
