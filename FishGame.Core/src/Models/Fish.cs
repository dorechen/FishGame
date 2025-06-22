using System;

namespace FishGame.Models
{
    public class Fish
    {
        public Guid Id { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
        public ConsoleColor Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool isFacingRight { get; set; }
        public int fullness { get; set; } // range from 0 to 100(%)

        private readonly string _rightSprite = "><(((°>";
        private readonly string _leftSprite = "<°)))><";
        private readonly Random _random = new Random(); // TODO: add a seed

        public Fish(int startX, int startY, bool facingRight, ConsoleColor color)
        {
            Id = Guid.NewGuid();
            X = startX;
            Y = startY;
            isFacingRight = facingRight;
            Color = color;
            fullness = 50;
        }

        public string GetSprite() => isFacingRight ? _rightSprite : _leftSprite;

        public int GetLength() => GetSprite().Length;

        public void Eat(int satiety)
        {
            fullness = fullness + satiety;
        }

        public void InteractWithFood(int foodX, int foodY)
        {
            if (X > foodX)
            {
                isFacingRight = false;
                X--;
            }
            else if (X < foodX)
            {
                isFacingRight = true;
                X++;
            }

            if (Y > foodY)
            {
                Y--;
            }
            else if (Y < foodY)
            {
                Y++;
            }
        }

        public void DecreaseFullness()
        {
            fullness = Math.Max(0, fullness - 5);
            LastUpdate = DateTime.UtcNow;
        }

        public void MaybeChangeDirection()
        {
            if (_random.Next(100) < 5)
            {
                isFacingRight = !isFacingRight;
            }
        }

        public bool MaybeIdle()
        {
            int rand = _random.Next(100);
            return rand < 45;
        }

        public void Move(int consoleWidth, int consoleHeight)
        {
            int rand = _random.Next(100);
            if (isFacingRight)
            {
                X++;
                if (X > consoleWidth - GetLength())
                {
                    isFacingRight = !isFacingRight;
                    X = consoleWidth - GetLength();
                }
            }
            else
            {
                X--;
                if (X < 0)
                {
                    isFacingRight = !isFacingRight;
                    X = 0;
                }
            }

                if (rand < 20){
                if (Y > consoleHeight - 5)
                {
                    Y--;
                }
                else if (Y < 3)
                {
                    Y++;

                }
                else
                {
                    if (rand < 10)
                    {
                        Y--;
                    }
                    else
                    {
                        Y++;
                    }
                }
            }
        }
    }
}