using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using Raylib_cs;

namespace Takeover.Components {
    public class Render : Component {
        public int X { get; set; }
        public int Y { get; set; }

        public int width { get; set; }
        public int height { get; set; }
        public Color Color { get; set; }

        public Render(int x, int y) {
            this.X = x;
            this.Y = y;
            this.width = 50;
            this.height = 50;
            Color = Color.BLACK;
        }

        public Render() {
            this.X = 0;
            this.Y = 0;
            this.width = 50;
            this.height = 50;
            Color = Color.BLACK;
        }

    }
}