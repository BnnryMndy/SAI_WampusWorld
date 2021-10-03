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
        public Label[,] labels = new Label[4, 4];
        public Label[,] agentLabel = new Label[4, 4];
        public int Score = 0;
        public World world = new World();

        public Form1()
        {
            
            InitializeComponent();

            #region ehe
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


            agentLabel[0, 0] = label17;
            agentLabel[0, 1] = label18;
            agentLabel[0, 2] = label19;
            agentLabel[0, 3] = label20;
            agentLabel[1, 0] = label21;
            agentLabel[1, 1] = label22;
            agentLabel[1, 2] = label23;
            agentLabel[1, 3] = label24;
            agentLabel[2, 0] = label25;
            agentLabel[2, 1] = label26;
            agentLabel[2, 2] = label27;
            agentLabel[2, 3] = label28;
            agentLabel[3, 0] = label29;
            agentLabel[3, 1] = label30;
            agentLabel[3, 2] = label31;
            agentLabel[3, 3] = label32;
            #endregion ehe



            world.GenerateMap();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            updateFields();
        }

        private void updateFields()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    labels[i, j].Text = world.ground[i, j].ToString();
                    agentLabel[i, j].Text = world.agent.memory[i, j].ToString();
                }
            }

            StatusLabel.Text = "SCORE: " + Convert.ToInt32(Score);
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

        private void newWorldButton_Click(object sender, EventArgs e)
        {
            world.GenerateMap();
            updateFields();
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            if (world.agent.isLive && !world.agent.isWIn) world.AgentStep();

            Score -= 1;

            if (!world.agent.isLive)
            {
                Score -= 1000;
                world.GenerateMap();
            }
            if (world.agent.isWIn)
            {
                Score += 1000;
                world.GenerateMap();
            }

            updateFields();
        }

       
    }
}
