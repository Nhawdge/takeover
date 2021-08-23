using System.Numerics;
using Raylib_cs;

namespace Takeover.Components
{
    public class Render : Component
    {

        public int width { get; set; }
        public int height { get; set; }
        public Vector2 Position { get; set; }
        public Render()
        {
            this.width = 50;
            this.height = 50;
        }
        public Render(int x, int y)
        {
            this.width = 50;
            this.height = 50;

            this.Position = new Vector2(x, y);
        }

    }
}