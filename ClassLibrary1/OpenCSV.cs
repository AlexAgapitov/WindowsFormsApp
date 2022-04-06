using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;


namespace ClassOpenAndSave
{
    public class OpenCSV
    {
        public static char GlobalChar;

        /// <summary>
        /// Фукнция создания листа с названиями столбцов
        /// </summary>
        /// <param name="_firstLine">Первая строка CSV файла (название столбцов)</param>
        /// <returns>Список с названием колонок</returns>
        public static List<string> MakeNameColumns(string _firstLine)
        {
            List<string> _listNameColomns = new List<string>();
            _listNameColomns = _firstLine.Split(GlobalChar).ToList();

            return _listNameColomns;
        }

        /// <summary>
        /// Функция создания листа с значениями ячеек
        /// </summary>
        /// <param name="_lines">Лист со строками</param>
        /// <returns>Список с значениями ячеек</returns>
        public static List<List<string>> MakeCells(List<string> _lines)
        {
            List<List<string>> _cellsValue = new List<List<string>>();
            for (int i = 1; i < _lines.Count; i++)
            {
                _cellsValue.Add(_lines[i].Split(GlobalChar).ToList());
            }

            return _cellsValue;
        }

        /// <summary>
        /// Функция, создающая список строк
        /// </summary>
        /// <param name="filenameopen">Путь к файлу</param>
        /// <returns>Список со строками</returns>
        public static List<string> MakeList(string filenameopen, bool maxcountline)
        {
            List<string> _allDB = new List<string>();

            using (StreamReader sr = new StreamReader(filenameopen))
            {
                int i = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[line.Length - 1] != GlobalChar)
                    {
                        return null;
                    }
                    if (maxcountline == true && i > 100)
                    {
                        break;
                    }
                    line = line.Remove(line.Length - 1);
                    _allDB.Add(line);
                    i++;
                }
            }
            return _allDB;
        }

        /// <summary>
        /// Функция, создания datatable для вывода 
        /// </summary>
        /// <param name="filenameopen">Путь к файлу</param>
        /// <param name="separator">Символ разделителя</param>
        /// <returns>datatable с значениями из БД</returns>
        public static DataTable MakeDataTable(string filenameopen, char separator, bool maxcountline)
        {
            GlobalChar = separator;
            List<string> ListCsv = MakeList(filenameopen, maxcountline);
            DataTable SaveTable = new DataTable();

            if (ListCsv == null)
            {
                return null;
            }

            List<string> Colums = MakeNameColumns(ListCsv[0]);
            List<List<string>> Cells = MakeCells(ListCsv);

            for (int i = 0; i < Colums.Count; i++)
                SaveTable.Columns.Add(Colums[i].ToString(), typeof(string));

            for (int i = 0; i < Cells.Count; i++)
            {
                DataRow drow = SaveTable.NewRow();
                for (int j = 0; j < Colums.Count; j++)
                {
                    drow[j] = Cells[i][j];
                }
                SaveTable.Rows.Add(drow);
            }

            return SaveTable;
        }
    }
}
