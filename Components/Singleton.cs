using Takeover.Enums;

namespace Takeover.Components
{
    public class Singleton : Component
    {
        public GameStates State { get; set; } = GameStates.Menu;

        public bool WorldGenerated = false;
    }
}