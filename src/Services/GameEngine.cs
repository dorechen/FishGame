using System;
using System.Threading;
using FishGame.Models;

namespace FishGame.Services
{
    class GameEngine
    {
        private readonly Fish _fish;
        private readonly ConsoleRenderer _renderer;
        private bool _isRunning = false;
        private Food? _food = null;

        private readonly Random _random = new Random();

        public GameEngine()
        {
            _fish = new Fish(10, 10, true, (ConsoleColor)_random.Next(0, 16));
            _renderer = new ConsoleRenderer();
        }

        public void Start()
        {
            _isRunning = true;
            _renderer.Initialize();

            while (_isRunning)
            {
                _renderer.RenderFish(_fish);

                if(_food !=null)
                {
                    _renderer.RenderFood(_food);
                }

                // Check for key press
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    HandleKeyPress(key.Key);
                }

                // Game logic
                _fish.MaybeChangeDirection();

                if (_fish.MaybeIdle())
                {
                    Thread.Sleep(1000);
                    continue;
                }

                _fish.Move(Console.WindowWidth, Console.WindowHeight);

                if(_food != null && _fish.fullness < 100)
                {
                    _food.Fall(Console.WindowHeight);

                    _fish.InteractWithFood(_food.X, _food.Y);
                    if (FishTouchesFood(_fish, _food))
                    {
                        _fish.Eat(_food.Satiety);
                        _food = null;
                    }
                }

                Thread.Sleep(200);
            }

            _renderer.Cleanup();
        }

        private void HandleKeyPress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape:
                    _isRunning = false;
                    break;
                case ConsoleKey.F:
                    _food = new Food(_random.Next(Console.WindowWidth));
                    break;
                case ConsoleKey.S:
                    Console.Clear();
                    
                    // Display status and wait for any key
                    _renderer.DisplayFishStatus(_fish);
                    Console.ReadKey(true);
                    break;
                default:
                    break;
            }
        }

        private bool FishTouchesFood( Fish fish, Food food)
        {
            bool xOverlap = ((food.X < fish.X+fish.GetLength()) && (food.X + food.GetLength() > fish.X));
            bool yOverlap = food.Y == fish.Y;
            return xOverlap && yOverlap;
        }
    }
}