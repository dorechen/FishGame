using System;
using FishGame.Models;

namespace FishGame.Services
{
    public class ConsoleRenderer
    {
        public void Initialize()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }

        public void Render(Fish fish)
        {
            Console.Clear();

            Console.SetCursorPosition(fish.X, fish.Y);
            Console.ForegroundColor = fish.Color;
            Console.Write(fish.GetSprite());
            Console.ResetColor();

            Console.SetCursorPosition(0, Console.WindowHeight);
            Console.WriteLine("Press Esc to exit ");
        }
        
        public void Cleanup()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Thanks for playing!");
        }
    }
}