using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfCreatingMaze
{
    class Cell
    {
        public int i { get; set; }
        public int j { get; set; }

        //public bool[] walls = { true, true, true, true }; // t, r, b, l

        public Cell(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        private bool[] walls = { true, true, true, true };

        public bool[] MyWalls
        {
            get { return walls; }
            set { walls = value; }
        }

    }
}
