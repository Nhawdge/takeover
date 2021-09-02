using System.Collections.Generic;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class RegenerationSystem : Systems.System
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
                var health = entity.GetComponentByType<Health>();
                var team = entity.GetComponentByType<Allegiance>();

                if (health == null || team == null)
                {
                    continue;
                }

                if (health.Current < health.Max)
                {
                    if (health.Current > 200 && team.Team != Enums.Factions.Neutral)
                    {
                        health.RegenPool += 10;
                    }
                    else if (health.Current > 150 && team.Team != Enums.Factions.Neutral)
                    {
                        health.RegenPool += 7;
                    }
                    else if (health.Current > 100 && team.Team != Enums.Factions.Neutral)
                    {
                        health.RegenPool += 5;
                    }
                    else if (health.Current > 75 && team.Team != Enums.Factions.Neutral)
                    {
                        health.RegenPool += 4;
                    }
                    else if (health.Current > 50 && team.Team != Enums.Factions.Neutral)
                    {
                        health.RegenPool += 3;
                    }
                    else if (health.Current > 25 && team.Team != Enums.Factions.Neutral)
                    {
                        health.RegenPool += 2;
                    }
                    else
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