using System;
using System.Collections.Generic;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;
using Takeover.Enums;

namespace Takeover.Systems
{
    public class LevelGeneratorSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }

            var toAdd = new List<Entity>();
            var singletonData = singleton.GetComponentByType<Singleton>();

            foreach (var entity in entities)
            {
                if (singleton != null && singletonData.WorldGenerated == false)
                {
                    var random = new Random();
                    for (var i = 0; i < 5; i++)
                    {
                        var node = new Entity();
                        var width = Raylib.GetScreenWidth();
                        var height = Raylib.GetScreenHeight();

                        var render = new Render(random.Next(0, width - 60), random.Next(0, height - 60));

                        node.Components.Add(render);

                        node.Components.Add(new Selectable());
                        node.Components.Add(new Target());
                        node.Components.Add(new Health());

                        var faction = (Factions)random.Next(0, 3);
                        node.Components.Add(new Allegiance(faction));

                        toAdd.Add(node);
                    }
                    singletonData.WorldGenerated = true;
                }
            }
            engine.Entities.AddRange(toAdd);
        }
    }
}