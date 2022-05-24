using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpenAndSave
{
    public class OpenXLS
    {
        int iLastRow, iLastCol;
        string filenameopen = string.Empty;
        Excel.Application excel = new Excel.Application();
        Excel.Workbook wb;
        Excel.Worksheet ws;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="filenameopen">путь к файлу</param>
        public OpenXLS(string filenameopen)
        {
            this.filenameopen = filenameopen;
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

            for (int i = 0; i < countlines - 2; i++)
            {
                DataRow drow = dataTable.NewRow();
                for (int j = 0; j < iLastCol; j++)
                {
                    var temp = (ws.Cells[i + 2, j + 1].Value).ToString();
                    if (temp == "" || temp == " " || temp == "-" || temp == "?")
                    {
                        temp = "<null>";
                    }
                    drow[j] = temp;
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
