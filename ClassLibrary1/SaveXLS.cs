using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpenAndSave
{
    public class SaveXLS
    {
        DataTable dataTable = new DataTable();
        string filenamesave = string.Empty;
        string filenamepath = string.Empty;
        Excel.Application excel = new Excel.Application();
        Excel.Workbook wb;
        Excel.Worksheet ws;

        public SaveXLS(string filenamepath, string filenamesave, DataTable dataTable, bool saveAs)
        {
            this.filenamesave = filenamesave;
            this.filenamepath = filenamepath;
            this.dataTable = dataTable;
            excel = new Excel.Application();
            if (saveAs == true)
            {
                excel.SheetsInNewWorkbook = 3;
                wb = excel.Workbooks.Add(1);
                ws = wb.Worksheets[1];
            }
            else
            {
                wb = excel.Workbooks.Open(filenamepath + filenamesave);
                ws = wb.Worksheets[1];
            }
        }

        /// <summary>
        /// Фукнция сохранения файла xls
        /// </summary>
        public void ExportToExcel()
        {
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                ws.Cells[1, 1 + i].Value = dataTable.Columns[i].ToString();
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow drow = dataTable.Rows[i];
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    ws.Cells[2 + i, 1 + j].Value = drow[j].ToString();
                }
            }

            wb.SaveAs(filenamepath + filenamesave, Excel.XlFileFormat.xlAddIn8);

            wb.Close(false);
            excel.Quit();
            excel = null;
            wb = null;
            ws = null;
            System.GC.Collect();
        }
    }
}
