namespace Labyrinth
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBox2 = new GroupBox();
            comboBox_method = new ComboBox();
            label3 = new Label();
            numericUpDown_M = new NumericUpDown();
            label2 = new Label();
            numericUpDown_N = new NumericUpDown();
            label1 = new Label();
            groupBox3 = new GroupBox();
            button_reset = new Button();
            button_search = new Button();
            button_generate = new Button();
            groupBox4 = new GroupBox();
            textBox_count = new TextBox();
            groupBox5 = new GroupBox();
            radioButton_All_Labyrinth = new RadioButton();
            radioButton_sourceScale = new RadioButton();
            trackBar1 = new TrackBar();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_M).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_N).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1431, 940);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Графика";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(15, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1400, 900);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(comboBox_method);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(numericUpDown_M);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(numericUpDown_N);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(1472, 114);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(398, 136);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Параметры модели";
            // 
            // comboBox_method
            // 
            comboBox_method.FormattingEnabled = true;
            comboBox_method.Items.AddRange(new object[] { "(DFS) Обход в глубину", "(BFS) Обход в ширину" });
            comboBox_method.Location = new Point(180, 86);
            comboBox_method.Name = "comboBox_method";
            comboBox_method.Size = new Size(199, 28);
            comboBox_method.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 89);
            label3.Name = "label3";
            label3.Size = new Size(168, 20);
            label3.TabIndex = 4;
            label3.Text = "Метод поиска выхода: ";
            // 
            // numericUpDown_M
            // 
            numericUpDown_M.Location = new Point(267, 32);
            numericUpDown_M.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            numericUpDown_M.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_M.Name = "numericUpDown_M";
            numericUpDown_M.Size = new Size(112, 27);
            numericUpDown_M.TabIndex = 3;
            numericUpDown_M.TextAlign = HorizontalAlignment.Center;
            numericUpDown_M.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDown_M.ValueChanged += numericUpDown_M_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(231, 34);
            label2.Name = "label2";
            label2.Size = new Size(30, 20);
            label2.TabIndex = 2;
            label2.Text = "✖";
            // 
            // numericUpDown_N
            // 
            numericUpDown_N.Location = new Point(113, 32);
            numericUpDown_N.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            numericUpDown_N.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_N.Name = "numericUpDown_N";
            numericUpDown_N.Size = new Size(112, 27);
            numericUpDown_N.TabIndex = 1;
            numericUpDown_N.TextAlign = HorizontalAlignment.Center;
            numericUpDown_N.UpDownAlign = LeftRightAlignment.Left;
            numericUpDown_N.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDown_N.ValueChanged += numericUpDown_N_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 34);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 0;
            label1.Text = "Размер поля:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button_reset);
            groupBox3.Controls.Add(button_search);
            groupBox3.Controls.Add(button_generate);
            groupBox3.Location = new Point(1472, 608);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(398, 333);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Элементы управления";
            // 
            // button_reset
            // 
            button_reset.Location = new Point(6, 235);
            button_reset.Name = "button_reset";
            button_reset.Size = new Size(386, 94);
            button_reset.TabIndex = 2;
            button_reset.Text = "Сброс";
            button_reset.UseVisualStyleBackColor = true;
            button_reset.Click += button_reset_Click;
            // 
            // button_search
            // 
            button_search.Location = new Point(6, 135);
            button_search.Name = "button_search";
            button_search.Size = new Size(386, 94);
            button_search.TabIndex = 1;
            button_search.Text = "Поиск выхода";
            button_search.UseVisualStyleBackColor = true;
            button_search.Click += button_search_Click;
            // 
            // button_generate
            // 
            button_generate.Location = new Point(6, 35);
            button_generate.Name = "button_generate";
            button_generate.Size = new Size(386, 94);
            button_generate.TabIndex = 0;
            button_generate.Text = "Генерация лабиринта";
            button_generate.UseVisualStyleBackColor = true;
            button_generate.Click += button_generate_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox_count);
            groupBox4.Location = new Point(1472, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(398, 96);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Счетчик шагов";
            // 
            // textBox_count
            // 
            textBox_count.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox_count.Location = new Point(104, 26);
            textBox_count.Name = "textBox_count";
            textBox_count.Size = new Size(191, 52);
            textBox_count.TabIndex = 5;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(radioButton_All_Labyrinth);
            groupBox5.Controls.Add(radioButton_sourceScale);
            groupBox5.Controls.Add(trackBar1);
            groupBox5.Location = new Point(1472, 256);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(398, 195);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "Изменение масштаба лабиринту";
            // 
            // radioButton_All_Labyrinth
            // 
            radioButton_All_Labyrinth.AutoSize = true;
            radioButton_All_Labyrinth.Font = new Font("Times New Roman", 13.8F);
            radioButton_All_Labyrinth.Location = new Point(43, 146);
            radioButton_All_Labyrinth.Name = "radioButton_All_Labyrinth";
            radioButton_All_Labyrinth.Size = new Size(175, 30);
            radioButton_All_Labyrinth.TabIndex = 7;
            radioButton_All_Labyrinth.TabStop = true;
            radioButton_All_Labyrinth.Text = "Весь лабиринт";
            radioButton_All_Labyrinth.UseVisualStyleBackColor = true;
            radioButton_All_Labyrinth.CheckedChanged += radioButton_All_Labyrinth_CheckedChanged;
            // 
            // radioButton_sourceScale
            // 
            radioButton_sourceScale.AutoSize = true;
            radioButton_sourceScale.Font = new Font("Times New Roman", 13.8F);
            radioButton_sourceScale.Location = new Point(43, 91);
            radioButton_sourceScale.Name = "radioButton_sourceScale";
            radioButton_sourceScale.Size = new Size(204, 30);
            radioButton_sourceScale.TabIndex = 6;
            radioButton_sourceScale.TabStop = true;
            radioButton_sourceScale.Text = "Исходный размер";
            radioButton_sourceScale.UseVisualStyleBackColor = true;
            radioButton_sourceScale.CheckedChanged += radioButton_sourceScale_CheckedChanged;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(43, 43);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(311, 56);
            trackBar1.TabIndex = 3;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1882, 957);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Лабиринт";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_M).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_N).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox textBox_count;
        private Label label1;
        private NumericUpDown numericUpDown_N;
        private Label label2;
        private NumericUpDown numericUpDown_M;
        private ComboBox comboBox_method;
        private Label label3;
        private Button button_generate;
        private Button button_search;
        private PictureBox pictureBox1;
        private Button button_reset;
        private GroupBox groupBox5;
        private TrackBar trackBar1;
        private RadioButton radioButton_All_Labyrinth;
        private RadioButton radioButton_sourceScale;
    }
}
