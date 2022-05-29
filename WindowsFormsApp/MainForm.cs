using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            numericUpDownKNN.Minimum = 2;
            numericUpDownKNN.Maximum = 3;
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
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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

                this.Text = "Репозиторий БД - [" + GlobalFileName + "]";

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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Нумерация строк
        /// </summary>
        private void CountRows()
        {
            int rows = dataGridView1.RowCount;
            for (int i = 0; i < rows; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
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
        private void FileCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                const string filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.Filter = filter;

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filepathsave = Path.GetDirectoryName(saveFileDialog1.FileName);
                string filenamesave = Path.GetFileName(saveFileDialog1.FileName);

                char separator = ReturnChar();
                table = (DataTable)dataGridView1.DataSource;

                string resultForSave = ClassOpenAndSave.SaveCSV.MakeOneCSV(table, separator);

                File.WriteAllText(filepathsave + "\\" + filenamesave, resultForSave);

                MessageBox.Show("Файл успешно сохранен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Функция добавления строки
        /// </summary>
        private void AddLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Функция добавление столбца
        /// </summary>
        private void AddColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
                        dt.Columns.Add(str, typeof(string));
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        table = (DataTable)dataGridView1.DataSource;
                        table.Columns.Add(str, typeof(string));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Функция удаления строки
        /// </summary>
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
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Функция удаления столбца
        /// </summary>
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
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Фукнция, сохраняющая файл без КАК
        /// </summary>
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
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Функция, сохраняющая файл КАК XLS
        /// </summary>
        private void SaveAsXLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                const string filter = "Excel Files|*.xls";
                saveFileDialog1.Filter = filter;

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filepathsave = Path.GetDirectoryName(saveFileDialog1.FileName);
                string filenamesave = Path.GetFileName(saveFileDialog1.FileName);

                GlobalSaveAs = true;
                GlobalSavePath = filepathsave + "\\";
                GlobalFileName = filenamesave;
                SaveFileXLS(GlobalSaveAs);
                MessageBox.Show("Файл успешно сохранен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

            if (checkBoxSeparator.Checked)
            {
                if (radioButtonComma.Checked)
                    SeparatorChar = ',';
                if (radioButtonCemicolon.Checked)
                    SeparatorChar = ';';
                if (radioButtonSpace.Checked)
                    SeparatorChar = ' ';
                if (radioButtonTabuletion.Checked)
                    SeparatorChar = '\t';
                if (radioButtonOther.Checked)
                    SeparatorChar = textBoxOtherSeparator.Text[0];
            }
            return SeparatorChar;
        }

        /// <summary>
        /// Пока checkBox не помечен, radioButton не активны
        /// </summary>
        private void checkBoxSeparator_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSeparator.Checked)
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
        private void radioButtonOther_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOtherSeparator.Enabled = radioButtonOther.Checked;
        }

        /// <summary>
        /// Приведение к одной единице измерения
        /// </summary>
        private void ResponseUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Приведение к категориям
        /// </summary>
        private void CategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Среднее значение
        /// </summary>
        private void MeanResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerResponse(ClassLibraryForData.EnumsMethod.MethodResponse.responseDouble);
        }

        /// <summary>
        /// Самое повторяющееся слово
        /// </summary>
        private void StringResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerResponse(ClassLibraryForData.EnumsMethod.MethodResponse.responseString);
        }

        /// <summary>
        /// Линейная интерполяция
        /// </summary>
        private void LineResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerResponse(ClassLibraryForData.EnumsMethod.MethodResponse.responseLinear);
        }

        /// <summary>
        /// Метод для восстановления данных
        /// </summary>
        private void GerResponse(ClassLibraryForData.EnumsMethod.MethodResponse methodResponse)
        {
            try
            {
                var table = (DataTable)dataGridView1.DataSource;
                var list = new List<string>();
                int s = 0;

                if (table == null)
                {
                    MessageBox.Show("Таблица пуста");
                    return;
                }

                MakeNewColumns makeNewColumns = new MakeNewColumns();
                makeNewColumns.labelOr.Text = "Введите название колонки";
                makeNewColumns.Text = "Восстановление данных";
                makeNewColumns.ShowDialog();

                string str = makeNewColumns.textBoxNameColumns.Text;
                if (str == string.Empty)
                {
                    MessageBox.Show("Отмена");
                    return;
                }

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

                var listresult = ClassLibraryForData.DataResponse.Recovery(list, methodResponse);

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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Вызов метода ближайший сосед
        /// </summary>
        private void buttonKNN_Click(object sender, EventArgs e)
        {
            try
            {
                int CountParam = int.Parse(numericUpDownKNN.Value.ToString());
                int kValue = int.Parse(textBoxkvalue.Text);
                var table = new DataTable();
                table = (DataTable)dataGridView1.DataSource;
                var listname = new List<string>();
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
                        foreach (string s in listname)
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

                var listresult = ClassLibraryForData.DataKNN.seachKNN(list, kValue, CountParam);

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == listname[listname.Count - 1])
                        {
                            row[column] = listresult[count];
                            count++;
                        }
                    }
                }
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Enable Кнопок
        /// </summary>
        private void checkBoxKnn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKnn.Checked)
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

        private void GerNormalize(ClassLibraryForData.EnumsMethod.MethodNormalize methodNormalize)
        {
            try
            {
                DataTable table = new DataTable();
                table = (DataTable)dataGridView1.DataSource;
                List<string> list = new List<string>();
                int s = 0;

                if (table == null)
                {
                    MessageBox.Show("Таблица пуста");
                    return;
                }

                MakeNewColumns makeNewColumns = new MakeNewColumns();
                makeNewColumns.labelOr.Text = "Введите название колонки";
                makeNewColumns.Text = "Нормализация данных";
                makeNewColumns.ShowDialog();

                string str = makeNewColumns.textBoxNameColumns.Text;
                if (str == string.Empty)
                {
                    MessageBox.Show("Отмена");
                    return;
                }
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

                var listresult = ClassLibraryForData.DataNormalize.Normalize(list, methodNormalize);

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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Normalize1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerNormalize(ClassLibraryForData.EnumsMethod.MethodNormalize.normalizeDouble);
        }

        private void Normalize2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerNormalize(ClassLibraryForData.EnumsMethod.MethodNormalize.normalizeMinMax);
        }

        private void Category1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerCategory(ClassLibraryForData.EnumsMethod.MethodCategory.categoryPython);
        }

        private void Category2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerCategory(ClassLibraryForData.EnumsMethod.MethodCategory.categoryKalabin);
        }

        private void GerCategory(ClassLibraryForData.EnumsMethod.MethodCategory methodCategory)
        {
            try
            {
                var table = (DataTable)dataGridView1.DataSource;
                var list = new List<string>();
                var listresult = new List<string>();
                bool isNull = false;
                int s = 0;

                if (table == null)
                {
                    MessageBox.Show("Таблица пуста");
                    return;
                }

                GetStringForCategory getStringForCategory = new GetStringForCategory();
                if (methodCategory == ClassLibraryForData.EnumsMethod.MethodCategory.categoryKalabin)
                {
                    getStringForCategory.textBoxListRange.Enabled = false;
                }
                getStringForCategory.ShowDialog();

                if (methodCategory == ClassLibraryForData.EnumsMethod.MethodCategory.categoryPython)
                {
                    if (getStringForCategory.textBoxNameColumn.Text == string.Empty || getStringForCategory.textBoxListCategory.Text == string.Empty || getStringForCategory.textBoxListRange.Text == string.Empty)
                    {
                        MessageBox.Show("Отмена");
                        return;
                    }
                }
                else
                {
                    if (getStringForCategory.textBoxNameColumn.Text == string.Empty || getStringForCategory.textBoxListCategory.Text == string.Empty)
                    {
                        MessageBox.Show("Отмена");
                        return;
                    }
                }

                string nameColumn = getStringForCategory.textBoxNameColumn.Text;
                string[] stringCategory = getStringForCategory.textBoxListCategory.Text.Split(';');
                string[] stringBin = getStringForCategory.textBoxListRange.Text.Split(';');
                if ((stringCategory.Length != stringBin.Length) && methodCategory == ClassLibraryForData.EnumsMethod.MethodCategory.categoryPython)
                {
                    MessageBox.Show("Кол-во названий и диапазона категорий должно быть одинаково");
                    return;
                }
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
                    if (methodCategory == ClassLibraryForData.EnumsMethod.MethodCategory.categoryPython)
                    {
                        listresult = ClassLibraryForData.DataCategorical.CategoryCut(list, listCategory, listBin);
                    }
                    else
                    {
                        listresult = ClassLibraryForData.DataCategorical.CategoryCutV2(list, listCategory);
                    }

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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}