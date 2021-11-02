using System;

namespace Takeover.Components
{
    public class TowerSecurity : Components.Component
    {
        public TowerSecurity Security { get; set; }

        public Guid RelatedTowerId { get; set; }

    }
}