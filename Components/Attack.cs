using System;
using System.Numerics;

namespace Takeover.Components
{
    public class Attack : Component
    {
        public int Value { get; set; }
        public int Speed { get; set; } = 3;
        public Guid TargetId { get; set; }
        public Vector2 OriginalPosition { get; set; }
    }
}