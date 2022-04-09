using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassOpenAndSave
{
    public class DataNormalize
    {
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

        private static List<string> NormalizeDouble(List<string> listfornormalize)
        {
            List<string> listforsearh = new List<string>();
            List<string> listforresult = new List<string>();
            listforsearh.AddRange(listfornormalize);
            listforsearh.Sort();
            double xmin = double.Parse(listforsearh[0]);
            double xmax = double.Parse(listforsearh[listforsearh.Count - 1]);

            for (int i = 0; i < listfornormalize.Count; i++)
            {
                double xnorm = (double.Parse(listfornormalize[i]) - xmin) / (xmax - xmin);
                listforresult.Add(xnorm.ToString("F3"));
            }

            return listforresult;
        }
    }
}
