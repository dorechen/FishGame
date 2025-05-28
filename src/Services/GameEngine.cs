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

        public GameEngine()
        {
            _fish = new Fish(10, 10, true, ConsoleColor.Cyan);
            _renderer = new ConsoleRenderer();
        }

        public void Start()
        {
            _isRunning = true;
            _renderer.Initialize();

            while (_isRunning)
            {
                _renderer.Render(_fish);

                // Check for key press
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        _isRunning = false;
                    }
                }

                // Game logic
                _fish.MaybeChangeDirection();

                if (_fish.MaybeIdle())
                {
                    Thread.Sleep(1000);
                    continue;
                }

                _fish.Move(Console.WindowWidth);

                Thread.Sleep(500);
            }

            _renderer.Cleanup();
        }
    }
}