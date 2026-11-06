using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Additional_Structures
{
    //клетка(узел) лабиринта
    //значение узла 0- в нем еще не разу не были, вокруг стены
    //значение узал 1-в нем уже были
    public class Node
    {
        public sbyte val;
        public Node top;
        public Node bottom;
        public Node left;
        public Node right;
        public Node() : this((sbyte)0,null,null,null,null)
        { 
            
        }
        public Node(sbyte val, Node top, Node bottom, Node left, Node right)
        { 
            this.val = val;
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
        }
    }
}
