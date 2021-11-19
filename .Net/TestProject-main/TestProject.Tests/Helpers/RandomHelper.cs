using System;

namespace TestProject.Tests.Helpers
{
    public static class RandomHelper
    {
        private static Random _random = new Random();

        public static int GetNumber()
        {
            return _random.Next();
        }
    }
}
