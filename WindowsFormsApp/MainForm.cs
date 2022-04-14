using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
            const string filterop = "txt files (*.txt)|*.txt" + "|Excel Files|*.xls"; //CSV (*.csv)|*.csv" 
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
                dt = ClassOpenAndSave.OpenCSV.MakeDataTable(filenameopen, separator, GlobalMaxCountLine);
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
            ClassOpenAndSave.OpenXLS excel = new ClassOpenAndSave.OpenXLS(@file, GlobalMaxCountLine) ;
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
                makeNewColumns.labelOr.Text = "Введите название колонки, которую хотите удалить";
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
            checkBoxSeparator.Checked = false;
            radioButtonComma.Checked = true;
            textBoxOtherSeparator.Enabled = false;
            //radioButton100Line.Checked = false;
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
        /// Восстановление данных
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
                        if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == " " || row[column].ToString() == "<null>")
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

            listresult = ClassLibraryForData.DataNormalize.Normalize(list);

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
        /// Нормализация данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void восстановлениеToolStripMenuItem_Click(object sender, EventArgs e)
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
                        if (row[column].ToString() == "" || row[column].ToString() == "-" || row[column].ToString() == " " || row[column].ToString() == "<null>")
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

            listresult = ClassLibraryForData.DataResponse.Recovery(list);

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
}
