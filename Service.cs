using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Additional_Structures;
using Labyrinth.Repositories;

namespace Labyrinth
{
    //  Сервис управляющий работой всех репозиториев

    internal class Service
    {
        //сервис все знает, о лабиринте 
        //и рассказывает другим о нем

        //кол-во строк в лабиринте 
        public int N;
        //кол-во столбцов в лабиринте
        public int M;

        //сам лабиринт
        Node[,] Labyrinth;
        //стартовый узел
        Node Start;
        //финишный узел
        Node Finish;

        //индексы отображаемого окна
        int i_window_start;
        int j_window_start;
        int i_window_finish;
        int j_window_finish;
        //центральный индекс относительно которого происходит масштабирование
        int i_window_center;
        int j_window_center;

        //на сколько процентов сдвигаем окно в движении с помощью клавиш
        int percentageShiftWindow = 50;


        public Service(int N, int M, int widthControll, int heigthControll)
        {
            this.N = N;
            this.M = M;
            Labyrinth = new Node[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Labyrinth[i, j] = new Node();
                }
            }
            //Пока старт и финиш генерируем сами
            //Дальше можно сделать, чтобы выбиралось рандомно

            //начинаем из левого верхнего
            Start = new Node();
            Labyrinth[0, 0] = Start;
            //заканчиваем в правом нижнем
            Finish = new Node();
            Labyrinth[N - 1, M - 1] = Finish;

            //инициализируем отображаемые индексы
            i_window_start = 0;
            j_window_start = 0;
            i_window_finish = heigthControll / DrawLabyrinthRepository.lenBorder + 1;
            if (i_window_finish >= N)
                i_window_finish = N - 1;
            j_window_finish = widthControll / DrawLabyrinthRepository.lenBorder + 1;
            if (j_window_finish >= M)
                j_window_finish = M - 1;

            j_window_center = (j_window_finish - j_window_start) / 2;
            i_window_center = (i_window_finish - i_window_start) / 2;
        }

        public int GetVisibleCountRow()
        {
            return i_window_finish - i_window_start+1;
        }
        public int GetVisibleCountCol()
        {
            return j_window_finish - j_window_start+1;
        }
        public void GetVisibleSizeLabyrinth(out int widthVisible, out int heigthVisible)
        {
            widthVisible = (j_window_finish-j_window_start+1)*DrawLabyrinthRepository.lenBorder;
            heigthVisible = (i_window_finish - i_window_start+1) * DrawLabyrinthRepository.lenBorder;
        }
        public void GetSizeLabyrinth(out int width, out int heigth)
        {
            width = M * DrawLabyrinthRepository.lenBorder;
            heigth = N * DrawLabyrinthRepository.lenBorder;
        }
        //новые индексы для отображения при масштабировании
        public void SetNewIndexForImage(int widthControll, int heigthControll)
        {
            //надо чтобы центр уже показанного изображения особо не спещался, просто уменьшаем на месте
            // + проверки
            //считаем сколько элементов поместится в новое окно

            //кол-во отображаемых строк
            int n = heigthControll % DrawLabyrinthRepository.lenBorder == 0 ?
                heigthControll / DrawLabyrinthRepository.lenBorder : heigthControll / DrawLabyrinthRepository.lenBorder + 1;
            //кол-во отображаемых столбцов
            int m = widthControll % DrawLabyrinthRepository.lenBorder == 0 ?
                widthControll / DrawLabyrinthRepository.lenBorder : widthControll / DrawLabyrinthRepository.lenBorder + 1;
            //сдвигаем текущие индексы
            i_window_start = 0;
            i_window_finish += n;
            if (i_window_finish >= N)
                i_window_finish = N - 1;

            j_window_start = 0;
            j_window_finish += m;
            if (j_window_finish >= M)
                j_window_finish = M - 1;

            
        }

        

        //функция отвечающая за рисование лабиринта на видимой области
        public void DrawLabyrinth(Graphics graphic)
        {
            //рисуем видимую сетку
            //лабиринт может оказаться больше, рисуем только видимую область 
            for (int i = i_window_start; i <=i_window_finish; i++)
            {
                for (int j = j_window_start; j <= j_window_finish; j++)
                {
                   
                    bool top = Labyrinth[i, j].top is null ?true :false;
                    bool bottom = Labyrinth[i, j].bottom is null ? true : false;
                    bool left = Labyrinth[i, j].left is null ? true : false;
                    bool right = Labyrinth[i, j].right is null ? true : false;

                    if (i == 0 && j == 0)
                        left= false;
                    if(i == N - 1 && j == M - 1)
                        right= false;

                    //закрашиваем
                    Color color;
                    if (Labyrinth[i,j].val==-1)
                        color = Color.Red;
                    else if(Labyrinth[i, j].val == 1)
                        color = Color.Green;
                    else
                        color = Color.White;

                    DrawLabyrinthRepository.DrawEl(graphic, i-i_window_start, j-j_window_start, color, top, bottom, left, right);
                }
            }
        }
        public Bitmap DrawLabyrinth(int widthControll, int heightControll, Dictionary<sbyte, SolidBrush> Palette, float step, int cellOffsetX, int cellOffsetY)
        {
            return  DrawRepositoryForScale.DrawLabyrinthInBitmap(Labyrinth, widthControll,heightControll, Palette,
                DrawLabyrinthRepository.lenBorder, step, cellOffsetX, cellOffsetY);
        }

        //функция отвечающая за рисование лабиринта на карте
        //возвращает кол-во посещенных индексов
        public int GenerateLabyrinth()
        { 
            return MathRepository.GenerateLabirinth(Labyrinth);
        }


        //сброс всего лабиринта 
        //если происходит сброс, то изображение возвращается в оригинальный масштаб
        //и отрисовывается с верхнегот левого угла
        public void ResetLabyrinth(int N, int M,int widthControll, int heigthControll)
        {
            this.N= N;
            this.M= M;
            Labyrinth = new Node[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Labyrinth[i, j] = new Node();
                }
            }

            //начинаем из левого верхнего
            Start = new Node();
            Labyrinth[0, 0] = Start;
            //заканчиваем в правом нижнем
            Finish = new Node();
            Labyrinth[N - 1, M - 1] = Finish;

            //перезаписываю отображаемые индексы
            i_window_start = 0;
            j_window_start = 0;
            i_window_finish = heigthControll / DrawLabyrinthRepository.lenBorder + 1;
            if (i_window_finish >= N)
                i_window_finish = N - 1;
            j_window_finish = widthControll / DrawLabyrinthRepository.lenBorder + 1;
            if (j_window_finish >= M)
                j_window_finish = M - 1;
        }

        //обход лабиринта в глубину с возвратом кол-ва посещенных клеток
        public int DFS()
        { 
            return MathRepository.DFS(Labyrinth);
        }

        //обход лабиринта в ширину с возвратом кол-ва посещенных клеток
        public int BFS()
        {
            return MathRepository.BFS(Labyrinth);
        }



        //функции движения для клавиш
        public bool MoveLeft(int widthControll,int heightControll)
        {
            return ShiftWindow(widthControll, heightControll, Enum_Orientation.Left);
        }
        public bool MoveRight(int widthControll, int heightControll)
        {
            
            return ShiftWindow(widthControll, heightControll,Enum_Orientation.Right);
        }
        public bool MoveUp(int widthControll, int heightControll)
        {
            return ShiftWindow(widthControll, heightControll, Enum_Orientation.Top);
        }
        public bool MoveDown(int widthControll, int heightControll)
        {
            return ShiftWindow(widthControll, heightControll, Enum_Orientation.Bottom);
        }
        


        //функция скользящего окна вывода
        //Изменяет две пару индексов: Start(i,j) - начиная с какого эл-та выводи
        // Finish(i,j)- до какого индекса включительно выводим
        //принимает размеры выводимой области и направление окна
        private bool ShiftWindow(int widthControll, int heigthControll, Enum_Orientation orientation)
        {
            int lenBorder = DrawLabyrinthRepository.lenBorder;
            //проверяем, нужно ли вообще двигать окно
            //возможно окно большего размера, чем сам лабиринт
            if (N < (float)heigthControll / lenBorder && M < (float)widthControll / lenBorder)
                return false;//тогда говорим, что окно можно не двигать
            else if ((orientation == Enum_Orientation.Left || orientation == Enum_Orientation.Right)
                && M < (float)widthControll / lenBorder)//если сдвигаем индекс,а ячеек меньше чем может быть на диспеее, то тоже не сдвигаем
                return false;
            else if ((orientation == Enum_Orientation.Top || orientation == Enum_Orientation.Bottom)
                && N < (float)heigthControll / lenBorder)
                return false;

            //сколько ячеек помещается строку
            int countColOnDiplay = (widthControll / lenBorder); //чтобы выводилось чуть больше чем есть
            int countRowOnDisplay = (heigthControll / lenBorder); //чтобы выводилось чуть больше чем есть

            //int countColOnDiplay = j_window_finish+1-j_window_start; //чтобы выводилось чуть больше чем есть
            //int countRowOnDisplay = i_window_finish+1-i_window_start; //чтобы выводилось чуть больше чем есть

            //насколько мы сдвигаемся
            int ShiftOnCol = (int)(countColOnDiplay * ((double)percentageShiftWindow / 100.0));
            int ShiftOnRow= (int)(countRowOnDisplay * ((double)percentageShiftWindow / 100.0));

            //двигаем индексы по заданному направлению
            if (orientation == Enum_Orientation.Right)
            {
                //проверки, чтобы не выйти за пределы
                if (j_window_finish + ShiftOnCol >= M)
                {
                    j_window_finish = M - 1;
                    j_window_start = j_window_finish - countColOnDiplay + 2;
                    // +2, +1 оказалось малочтобы был небольшой отспут от края контролла
                }
                else
                {
                    j_window_finish += ShiftOnCol;
                    //j_window_start = j_window_finish - countColOnDiplay - 1;
                    j_window_start = j_window_finish - countColOnDiplay ;
                    // -1 один чтобы сетка как будто продолжалась 
                }


            }
            else if (orientation == Enum_Orientation.Left)
            {
                if (j_window_start - ShiftOnCol < 0)//стартовый индекс не должен стать отрицательным
                    j_window_start = 0;
                else
                    j_window_start -= ShiftOnCol;
                j_window_finish = j_window_start+ countColOnDiplay + 1;//финишный индекс зависит от стартого
                // +1 один чтобы сетка как будто продолжалась 
            }
            else if (orientation == Enum_Orientation.Top)
            {
                //проверки, чтобы не выйти за пределы
                if(i_window_start- ShiftOnRow < 0)//стартовый индекс не должен стать отрицательным
                    i_window_start = 0;
                else 
                    i_window_start -= ShiftOnRow;
                i_window_finish= i_window_start+ countRowOnDisplay + 1;//финишный индекс зависит от стартого
                //+1 чтобы лабиринт не обрывался, ведь снизу еще что-то есть
            }
            else 
            {
                if (i_window_finish + ShiftOnRow >= N)//финишный индекс не должен стать по индекс больше последнего эл-та
                {
                    i_window_finish = N - 1;
                    i_window_start = i_window_finish - countRowOnDisplay + 1;//стартовый зависит от финишного
                    //чтобы последняя ячейка отобразилась +1 
                    //чтобы был небольшой отспут от края контролла
                }
                else
                {
                    i_window_finish += ShiftOnRow;
                    i_window_start = i_window_finish - countRowOnDisplay - 1;//стартовый зависит от финишного
                    //-1 чтобы сетка не обрывалась, ведь дальше еще есть ячейки
                }
            }
            return true;
        }
    }
}
