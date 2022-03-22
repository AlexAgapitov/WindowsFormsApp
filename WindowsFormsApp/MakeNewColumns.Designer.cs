namespace WindowsFormsApp
{
    partial class MakeNewColumns
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddNewColumnBtn = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.textBoxNameColumns = new System.Windows.Forms.TextBox();
            this.labelOr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddNewColumnBtn
            // 
            this.AddNewColumnBtn.Location = new System.Drawing.Point(247, 56);
            this.AddNewColumnBtn.Name = "AddNewColumnBtn";
            this.AddNewColumnBtn.Size = new System.Drawing.Size(75, 23);
            this.AddNewColumnBtn.TabIndex = 0;
            this.AddNewColumnBtn.Text = "Ок";
            this.AddNewColumnBtn.UseVisualStyleBackColor = true;
            this.AddNewColumnBtn.Click += new System.EventHandler(this.AddNewColumnBtn_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(166, 56);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // textBoxNameColumns
            // 
            this.textBoxNameColumns.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxNameColumns.Location = new System.Drawing.Point(12, 27);
            this.textBoxNameColumns.Name = "textBoxNameColumns";
            this.textBoxNameColumns.Size = new System.Drawing.Size(310, 20);
            this.textBoxNameColumns.TabIndex = 2;
            // 
            // labelOr
            // 
            this.labelOr.AutoSize = true;
            this.labelOr.Location = new System.Drawing.Point(12, 9);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(35, 13);
            this.labelOr.TabIndex = 3;
            this.labelOr.Text = "label1";
            // 
            // MakeNewColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 91);
            this.Controls.Add(this.labelOr);
            this.Controls.Add(this.textBoxNameColumns);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.AddNewColumnBtn);
            this.Name = "MakeNewColumns";
            this.Text = "Создание нового столбца";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.Button AddNewColumnBtn;
        private System.Windows.Forms.Button Cancel;
        public System.Windows.Forms.TextBox textBoxNameColumns;
        public System.Windows.Forms.Label labelOr;
    }
}