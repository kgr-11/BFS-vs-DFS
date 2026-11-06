using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Repositories
{
    public static class DrawLabyrinthRepository
    {

        public const int lenBorder = 20;

        //отрисовка самого элемента
        public static void DrawEl(Graphics graphic, int i, int j, Color color,
            bool IsDrawTop, bool IsDrawBottom, bool IsDrawLeft, bool IsDrawRight)
        {
            Point point = CulculationPoint(i, j);

            //ширина стенок
            float widthBorder = 4f;

            Pen penForTop = IsDrawTop ? new Pen(Color.Black, widthBorder) : new Pen(color, widthBorder);
            Pen penForBottom = IsDrawBottom ? new Pen(Color.Black, widthBorder) : new Pen(color, widthBorder);
            Pen penForLeft = IsDrawLeft ? new Pen(Color.Black, widthBorder) : new Pen(color, widthBorder);
            Pen penForRight = IsDrawRight ? new Pen(Color.Black, widthBorder) : new Pen(color, widthBorder);

            //заливаем получившийся квадрат
            Brush brush = new SolidBrush(color);
            graphic.FillRectangle(brush, new Rectangle(point, new Size(lenBorder, lenBorder)));

            // рисуем границы клетки
            //рисуем вверх
            graphic.DrawLine(penForTop, point, new Point(point.X + lenBorder, point.Y));
            //рисуем левую
            graphic.DrawLine(penForLeft, point, new Point(point.X, point.Y + lenBorder));
            //рисуем правую
            graphic.DrawLine(penForRight, new Point(point.X+lenBorder,point.Y), new Point(point.X + lenBorder, point.Y+lenBorder));
            //рисуем нижнюю
            graphic.DrawLine(penForBottom, new Point(point.X, point.Y + lenBorder), new Point(point.X + lenBorder, point.Y + lenBorder));
            
            //освобождаем ресурсы
            penForTop.Dispose();
            penForBottom.Dispose();
            penForLeft.Dispose();
            penForRight.Dispose();
            brush.Dispose();
        }


        //функция расчета координат по пережанным индексам элемента
        private static Point CulculationPoint(int i, int j)
        {
            // j- координата x
            // i- координата y
            return new Point(j * lenBorder, i * lenBorder);
        }
        
    }
}
