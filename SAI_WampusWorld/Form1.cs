using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAI_WampusWorld
{
    public partial class Form1 : Form
    {
        public Label[,] labels = new Label[4,4];
        public World world = new World();
        public Form1()
        {
            
            InitializeComponent();
            
            labels[0, 0] = label1;
            labels[0, 1] = label2;
            labels[0, 2] = label3;
            labels[0, 3] = label4;
            labels[1, 0] = label5;
            labels[1, 1] = label6;
            labels[1, 2] = label7;
            labels[1, 3] = label8;
            labels[2, 0] = label9;
            labels[2, 1] = label10;
            labels[2, 2] = label11;
            labels[2, 3] = label12;
            labels[3, 0] = label13;
            labels[3, 1] = label14;
            labels[3, 2] = label15;
            labels[3, 3] = label16;

            world.GenerateMap();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    labels[i, j].Text = world.ground[i,j].ToString();
                }
            }
        }

        //don't look at this {{{(>_<)}}}
        #region deathcode
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
