using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ERSApp.Views
{
    public partial class TestWindow : Window
    {

        //This list describes the bonus red pieces of food on the canvas
        private List<Point> BonusPoints = new List<Point>();

        //This list describes the body of the snake on the canvas
        private List<Point> SnakePoints = new List<Point>();

        private Brush SnakeColor = Brushes.Green;

        private enum SnakeSize
        {
            Thin = 4,
            Normal = 6, 
            Thick = 8
        }

        private enum MovingDirection
        {
            Upwards = 8,
            Downwards = 2, 
            ToLeft = 4,
            ToRight = 6
        }

        //TimeSpan values
        private enum GameSpeed
        {
            Fast = 1,
            Moderate = 10000,
            Slow = 50000,
            DamnSlow = 500000
        };

        private Point StartingPoint = new Point(100, 100);
        private Point CurrentPosition = new Point();

        // Movement direction initialisation
        private int Direction = 0;

        // Placeholder for the previous movement direction - snake needs this to avoid its own body
        private int PreviousDirection = 0;

        // Here user can change the size of the snake - thin, normal, thick 
        private int HeadSize = (int)SnakeSize.Normal;

        private int Length = 100;
        private int Score = 0;
        private Random rand = new Random();


        public TestWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);

            //Here user can change the speed of the snake - fast, moderate, slow and damnslow
            timer.Interval = new TimeSpan((int)GameSpeed.Moderate);
            timer.Start();

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            PaintSnake(StartingPoint);
            CurrentPosition = StartingPoint;

            //Instantiate food
            for(var i = 0; i < 10; i++)
            {
                PaintBonus(i);
            }
        }

        //Method for painting frame of the snake's body
        private void PaintSnake(Point currentPosition)
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = SnakeColor,
                Width = HeadSize,
                Height = HeadSize
            };

            Canvas.SetTop(ellipse, currentPosition.Y);
            Canvas.SetLeft(ellipse, currentPosition.X);

            int count = PaintCanvas.Children.Count;
            PaintCanvas.Children.Add(ellipse);
            SnakePoints.Add(currentPosition);

            //Restrict the snake's tail
            if(count > Length)
            {
                PaintCanvas.Children.RemoveAt(count - Length + 9);
                SnakePoints.RemoveAt(count - Length);
            }
        }

        //Method for painting bonus points to frame
        private void PaintBonus(int index)
        {
            Point bonusPoint = new Point(rand.Next(5, 780), rand.Next(5, 480));

            Ellipse ellipse = new Ellipse
            {
                Fill = Brushes.Red,
                Width = HeadSize,
                Height = HeadSize
            };

            Canvas.SetTop(ellipse, bonusPoint.Y);
            Canvas.SetLeft(ellipse, bonusPoint.X);
            PaintCanvas.Children.Insert(index, ellipse);
            BonusPoints.Insert(index, bonusPoint);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            //Control snake's direction
            switch (Direction)
            {
                case (int)MovingDirection.Downwards:
                    CurrentPosition.Y += 1;
                    PaintSnake(CurrentPosition);
                    break;
                case (int)MovingDirection.Upwards:
                    CurrentPosition.Y -= 1;
                    PaintSnake(CurrentPosition);
                    break;
                case (int)MovingDirection.ToRight:
                    CurrentPosition.X += 1;
                    PaintSnake(CurrentPosition);
                    break;
                case (int)MovingDirection.ToLeft:
                    CurrentPosition.X -= 1;
                    PaintSnake(CurrentPosition);
                    break;
            }

            //Restrict boundaries of the canvas
            if((CurrentPosition.X < 5) || (CurrentPosition.X > 780) || (CurrentPosition.Y < 5) || (CurrentPosition.Y > 480))
            {
                GameOver();
            }

            //Hitting a bonus point causes the lengthen snake effect
            int n = 0;
            foreach(Point point in BonusPoints)
            {
                if((Math.Abs(point.X - CurrentPosition.X) < HeadSize) && (Math.Abs(point.Y - CurrentPosition.Y) < HeadSize))
                {
                    Length += 10;
                    Score += 10;

                    //Erase food object as eaten
                    BonusPoints.RemoveAt(n);
                    PaintCanvas.Children.RemoveAt(n);
                    PaintBonus(n);
                    break;
                }
                n++;
            }

            //Restrict hits to the snake's body
            for(int i = 0; i < (SnakePoints.Count - HeadSize * 2); i++)
            {
                Point point = new Point(SnakePoints[i].X, SnakePoints[i].Y);
                //Restrict boundaries of the canvas
                if ((Math.Abs(point.X - CurrentPosition.X) < (HeadSize)) && (Math.Abs(point.Y - CurrentPosition.Y) < (HeadSize)))
                {
                    GameOver();
                    break;
                }
            }
        }

        //Key button down event handler
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if(PreviousDirection != (int)MovingDirection.Upwards)
                    {
                        Direction = (int)MovingDirection.Downwards;
                    }
                    break;
                case Key.Up:
                    if (PreviousDirection != (int)MovingDirection.Downwards)
                    {
                        Direction = (int)MovingDirection.Upwards;
                    }
                    break;
                case Key.Right:
                    if (PreviousDirection != (int)MovingDirection.ToLeft)
                    {
                        Direction = (int)MovingDirection.ToRight;
                    }
                    break;
                case Key.Left:
                    if (PreviousDirection != (int)MovingDirection.ToRight)
                    {
                        Direction = (int)MovingDirection.ToLeft;
                    }
                    break;
            }
            PreviousDirection = Direction;
        }

        private void GameOver()
        {
            MessageBox.Show($@"You Lose! Your score is { Score }", "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            this.Close();
        }
    }
}
