using System.Collections.Generic;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class RegenerationSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {

            foreach (var entity in entities)
            {
                var health = entity.GetComponentByType<Health>();
                if (health == null)
                {
                    continue;
                }

                if (health.Current < health.Max)
                {
                    if (health.Current > 200)
                    {
                        health.RegenPool += 10;
                    }
                    else if (health.Current > 150)
                    {
                        health.RegenPool += 7;
                    }
                    else if (health.Current > 100)
                    {
                        health.RegenPool += 5;
                    }
                    else if (health.Current > 75)
                    {
                        health.RegenPool += 4;
                    }
                    else if (health.Current > 50)
                    {
                        health.RegenPool += 3;
                    }
                    else if (health.Current > 25)
                    {
                        health.RegenPool += 2;
                    }
                    else if (health.Current > 1)
                    {
                        health.RegenPool += 1;
                    }

                    if (health.RegenPool >= 100)
                    {
                        health.Current += 1;
                        health.RegenPool = 0;
                    }
                }
            }
        }
    }
}