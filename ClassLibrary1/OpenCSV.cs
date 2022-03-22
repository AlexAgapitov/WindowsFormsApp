using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;


namespace ClassOpenAndSave
{
    public class OpenCSV
    {
        /// <summary>
        /// Фукнция создания листа с названиями столбцов
        /// </summary>
        /// <param name="_firstLine">Первая строка CSV файла (название столбцов)</param>
        /// <returns></returns>
        public static List<string> MakeNameColumns(string _firstLine)
        {
            List<string> _listNameColomns = new List<string>();
            _listNameColomns = _firstLine.Split(',').ToList();

            return _listNameColomns;
        }

        /// <summary>
        /// Функция создания листа с значениями ячеек
        /// </summary>
        /// <param name="_lines">Лист со строками</param>
        /// <returns></returns>
        public static List<List<string>> MakeCells(List<string> _lines)
        {
            List<List<string>> _cellsValue = new List<List<string>>();
            for (int i = 1; i < _lines.Count; i++)
            {
                _cellsValue.Add(_lines[i].Split(',').ToList());
            }

            return _cellsValue;
        }

        /// <summary>
        /// Функция таблицы данных
        /// </summary>
        /// <param name="table"></param>
        /// <param name="_colums">Лист с названием колонок</param>
        /// <param name="_cells">Лист с значениями ячеек</param>
        /// <returns></returns>
        public static DataTable MakeDataTable(string filenameopen)
        {
            List<string> ListCsv = MakeList(filenameopen);
            DataTable SaveTable = new DataTable();
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

        /// <summary>
        /// Фукнция записывающая данные в лист
        /// </summary>
        /// <param name="filenameopen">путь к файлу</param>
        /// <returns></returns>
        public static List<string> MakeList(string filenameopen)
        {
            List<string> _allDB = new List<string>();

            using (StreamReader sr = new StreamReader(filenameopen))
            {
                int i = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Remove(line.Length - 1);
                    _allDB.Add(line);
                    i++;
                }
            }
            return _allDB;
        }
    }
}
