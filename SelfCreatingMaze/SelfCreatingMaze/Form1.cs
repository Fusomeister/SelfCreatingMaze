using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SelfCreatingMaze
{
    public partial class Form1 : Form
    {

        private Bitmap drawingArea = new Bitmap(403, 403);
        private Bitmap drawingArea2 = new Bitmap(403, 403);
        private int cols, rows;
        private int w = 10;
        private int delay = 10;
        private List<Cell> grid = new List<Cell>();
        private List<Cell> stack = new List<Cell>();
        private Graphics g;
        private Random rnd = new Random();
        private Cell current;
        private bool isStarted = false;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = w + "";
            textBox2.Text = delay + "";
        }

        //sets up the grid
        public void InitGrid()
        {
            cols = drawingArea.Width / w;
            rows = drawingArea.Height / w;

            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    Cell cell = new Cell(i, j);
                    grid.Add(cell);
                }
            }
            current = grid[0];          
        }
        
        //paint screen "update"
        public void PaintToScreen()
        {
            if(isStarted==true)
            {           
                g = Graphics.FromImage(drawingArea);

                //for (int i = 0; i < grid.Count; i++)
                //{
                //    MakeWalls(grid[i]);
                //}
                
                current.IsVisited = true;
                Highlighted(current);
                MakeWalls(current);

                //step 1
                Cell next = CheckNeigbors(current);

                if (next != null)
                {                
                    next.IsVisited = true;
                    Highlighted(next);
                    //step 2
                    stack.Add(current);

                    //sted 3
                    RemoveWalls(current, next);

                    //step 4
                    current = next;
                }
                else if (stack.Count > 0)
                {
                    current = stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);
                    Highlighted(current);
                }

                pictureBox1.Image = drawingArea;               
            }
        }

        //remove walls, so when update they are not painted again
        private void RemoveWalls(Cell theCurrent, Cell theNext)
        {
            int x = theCurrent.i - theNext.i;
            int y = theCurrent.j - theNext.j;

            if (x == 1)
            {
                theCurrent.walls[3] = false;
                theNext.walls[1] = false;
            }
            else if (x == -1)
            {
                theCurrent.walls[1] = false;
                theNext.walls[3] = false;
            }

            if (y == 1)
            {
                theCurrent.walls[0] = false;
                theNext.walls[2] = false;
            }
            else if (y == -1)
            {
                theCurrent.walls[2] = false;
                theNext.walls[0] = false;
            }
        }

        //indexes the list of Cells
        private int Index(int i, int j)
        {
            if (i < 0 || j < 0 || i > cols - 1 || j > rows - 1)
            {
                return -1;
            }
            return i + j * cols;
        }

        //check who's around and visisted       
        private Cell CheckNeigbors(Cell current)
        {
            List<Cell> neighbors = new List<Cell>();

            if (Index(current.i, current.j - 1) > -1)
            {
                Cell top = grid[Index(current.i, current.j - 1)];

                if (top != null && !top.IsVisited)
                {
                    neighbors.Add(top);
                }
            }

            if (Index(current.i + 1, current.j) > -1)
            {
                Cell right = grid[Index(current.i + 1, current.j)];

                if (right != null && !right.IsVisited)
                {
                    neighbors.Add(right);
                }
            }

            if (Index(current.i, current.j + 1) > -1)
            {
                Cell bottom = grid[Index(current.i, current.j + 1)];

                if (bottom != null && !bottom.IsVisited)
                {
                    neighbors.Add(bottom);
                }
            }

            if (Index(current.i - 1, current.j) > -1)
            {
                Cell left = grid[Index(current.i - 1, current.j)];

                if (left != null && !left.IsVisited)
                {
                    neighbors.Add(left);
                }
            }
              

            if (neighbors.Count > 0)
            {
                int r = rnd.Next(0, neighbors.Count);
                return neighbors[r];
            }
            else
            {
                return null;
            }
            
        }

        //init grid building, make Trump proud!
        private void MakeWalls(Cell c)
        {
            int x = c.i * w;
            int y = c.j * w;

            if (c.walls[0])
            {
                g.DrawLine(new Pen(Color.White, 3), x, y, x + w, y);
            }
            if (c.walls[1])
            {
                g.DrawLine(new Pen(Color.White, 3), x + w, y, x + w, y + w);
            }
            if (c.walls[2])
            {
                g.DrawLine(new Pen(Color.White, 3), x + w, y + w, x, y + w);
            }
            if (c.walls[3])
            {
                g.DrawLine(new Pen(Color.White, 3), x, y + w, x, y);
            }
            if (c.IsVisited)
            {
                g.FillRectangle(new SolidBrush(Color.Blue), x, y, w, w);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isStarted = true;
            InitGrid();
            pictureBox1.Refresh();
            //PaintToScreen();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
                Thread.Sleep(delay);
                PaintToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            isStarted = false;
            drawingArea = new Bitmap(403, 403);
            g = Graphics.FromImage(drawingArea);
            pictureBox1.Invalidate();
            grid.Clear();
            stack.Clear();

            InitGrid();           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (isStarted) { }
            else
            {
                bool isNum = int.TryParse(textBox1.Text, out int n);

                if (isNum)
                {
                    w = int.Parse(textBox1.Text);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (isStarted) { }
            else
            {
                bool isNum = int.TryParse(textBox2.Text, out int n);

                if (isNum)
                {
                    delay = int.Parse(textBox2.Text);
                }
            }
        }

        private void Highlighted(Cell c)
        {
            int x = c.i * w;
            int y = c.j * w;

            g.FillRectangle(new SolidBrush(Color.Green), x, y, w, w);
        }
    }
}
