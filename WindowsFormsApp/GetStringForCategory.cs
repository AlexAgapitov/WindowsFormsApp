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
    public partial class GetStringForCategory : Form
    {
        public GetStringForCategory()
        {
            InitializeComponent();
        }

        public string columnsName = string.Empty;
        public string listcategory = string.Empty;
        public string listbin = string.Empty;

        private void buttonOk_Click(object sender, EventArgs e)
        {
            columnsName = textBoxNameColumn.Text;
            listcategory = textBoxListCategory.Text;
            listbin = textBoxListRange.Text;
            MainForm mainForm = new MainForm();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            columnsName = string.Empty;
            listcategory = string.Empty;
            listbin = string.Empty;
            MainForm mainForm = new MainForm();
            Close();
        }
    }
}
