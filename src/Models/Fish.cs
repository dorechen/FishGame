using System;

namespace FishGame.Models
{
    public class Fish
    {
        public ConsoleColor Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool isFacingRight { get; set; }

        private readonly string _rightSprite = "><(((°>";
        private readonly string _leftSprite = "<°)))><";
        private readonly Random _random = new Random();

        public Fish(int startX, int startY, bool facingRight, ConsoleColor color)
        {
            X = startX;
            Y = startY;
            isFacingRight = facingRight;
            Color = color;
        }

        public string GetSprite() => isFacingRight ? _rightSprite : _leftSprite;

        public int GetLength() => GetSprite().Length;   

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

        public void Move(int consoleWidth)
        {
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
        }
    }
}