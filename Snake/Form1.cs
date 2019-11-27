using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> snake = new List<Circle>();
        private Circle food = new Circle();
        private Circle badFood = new Circle();

        public Form1()
        {
            InitializeComponent();

            // Set settings to default
            new Settings();

            // Set game speed and start timer
            gameTimer.Interval = 1000/Settings.Speed;
            gameTimer.Tick += UpdateScreen();
            gameTimer.Start();

            // Start New game
            StartGame();
        }

        // remove?
        private void label1_Click(object sender, EventArgs e)
        {

        }

        // remove?
        private void lblScore_Click(object sender, EventArgs e)
        {

        }
    }
}
