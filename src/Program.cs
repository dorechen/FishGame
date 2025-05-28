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
            
            string fishRight = "><(((°>";
            string fishLeft = "<°)))><";
            
            bool running = true;
            int position = 10;
            bool movingRight = true;
            Random random = new Random();
            int nextAction;
            
            while (running)
            {
                Console.Clear();
                
                // Display fish
                Console.SetCursorPosition(position, 10);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(movingRight ? fishRight: fishLeft);
                Console.ResetColor();

                // Movement
                nextAction = random.Next(100);

                // Display instructions
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Press Esc to exit ", nextAction);
                
                // Check for key press
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        running = false;
                    }
                }

                if (nextAction < 5)
                {
                    movingRight = !movingRight;
                }
                else if (nextAction > 3 && nextAction < 50)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (movingRight)
                    {
                        position++;
                        if (position > Console.WindowWidth - fishRight.Length)
                        {
                            movingRight = false;
                            position = Console.WindowWidth - fishRight.Length;
                        }
                    }
                    else
                    {
                        position--;
                        if (position < 0)
                        {
                            movingRight = true;
                            position = 0;
                        }
                    }
                
                Thread.Sleep(500);
            }
            
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Thanks for playing!");
        }
    }
}
