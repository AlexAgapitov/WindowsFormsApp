using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            numericUpDownk.Minimum = 2;
            numericUpDownk.Maximum = 3;
            numericUpDownKNN.Minimum = 2;
            numericUpDownKNN.Maximum = 3;
            numericUpDownafterdot.Minimum = 1;
            numericUpDownafterdot.Maximum = 5;
        }

        /// <summary>
        /// Создание таблицы данных
        /// </summary>
        public DataTable table = new DataTable();

        /// <summary>
        /// Глобальные переменные типа стринг для передачи пути к файлу
        /// </summary>
        public string GlobalExtension = string.Empty;
        public string GlobalFilePath = string.Empty;
        public string GlobalFileName = string.Empty;
        public string GlobalSavePath = string.Empty;
        public bool GlobalSaveAs = false;
        public bool GlobalMaxCountLine = false;

        /// <summary>
        /// Функция, октрывающая главную форму и заполняющая datagrid данными
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            const string filterop = "txt files (*.txt)|*.txt" + "|Excel Files|*.xls";
            openFileDialog1.Filter = filterop;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filenameopen = openFileDialog1.FileName;

            string filenameextension = Path.GetFileNameWithoutExtension(filenameopen) + Path.GetExtension(filenameopen);
            int nfilenameextension = filenameextension.Length;
            int countfilenameextension = filenameopen.Length;
            string filepath = filenameopen.Remove(countfilenameextension - nfilenameextension, nfilenameextension);

            string extension = Path.GetExtension(filenameopen);
            GlobalExtension = extension;
            GlobalFilePath = filepath;
            GlobalFileName = filenameextension;

            this.Text = "[" + GlobalFileName + "]";

            if (extension == ".txt")
            {
                char separator = ReturnChar();
                DataTable dt = new DataTable();
                dt = ClassOpenAndSave.OpenCSV.MakeDataTable(filenameopen, separator);
                if (dt == null)
                {
                    MessageBox.Show("Символ " + separator + " не является разделителем в данном файле.");
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
            else if (extension == ".xls")
            {
                OpenFileXLS();
            }
            else
            {
                MessageBox.Show("Файл не соответсвует типу CSV или XLS.");
            }
            CountRows();
            RadioBtnEnabled();
            ToolsStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Нумерация строк
        /// </summary>
        private void CountRows()
        {
            int rows = dataGridView1.RowCount;
            for (int i = 0; i < rows; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();   // i+1 потому, что нумерация нужна 
            }
        }

        /// <summary>
        /// Метод, обращающийся к классу OpenXLS
        /// </summary>
        public void OpenFileXLS()
        {
            string file = GlobalFilePath + GlobalFileName;
            ClassOpenAndSave.OpenXLS excel = new ClassOpenAndSave.OpenXLS(@file);
            dataGridView1.DataSource = excel.MakeDataTable();
        }

        /// <summary>
        /// Функция, сохраняющая файл КАК CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                const string filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.Filter = filter;

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filepathsave = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
                string filenamesave = System.IO.Path.GetFileName(saveFileDialog1.FileName);

                char separator = ReturnChar();
                table = (DataTable)dataGridView1.DataSource;

                string resultForSave = ClassOpenAndSave.SaveCSV.MakeOneCSV(table, separator);

                File.WriteAllText(filepathsave + "\\" + filenamesave, resultForSave);

                MessageBox.Show("Файл успешно сохранен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Функция добавления строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount == 0)
            {
                MessageBox.Show("Создайте колонку, чтобы добавить строку.");
            }
            else
            {
                table = (DataTable)dataGridView1.DataSource;
                table.Rows.Add();
                CountRows();
            }
        }

        /// <summary>
        /// Функция добавление столбца
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeNewColumns makeNewColumns = new MakeNewColumns();
            makeNewColumns.labelOr.Text = "Введите название для новой колонки";
            makeNewColumns.Text = "Создание нового столбца";
            makeNewColumns.ShowDialog();

            string str = makeNewColumns.columnsName;

            if (str != string.Empty)
            {
                if (dataGridView1.ColumnCount == 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(str.ToString(), typeof(string));
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    table = (DataTable)dataGridView1.DataSource;
                    table.Columns.Add(str.ToString(), typeof(string));
                }
            }
        }

        /// <summary>
        /// Функция удаления строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int delet = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows.RemoveAt(delet);
                CountRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// Функция удаления столбца
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MakeNewColumns makeNewColumns = new MakeNewColumns();
                makeNewColumns.labelOr.Text = "Введите название колонки";
                makeNewColumns.Text = "Удалиние столбца";
                makeNewColumns.ShowDialog();

                string str = makeNewColumns.textBoxNameColumns.Text;
                dataGridView1.Columns.Remove(str.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Фукнция, сохраняющая файл без КАК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { 
                string extension = GlobalExtension;
                char separator = ReturnChar();
                if (extension == ".txt")
                {
                    table = (DataTable)dataGridView1.DataSource;

                    string resultForSave = ClassOpenAndSave.SaveCSV.MakeOneCSV(table, separator);

                    File.WriteAllText(GlobalFilePath + "\\" + GlobalFileName, resultForSave);
                }
                else
                {
                    GlobalSaveAs = false;
                    SaveFileXLS(GlobalSaveAs);
                }

                MessageBox.Show("Файл успешно сохранен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Функция, сохраняющая файл КАК XLS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsXLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                const string filter = "Excel Files|*.xls";
                saveFileDialog1.Filter = filter;

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filepathsave = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
                string filenamesave = System.IO.Path.GetFileName(saveFileDialog1.FileName);

                GlobalSaveAs = true;
                GlobalSavePath = filepathsave + "\\";
                GlobalFileName = filenamesave;
                SaveFileXLS(GlobalSaveAs);
                MessageBox.Show("Файл успешно сохранен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Метод, обращающийся к классу SaveXLS
        /// </summary>
        /// <param name="saveAs">Булевая переменная, сохранить в этот файл или новый</param>
        public void SaveFileXLS(bool saveAs)
        {
            table = (DataTable)dataGridView1.DataSource;
            if (saveAs != true)
                GlobalSavePath = GlobalFilePath; 
            ClassOpenAndSave.SaveXLS excel = new ClassOpenAndSave.SaveXLS(GlobalSavePath, GlobalFileName, table, saveAs);
            excel.ExportToExcel();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //List<string> l1 = new List<string>() { "Коньяк Киргиз", "Коньяк Киргизия", "Водка Белуга", "Пиво Балтика", "Кола" };
            //List<string> l2 = new List<string>() { "Водка", "Коньяк" };

            //List<string> l3 = new List<string>();
            //l3 = ClassLibraryForData.DataCategorical.CategoryCutV2(l1, l2);

            //foreach (string s in l3)
            //    MessageBox.Show(s);

            checkBoxSeparator.Checked = false;
            radioButtonComma.Checked = true;
            textBoxOtherSeparator.Enabled = false;
            checkBoxKmeansEnable();
            checkBoxKnnEnable();
            RadioBtnEnabled();
        }

        /// <summary>
        /// Метод, radioButton не активны
        /// </summary>
        private void RadioBtnEnabled()
        {
            checkBoxSeparator.Checked = false;
            radioButtonComma.Checked = true;
            radioButtonSpace.Enabled = false;
            radioButtonOther.Enabled = false;
            radioButtonComma.Enabled = false;
            radioButtonCemicolon.Enabled = false;
            radioButtonTabuletion.Enabled = false;
            textBoxOtherSeparator.Text = string.Empty;
        }

        /// <summary>
        /// Метод, определяющий символ разделителя
        /// </summary>
        /// <returns>Символ разделителя</returns>
        private char ReturnChar()
        {
            char SeparatorChar = ',';

            if (checkBoxSeparator.Checked == true)
            {
                if (radioButtonComma.Checked == true)
                    SeparatorChar = ',';
                if (radioButtonCemicolon.Checked == true)
                    SeparatorChar = ';';
                if (radioButtonSpace.Checked == true)
                    SeparatorChar = ' ';
                if (radioButtonTabuletion.Checked == true)
                    SeparatorChar = '\t';
                if (radioButtonOther.Checked == true)
                    SeparatorChar = textBoxOtherSeparator.Text[0];
            }
            return SeparatorChar;
        }

        /// <summary>
        /// Пока checkBox не помечен, radioButton не активны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSeparator_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSeparator.Checked == true)
            {
                radioButtonTabuletion.Enabled = true;
                radioButtonSpace.Enabled = true;
                radioButtonOther.Enabled = true;
                radioButtonComma.Enabled = true;
                radioButtonCemicolon.Enabled = true;
            }
            else
            {
                radioButtonTabuletion.Enabled = false;
                radioButtonSpace.Enabled = false;
                radioButtonOther.Enabled = false;
                radioButtonComma.Enabled = false;
                radioButtonCemicolon.Enabled = false;
            }
        }

        /// <summary>
        /// При нажатии Другое textbox становится доступным
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonOther_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonOther.Checked)
                textBoxOtherSeparator.Enabled = true;
            else
                textBoxOtherSeparator.Enabled = false;
        }

        /// <summary>
        /// Нормализация данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NormalizeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)dataGridView1.DataSource;
            List<string> list = new List<string>();
            List<string> listresult = new List<string>();
            int s = 0;

            MakeNewColumns makeNewColumns = new MakeNewColumns();
            makeNewColumns.labelOr.Text = "Введите название колонки";
            makeNewColumns.Text = "Нормализация данных";
            makeNewColumns.ShowDialog();

            string str = makeNewColumns.textBoxNameColumns.Text;

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == str)
                    {
                        if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == "?" || row[column].ToString() == " " || row[column].ToString() == "<null>")
                        {
                            list.Add(null);
                        }
                        else
                        {
                            list.Add(row[column].ToString());
                        }
                    }
                }
            }

            listresult = ClassLibraryForData.DataNormalize.Normalize(list, "sqrt");

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == str)
                    {
                        row[column] = listresult[s];
                        s++;
                    }
                }
            }
            dataGridView1.DataSource = table;
        }

        /// <summary>
        /// Восстановление данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void восстановлениеToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    DataTable table = new DataTable();
        //    table = (DataTable)dataGridView1.DataSource;
        //    List<string> list = new List<string>();
        //    List<string> listresult = new List<string>();
        //    int s = 0;

        //    MakeNewColumns makeNewColumns = new MakeNewColumns();
        //    makeNewColumns.labelOr.Text = "Введите название колонки";
        //    makeNewColumns.Text = "Восстановление данных";
        //    makeNewColumns.ShowDialog();

        //    string str = makeNewColumns.textBoxNameColumns.Text;

        //    foreach (DataRow row in table.Rows)
        //    {
        //        foreach (DataColumn column in table.Columns)
        //        {
        //            if (column.ColumnName == str)
        //            {
        //                if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == "?" || row[column].ToString() == " " || row[column].ToString() == "<null>")
        //                {
        //                    list.Add(null);
        //                }
        //                else
        //                {
        //                    list.Add(row[column].ToString());
        //                }
        //            }
        //        }
        //    }

        //    listresult = ClassLibraryForData.DataResponse.Recovery(list, "Mean");

        //    foreach (DataRow row in table.Rows)
        //    {
        //        foreach (DataColumn column in table.Columns)
        //        {
        //            if (column.ColumnName == str)
        //            {
        //                row[column] = listresult[s];
        //                s++;
        //            }
        //        }
        //    }
        //    dataGridView1.DataSource = table;

        //}

        /// <summary>
        /// Приведение к одной единице измерения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResponseUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)dataGridView1.DataSource;
            List<string> list = new List<string>();
            List<string> listresult = new List<string>();
            bool isNull = false;
            int s = 0;

            MakeNewColumns makeNewColumns = new MakeNewColumns();
            makeNewColumns.labelOr.Text = "Введите название колонки";
            makeNewColumns.Text = "Приведение к одной ед.измр.";
            makeNewColumns.ShowDialog();

            string str = makeNewColumns.textBoxNameColumns.Text;

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == str)
                    {
                        if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == "?" || row[column].ToString() == " " || row[column].ToString() == "<null>")
                        {
                            MessageBox.Show("Таблица содержит пустые данные.");
                            isNull = true;
                            break;
                        }
                        else
                        {
                            list.Add(row[column].ToString());
                        }
                    }
                }
                if (isNull) break;
            }

            if (!isNull)
            {
                listresult = ClassLibraryForData.DataResponseUnit.ConvertUnit(list);

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == str)
                        {
                            row[column] = listresult[s];
                            s++;
                        }
                    }
                }
                dataGridView1.DataSource = table;
            }
        }

        /// <summary>
        /// Приведение к категориям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table = (DataTable)dataGridView1.DataSource;
            List<string> list = new List<string>();
            List<string> listresult = new List<string>();
            bool isNull = false;
            int s = 0;

            GetStringForCategory getStringForCategory = new GetStringForCategory();
            getStringForCategory.ShowDialog();

            string nameColumn = getStringForCategory.textBoxNameColumn.Text;
            string[] stringCategory = getStringForCategory.textBoxListCategory.Text.Split(';');
            string[] stringBin = getStringForCategory.textBoxListRange.Text.Split(';');
            List<string> listCategory = new List<string>();
            List<string> listBin = new List<string>();
            for (int i = 0; i < stringCategory.Length; i++)
            {
                listCategory.Add(stringCategory[i]);
            }
            for (int i = 0; i < stringBin.Length; i++)
            {
                listBin.Add(stringBin[i]);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == nameColumn)
                    {
                        if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == "?" || row[column].ToString() == " " || row[column].ToString() == "<null>")
                        {
                            MessageBox.Show("Таблица содержит пустые данные.");
                            isNull = true;
                            break;
                        }
                        else
                        {
                            list.Add(row[column].ToString());
                        }
                    }
                }
                if (isNull) break;
            }

            if (!isNull)
            {
                listresult = ClassLibraryForData.DataCategorical.CategoryCut(list, listCategory, listBin);

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == nameColumn)
                        {
                            row[column] = listresult[s];
                            s++;
                        }
                    }
                }
                dataGridView1.DataSource = table;
            }
        }

        /// <summary>
        /// Среднее значение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeanResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nameMethod = "Mean";
            GerResponse(nameMethod);
        }

        /// <summary>
        /// Самое повторяющееся слово
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StringResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nameMethod = "String";
            GerResponse(nameMethod);
        }

        /// <summary>
        /// Линейная интерполяция
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nameMethod = "Linear";
            GerResponse(nameMethod);
        }

        /// <summary>
        /// Метод для восстановления данных
        /// </summary>
        /// <param name="nameMethod"></param>
        private void GerResponse(string nameMethod)
        {
            DataTable table = new DataTable();
            table = (DataTable)dataGridView1.DataSource;
            List<string> list = new List<string>();
            List<string> listresult = new List<string>();
            int s = 0;

            MakeNewColumns makeNewColumns = new MakeNewColumns();
            makeNewColumns.labelOr.Text = "Введите название колонки";
            makeNewColumns.Text = "Восстановление данных";
            makeNewColumns.ShowDialog();

            string str = makeNewColumns.textBoxNameColumns.Text;

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == str)
                    {
                        if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == "?" || row[column].ToString() == " " || row[column].ToString() == "<null>")
                        {
                            list.Add(null);
                        }
                        else
                        {
                            list.Add(row[column].ToString());
                        }
                    }
                }
            }

            listresult = ClassLibraryForData.DataResponse.Recovery(list, nameMethod);

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == str)
                    {
                        row[column] = listresult[s];
                        s++;
                    }
                }
            }
            dataGridView1.DataSource = table;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        int k;
        double centerX1;
        double centerY1;
        double centerX2;
        double centerY2;
        double centerX3;
        double centerY3;
        List<double> newCenterX1 = new List<double>();
        List<double> newCenterY1 = new List<double>();
        List<double> newCenterX2 = new List<double>();
        List<double> newCenterY2 = new List<double>();
        List<double> newCenterX3 = new List<double>();
        List<double> newCenterY3 = new List<double>();
        double newCenterPointX1 = 0;
        double newCenterPointY1 = 0;
        double newCenterPointX2 = 0;
        double newCenterPointY2 = 0;
        double newCenterPointX3 = 0;
        double newCenterPointY3 = 0;
        int counter = 0;
        bool repeat = true;
        int digitNumberAfterDot;
        bool runOnce = false;

        private double distanceToCenterFunction(double centerX, double centerY, double pointX, double pointY)
        {
            var result = Math.Sqrt(Math.Pow(centerX - pointX, 2) + Math.Pow(centerY - pointY, 2));
            return result;
        }

        /// <summary>
        /// Поиск центров класстеров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonKmeans_Click(object sender, EventArgs e)
        {
            string s1 = string.Empty;
            string s2 = string.Empty;
            string s3 = string.Empty;

            repeat = true;
            runOnce = false;
            k = Convert.ToInt32(numericUpDownk.Value);
            digitNumberAfterDot = Convert.ToInt32(numericUpDownafterdot.Value);
            centerX1 = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
            centerY1 = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
            centerX2 = Convert.ToDouble(dataGridView1.Rows[2].Cells[0].Value);
            centerY2 = Convert.ToDouble(dataGridView1.Rows[2].Cells[1].Value);
            centerX3 = Convert.ToDouble(dataGridView1.Rows[3].Cells[0].Value);
            centerY3 = Convert.ToDouble(dataGridView1.Rows[3].Cells[1].Value);
            int rowLenght = Convert.ToInt32(dataGridView1.Rows.GetRowCount(0)) - 1;

            if (k == 2)
            {
                while (repeat == true)
                {
                    for (int i = 0; i < rowLenght; i++)
                    {
                        double pointX = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                        double pointY = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                        double centerA = distanceToCenterFunction(centerX1, centerY1, pointX, pointY);
                        double centerB = distanceToCenterFunction(centerX2, centerY2, pointX, pointY);

                        if (centerA < centerB)
                        {
                            newCenterX1.Add(pointX);
                            newCenterY1.Add(pointY);
                        }
                        else
                        {
                            newCenterX2.Add(pointX);
                            newCenterY2.Add(pointY);
                        }
                    }
                    for (int i = 0; i < newCenterX1.Count; i++)
                    {
                        newCenterPointX1 = newCenterPointX1 + newCenterX1[i];
                    }
                    for (int i = 0; i < newCenterY1.Count; i++)
                    {
                        newCenterPointY1 = newCenterPointY1 + newCenterY1[i];
                    }
                    for (int i = 0; i < newCenterX2.Count; i++)
                    {
                        newCenterPointX2 = newCenterPointX2 + newCenterX2[i];
                    }
                    for (int i = 0; i < newCenterY2.Count; i++)
                    {
                        newCenterPointY2 = newCenterPointY2 + newCenterY2[i];
                    }
                    newCenterPointX1 = newCenterPointX1 / newCenterX1.Count;
                    newCenterPointY1 = newCenterPointY1 / newCenterY1.Count;
                    newCenterPointX2 = newCenterPointX2 / newCenterX2.Count;
                    newCenterPointY2 = newCenterPointY2 / newCenterY2.Count;

                    centerX1 = Math.Round(centerX1, digitNumberAfterDot);
                    centerY1 = Math.Round(centerY1, digitNumberAfterDot);
                    centerX2 = Math.Round(centerX2, digitNumberAfterDot);
                    centerY2 = Math.Round(centerY2, digitNumberAfterDot);
                    newCenterPointX1 = Math.Round(newCenterPointX1, digitNumberAfterDot);
                    newCenterPointY1 = Math.Round(newCenterPointY1, digitNumberAfterDot);
                    newCenterPointX2 = Math.Round(newCenterPointX2, digitNumberAfterDot);
                    newCenterPointY2 = Math.Round(newCenterPointY2, digitNumberAfterDot);

                    if (centerX1 == newCenterPointX1 && centerY1 == newCenterPointY1 && centerX2 == newCenterPointX2 && centerY2 == newCenterPointY2)
                    {
                        //toolStripStatusLabel1.Text = "Process Complete.";
                        //if (counter == maxRepeat)
                        //{
                        //    toolStripStatusLabel1.Text = "Program stopped because the algorithm was repeated" + " " + maxRepeat + " " + "times.";
                        //}

                        if (runOnce == false)
                        {
                        //    dataGridView2.Columns.Add("ColunmName", " ");
                        //    dataGridView2.Columns.Add("ColunmName", "X");
                        //    dataGridView2.Columns.Add("ColunmName", "Y");
                            runOnce = true;
                        }

                        //for (int i = 0; i < k; i++)
                        //{
                        //    dataGridView2.Rows.Add();
                        //    dataGridView2.Rows[i].Cells[0].Value = "Cluster Center" + " " + (i + 1);
                        //}
                        //dataGridView2.Rows[0].Cells[1].Value = newCenterPointX1;
                        //dataGridView2.Rows[0].Cells[2].Value = newCenterPointY1;
                        //dataGridView2.Rows[1].Cells[1].Value = newCenterPointX2;
                        //dataGridView2.Rows[1].Cells[2].Value = newCenterPointY2;

                        MessageBox.Show("X: " + newCenterPointX1 + " Y:" + newCenterPointY1 + "\nX: " + newCenterPointX2 + " Y: " + newCenterPointY2);

                        repeat = false;
                    }
                    else
                    {
                        repeat = true;
                        counter = counter + 1;
                    }
                    centerX1 = newCenterPointX1;
                    centerY1 = newCenterPointY1;
                    centerX2 = newCenterPointX2;
                    centerY2 = newCenterPointY2;
                }
            }
            else if (k == 3)
            {
                while (repeat == true)
                {
                    for (int i = 0; i < rowLenght; i++)
                    {
                        double pointX = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                        double pointY = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                        double distanceToCenter1 = distanceToCenterFunction(centerX1, centerY1, pointX, pointY);
                        double distanceToCenter2 = distanceToCenterFunction(centerX2, centerY2, pointX, pointY);
                        double distanceToCenter3 = distanceToCenterFunction(centerX3, centerY3, pointX, pointY);

                        if (distanceToCenter1 < distanceToCenter2 && distanceToCenter1 < distanceToCenter3)
                        {
                            newCenterX1.Add(pointX);
                            newCenterY1.Add(pointY);
                        }
                        else if (distanceToCenter2 < distanceToCenter1 && distanceToCenter2 < distanceToCenter3)
                        {
                            newCenterX2.Add(pointX);
                            newCenterY2.Add(pointY);
                        }
                        else if (distanceToCenter3 < distanceToCenter1 && distanceToCenter3 < distanceToCenter2)
                        {
                            newCenterX3.Add(pointX);
                            newCenterY3.Add(pointY);
                        }
                        else
                        {
                            MessageBox.Show("Error Code: distanceToCenter ");
                        }
                    }
                    for (int i = 0; i < newCenterX1.Count; i++)
                    {
                        newCenterPointX1 = newCenterPointX1 + newCenterX1[i];
                    }
                    for (int i = 0; i < newCenterY1.Count; i++)
                    {
                        newCenterPointY1 = newCenterPointY1 + newCenterY1[i];
                    }
                    for (int i = 0; i < newCenterX2.Count; i++)
                    {
                        newCenterPointX2 = newCenterPointX2 + newCenterX2[i];
                    }
                    for (int i = 0; i < newCenterY2.Count; i++)
                    {
                        newCenterPointY2 = newCenterPointY2 + newCenterY2[i];
                    }
                    for (int i = 0; i < newCenterX3.Count; i++)
                    {
                        newCenterPointX3 = newCenterPointX3 + newCenterX3[i];
                    }
                    for (int i = 0; i < newCenterY3.Count; i++)
                    {
                        newCenterPointY3 = newCenterPointY3 + newCenterY3[i];
                    }
                    newCenterPointX1 = newCenterPointX1 / newCenterX1.Count;
                    newCenterPointY1 = newCenterPointY1 / newCenterY1.Count;
                    newCenterPointX2 = newCenterPointX2 / newCenterX2.Count;
                    newCenterPointY2 = newCenterPointY2 / newCenterY2.Count;
                    newCenterPointX3 = newCenterPointX3 / newCenterX3.Count;
                    newCenterPointY3 = newCenterPointY3 / newCenterY3.Count;

                    centerX1 = Math.Round(centerX1, digitNumberAfterDot);
                    centerY1 = Math.Round(centerY1, digitNumberAfterDot);
                    centerX2 = Math.Round(centerX2, digitNumberAfterDot);
                    centerY2 = Math.Round(centerY2, digitNumberAfterDot);
                    centerX3 = Math.Round(centerX3, digitNumberAfterDot);
                    centerY3 = Math.Round(centerY3, digitNumberAfterDot);
                    newCenterPointX1 = Math.Round(newCenterPointX1, digitNumberAfterDot);
                    newCenterPointY1 = Math.Round(newCenterPointY1, digitNumberAfterDot);
                    newCenterPointX2 = Math.Round(newCenterPointX2, digitNumberAfterDot);
                    newCenterPointY2 = Math.Round(newCenterPointY2, digitNumberAfterDot);
                    newCenterPointX3 = Math.Round(newCenterPointX3, digitNumberAfterDot);
                    newCenterPointY3 = Math.Round(newCenterPointY3, digitNumberAfterDot);

                    if (centerX1 == newCenterPointX1 && centerY1 == newCenterPointY1 && centerX2 == newCenterPointX2 && centerY2 == newCenterPointY2 && centerX3 == newCenterPointX3 && centerY3 == newCenterPointY3)
                    {
                        //toolStripStatusLabel1.Text = "Process Complete.";
                        //if (counter == maxRepeat)
                        //{
                        //    toolStripStatusLabel1.Text = "Program stopped because the algorithm was repeated" + " " + maxRepeat + " " + "times.";
                        //}

                        if (runOnce == false)
                        {
                            //dataGridView2.Columns.Add("ColunmName", " ");
                            //dataGridView2.Columns.Add("ColunmName", "X");
                            //dataGridView2.Columns.Add("ColunmName", "Y");
                            runOnce = true;
                        }

                        //for (int i = 0; i < k; i++)
                        //{
                        //    dataGridView2.Rows.Add();
                        //    dataGridView2.Rows[i].Cells[0].Value = "Cluster Center" + " " + (i + 1);
                        //}
                        //dataGridView2.Rows[0].Cells[1].Value = newCenterPointX1;
                        //dataGridView2.Rows[0].Cells[2].Value = newCenterPointY1;
                        //dataGridView2.Rows[1].Cells[1].Value = newCenterPointX2;
                        //dataGridView2.Rows[1].Cells[2].Value = newCenterPointY2;
                        //dataGridView2.Rows[2].Cells[1].Value = newCenterPointX3;
                        //dataGridView2.Rows[2].Cells[2].Value = newCenterPointY3;

                        MessageBox.Show("X: " + newCenterPointX1 + " Y:" + newCenterPointY1 + "\nX: " + newCenterPointX2 + " Y: " + newCenterPointY2 + "\nX: " + newCenterPointX3 + " Y: " + newCenterPointY3);

                        repeat = false;
                    }
                    else
                    {
                        repeat = true;
                        counter = counter + 1;
                    }
                    centerX1 = newCenterPointX1;
                    centerY1 = newCenterPointY1;
                    centerX2 = newCenterPointX2;
                    centerY2 = newCenterPointY2;
                    centerX3 = newCenterPointX3;
                    centerY3 = newCenterPointY3;
                }
            }
            else
            {
                MessageBox.Show("Unhandled Exception!");
            }

        }

        /// <summary>
        /// Enable Кнопок
        /// </summary>
        private void checkBoxkmeans_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxkmeans.Checked == true)
            {
                numericUpDownk.Enabled = true;
                numericUpDownafterdot.Enabled = true;
                buttonKmeans.Enabled = true;
            }
            else
            {
                numericUpDownk.Enabled = false;
                numericUpDownafterdot.Enabled = false;
                buttonKmeans.Enabled = false;
            }
        }

        /// <summary>
        /// Enable Кнопок
        /// </summary>
        private void checkBoxKmeansEnable()
        {
            numericUpDownk.Enabled = false;
            numericUpDownafterdot.Enabled = false;
            buttonKmeans.Enabled = false;
        }

        /// <summary>
        /// Вызов метода ближайший сосед
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonKNN_Click(object sender, EventArgs e)
        {
            int CountParam = int.Parse(numericUpDownKNN.Value.ToString());
            int kValue = int.Parse(textBoxkvalue.Text);
            var table = new DataTable();
            table = (DataTable)dataGridView1.DataSource;
            var listname = new List<string>();
            var listresult = new List<string>();
            var list = new List<string>();
            int count = 0;

            GetColumns getColumns = new GetColumns();
            if (CountParam == 2)
            {
                getColumns.textBoxParam3.Enabled = false;
                getColumns.ShowDialog();
                listname.Add(getColumns.textBoxParam1.Text);
                listname.Add(getColumns.textBoxParam2.Text);
                listname.Add(getColumns.textBoxProrerty.Text);
            }
            else
            {
                getColumns.ShowDialog();
                listname.Add(getColumns.textBoxParam1.Text);
                listname.Add(getColumns.textBoxParam2.Text);
                listname.Add(getColumns.textBoxParam3.Text);
                listname.Add(getColumns.textBoxProrerty.Text);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    foreach(string s in listname)
                    if (column.ColumnName == s)
                    {
                        if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == "?" || row[column].ToString() == " " || row[column].ToString() == "<null>")
                        {
                            list.Add(null);
                        }
                        else
                        {
                            list.Add(row[column].ToString());
                        }
                    }
                }
            }

            listresult = ClassLibraryForData.DataKNN.seachKNN(list, kValue, CountParam);

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == listname[listname.Count-1])
                    {
                        row[column] = listresult[count];
                        count++;
                    }
                }
            }
            dataGridView1.DataSource = table;
        }

        /// <summary>
        /// Enable Кнопок
        /// </summary>
        private void checkBoxKnn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKnn.Checked == true)
            {
                numericUpDownKNN.Enabled = true;
                textBoxkvalue.Enabled = true;
                buttonKNN.Enabled = true; 
            }
            else
            {
                numericUpDownKNN.Enabled = false;
                textBoxkvalue.Enabled = false;
                buttonKNN.Enabled = false;
            }
        }

        /// <summary>
        /// Enable Кнопок
        /// </summary>
        private void checkBoxKnnEnable()
        {
            numericUpDownKNN.Enabled = false;
            textBoxkvalue.Enabled = false;
            buttonKNN.Enabled = false;
        }
    }
}