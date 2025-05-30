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

        public void RenderFish(Fish fish)
        {
            Console.Clear();

            Console.SetCursorPosition(fish.X, fish.Y);
            Console.ForegroundColor = fish.Color;
            Console.Write(fish.GetSprite());
            Console.ResetColor();

            Console.SetCursorPosition(0, Console.WindowHeight);
            Console.WriteLine("Press Esc to exit ");
        }

        public void RenderFood(Food food)
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.ForegroundColor = food.Color;
            Console.Write(food.GetSprite());
            Console.ResetColor();
        }

        public void DisplayFishStatus(Fish fish){
            // Console.Clear();

            // Create a status box in the middle of the screen
            int boxWidth = 32;
            int boxHeight = 9;
            int startX = (Console.WindowWidth - boxWidth) / 2;
            int startY = (Console.WindowHeight - boxHeight) / 2;
            
            // Draw the box
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int y = 0; y < boxHeight; y++)
            {
                Console.SetCursorPosition(startX, startY + y);
                Console.Write(new string(' ', boxWidth));
            }
            
            // Display fish information
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(startX + 2, startY + 1);
            Console.Write("Fish Status");
            Console.SetCursorPosition(startX + 2, startY + 3);
            Console.Write($"Name: ");
            Console.SetCursorPosition(startX + 2, startY + 4);
            Console.Write($"Colour: {(fish.Color)}");
            Console.SetCursorPosition(startX + 2, startY + 5);
            Console.Write($"Fullness: {fish.fullness}%");


            Console.SetCursorPosition(startX + 2, startY + 7);
            Console.Write("Press any key to continue...");
            
            // Reset console settings
            Console.ResetColor();
        }
        
        public void Cleanup()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Thanks for playing!");
        }
    }
}