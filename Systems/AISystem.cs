using System;
using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class AISystem : Systems.System
    {
        Random Random { get; set; } = new Random();
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }
            foreach (var entity in entities)
            {
                var team = entity.GetComponentByType<Allegiance>();
                if (team == null) continue;
                if (team.Team == Enums.Factions.AI)
                {
                    var myTarget = entity.GetComponentByType<Target>();
                    if (myTarget == null) continue;
                    myTarget.TimeOnTarget += 1;
                    var targets = GetAllHostileTargets(entity, entities);
                    if (myTarget.TargetId == Guid.Empty)
                    {
                        var index = Random.Next(0, targets.Count());
                        myTarget.TargetId = targets.ElementAt(index).Id;
                        continue;
                    }
                    var target = entities.Find(x => x.Id == myTarget.TargetId);
                    if (target == null)
                        continue;

                    var targetHp = target.GetComponentByType<Health>();
                    var targetteam = target.GetComponentByType<Allegiance>();

                    var myTeam = entity.GetComponentByType<Allegiance>();
                    var priorityTargets = entities.Where(x => x.GetComponentByType<Health>() != null)
                    .ToList().Select(x =>
                    {
                        var priority = 1;
                        var team = x.GetComponentByType<Allegiance>();
                        var hp = x.GetComponentByType<Health>();
                        if (team.Team == myTeam.Team)
                        {
                            if (hp.Current < 10)
                            {
                                priority += 10;
                            }
                            if (hp.Current < 20){
                                
                            }
                            if (hp.Current > 100)
                            {
                                priority -= 10;
                            }
                        }
                        else
                        {
                            if (hp.Current < 10)
                            {
                                priority += 5;
                            }
                            if (hp.Current > 100)
                            {
                                priority -= 5;
                            }
                        }
                        return new { x.Id, priority };
                    });

                    if (priorityTargets.Count() >= 1)
                    {
                        myTarget.TargetId = priorityTargets.ElementAtOrDefault(0).Id;
                    }
                }
            }
        }
        private IEnumerable<Entity> GetAllHostileTargets(Entity entity, IEnumerable<Entity> entities)
        {
            var myTeam = entity.GetComponentByType<Allegiance>();

            return entities.Where(x => x.GetComponentByType<Singleton>() == null && x.GetComponentByType<Allegiance>()?.Team != myTeam.Team);

        }
    }
}