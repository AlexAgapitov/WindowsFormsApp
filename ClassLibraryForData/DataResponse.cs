using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForData
{
    public class DataResponse
    {
        /// <summary>
        /// Метод, который находит среднее значение среди всех чисел
        /// </summary>
        /// <param name="listformean">список для восстановления</param>
        /// <returns>среднее число</returns>
        private static double MeanDouble(List<string> listformean)
        {
            double mean = 0.0;
            int countnotnull = 0;

            for (int i = 0; i < listformean.Count; i++)
            {
                if (listformean[i] != null)
                {
                    mean += Convert.ToDouble(listformean[i]);
                    countnotnull++;
                }
            }
            mean = mean / countnotnull;
            return mean;
        }

        /// <summary>
        /// Метод, который находит больше всего повторяющийся в списке
        /// </summary>
        /// <param name="listformean">список для восстановления</param>
        /// <returns>популярное слово</returns>
        private static string MeanString(List<string> listformean)
        {
            List<string> listNoRepeat = new HashSet<string>(listformean).ToList();
            List<int> listCountRepeat = new List<int>();
            string PopularString = string.Empty;

            for (int i = 0; i < listNoRepeat.Count; i++)
            {
                listCountRepeat.Add(0);
            }

            for (int i = 0; i < listformean.Count; i++)
            {
                for (int j = 0; j < listNoRepeat.Count; j++)
                {
                    if (listformean[i] == listNoRepeat[j])
                    {
                        listCountRepeat[j] += 1;
                    }
                }
            }
            int maxcount = listCountRepeat.Max();
            int forresult = 0;
            for (int i = 0; i < listCountRepeat.Count; i++)
            {
                if (maxcount == listCountRepeat[i])
                    forresult = i;
            }
            PopularString = listNoRepeat[forresult].ToString();
            return PopularString;
        }

        /// <summary>
        /// Главный метод 
        /// </summary>
        /// <param name="listforrecovery">лист с восстановленными данными</param>
        /// <returns></returns>
        public static List<string> Recovery(List<string> listforrecovery)
        {
            List<string> resultlist = new List<string>();
            resultlist.AddRange(listforrecovery);
            string checkDouble = string.Empty;

            foreach (string res in resultlist)
            {
                if (res != null)
                {
                    checkDouble = res;
                }
            }

            if (double.TryParse(checkDouble, out var check))
            {
                double meandouble = MeanDouble(listforrecovery);
                for (int i = 0; i < resultlist.Count; i++)
                {
                    if (resultlist[i] == null)
                    {
                        resultlist[i] = meandouble.ToString();
                    }
                }
            }
            else
            {
                string meanstring = MeanString(listforrecovery);
                for (int i = 0; i < resultlist.Count; i++)
                {
                    if (resultlist[i] == null)
                    {
                        resultlist[i] = meanstring;
                    }
                }
            }

            return resultlist;
        }
    }   
}
