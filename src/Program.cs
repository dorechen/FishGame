using System;
using FishGame.Services;

namespace FishGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameEngine();
            game.Start();
        }
    }
}
