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
            // Check for game over
            if(Settings.GameOver == true) {
                // Check if enter is pressed
                if(Inputs.KeyPressed(Keys.Enter)) {
                    StartGame();
                }
            }
            else {
                if(Inputs.KeyPressed(Keys.Right) && Settings.PlayerDirection != Direction.Left) {
                    Settings.PlayerDirection = Direction.Right;
                }
                else if (Inputs.KeyPressed(Keys.Left) && Settings.PlayerDirection != Direction.Right) {
                    Settings.PlayerDirection = Direction.Left;
                }
                else if (Inputs.KeyPressed(Keys.Up) && Settings.PlayerDirection != Direction.Down) {
                    Settings.PlayerDirection = Direction.Up;
                }
                else if (Inputs.KeyPressed(Keys.Down) && Settings.PlayerDirection != Direction.Up) {
                    Settings.PlayerDirection = Direction.Down;
                }

                MovePlayer();
            }

            pbCanvas.Invalidate();

        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e) {
            Graphics canvas = e.Graphics;

            if(Settings.GameOver != false) {
                // Set color of Snake
                Brush snakeColor;
                
                // Draw Snake
                for(int i = 0; i < snake.Count; i++) {
                    // Draw head
                    if (i == 0) {
                        snakeColor = Brushes.Red;
                    }
                    // Draw rest of body
                    else {
                        snakeColor = Brushes.Green;
                    }

                    // Draw Snake
                    canvas.FillEllipse(snakeColor,
                        new Rectangle(snake[i].X * Settings.Width,
                                      snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));

                    // Draw Food
                }
            }
            else {

            }
        }
    }
}
