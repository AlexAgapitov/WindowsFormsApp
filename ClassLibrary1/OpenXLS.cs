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
        int iLastRow, iLastCol;
        string filenameopen = string.Empty;
        bool maxcountline = false;
        int iLastRow, iLastCol;
        Excel.Application excel = new Excel.Application();
        Excel.Workbook wb;
        Excel.Worksheet ws;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenameopen">путь к файлу</param>
        /// <param name="maxcountline">булевая перменная, все строки или 100</param>
        public OpenXLS(string filenameopen, bool maxcountline)
        {
            this.filenameopen = filenameopen;
            this.maxcountline = maxcountline;
            wb = excel.Workbooks.Open(filenameopen);
            ws = wb.Worksheets[1];
            iLastRow = ws.Cells[ws.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;
            iLastCol = ws.Cells[1, ws.Columns.Count].End[Excel.XlDirection.xlToLeft].Column;
        }

        /// <summary>
        /// Функция заполнения datatable значениями из excel
        /// </summary>
        /// <returns>Заполненая datatable</returns>
        public DataTable MakeDataTable()
        {
            DataTable dataTable = new DataTable();

            for (int i = 0; i < iLastCol; i++)
            {
                dataTable.Columns.Add(ws.Cells[1, i + 1].Value2, typeof(string));
            }

            int countlines = iLastRow;
            /*while (ws.Cells[countlines, 1].Value != null)
                countlines++;*/

            for (int i = 0; i < countlines - 2; i++)
            {
                DataRow drow = dataTable.NewRow();
                for (int j = 0; j < iLastCol; j++)
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
