using System;
using System.Collections.Generic;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;
using Takeover.Enums;

namespace Takeover.Systems
{
    public class RenderSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            foreach (var entity in entities)
            {
                var render = entity.GetComponentByType<Render>();
                if (render == null)
                {
                    continue;
                }
                var health = entity.GetComponentByType<Health>();
                if (health != null)
                {
                    Raylib.DrawText($"{health.Current}/{health.Max}", (int)render.Position.X, (int)render.Position.Y - 15, 12, Color.BLACK);
                }

                var selectable = entity.GetComponentByType<Selectable>();
                if (selectable != null && selectable.IsSelected)
                {
                    Raylib.DrawRectangle((int)render.Position.X - 2, (int)render.Position.Y - 2, render.width + 4, render.height + 4, Color.BLACK);

                }

                var color = Color.GRAY;
                var team = entity.GetComponentByType<Allegiance>();
                switch (team.Team)
                {
                    case (Factions.Player):
                        color = Color.BLUE;
                        break;
                    case (Factions.AI):
                        color = Color.RED;
                        break;
                    default:
                        color = Color.GRAY;
                        break;
                }

                Raylib.DrawRectangle((int)render.Position.X, (int)render.Position.Y, render.width, render.height, color);

            }

        }
    }
}
