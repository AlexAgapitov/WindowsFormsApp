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
        /// Фукнция, которая создает строку для CSV
        /// </summary>
        /// <param name="table">Таблица с данными, которые нужно сохранить</param>
        /// <param name="separator">Символ разделителя</param>
        /// <returns>Строка с данными в формате csv</returns>
        public static string MakeOneCSV(DataTable table, char separator)
        {
            string csv = string.Empty;

            for(int i = 0; i < table.Columns.Count; i++)
            { 
                csv += table.Columns[i].ToString() + separator;
            }

            csv += "\r\n";

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    csv += row[column].ToString() + separator; 
                }
                csv += "\r\n";
            }

            return csv;
        }
    }
}
