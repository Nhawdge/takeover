using Takeover.Enums;

namespace Takeover.Components
{
    public class Allegiance : Component
    {
        public Factions Team { get; set; }
        public Allegiance(Factions allegiance)
        {
            this.Team = allegiance;
        }
    }
}