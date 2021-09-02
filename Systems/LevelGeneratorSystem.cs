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

            var singletonData = singleton.GetComponentByType<Singleton>();
            var toAdd = new List<Entity>();

            foreach (var entity in entities)
            {
                if (singleton != null && singletonData.WorldGenerated == false)
                {
                    toAdd.AddRange(GenerateNodes(Factions.AI, 2));
                    toAdd.AddRange(GenerateNodes(Factions.Neutral, 10));
                    toAdd.AddRange(GenerateNodes(Factions.Player, 2));
                    singletonData.WorldGenerated = true;
                }
            }
            engine.Entities.AddRange(toAdd);

        }
        private IEnumerable<Entity> GenerateNodes(Factions faction, int count = 1)
        {
            var toAdd = new List<Entity>();

            for (var i = 0; i < count; i++)
            {
                var random = new Random();

                var node = new Entity();
                var width = Raylib.GetScreenWidth();
                var height = Raylib.GetScreenHeight();

                var render = new Render(random.Next(60, width - 60), random.Next(60, height - 60));

                node.Components.Add(render);

                node.Components.Add(new Selectable());
                node.Components.Add(new Target());
                node.Components.Add(new Health());

                node.Components.Add(new Allegiance(faction));

                toAdd.Add(node);
            }
            return toAdd;
        }
    }
}