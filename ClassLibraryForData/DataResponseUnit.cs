using System;
using System.Collections.Generic;

namespace ClassLibraryForData
{
    public class DataResponseUnit
    {
        public static List<string> ConvertUnit(List<string> listforresponse)
        {
            List<string> listresult = new List<string>();

            foreach (string s in listforresponse)
            {
                var temp = s;
                double value;
                if (temp.Contains("см"))// todo: contains НЕ игнорирует регистр
                {
                    value = Convert.ToDouble(temp.Replace("см", "")) / 100;// todo: а если там ещё пробелы ?
                    temp = value + "м";
                }
                else if (temp.Contains("км"))
                {
                    value = Convert.ToDouble(temp.Replace("км", "")) * 100;
                    temp = value + "м";
                }
                else if (temp.Contains("мм"))
                {
                    value = Convert.ToDouble(temp.Replace("мм", "")) / 1000;
                    temp = value + "м";
                }
                else if (temp.Contains("м"))
                {
                    /*value = Convert.ToDouble(temp.Replace("мм", "")) / 1000;
                    temp = value.ToString() + "м";*/
                }
                else if (temp.Contains("г"))
                {
                    value = Convert.ToDouble(temp.Replace("г", "")) * 1000;
                    temp = value + "кг";
                }
                else if (temp.Contains("т"))
                {
                    value = Convert.ToDouble(temp.Replace("т", "")) / 1000;
                    temp = value + "кг";
                }
                else if (temp.Contains("кг"))
                {
                    /*value = Convert.ToDouble(temp.Replace("мм", "")) / 1000;
                    temp = value.ToString() + "м";*/
                }
                listresult.Add(temp);
            }

            return listresult;
        }
    }
}
