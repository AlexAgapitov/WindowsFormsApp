using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForData
{
    public class DataCategorical
    {
        public static List<string> CategoryCut(List<string> listforcategory, List<string> labels, List<string> bins)
        {
            List<string> listresult = new List<string>();

            for (int i = 0; i < listforcategory.Count; i++)
            {
                for (int j = 0; j < bins.Count; j++)
                {
                    if (Convert.ToDouble(listforcategory[i]) < Convert.ToDouble(bins[0]))
                    {
                        listresult.Add("Вне диапазона");
                        break;
                    }
                    else if (Convert.ToDouble(listforcategory[i]) < Convert.ToDouble(bins[j]))
                    {
                        listresult.Add(labels[j - 1]);
                        break;
                    }
                }
            }

            return listresult;
        }
    }
}
