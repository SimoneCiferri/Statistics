using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace homework1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Bitmap b;
        public Graphics g;


        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Homework 1";
            label1.Font = new Font("Arial", 12, FontStyle.Bold);

            b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            g = Graphics.FromImage(b);

            g.FillRectangle(Brushes.Black, 10, 5, 2, 2);
            
            g.FillRectangle(Brushes.Green, 10, 30, 100, 2);

            g.FillRectangle(Brushes.Orange, new Rectangle(10, 60, 25, 150));

            g.FillEllipse(Brushes.Purple, 10, 220, 30, 30);

            this.pictureBox1.Image = b;

        }

    }
}
