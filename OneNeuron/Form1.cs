using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace OneNeuron
{
    public partial class Form1 : Form
    {

        private List<Object> points = new List<Object>();

        public Form1()
        {

            InitializeComponent();
            Graphics g = this.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RandomiseData(200);
            SelectClassType(points);
            if (!File.Exists("C:\\foo.txt"))
            {
                StreamWriter sw = File.CreateText("D:\\foo.txt");
                for (var i = 0; i < points.Count; i++)
                {
                    sw.Write(i);
                    sw.Write("-");
                    sw.Write(points[i].X);
                    sw.Write(" ");
                    sw.Write(points[i].Y);
                    sw.Write(" ");
                    sw.WriteLine(points[i].ClassType);
                }
            }
        }

        public void RandomiseData(int n)
        {
            Random rnd = new Random();
            for (var i = 0; i < n; i++)
            {

                Object point = new Object(rnd);
                points.Add(point);
            }
        }

        private void SelectClassType(List<Object> array)
        {
            for(var i = 0; i < array.Count; i++)
            {
                if (Function(array[i].X) <= array[i].Y)
                {
                    array[i].ClassType = 1;
                }
            }

        }

        public double Function(double x)
        {
            return x;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
          /*  Graphics g = e.Graphics;
            for(var i =0; i<points.Count; i++)
            {
                if (points[i].ClassType == 1)
                {
                    g.DrawEllipse(System.Drawing.Pens.Red, new Rectangle(Convert.ToInt32(points[i].X * 100), Convert.ToInt32(points[i].Y * 100), 3, 3));
                }
                else
                {
                    g.DrawEllipse(System.Drawing.Pens.Blue, new Rectangle(Convert.ToInt32(points[i].X * 100), Convert.ToInt32(points[i].Y * 100), 3, 3));
                }
            }
            g.DrawLine(System.Drawing.Pens.Black,this.Size.Width ,Convert.ToInt32(Function(this.Size.Width)*100),this.Size.Width*100, Convert.ToInt32(Function(this.Size.Width) * 100));
    */    
    }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (var i = 0; i < points.Count; i++)
            {
                if (points[i].ClassType == 1)
                {
                    g.DrawEllipse(System.Drawing.Pens.Red, new Rectangle( Convert.ToInt32(points[i].X *50 ), pictureBox1.Size.Height - Convert.ToInt32(points[i].Y *50), 3, 3));
                }
                else
                {
                    g.DrawEllipse(System.Drawing.Pens.Blue, new Rectangle(Convert.ToInt32(points[i].X *50), pictureBox1.Size.Height - Convert.ToInt32(points[i].Y *50), 3, 3));
                }
            }
           g.DrawLine(System.Drawing.Pens.Black, 0, pictureBox1.Size.Height - Convert.ToInt32(Function(0) * 50), pictureBox1.Size.Width * 50, Convert.ToInt32(pictureBox1.Size.Height - Function(pictureBox1.Size.Width) * 50));
            //pictureBox1.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

    }
}
