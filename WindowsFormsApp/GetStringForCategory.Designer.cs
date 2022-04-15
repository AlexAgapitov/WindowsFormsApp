
namespace WindowsFormsApp
{
    partial class GetStringForCategory
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNameColumn = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxListCategory = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxListRange = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите название колонки.";
            // 
            // textBoxNameColumn
            // 
            this.textBoxNameColumn.Location = new System.Drawing.Point(12, 25);
            this.textBoxNameColumn.Name = "textBoxNameColumn";
            this.textBoxNameColumn.Size = new System.Drawing.Size(360, 20);
            this.textBoxNameColumn.TabIndex = 1;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(297, 128);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ок";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Введите название категорий через запятую.";
            // 
            // textBoxListCategory
            // 
            this.textBoxListCategory.Location = new System.Drawing.Point(12, 64);
            this.textBoxListCategory.Name = "textBoxListCategory";
            this.textBoxListCategory.Size = new System.Drawing.Size(360, 20);
            this.textBoxListCategory.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(216, 128);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxListRange
            // 
            this.textBoxListRange.Location = new System.Drawing.Point(12, 103);
            this.textBoxListRange.Name = "textBoxListRange";
            this.textBoxListRange.Size = new System.Drawing.Size(360, 20);
            this.textBoxListRange.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Введите диапазон категорий через запятую.";
            // 
            // GetStringForCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.textBoxListRange);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxListCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxNameColumn);
            this.Controls.Add(this.label1);
            this.Name = "GetStringForCategory";
            this.Text = "GetStringForCategory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxNameColumn;
        public System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxListCategory;
        public System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBoxListRange;
        private System.Windows.Forms.Label label3;
    }
}