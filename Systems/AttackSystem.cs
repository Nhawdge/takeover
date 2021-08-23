using System;
using System.Collections.Generic;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class AttackSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            foreach (var entity in entities)
            {
                var target = entity.GetComponentByType<Target>();
                if (target != null && target.TargetId != null)
                {
                    var hp = entity.GetComponentByType<Health>();
                    target.AttackCharge += GetAttackChargeRate(hp);

                    var targetNode = entities.Find(x => x.Id == target.TargetId);
                    if (targetNode != null)
                    {
                        var sourceRender = entity.GetComponentByType<Render>();
                        var targetRender = targetNode.GetComponentByType<Render>();

                        if (sourceRender != null && targetRender != null)
                        {
                            Raylib.DrawLine(
                                (int)sourceRender.Position.X + (sourceRender.width / 2),
                                (int)sourceRender.Position.Y + (sourceRender.height / 2),
                                (int)targetRender.Position.X + (targetRender.width / 2),
                                (int)targetRender.Position.Y + (targetRender.height / 2),
                                  Color.DARKBLUE);
                        }
                        if (target.AttackCharge > target.ChargeThreshold)
                        {
                            var targetHP = targetNode.GetComponentByType<Health>();
                            var team = entity.GetComponentByType<Allegiance>();
                            var tarTeam = targetNode.GetComponentByType<Allegiance>();

                            if (team.Team == tarTeam.Team)
                            {
                                targetHP.Current += 1;
                            }
                            else
                            {
                                targetHP.Current -= 1;
                            }

                            target.AttackCharge = 0;

                            if (targetHP.Current <= 0)
                            {
                                tarTeam.Team = team.Team;
                            }
                        }
                    }
                }
            }
        }
        private int GetAttackChargeRate(Health health)
        {
            if (health.Current > 100)
            {
                return 3;
            }
            else if (health.Current > 50)
            {
                return 2;
            }
            return 1;

        }
    }
}