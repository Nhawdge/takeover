using System.Numerics;
using Raylib_cs;

namespace Takeover.Components
{
    public class Render : Component
    {

        public int width { get; set; } = 50;
        public int height { get; set; } = 50;
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Render()
        {
        }
        public Render(int x, int y)
        {
            this.Position = new Vector2(x, y);
        }
        public Render(Vector2 pos)
        {
            this.Position = pos;
        }
    }
}