using System;
using System.Threading;

namespace FishGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Clear();
            
            string fishArt = "><(((°>";
            
            bool running = true;
            int position = 10;
            
            Console.WriteLine("Press Esc to exit.");
            
            while (running)
            {
                Console.Clear();
                
                // Display fish
                Console.SetCursorPosition(position, 10);
                Console.Write(fishArt);
                
                // Display instructions
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Press Esc to exit");
                
                // Check for key press
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        running = false;
                    }
                }
                
                // Move right
                position = (position + 1) % (Console.WindowWidth - fishArt.Length);
                
                Thread.Sleep(200);
            }
            
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Thanks for playing!");
        }
    }
}
