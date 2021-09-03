using System;
using System.Numerics;

namespace Takeover
{
    public static class Utilities
    {
        public static int DistanceBetween(Vector2 a, Vector2 b)
        {
            var xDiff = a.X - b.X;
            var ydiff = a.Y - b.Y;
            var aSq = Math.Pow(xDiff, 2);
            var bSq = Math.Pow(ydiff, 2);
            var c = Math.Sqrt(aSq + bSq);
            return (int)c;
        }
    }
}