using System;

namespace Takeover.Components
{
    public class Target : Component
    {
        public Guid TargetId { get; set; }
        public int AttackCharge { get; set; }
        public int ChargeThreshold { get; set; } = 100;
        public int TimeOnTarget { get; set; }
    }
}