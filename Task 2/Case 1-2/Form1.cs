using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //init variables 
        public int initalX;
        public int initalY;
        public int finalX;
        public int finalY;
        public List<Point> Data = new List<Point>();


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pB_MouseDown(object sender, MouseEventArgs e)
        {
            initalX = e.X;
            initalY = e.Y;
        }

        private void pB_MouseUp(object sender, MouseEventArgs e)
        {
            finalX = e.X;
            finalY = e.Y;
            if(finalX < initalX)
            {
                initalX = initalX + finalX;
                finalX = initalX - finalX;
                initalX = initalX - finalX;
            }
            if(finalY < initalY)
            {
                initalY = initalY + finalY;
                finalY = initalY - finalY;
                initalY = initalY - finalY;
            }
            DrawRectangle(initalX, initalY, finalX, finalY);
        }

        private void DrawRectangle(int initalX, int initalY, int finalX, int finalY)
        {
            System.Drawing.Graphics g;
            //pB.Refresh();
            g = pB.CreateGraphics();
            Pen mypen = new System.Drawing.Pen(Brushes.Black);
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            g.DrawRectangle(mypen, initalX, initalY, Math.Abs(initalX-finalX), Math.Abs(initalY - finalY));

            Random rng = new Random();
            List<Point> temp = new List<Point>();
            int pointer = Data.Count();
            for(int i = 0; i < Convert.ToInt32(tBpoints.Text); i++)
            {
                Point p = new Point(rng.Next(initalX, finalX), rng.Next(initalY, finalY));
                Data.Add(p);
                Rectangle rect = new Rectangle(Data[pointer + i], new Size(4, 4));
                g.FillRectangle(brush, rect);
            }
        }

        private void InitDraw()
        {
            System.Drawing.Graphics g = pB.CreateGraphics(); ;
            pB.Refresh();
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            for(int i = 0; i < Data.Count(); i++)
            {
                Rectangle rect = new Rectangle(Data[i], new Size(4, 4));
                g.FillRectangle(brush, rect);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitDraw();
        }
    }
}
