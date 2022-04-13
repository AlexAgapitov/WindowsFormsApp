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
        private static List<string> MeanDouble(List<string> listformean)
        {
            double mean = 0.0;
            int countnotnull = 0;
            List<string> listresult = new List<string>();
            listresult.AddRange(listformean);

            for (int i = 0; i < listformean.Count; i++)
            {
                if (listformean[i] != null)
                {
                    mean += Convert.ToDouble(listformean[i]);
                    countnotnull++;
                }
            }
            mean = mean / countnotnull;

            for (int i = 0; i < listresult.Count; i++)
            {
                if (listresult[i] == null)
                {
                    listresult[i] = mean.ToString();
                }
            }

            return listresult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listformean"></param>
        /// <returns></returns>
        private static List<string> LinearDouble(List<string> listformean)
        {
            List<string> listresult = new List<string>();
            listresult.AddRange(listformean);
            double difference = 0.0;

            int count = 0;
            double temp = 0.0;
            bool istemp = false;
            /*while (count < listformean.Count)
            {
                if (istemp == false)
                {
                    if (listformean[count] != null)
                    {
                        temp = Convert.ToDouble(listformean[count]);
                        istemp = true;
                    }
                }
                else
                {
                    if (listformean[count] != null)
                    {
                        difference = Convert.ToDouble(listformean[count]) - temp;
                        break;
                    }
                }
                count++;
            }*/

            for (int i = 0; i < listformean.Count; i++)
            {
                if (istemp == false)
                {
                    if (listformean[i] != null)
                    {
                        temp = Convert.ToDouble(listformean[i]);
                        istemp = true;
                    }
                }
                else
                {
                    if (listformean[i] != null)
                    {
                        difference = Convert.ToDouble(listformean[i]) - temp;
                        break;
                    }
                    else
                    {
                        istemp = false;
                    }
                }
                count++;
            }

            temp = 0.0;
            istemp = false;
            int countnull = 1;

            if (listresult[0] == null)
            {
                while (countnull < listformean.Count)
                {
                    countnull++;
                    if (listformean[countnull] != null)
                    {
                        temp = Convert.ToDouble(listformean[countnull]);
                        break;
                    }
                }
                while (countnull - 1 >= 0)
                {
                    listresult[countnull - 1] = (temp - difference).ToString();
                    temp = Convert.ToDouble(listresult[countnull - 1]);
                    countnull--;
                }
            }

            for (int i = 0; i < listresult.Count; i++)
            {
                
                if (istemp == false)
                {
                    if (listresult[i] != null)
                    {
                        temp = Convert.ToDouble(listformean[i]);
                        istemp = true;
                    }
                    else
                    {
                        istemp = false;
                    }
                } 
                else
                {
                    if (listresult[i] == null)
                    {
                        listresult[i] = (temp + difference).ToString();
                        temp = Convert.ToDouble(listresult[i]);
                        istemp = true;
                    }
                    else if (listresult[i] != null)
                    {
                        temp = Convert.ToDouble(listformean[i]);
                        istemp = true;
                    }
                }
            }

            return listresult;
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
                //resultlist = MeanDouble(listforrecovery);
                resultlist = LinearDouble(listforrecovery);
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
