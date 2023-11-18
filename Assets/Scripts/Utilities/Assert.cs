using System;

namespace Utilities
{
    public static class Assert
    {
        public static void IsPositiveOrZero(int value)
        {
            if (value < 0)
                throw new ArgumentException($"Value {value} should be positive or equal zero.");
        }
    }
}