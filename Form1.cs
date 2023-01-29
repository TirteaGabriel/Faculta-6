using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
            g.Clear(Color.AliceBlue);
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
            pictureBox1.Image = b;
        }
        int steps = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            data.Add(Count());
            Tick();
            Draw();
            steps++;
            if (steps == 1000)
                timer1.Enabled = false;
            Save(@"DataOut.txt");
        }

        private void generateNew_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                timer1.Enabled = false;
            else
                timer1.Enabled = true;
        }

        private void Save(string fileName)
        {
            TextWriter save = new StreamWriter(fileName);
            foreach(int i in data)
            {
                save.WriteLine(i);
            }
            save.Close();
        }
        public int Count()
        {
            int nr = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (A[i, j] == 1)
                        nr++;
            return nr;
        }
        List<int> data = new List<int>();
        private void Tick()
        {
            int[,] M = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    int s = 0;
                    if ((i - 1 >= 0 && j - 1 >= 0) && (A[i - 1, j - 1] == 1))
                        s++;
                    if ((i - 1 >= 0) && (A[i - 1, j] == 1))
                        s++;
                    if ((i - 1 >= 0 && j + 1 < m) && (A[i - 1, j + 1] == 1))
                        s++;
                    if ((j - 1 >= 0) && (A[i, j - 1] == 1))
                        s++;
                    if ((j + 1 < m) && (A[i, j + 1] == 1))
                        s++;
                    if ((i + 1 < n && j - 1 >= 0) && (A[i + 1, j - 1] == 1))
                        s++;
                    if ((i + 1 < n) && (A[i + 1, j] == 1))
                        s++;
                    if ((i + 1 < n && j + 1 < m) && (A[i + 1, j + 1] == 1))
                        s++;
                    if (A[i, j] == 1)
                    {
                        if (s < 2)
                            M[i, j] = 0;
                        if (s == 2 || s == 3)
                            M[i, j] = 1;
                        if (s > 3)
                            M[i, j] = 0;
                    }
                    else
                    {
                        if (s == 3)
                            M[i, j] = 1;
                    }
                }
            }
            A = M;
        }
    }
}
