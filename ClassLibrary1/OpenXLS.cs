using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpenAndSave
{
    public class OpenXLS
    {
        string filenameopen = string.Empty;
        bool maxcountline = false;
        Excel.Application excel = new Excel.Application();
        Excel.Workbook wb;
        Excel.Worksheet ws;

        public OpenXLS(string filenameopen, bool maxcountline)
        {
            this.filenameopen = filenameopen;
            this.maxcountline = maxcountline;
            wb = excel.Workbooks.Open(filenameopen);
            ws = wb.Worksheets[1];  
        }

        /// <summary>
        /// Функция заполнения datatable значениями из excel
        /// </summary>
        /// <returns></returns>
        public DataTable MakeDataTable()
        {
            DataTable dataTable = new DataTable();

            string res = string.Empty;
            int countrows = 0;
            while (ws.Cells[1, countrows + 1].Value != null)
            {
                dataTable.Columns.Add(ws.Cells[1, countrows + 1].Value2, typeof(string));
                countrows++;
            }

            int countlines = 2;
            while (ws.Cells[countlines, 1].Value != null)
                countlines++;

            if (maxcountline == true && countlines > 100)
                countlines = 102;

            for (int i = 0; i < countlines-2; i++)
            {
                DataRow drow = dataTable.NewRow();
                for (int j = 0; j < countrows; j++)
                {
                    drow[j] = ws.Cells[i + 2, j + 1].Value;
                }
                dataTable.Rows.Add(drow);
            }
            wb.Close(false);
            excel.Quit();
            excel = null;
            wb = null;
            ws = null;
            System.GC.Collect();

            return dataTable;
        }
    }
}
