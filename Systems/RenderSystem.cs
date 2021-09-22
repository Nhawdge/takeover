using System.Net.Mime;
using System.Text.RegularExpressions;
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
                    Raylib.DrawText($"{health.Current}/{health.Max}", (int)render.Position.X, (int)render.Position.Y - 15, 12, Color.BLACK);
                }
                var target = entity.GetComponentByType<Target>();

                var selectable = entity.GetComponentByType<Selectable>();
                if (selectable != null && selectable.IsSelected)
                {
                    var bgcolor = new Color(0, 0, 0, 150);
                    Raylib.DrawRectangle((int)render.Position.X - 2, (int)render.Position.Y - 2, render.width + 4, render.height + 4, bgcolor);
                }

                var color = Color.GRAY;
                var team = entity.GetComponentByType<Allegiance>()?.Team ?? Enums.Factions.Neutral;
                var texture = render.Texture;

                source = new Rectangle(0, 0, texture.width, texture.height);
                var destination = new Rectangle((int)render.Position.X, (int)render.Position.Y, render.width, render.height);

                var rotation = 0f;
                var origin = new Vector2(0, 0);

                var myControllable = entity.GetComponentByType<Controllable>();
                if (myControllable != null)
                {
                    var mousePos = Raylib.GetMousePosition();
                    var myPos = render.Position;
                    var xDiff = myPos.X - mousePos.X;
                    var yDiff = myPos.Y - mousePos.Y;
                    var angle = Math.Atan2(yDiff, xDiff) * (180 / Math.PI);
                    rotation = (float)angle;
                    origin = new Vector2(render.width / 2, render.height / 2);
                    Shader shader = new Shader();

                    var value = 100;
                    Raylib.SetShaderValue(shader, 10, ref value, ShaderUniformDataType.SHADER_UNIFORM_INT);

                }

                Raylib.DrawTexturePro(texture, source, destination, origin, rotation, color);
            }
        }
    }
}
