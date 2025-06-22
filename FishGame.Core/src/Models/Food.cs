using System;

namespace FishGame.Models
{
    public class Food
    {
        public ConsoleColor Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Satiety { get; set; }

        private readonly string _sprite = "〇";

        public Food(int startX)
        {
            Color = ConsoleColor.Green;
            X = startX;
            Y = 0;
            Satiety = 10;
        }

        public int GetSatiety() => Satiety;

        public string GetSprite() => _sprite;

        public int GetLength() => GetSprite().Length;

        public void Fall(int consoleHeight)
        {
            if (Y < consoleHeight - 3)
            {
                Y++;
            }
        }
    }
}