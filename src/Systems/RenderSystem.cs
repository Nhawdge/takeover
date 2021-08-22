using System.Collections.Generic;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems {
    public class RenderSystem : Systems.System {
        public override void UpdateAll(List<Entity> entities) {
            foreach (var entity in entities) {
                Render render = entity.GetComponentByType<Render>();
                if (render == null) {
                    return;
                }
                Raylib.DrawRectangle(render.X, render.Y, render.width, render.height, render.Color);
                render.X += 1;
                render.Y += 1;
            }
        }
    }
}