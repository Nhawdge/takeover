using System.Collections.Generic;
using Raylib_cs;
using Takeover.Entities;
using Takeover.Components;
using Takeover.Enums;

namespace Takeover.Systems
{
    public class MovementSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }
            foreach (var entity in entities)
            {

                var myControllable = entity.GetComponentByType<Controllable>();
                var myRender = entity.GetComponentByType<Render>();
                if (myControllable == null || myRender == null)
                {
                    continue;
                }
                var pos = myRender.Position;
                if (Raylib.IsKeyDown(myControllable.Left))
                {
                    pos.X -= 2;
                }
                if (Raylib.IsKeyDown(myControllable.Up))
                {
                    pos.Y -= 2;
                }
                if (Raylib.IsKeyDown(myControllable.Right))
                {
                    pos.X += 2;
                }
                if (Raylib.IsKeyDown(myControllable.Down))
                {
                    pos.Y += 2;
                }
                myRender.Position = pos;
            }
        }
    }
}