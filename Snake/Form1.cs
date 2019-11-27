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

        private void StartGame() {
            // Set settings to default
            new Settings();

            // Create new player object
            snake.Clear();
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();
        }

        // Place random food on game screen
        private void GenerateFood() {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            /*  Needs a fix
             *  Make sure food and badfood aren't spawning on each other or snake!
             */
            Random random = new Random();
            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(maxYPos, 0);

            badFood = new Circle();
            badFood.X = random.Next(0, maxXPos);
            badFood.Y = random.Next(maxYPos, 0);

        }

        private void UpdateScreen(object sender, EventArgs e) {

        }

        // remove?
        private void label1_Click(object sender, EventArgs e)
        {

        }

        // remove?
        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        // remove?
        private void PbCanvas_Click(object sender, EventArgs e)
        {

        }
    }
}
