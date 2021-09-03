namespace Takeover.Components
{
    public class Health : Component
    {
        public int Max { get; set; }
        public int Current { get; set; }
        public int Generation { get; set; }
        public int RegenPool { get; set; } = 0;
        public Health()
        {
            Max = 150;
            Current = 5;
        }

    }
}