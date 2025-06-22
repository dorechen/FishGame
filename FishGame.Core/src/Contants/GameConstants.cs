using System;

namespace FishGame.Constants
{
    public static class GameConstants{
        public static class Hunger
        {
            // <summary>
            // amount of fullness decrease per hunger cycle
            public const int DecreaseAmount = 5;

            public const int DecreaseInterval = 10; // minutes

            // TODO: what is TimeSpan, what is the difference between static and const?
            public static readonly TimeSpan DecreaseIntervalTimeSpan = TimeSpan.FromMinutes(DecreaseInterval);

            // public const int StartingFullness = 50;

            public const int MaxFullness = 100;
            
        }
        public static class Food
        {
            // amound of fullness standard food fills
            // public const int StandardSatiety = 10;
         }
        public static class Timing
        {
            public static readonly TimeSpan SaveIntervalTimeSpan = TimeSpan.FromMinutes(1);

            public const int GameLoopDelayMS = 200;
        }
    }

}