using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Faculta_6
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap b;
        int n, m;
        int[,] A;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialise();
            LoadMatrix(@"Resources.txt");
            Draw();
            pictureBox1.Image = b;
        }

        private void LoadMatrix(string fileName)
        {
            TextReader load = new StreamReader(fileName);
            List<string> T = new List<string>();
            string buffer;
            while ((buffer = load.ReadLine()) != null)
                T.Add(buffer);
            load.Close();
            n = T.Count();
            m = T[0].Split(' ').Length;
            A = new int[n,m];
            for(int i = 0; i < n; i++)
            {
                string[] l = T[i].Split(' ');
                for (int j = 0; j < m; j++)
                    A[i, j] = int.Parse(l[j]);
            }
        }
        private void Initialise()
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
        }
        private void Draw()
        {
            float dx = (float)pictureBox1.Width / m;
            float dy = (float)pictureBox1.Height / n;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (A[i, j] == 0)
                        g.FillRectangle(Brushes.White, j * dx, i * dy, dx, dy);
                    else
                        g.FillRectangle(Brushes.Green, j * dx, i * dy, dx, dy);
                    g.DrawRectangle(Pens.Black, j * dx, i * dy, dx, dy);
                }     
        }
    }
}
