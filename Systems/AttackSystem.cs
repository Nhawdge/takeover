using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;
using Takeover.Enums;

namespace Takeover.Systems
{
    public class AttackSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }
            var toAdd = new List<Entity>();
            var toRemove = new List<Entity>();
            foreach (var entity in entities)
            {
                var myTarget = entity.GetComponentByType<Target>();
                var myRender = entity.GetComponentByType<Render>();
                var myTeam = entity.GetComponentByType<Allegiance>();
                if (myTarget != null && myRender != null && myTeam != null)
                {
                    if (myTarget.TargetId != Guid.Empty)
                    {
                        myTarget.AttackCharge++;
                    }

                    if (myTarget.AttackCharge > myTarget.ChargeThreshold)
                    {
                        var attack = CreateNewAttack(myRender.Position, myTeam.Team, 1, myTarget.TargetId);
                        Console.WriteLine($"{entity.ShortId()} is creating {attack.ShortId()}, at {myRender.Position.X},{myRender.Position.Y}");
                        myTarget.AttackCharge -= myTarget.ChargeThreshold;
                        toAdd.Add(attack);
                    }
                }
                var myAttack = entity.GetComponentByType<Attack>();
                if (myAttack != null && myAttack.TargetId != null)
                {
                    var targetEntity = engine.Entities.Find(x => x.Id == myAttack.TargetId);
                    if (targetEntity == null)
                    {
                        continue;
                    }
                    var targetRender = targetEntity.GetComponentByType<Render>();
                    var hit = Raylib.CheckCollisionCircleRec(myRender.Position, 5f, new Rectangle(targetRender.Position.X, targetRender.Position.Y, targetRender.width, targetRender.height));
                    if (hit)
                    {
                        var targetTeam = targetEntity.GetComponentByType<Allegiance>();
                        var targetHealth = targetEntity.GetComponentByType<Health>();
                        if (myTeam.Team == targetTeam.Team)
                        {
                            targetHealth.Current += myAttack.Value;
                        }
                        else
                        {
                            targetHealth.Current -= myAttack.Value;
                        }
                        toRemove.Add(entity);
                        if (targetHealth.Current <= 0)
                        {
                            targetTeam.Team = myTeam.Team;
                        }
                    }
                }

            }
            engine.Entities.AddRange(toAdd);
            foreach (var entity in toRemove)
            {
                engine.Entities.Remove(entity);
            }
        }
        private int GetAttackChargeRate(Health health)
        {
            if (health.Current > 100)
            {
                return 5;
            }
            else if (health.Current > 50)
            {
                return 3;
            }
            else if (health.Current > 25)
            {
                return 2;
            }
            return 1;

        }

        private Entity CreateNewAttack(Vector2 origin, Factions faction, int value, Guid targetId)
        {
            var entity = new Entity();
            var render = new Render(origin) { RenderType = RenderType.Attack };
            var allegiance = new Allegiance(faction);
            var attack = new Attack { Value = value, TargetId = targetId, OriginalPosition = origin };
            entity.Components.Add(render);
            entity.Components.Add(allegiance);
            entity.Components.Add(attack);
            return entity;
        }
    }
}