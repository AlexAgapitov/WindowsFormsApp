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
    public partial class GetColumns : Form
    {
        public GetColumns()
        {
            InitializeComponent();
        }

        public string param1 = string.Empty;
        public string param2 = string.Empty;
        public string param3 = string.Empty;
        public string property = string.Empty;

        private void buttonOk_Click(object sender, EventArgs e)
        {
            param1 = textBoxParam1.Text;
            param2 = textBoxParam2.Text;
            param3 = textBoxParam3.Text;
            property = textBoxProrerty.Text;
            MainForm mainForm = new MainForm();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            Close();
        }
    }
}
