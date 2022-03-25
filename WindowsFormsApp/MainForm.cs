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

        /// <summary>
        /// Функция, октрывающая главную форму и заполняющая datagrid данными
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            const string filterop = "CSV (*.csv)|*.csv" + "|Excel Files|*.xls";
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

            if (extension == ".csv")
            {
                dataGridView1.DataSource = ClassOpenAndSave.OpenCSV.MakeDataTable(filenameopen);
            }
            else if (extension == ".xls")
            {
                OpenFileXLS();
            }
            else
            {
                MessageBox.Show("Файл не соответсвует типу CSV или XLS.");
            }
            ToolsStripMenuItem.Enabled = true;
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
                const string filter = "CSV (*.csv)|*.csv";
                saveFileDialog1.Filter = filter;

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filepathsave = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName);
                string filenamesave = System.IO.Path.GetFileName(saveFileDialog1.FileName);

                table = (DataTable)dataGridView1.DataSource;

                string resultForSave = ClassOpenAndSave.SaveCSV.MakeOneCSV(table);

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
            table = (DataTable)dataGridView1.DataSource;
            table.Rows.Add();
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

            string str = makeNewColumns.textBoxNameColumns.Text;

            table = (DataTable)dataGridView1.DataSource;

            table.Columns.Add(str.ToString(), typeof(string));
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
                if (extension == ".csv")
                {
                    table = (DataTable)dataGridView1.DataSource;

                    string resultForSave = ClassOpenAndSave.SaveCSV.MakeOneCSV(table);

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
            ToolsStripMenuItem.Enabled = false;
        }
    }
}
