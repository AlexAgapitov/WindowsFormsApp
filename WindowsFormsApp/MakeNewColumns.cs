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

        private void Cancel_Click(object sender, EventArgs e)
        {

        }

        private void AddNewColumnBtn_Click(object sender, EventArgs e)
        {
            string NameColumn = textBoxNameColumns.Text;
            MainForm mainForm = new MainForm();
            Close();
        }
    }
}
