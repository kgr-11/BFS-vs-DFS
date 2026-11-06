using System.Drawing;
using System;
using System.Windows.Forms;
using Labyrinth.Repositories;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Drawing2D;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Labyrinth
{
    public partial class Form1 : Form
    {
        Service service;

        //кф-ты масштабирования
        float scale;

        // степень 10 насколько увеличили кф-т масштабирования
        // при округлении до целых
        int countDegree;
        public Form1()
        {
            InitializeComponent();


            //кнопки сброса и поиска выхода изначально заблокированы
            button_reset.Enabled = false;
            button_search.Enabled = false;

            //по умолчанию выбирается первый метод поиска
            comboBox_method.SelectedIndex = 0;

            //поле для вывода кол-во шагов делаем недоступным 
            textBox_count.Enabled = false;

            //изначальные размеры сетки
            numericUpDown_N.Value = 500;
            numericUpDown_M.Value = 800;

            //создание сервиса
            service = new Service((int)numericUpDown_N.Value, (int)numericUpDown_M.Value,
                pictureBox1.Size.Width, pictureBox1.Size.Height);

            //обработчики клавиш для движения по лабиринту
            this.KeyPreview = true;//чтобы контроллы не перехватили управление
            this.KeyDown += Form1_KeyDown;
            //установка значений для трек бара 
            SetValueForTrackBar();

            radioButton_sourceScale.Checked = true;
        }
        void SetValueForTrackBar()
        {
            //шаг изменения масштаба 
            service.GetSizeLabyrinth(out int width, out int heigth);
            float min;
            if ((float)pictureBox1.Width >= width && (float)pictureBox1.Height >= heigth)
                min = 1;
            else
                min = Math.Min((float)pictureBox1.Width / width, (float)pictureBox1.Height / heigth);
            countDegree = 0;
            while (true)
            {
                if (min < 100000)//100 -  тк у нас 2 значимх знака после запятой 
                    min *= 10;
                else
                    break;
                countDegree++;
            }
            trackBar1.Minimum = (int)min;
            trackBar1.Maximum = (int)(1*Math.Pow(10, countDegree));
            trackBar1.TickFrequency = (trackBar1.Maximum- trackBar1.Minimum)/10;
            trackBar1.Value = trackBar1.Maximum;
            trackBar1.LargeChange = (trackBar1.Maximum - trackBar1.Minimum) / 5;
            trackBar1.SmallChange = (trackBar1.Maximum - trackBar1.Minimum) / 5;
        }

        //обработчик события клавиш для движения по  лабиринту
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    {
                        //if (service.MoveLeft(pictureBox1.Size.Width, pictureBox1.Size.Height))
                        if (service.MoveLeft((int)(1.0 / scale * pictureBox1.Size.Width), (int)(1.0 / scale * pictureBox1.Size.Height)))
                            pictureBox1.Invalidate();
                        e.Handled = true;//чтобы до контролла, который в фокусе ничего не дошло
                    }
                ; break;
                case Keys.Right:
                    {
                        /*if (service.MoveRight(pictureBox1.Size.Width, pictureBox1.Size.Height))*/
                        if (service.MoveRight((int)(1.0 / scale * pictureBox1.Size.Width), (int)(1.0 / scale * pictureBox1.Size.Height)))
                            pictureBox1.Invalidate();
                        e.Handled = true;//чтобы до контролла, который в фокусе ничего не дошло
                    }
                ; break;
                case Keys.Up:
                    {
                        //if (service.MoveUp(pictureBox1.Size.Width, pictureBox1.Size.Height))
                        if (service.MoveUp((int)(1.0 / scale * pictureBox1.Size.Width), (int)(1.0 / scale * pictureBox1.Size.Height)))
                            pictureBox1.Invalidate();
                        e.Handled = true;//чтобы до контролла, который в фокусе ничего не дошло
                    }
                ; break;
                case Keys.Down:
                    {
                        //if (service.MoveDown(pictureBox1.Size.Width, pictureBox1.Size.Height))
                        if (service.MoveDown((int)(1.0 / scale * pictureBox1.Size.Width), (int)(1.0 / scale * pictureBox1.Size.Height)))
                            pictureBox1.Invalidate();
                        e.Handled = true;//чтобы до контролла, который в фокусе ничего не дошло
                    }
                ; break;

            }
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (scale > 1.0f/DrawLabyrinthRepository.lenBorder)
            {
                Graphics graphic = e.Graphics;
                graphic.Clear(Color.Silver);    
                graphic.ScaleTransform(scale, scale);
                //рисуем эл-ты в форме
                service.DrawLabyrinth(graphic);
            }
            else 
            {
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                using (SolidBrush redBrush = new SolidBrush(Color.Red))
                using (SolidBrush greenBrush = new SolidBrush(Color.Green))
                {
                    Dictionary<sbyte, SolidBrush> Palette = new Dictionary<sbyte, SolidBrush>();
                    Palette[0] = whiteBrush;
                    Palette[1] = greenBrush;
                    Palette[-1] = redBrush;
                    //int countRowAndColInPx = Math.Max(1, (int)Math.Ceiling(1.0 / (DrawLabyrinthRepository.lenBorder * scale)));
                    float stepX = (float)service.GetVisibleCountCol() / pictureBox1.Width;
                    float stepY = (float)service.GetVisibleCountRow() / pictureBox1.Height;
                    float step = Math.Max(stepX, stepY);
                    Bitmap map = service.DrawLabyrinth(pictureBox1.ClientSize.Width,
                        pictureBox1.ClientSize.Height, Palette,step, 0, 0);
                    e.Graphics.Clear(Color.Silver);
                    e.Graphics.DrawImageUnscaled(map, 0, 0);
                    map.Dispose();
                }
            }
        }


        private void button_generate_Click(object sender, EventArgs e)
        {
            button_generate.Enabled = false;
            button_search.Enabled = true; ;
            numericUpDown_M.Enabled = false;
            numericUpDown_N.Enabled = false;
            button_reset.Enabled = true;
            textBox_count.Text = service.GenerateLabyrinth().ToString();
            pictureBox1.Invalidate();
            //из-за этой кнопки, клавиши перестают регировать
            this.ActiveControl = null;//снять фокус с кнопки
            this.Focus();//возвращаем фокус форме
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            //сбросываю лабиринт в таблицу
            service.ResetLabyrinth((int)numericUpDown_N.Value, (int)numericUpDown_M.Value,
                pictureBox1.Size.Width, pictureBox1.Size.Height);

            //сбрасываем кф-ты масшабирования 
            trackBar1.Value = trackBar1.Maximum;
            trackBar1_Scroll(null, EventArgs.Empty);
            radioButton_All_Labyrinth.Checked = false;
            radioButton_sourceScale.Checked = true;

            pictureBox1.Invalidate();

            button_generate.Enabled = true;
            numericUpDown_M.Enabled = true;
            numericUpDown_N.Enabled = true;
            button_reset.Enabled = false;
            textBox_count.Text = "";
            button_search.Enabled = true;

            //из-за этой кнопки, клавиши перестают регировать
            this.ActiveControl = null;//снять фокус с кнопки
            this.Focus();//возвращаем фокус форме
        }

        private void numericUpDown_N_ValueChanged(object sender, EventArgs e)
        {
            if (service is not null)
            {
                //сброс лабиринта с новым размером 
                service.ResetLabyrinth((int)numericUpDown_N.Value, (int)numericUpDown_M.Value,
                    pictureBox1.Size.Width, pictureBox1.Size.Height);
               
                //переустановка значений трекбара, тк размеры изменились
                SetValueForTrackBar();
                trackBar1_Scroll(null, EventArgs.Empty);
                pictureBox1.Invalidate();
                this.ActiveControl = null;//снять фокус с кнопки
                this.Focus();//возвращаем фокус форме
            }
        }

        private void numericUpDown_M_ValueChanged(object sender, EventArgs e)
        {
            if (service is not null)
            {
                service.ResetLabyrinth((int)numericUpDown_N.Value, (int)numericUpDown_M.Value,
                    pictureBox1.Size.Width, pictureBox1.Size.Height);
                
                //переустановка значений трекбара, тк размеры изменились
                SetValueForTrackBar();
                trackBar1_Scroll(null, EventArgs.Empty);
                pictureBox1.Invalidate();
                this.ActiveControl = null;//снять фокус с кнопки
                this.Focus();//возвращаем фокус форме
            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            button_reset.Enabled = false;
            if (comboBox_method.SelectedIndex == 0)
                textBox_count.Text = Convert.ToString(service.DFS());
            else
                textBox_count.Text = Convert.ToString(service.BFS());
            pictureBox1.Invalidate();
            button_reset.Enabled = true;
            button_search.Enabled = false;
            //из-за этой кнопки, клавиши перестают регировать
            this.ActiveControl = null;//снять фокус с кнопки
            this.Focus();//возвращаем фокус форме
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == trackBar1.Maximum)
                radioButton_sourceScale.Checked = true;
            else if (trackBar1.Value == trackBar1.Minimum)
                radioButton_All_Labyrinth.Checked = true;
            else
            {
                radioButton_All_Labyrinth.Checked = false;
                radioButton_sourceScale.Checked = false;
            }
            
            //получаем значение
            int value = trackBar1.Value;

            
            //находим масштабный кф-n
            scale = (float)value / (float)Math.Pow(10, countDegree);
            //изменяем значения индексов выводих элементов
            service.SetNewIndexForImage((int)(1.0 / scale * pictureBox1.Size.Width), (int)(1.0 / scale * pictureBox1.Size.Height));
            //перерисовываем в нужном масштабе
            pictureBox1.Invalidate();
            //из-за этой кнопки, клавиши перестают регировать
            this.ActiveControl = null;//снять фокус с кнопки
            this.Focus();//возвращаем фокус форме
        }

        private void radioButton_sourceScale_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_sourceScale.Checked)
            {
                trackBar1.Value = trackBar1.Maximum;
                trackBar1_Scroll(null, EventArgs.Empty);
                //из-за этой кнопки, клавиши перестают регировать
                this.ActiveControl = null;//снять фокус с кнопки
                this.Focus();//возвращаем фокус форме
            }

        }

        private void radioButton_All_Labyrinth_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_All_Labyrinth.Checked)
            {
                trackBar1.Value = trackBar1.Minimum;
                trackBar1_Scroll(null, EventArgs.Empty);
                //из-за этой кнопки, клавиши перестают регировать
                this.ActiveControl = null;//снять фокус с кнопки
                this.Focus();//возвращаем фокус форме
            }
        }

    }
}
