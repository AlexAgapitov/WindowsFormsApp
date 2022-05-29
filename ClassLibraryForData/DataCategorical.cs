using System;
using System.Collections.Generic;

namespace ClassLibraryForData
{
    public class DataCategorical
    {
        /// <summary>
        /// Категориальный метод с диапазонами
        /// </summary>
        /// <param name="listforcategory">Список со столбцом для обработки</param>
        /// <param name="labels">Список с названиями категорий</param>
        /// <param name="bins">Список с диапазонами категорий</param>
        /// <returns>Категориальный список</returns>
        public static List<string> CategoryCut(List<string> listforcategory, List<string> labels, List<string> bins)
        {
            List<string> listresult = new List<string>();
            for (int i = 0; i < listforcategory.Count; i++)
                listresult.Add("Вне диапазона");
            for (int i = 0; i < listforcategory.Count; i++)
            {
                for (int j = 0; j < bins.Count; j++)
                {
                    if (Convert.ToDouble(listforcategory[i]) < Convert.ToDouble(bins[j]))
                    {
                        listresult[i] =  labels[j];
                        break;
                    }
                }
            }
            return listresult;
        }

        /// <summary>
        /// Категориальный метод с поиском названия категории в строке
        /// </summary>
        /// <param name="listforcategory">Список со столбцом для обработки</param>
        /// <param name="labels">Список с названиями категорий</param>
        /// <returns>Категориальный список</returns>
        public static List<string> CategoryCutV2(List<string> listforcategory, List<string> labels)
        {
            List<string> listresult = new List<string>();
            int count = listforcategory.Count;
            for (int i = 0; i < count; i++)
                listresult.Add("Нет категории");

            for (int i = 0; i < listforcategory.Count; i++)
            {
                for (int j = 0; j < labels.Count; j++)
                {
                    if (listforcategory[i].Contains(labels[j]))
                    {
                        listresult[i] = labels[j];
                        break;
                    }
                }
            }

            return listresult;
        }
    }
}
