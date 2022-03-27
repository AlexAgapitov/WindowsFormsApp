using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class MakeNewColumns : Form
    {
        public MakeNewColumns()
        {
            InitializeComponent();
        }

        public string columnsName = string.Empty;

        private void Cancel_Click(object sender, EventArgs e)
        {
            columnsName = string.Empty;
            MainForm mainForm = new MainForm();
            Close();
        }

        private void AddNewColumnBtn_Click(object sender, EventArgs e)
        {
            columnsName = textBoxNameColumns.Text;
            MainForm mainForm = new MainForm();
            Close();
        }
    }
}
