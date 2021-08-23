using System;
using System.Collections.Generic;
using System.Linq;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class AISystem : Systems.System
    {
        Random Random { get; set; } = new Random();
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            foreach (var entity in entities)
            {
                var team = entity.GetComponentByType<Allegiance>();
                if (team == null) continue;
                if (team.Team == Enums.Factions.AI)
                {
                    var myTarget = entity.GetComponentByType<Target>();
                    if (myTarget != null && myTarget.TargetId == Guid.Empty)
                    {
                        var targets = GetAllHostileTargets(entity, entities);
                        var index = Random.Next(0, targets.Count());
                        myTarget.TargetId = targets.ElementAt(index).Id;
                    }
                }
            }

        }
        private IEnumerable<Entity> GetAllHostileTargets(Entity entity, IEnumerable<Entity> entities)
        {
            var myTeam = entity.GetComponentByType<Allegiance>();

            return entities.Where(x => x.GetComponentByType<Allegiance>()?.Team != myTeam.Team);

        }
    }
}