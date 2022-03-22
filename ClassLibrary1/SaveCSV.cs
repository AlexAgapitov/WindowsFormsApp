using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ClassOpenAndSave
{
    public class SaveCSV
    {
        /// <summary>
        /// Создание строки
        /// </summary>
        /// <param name="table">таблица</param>
        /// <returns></returns>
        public static string MakeOneCSV(DataTable table)
        {
            string csv = string.Empty;

            for(int i = 0; i < table.Columns.Count; i++)
            { 
                csv += table.Columns[i].ToString() + ',';
            }

            csv += "\r\n";

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    csv += row[column].ToString() + ','; 
                }
                csv += "\r\n";
            }

            return csv;
        }
    }
}
