namespace WindowsFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsXLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NormalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MeanResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StringResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResponseUnitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.radioButtonOther = new System.Windows.Forms.RadioButton();
            this.radioButtonCemicolon = new System.Windows.Forms.RadioButton();
            this.radioButtonSpace = new System.Windows.Forms.RadioButton();
            this.radioButtonComma = new System.Windows.Forms.RadioButton();
            this.radioButtonTabuletion = new System.Windows.Forms.RadioButton();
            this.checkBoxSeparator = new System.Windows.Forms.CheckBox();
            this.textBoxOtherSeparator = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 133);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(560, 266);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ToolsStripMenuItem,
            this.данныеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.OpenToolStripMenuItem.Text = "Открыть";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveAsCSVToolStripMenuItem,
            this.SaveAsXLSToolStripMenuItem});
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.SaveAsToolStripMenuItem.Text = "Сохранить как";
            // 
            // SaveAsCSVToolStripMenuItem
            // 
            this.SaveAsCSVToolStripMenuItem.Name = "SaveAsCSVToolStripMenuItem";
            this.SaveAsCSVToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.SaveAsCSVToolStripMenuItem.Text = "Файл CSV";
            this.SaveAsCSVToolStripMenuItem.Click += new System.EventHandler(this.FileCSVToolStripMenuItem_Click);
            // 
            // SaveAsXLSToolStripMenuItem
            // 
            this.SaveAsXLSToolStripMenuItem.Name = "SaveAsXLSToolStripMenuItem";
            this.SaveAsXLSToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.SaveAsXLSToolStripMenuItem.Text = "Файл XLS";
            this.SaveAsXLSToolStripMenuItem.Click += new System.EventHandler(this.SaveAsXLSToolStripMenuItem_Click);
            // 
            // ToolsStripMenuItem
            // 
            this.ToolsStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddLineToolStripMenuItem,
            this.AddColumnsToolStripMenuItem,
            this.DeleteLineToolStripMenuItem,
            this.DeleteColumnToolStripMenuItem});
            this.ToolsStripMenuItem.Name = "ToolsStripMenuItem";
            this.ToolsStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.ToolsStripMenuItem.Text = "Редактор";
            // 
            // AddLineToolStripMenuItem
            // 
            this.AddLineToolStripMenuItem.Name = "AddLineToolStripMenuItem";
            this.AddLineToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.AddLineToolStripMenuItem.Text = "Добавить строку";
            this.AddLineToolStripMenuItem.Click += new System.EventHandler(this.AddLineToolStripMenuItem_Click);
            // 
            // AddColumnsToolStripMenuItem
            // 
            this.AddColumnsToolStripMenuItem.Name = "AddColumnsToolStripMenuItem";
            this.AddColumnsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.AddColumnsToolStripMenuItem.Text = "Добавить столбец";
            this.AddColumnsToolStripMenuItem.Click += new System.EventHandler(this.AddColumnToolStripMenuItem_Click);
            // 
            // DeleteLineToolStripMenuItem
            // 
            this.DeleteLineToolStripMenuItem.Name = "DeleteLineToolStripMenuItem";
            this.DeleteLineToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DeleteLineToolStripMenuItem.Text = "Удалить строку";
            this.DeleteLineToolStripMenuItem.Click += new System.EventHandler(this.DeleteLineToolStripMenuItem_Click);
            // 
            // DeleteColumnToolStripMenuItem
            // 
            this.DeleteColumnToolStripMenuItem.Name = "DeleteColumnToolStripMenuItem";
            this.DeleteColumnToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.DeleteColumnToolStripMenuItem.Text = "Удалить стоблец";
            this.DeleteColumnToolStripMenuItem.Click += new System.EventHandler(this.DeleteColumnToolStripMenuItem_Click);
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NormalizeToolStripMenuItem,
            this.ResponseToolStripMenuItem,
            this.ResponseUnitToolStripMenuItem,
            this.CategoryToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.данныеToolStripMenuItem.Text = "Данные";
            // 
            // NormalizeToolStripMenuItem
            // 
            this.NormalizeToolStripMenuItem.Name = "NormalizeToolStripMenuItem";
            this.NormalizeToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.NormalizeToolStripMenuItem.Text = "Нормализация";
            this.NormalizeToolStripMenuItem.Click += new System.EventHandler(this.NormalizeToolStripMenuItem_Click_1);
            // 
            // ResponseToolStripMenuItem
            // 
            this.ResponseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MeanResponseToolStripMenuItem,
            this.StringResponseToolStripMenuItem,
            this.LineResponseToolStripMenuItem});
            this.ResponseToolStripMenuItem.Name = "ResponseToolStripMenuItem";
            this.ResponseToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.ResponseToolStripMenuItem.Text = "Восстановление";
            // 
            // MeanResponseToolStripMenuItem
            // 
            this.MeanResponseToolStripMenuItem.Name = "MeanResponseToolStripMenuItem";
            this.MeanResponseToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.MeanResponseToolStripMenuItem.Text = "По среднему значению";
            this.MeanResponseToolStripMenuItem.Click += new System.EventHandler(this.MeanResponseToolStripMenuItem_Click);
            // 
            // StringResponseToolStripMenuItem
            // 
            this.StringResponseToolStripMenuItem.Name = "StringResponseToolStripMenuItem";
            this.StringResponseToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.StringResponseToolStripMenuItem.Text = "По строкам";
            this.StringResponseToolStripMenuItem.Click += new System.EventHandler(this.StringResponseToolStripMenuItem_Click);
            // 
            // LineResponseToolStripMenuItem
            // 
            this.LineResponseToolStripMenuItem.Name = "LineResponseToolStripMenuItem";
            this.LineResponseToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.LineResponseToolStripMenuItem.Text = "Линейная интерполяция";
            this.LineResponseToolStripMenuItem.Click += new System.EventHandler(this.LineResponseToolStripMenuItem_Click);
            // 
            // ResponseUnitToolStripMenuItem
            // 
            this.ResponseUnitToolStripMenuItem.Name = "ResponseUnitToolStripMenuItem";
            this.ResponseUnitToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.ResponseUnitToolStripMenuItem.Text = "Приведение к одной ед.изм.";
            this.ResponseUnitToolStripMenuItem.Click += new System.EventHandler(this.ResponseUnitToolStripMenuItem_Click);
            // 
            // CategoryToolStripMenuItem
            // 
            this.CategoryToolStripMenuItem.Name = "CategoryToolStripMenuItem";
            this.CategoryToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.CategoryToolStripMenuItem.Text = "Приведение к категориям";
            this.CategoryToolStripMenuItem.Click += new System.EventHandler(this.CategoryToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // radioButtonOther
            // 
            this.radioButtonOther.Location = new System.Drawing.Point(221, 40);
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.Size = new System.Drawing.Size(64, 24);
            this.radioButtonOther.TabIndex = 11;
            this.radioButtonOther.Text = "Другой";
            // 
            // radioButtonCemicolon
            // 
            this.radioButtonCemicolon.AutoSize = true;
            this.radioButtonCemicolon.Location = new System.Drawing.Point(104, 44);
            this.radioButtonCemicolon.Name = "radioButtonCemicolon";
            this.radioButtonCemicolon.Size = new System.Drawing.Size(108, 17);
            this.radioButtonCemicolon.TabIndex = 6;
            this.radioButtonCemicolon.TabStop = true;
            this.radioButtonCemicolon.Text = "Точка с запятой";
            this.radioButtonCemicolon.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpace
            // 
            this.radioButtonSpace.AutoSize = true;
            this.radioButtonSpace.Location = new System.Drawing.Point(13, 67);
            this.radioButtonSpace.Name = "radioButtonSpace";
            this.radioButtonSpace.Size = new System.Drawing.Size(63, 17);
            this.radioButtonSpace.TabIndex = 8;
            this.radioButtonSpace.TabStop = true;
            this.radioButtonSpace.Text = "Пробел";
            this.radioButtonSpace.UseVisualStyleBackColor = true;
            // 
            // radioButtonComma
            // 
            this.radioButtonComma.AutoSize = true;
            this.radioButtonComma.Location = new System.Drawing.Point(13, 44);
            this.radioButtonComma.Name = "radioButtonComma";
            this.radioButtonComma.Size = new System.Drawing.Size(67, 17);
            this.radioButtonComma.TabIndex = 5;
            this.radioButtonComma.TabStop = true;
            this.radioButtonComma.Text = "Запятая";
            this.radioButtonComma.UseVisualStyleBackColor = true;
            // 
            // radioButtonTabuletion
            // 
            this.radioButtonTabuletion.AutoSize = true;
            this.radioButtonTabuletion.Location = new System.Drawing.Point(104, 67);
            this.radioButtonTabuletion.Name = "radioButtonTabuletion";
            this.radioButtonTabuletion.Size = new System.Drawing.Size(79, 17);
            this.radioButtonTabuletion.TabIndex = 9;
            this.radioButtonTabuletion.TabStop = true;
            this.radioButtonTabuletion.Text = "Табуляция";
            this.radioButtonTabuletion.UseVisualStyleBackColor = true;
            // 
            // checkBoxSeparator
            // 
            this.checkBoxSeparator.AutoSize = true;
            this.checkBoxSeparator.Location = new System.Drawing.Point(6, 18);
            this.checkBoxSeparator.Name = "checkBoxSeparator";
            this.checkBoxSeparator.Size = new System.Drawing.Size(92, 17);
            this.checkBoxSeparator.TabIndex = 4;
            this.checkBoxSeparator.Text = "Разделитель";
            this.checkBoxSeparator.UseVisualStyleBackColor = true;
            this.checkBoxSeparator.CheckedChanged += new System.EventHandler(this.checkBoxSeparator_CheckedChanged);
            // 
            // textBoxOtherSeparator
            // 
            this.textBoxOtherSeparator.Location = new System.Drawing.Point(291, 43);
            this.textBoxOtherSeparator.Name = "textBoxOtherSeparator";
            this.textBoxOtherSeparator.Size = new System.Drawing.Size(49, 20);
            this.textBoxOtherSeparator.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxSeparator);
            this.groupBox1.Controls.Add(this.radioButtonCemicolon);
            this.groupBox1.Controls.Add(this.textBoxOtherSeparator);
            this.groupBox1.Controls.Add(this.radioButtonComma);
            this.groupBox1.Controls.Add(this.radioButtonOther);
            this.groupBox1.Controls.Add(this.radioButtonTabuletion);
            this.groupBox1.Controls.Add(this.radioButtonSpace);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 100);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Разделитель";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridView1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Репозиторий БД ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsCSVToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem ToolsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteColumnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsXLSToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonOther;
        private System.Windows.Forms.RadioButton radioButtonCemicolon;
        private System.Windows.Forms.RadioButton radioButtonSpace;
        private System.Windows.Forms.RadioButton radioButtonComma;
        private System.Windows.Forms.RadioButton radioButtonTabuletion;
        private System.Windows.Forms.CheckBox checkBoxSeparator;
        private System.Windows.Forms.TextBox textBoxOtherSeparator;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NormalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResponseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResponseUnitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MeanResponseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StringResponseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineResponseToolStripMenuItem;
    }
}

