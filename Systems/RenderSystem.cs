using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;
using Takeover.Enums;

namespace Takeover.Systems
{
    public class RenderSystem : Systems.System
    {
        private Texture2D playerTexture { get; set; }
        private Texture2D aiTexture { get; set; }
        private Texture2D neutralTexture { get; set; }
        private Texture2D backgroundTexture { get; set; }
        public RenderSystem()
        {
            playerTexture = Raylib.LoadTexture("Assets/house.png");
            aiTexture = Raylib.LoadTexture("Assets/houseViking.png");
            neutralTexture = Raylib.LoadTexture("Assets/tipi.png");
            backgroundTexture = Raylib.LoadTexture("Assets/parchmentBasic.png");
        }
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var source = new Rectangle(0, 0, backgroundTexture.width, backgroundTexture.height);
            Raylib.DrawTexturePro(backgroundTexture, source, new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), new Vector2(0, 0), 0f, Color.ORANGE);

            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }
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
                    Raylib.DrawText($"{health.Current}/{health.Max}({entity.ShortId()}-{render.Position.X},{render.Position.Y} )", (int)render.Position.X, (int)render.Position.Y - 15, 12, Color.BLACK);
                }
                var myTarget = entity.GetComponentByType<Target>();

                var selectable = entity.GetComponentByType<Selectable>();
                if (selectable != null && selectable.IsSelected)
                {
                    var bgcolor = new Color(0, 0, 0, 150);
                    Raylib.DrawRectangle((int)render.Position.X - 2, (int)render.Position.Y - 2, render.width + 4, render.height + 4, bgcolor);
                }
                if (render.RenderType == RenderType.Node)
                {
                    var color = Color.GRAY;
                    var team = entity.GetComponentByType<Allegiance>();
                    var texture = neutralTexture;
                    switch (team.Team)
                    {
                        case (Factions.Player):
                            color = Color.BLUE;
                            texture = playerTexture;
                            break;
                        case (Factions.AI):
                            color = Color.RED;
                            texture = aiTexture;
                            break;
                        default:
                            color = Color.GRAY;
                            texture = neutralTexture;
                            break;
                    }
                    source = new Rectangle(0, 0, texture.width, texture.height);
                    var destination = new Rectangle((int)render.Position.X, (int)render.Position.Y, render.width, render.height);


                    Raylib.DrawTexturePro(texture, source, destination, new Vector2(0, 0), 0f, color);

                    var target = engine.Entities.Find(x => x.Id == myTarget.TargetId);
                    if (target != null)
                    {
                        var targetRender = target.GetComponentByType<Render>();
                        Raylib.DrawLine((int)render.Position.X, (int)render.Position.Y, (int)targetRender.Position.X, (int)targetRender.Position.Y, Color.DARKBLUE);
                    }
                }
                if (render.RenderType == RenderType.Attack)
                {
                    Console.WriteLine($"Rendering {entity.ShortId()} at {render.Position.X},{render.Position.Y}");
                    var myAttack = entity.GetComponentByType<Attack>();
                    var target = engine.Entities.Find(x => x.Id == myAttack.TargetId);
                    if (target == null)
                    {
                        continue;
                    }
                    var targetRender = target.GetComponentByType<Render>();

                    var distance = Utilities.DistanceBetween(render.Position, targetRender.Position);

                    var xdiff = render.Position.X - targetRender.Position.X;
                    var ydiff = render.Position.Y - targetRender.Position.Y;
                    var angleRadians = Math.Atan2(ydiff, xdiff) * (180 / Math.PI);
                    var pos = render.Position;
                    
                    pos.X += (float)Math.Cos(angleRadians) * myAttack.Speed;
                    pos.Y += (float)Math.Sin(angleRadians) * myAttack.Speed;
                    Console.WriteLine($"{angleRadians} between {xdiff}, {ydiff}, new position is {pos.X},{pos.Y}");
                    render.Position = pos;

                    Raylib.DrawText($"{entity.ShortId()}-{render.Position.X},{render.Position.Y} )", (int)render.Position.X, (int)render.Position.Y - 15, 12, Color.BLACK);

                    Raylib.DrawCircle((int)render.Position.X, (int)render.Position.Y, 5, Color.BLACK);

                }
            }
        }
    }
}
