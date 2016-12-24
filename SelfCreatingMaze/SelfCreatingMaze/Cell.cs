using System;
using System.Collections.Generic;
using System.Drawing;

namespace SelfCreatingMaze
{
    class Cell
    {
        public int i { get; set; } //x
        public int j { get; set; } //y
        public bool[] walls = { true, true, true, true }; // t, r, b, l
        public bool IsVisited = false;


        //constructor
        public Cell(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }
}
