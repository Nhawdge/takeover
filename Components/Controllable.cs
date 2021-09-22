using Raylib_cs;

namespace Takeover.Components
{
    public class Controllable : Components.Component
    {
        public KeyboardKey Left { get; set; }
        public KeyboardKey Up { get; set; }
        public KeyboardKey Right { get; set; }
        public KeyboardKey Down { get; set; }
        public Controllable()
        {
            this.Left = KeyboardKey.KEY_A;
            this.Up = KeyboardKey.KEY_W;
            this.Right = KeyboardKey.KEY_D;
            this.Down = KeyboardKey.KEY_S;
        }
    }
}