using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Additional_Structures;
namespace Labyrinth.Repositories
{
    public static class DrawRepositoryForScale
    {

        public static Bitmap DrawLabyrinthInBitmap(Node[,] labyrinth, int widthControll, int heightControll,
            Dictionary<sbyte,SolidBrush>Palette,int borderLen,float step, int cellOffsetX, int cellOffsetY)
        {
            Bitmap map =new Bitmap(widthControll, heightControll, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(map);
            graphics.Clear(Color.Silver);
            int N = labyrinth.GetUpperBound(0)+1;
            int M=labyrinth.GetUpperBound(1)+1;
            bool PxIsGreen = false;
            bool PxIsRed = false;
            bool pxIsWhite = false;
            //идем по строкам пикселей
            for (int i = 0; i < heightControll; i++)
            {
                //идем по пикселям в строке
                for (int j = 0; j < widthControll; j++)
                {
                    //принимаем решение в какой цвет закрашивать пиксель
                    PxIsGreen = false;
                    PxIsRed = false;
                    pxIsWhite = false;
                    int countRed = 0;
                    //int x_start = cellOffsetX+j*countRowAndColInPx;
                    int x_start = (int)(cellOffsetX + j * step);
                    if (x_start<0)
                        x_start = 0;
                    //int y_start = cellOffsetY+i*countRowAndColInPx;
                    int y_start = (int)(cellOffsetY + i * step);
                    if (y_start<0)
                        y_start = 0;
                    //int x_finish = x_start + countRowAndColInPx - 1;
                    int x_finish = (int)(cellOffsetX + (j+1) * step)-1;
                    if (x_finish >= M)
                        x_finish = M - 1;
                    //int y_finish = y_start + countRowAndColInPx - 1;
                    int y_finish = (int)(cellOffsetY + (i+1) * step) - 1;
                    if (y_finish >= N)
                        y_finish = N - 1;

                    //здесь цикл
                    for (int y = y_start; y <= y_finish; y++)//это строка
                    {
                        for (int x = x_start; x <= x_finish; x++)//это столбец
                        {
                            //если наткнулись на зеленый
                            if (labyrinth[y, x].val == 1)
                            {
                                PxIsGreen = true;
                                goto Metka;
                            }
                            else if (labyrinth[y, x].val == -1)
                            {
                                PxIsRed = true;
                                countRed++;
                            }
                            else if (labyrinth[y, x].val == 0)
                            { 
                                pxIsWhite = true;
                            }
                        }
                    }
                Metka:
                    //здесь принятие решения
                    //если есть зеленый - то в первую очередь в зеленый
                    if (PxIsGreen)
                    {
                        graphics.FillRectangle(Palette[1], j,i,1,1);
                    }
                //приоритетный цвет
                    /*else if (PxIsRed)//иначе в красный
                    {
                        graphics.FillRectangle(Palette[-1], j, i, 1, 1);
                    }
                    else if(pxIsWhite) //нет ни красного, ни зеленого в белый
                    {
                        graphics.FillRectangle(Palette[0], j, i, 1, 1);
                    }*/
                //наибольшее кол-во раз встречающийся 
                    else if(countRed>(int)(step*step)/2-1)
                        graphics.FillRectangle(Palette[-1], j, i, 1, 1);
                    else
                        graphics.FillRectangle(Palette[0], j, i, 1, 1);
                }
            }
            graphics.Dispose();
            return map;
        }
    }
}
