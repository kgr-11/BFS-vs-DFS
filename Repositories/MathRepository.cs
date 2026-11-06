using Labyrinth.Additional_Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Labyrinth.Repositories
{
    // Класс всех расчетов
    public static class MathRepository
    {
        //генерация лабиринта
        public static int GenerateLabirinth(Node[,] Labyrinth)
        {
            Random rand = new Random(); 
            //нужно оргнизовать обход графа в глубину
            HashSet<Node>visited= new HashSet<Node>(capacity:((Labyrinth.GetUpperBound(0)+1)* (Labyrinth.GetUpperBound(1)+1))*2);
            Stack<KeyValuePair<Node,Point>>stack = new Stack<KeyValuePair<Node, Point>>();
            // X=j
            // Y=i
            int count = 0;
            stack.Push(new KeyValuePair<Node,Point>(Labyrinth[0, 0],new Point(0,0)));//начинаем со старта
            while (stack.Count > 0)
            { 
                //смотрим на верхний эл-т стека
                var pair= stack.Peek();

                Node node = pair.Key;
                int i = pair.Value.Y;
                int j=pair.Value.X;
                count++;
                //отмечаем узел посещенным
                visited.Add(node);
                //решаем куда идти и кого делать своим соседом и надо проверить сосед не посещен ли был,чтобы цикла не было

                List<Additional_Structures.Enum_Orientation> orientations = new List<Additional_Structures.Enum_Orientation>();//хранит разрешенные направления
                //проверяем можно ли вверх
                if(node.top is null&&i-1>=0&& !visited.Contains(Labyrinth[i - 1, j]))//мы там не были раз null и индекс допустимый
                    orientations.Add(Additional_Structures.Enum_Orientation.Top);
                //проверяем можно ли вниих
                if (node.bottom is null && i +1<= Labyrinth.GetUpperBound(0)
                    && !visited.Contains(Labyrinth[i + 1, j]))//мы там не были раз null и индекс допустимый
                    orientations.Add(Additional_Structures.Enum_Orientation.Bottom);
                //проверяем можно ли влево
                if (node.left is null && j-1 >= 0 
                    && !visited.Contains(Labyrinth[i, j - 1]))//мы там не были раз null и индекс допустимый
                    orientations.Add(Additional_Structures.Enum_Orientation.Left);
                //проверяем можно ли вправо
                if (node.right is null && j+1<= Labyrinth.GetUpperBound(1)
                    && !visited.Contains(Labyrinth[i, j + 1]))//мы там не были раз null и индекс допустимый
                    orientations.Add(Additional_Structures.Enum_Orientation.Right);
                //если из текущего некуда идти, удаляем его из стека
                if (orientations.Count() == 0)
                {
                    stack.Pop();
                    continue;
                }

                //если какие-то допустимое направления есть, то генерируем индекс и идем туда

                int index = rand.Next(orientations.Count());
                //определяем, что узел посетим следующий
                // +
                //создание связи с текущим узлом
                int new_i,new_j;
                if (orientations[index] == Additional_Structures.Enum_Orientation.Top)
                {
                    new_i = i - 1;
                    new_j = j;
                    node.top = Labyrinth[new_i, new_j];
                    Labyrinth[new_i, new_j].bottom = node;
                }
                else if (orientations[index] == Additional_Structures.Enum_Orientation.Bottom)
                {
                    new_i = i + 1;
                    new_j = j;
                    node.bottom = Labyrinth[new_i, new_j];
                    Labyrinth[new_i, new_j].top = node;
                }
                else if (orientations[index] == Additional_Structures.Enum_Orientation.Left)
                {
                    new_i = i;
                    new_j = j-1;
                    node.left = Labyrinth[new_i, new_j];
                    Labyrinth[new_i, new_j].right = node;
                }
                else
                {
                    new_i = i;
                    new_j = j+1;
                    node.right = Labyrinth[new_i, new_j];
                    Labyrinth[new_i, new_j].left = node;
                }

                //идем по сгенерированному направлению
                stack.Push(new KeyValuePair<Node, Point>(Labyrinth[new_i,new_j],new Point(new_j,new_i)));
            }
            return count;
        }

        // поиск в глубину
        //те,что относятся к пути будем помечать 1
        //все, куда заходил помечаю -1
        public static int DFS(Node[,] Labyrinth)
        { 
            HashSet<Node>visited = new HashSet<Node>();  
            Stack<Node>stack= new Stack<Node>();
            stack.Push(Labyrinth[0, 0]);
            int index_i_finish = Labyrinth.GetUpperBound(0);
            int index_j_finish= Labyrinth.GetUpperBound(1);
            int count = 0;
            while (stack.Peek() != Labyrinth[index_i_finish, index_j_finish])
            { 
                Node node = stack.Peek();
                count++;
                if (visited.Add(node))
                    node.val = (sbyte)1;

                //пробуем идти вверх
                if (node.top is not null && !visited.Contains(node.top))
                    stack.Push(node.top);
                //пробуем идти вниз
                else if (node.bottom is not null && !visited.Contains(node.bottom))
                    stack.Push(node.bottom);
                //пробуем идти влево
                else if (node.left is not null && !visited.Contains(node.left))
                    stack.Push(node.left);
                //пробуем идти вправо
                else if (node.right is not null && !visited.Contains(node.right))
                    stack.Push(node.right);
                //если из текущего некуда идти, удаляем его из стека
                else
                {
                    node.val = (sbyte)-1;
                    stack.Pop();
                }
            }
            stack.Peek().val = (sbyte)1;
            return count;
        }
        //поиск в ширину
        public static int BFS(Node[,] Labyrinth)
        {
            HashSet<Node>visited= new HashSet<Node>();
            Queue<Node>queue=new Queue<Node>();
            queue.Enqueue(Labyrinth[0, 0]);
            int index_i_finish = Labyrinth.GetUpperBound(0);
            int index_j_finish = Labyrinth.GetUpperBound(1);
            int count = 0;
           
            //создае словаря предков
            Dictionary<Node,Node>parents= new Dictionary<Node,Node>();
            //key-потомок
            //value-предок того самого потомка, т.е. запоминаем через кого мы пришли в этот узел
            parents[Labyrinth[0, 0]] = null;
            while (queue.Count>0)
            { 
                Node node= queue.Dequeue();
                count++;
                //если наткнулись на финиш
                if (node == Labyrinth[index_i_finish, index_j_finish])
                {
                    break;
                }
                if (visited.Add(node))
                    node.val = (sbyte)-1;
                //пробуем идти вверх
                if (node.top is not null && !visited.Contains(node.top))
                {
                    //пришли в узел node.top через node
                    parents[node.top] = node;
                    queue.Enqueue(node.top);
                }
                //пробуем идти вниз
                if (node.bottom is not null && !visited.Contains(node.bottom))
                {
                    //пришли в узел node.bottom через node
                    parents[node.bottom] = node;
                    queue.Enqueue(node.bottom);
                }
                //пробуем идти влево
                if (node.left is not null && !visited.Contains(node.left))
                {
                    //пришли в узел node.left через node
                    parents[node.left] = node;
                    queue.Enqueue(node.left);
                }
                //пробуем идти вправо
                if (node.right is not null && !visited.Contains(node.right))
                {
                    //пришли в узел node.right через node
                    parents[node.right] = node;
                    queue.Enqueue(node.right);
                }
            }
            //перебор предков в обратном направлении
            Node child = Labyrinth[index_i_finish, index_j_finish];
            child.val = (sbyte)1;
            while (parents[child] != null)
            { 
                child=parents[child];
                child.val = (sbyte)1;
            }

            return count;
        }
    }
}
