using System;
using System.Numerics;

namespace Takeover.Helpers
{
    public static class GeneralHelpers
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