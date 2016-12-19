using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfCreatingMaze
{
    public partial class Form1 : Form
    {
        Bitmap drawingArea = new Bitmap(401, 401);
        int cols, rows;
        int w = 40;
        List<Cell> grid = new List<Cell>();

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
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
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = Graphics.FromImage(drawingArea);

            for (int i = 0; i < grid.Count; i++)
            {
                int x = grid[i].i * w;
                int y = grid[i].j * w;

                if (grid[i].MyWalls[0])
                {
                    g.DrawLine(new Pen(Color.Black), x, y, x + w, y);
                }
                if (grid[i].MyWalls[1])
                {
                    g.DrawLine(new Pen(Color.Black), x + w, y, x + w, y + w);
                }
                if (grid[i].MyWalls[2])
                {
                    g.DrawLine(new Pen(Color.Black), x + w, y + w, x, y + w);
                }
                if (grid[i].MyWalls[3])
                {
                    g.DrawLine(new Pen(Color.Black), x, y + w, x, y);
                }
            }

            pictureBox1.Image = drawingArea;
        }
    }
}
