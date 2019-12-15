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
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            // Start New game
            StartGame();
        }

        private void StartGame() {

            lblGameOver.Visible = false;

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
            food.Y = random.Next(0, maxYPos);

            badFood = new Circle();
            badFood.X = random.Next(0, maxXPos);
            badFood.Y = random.Next(0, maxYPos);

        }

        private void UpdateScreen(object sender, EventArgs e) {
            // Check for game over
            if(Settings.GameOver) {
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

            if(!Settings.GameOver) {
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
                    canvas.FillEllipse(Brushes.Yellow,
                       new Rectangle(food.X * Settings.Width,
                                     food.Y * Settings.Height,
                                     Settings.Width, Settings.Height));

                    // Draw bad food
                    canvas.FillEllipse(Brushes.Purple,
                        new Rectangle(badFood.X * Settings.Width,
                                      badFood.Y * Settings.Height,
                                      Settings.Width, Settings.Height));
                }
            }
            else {
                string gameOver = "Game Over!\n Your final score is: " + Settings.Score + "\nPress enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void MovePlayer() {
            for(int i = snake.Count - 1; i >= 0; i--) {
                // Move Head
                if (i == 0) {
                    switch(Settings.PlayerDirection) {
                        case Direction.Right:
                            snake[i].X++;
                            break;
                        case Direction.Left:
                            snake[i].X--;
                            break;
                        case Direction.Up:
                            snake[i].Y--;
                            break;
                        case Direction.Down:
                            snake[i].Y++;
                            break;
                    }

                    // Get maximum X and Y position
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;

                    // Detect collision with game borders
                    if(snake[i].X < 0 || snake[i].Y < 0 ||
                       snake[i].X >= maxXPos || snake[i].Y >= maxYPos) {
                        Die();
                    }

                    // Detect collision with body
                    for (int j = 1; j < snake.Count; j++) {
                        if(snake[i].X == snake[j].X &&
                           snake[i].Y == snake[j].Y) {
                            Die();
                        }
                    }

                    // Detect collision with food piece
                    if(snake[0].X == food.X && snake[0].Y == food.Y) {
                        Eat();
                    }

                    /* Detect collision with bad food piece
                    if(snake[0].X == badFood.X && snake[0].Y == badFood.Y) {

                        // If the body is only a head...
                        if() {
                            Die()
                        }

                        // If there is a body...
                        else() {
                            Puke();
                        }
                    }
                    */
                }
                else {
                    // Move Body
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
            }
        }

        private void Eat() {
            // Add Circle to body
            Circle food = new Circle();
            food.X = snake[snake.Count - 1].X;
            food.Y = snake[snake.Count - 1].Y;

            snake.Add(food);

            // Update Score
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
        }

        private void Die() {
            Settings.GameOver = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            Inputs.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            Inputs.ChangeState(e.KeyCode, false);
        }
    }
}
