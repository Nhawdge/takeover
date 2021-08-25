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
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }
            foreach (var entity in entities)
            {
                var target = entity.GetComponentByType<Target>();
                if (target != null && target.TargetId != null)
                {
                    var hp = entity.GetComponentByType<Health>();

                    var targetNode = entities.Find(x => x.Id == target.TargetId);
                    if (targetNode != null)
                    {
                        target.AttackCharge += GetAttackChargeRate(hp);

                        var sourceRender = entity.GetComponentByType<Render>();
                        var targetRender = targetNode.GetComponentByType<Render>();

                        if (sourceRender != null && targetRender != null)
                        {
                            var sourceCenterX = (int)sourceRender.Position.X + (sourceRender.width / 2);
                            var sourceCenterY = (int)sourceRender.Position.Y + (sourceRender.height / 2);

                            var targetCenterX = (int)targetRender.Position.X + (targetRender.width / 2);
                            var targetCenterY = (int)targetRender.Position.Y + (targetRender.height / 2);

                            Raylib.DrawLine(sourceCenterX, sourceCenterY, targetCenterX, targetCenterY, Color.DARKBLUE);

                            var circlex = sourceCenterX + ((targetCenterX - sourceCenterX) * ((float)target.AttackCharge / target.ChargeThreshold));
                            var circley = sourceCenterY + ((targetCenterY - sourceCenterY) * ((float)target.AttackCharge / target.ChargeThreshold));
                            Raylib.DrawCircle((int)circlex, (int)circley, 5, Color.BLACK);

                        }
                        if (target.AttackCharge > target.ChargeThreshold)
                        {
                            var targetHP = targetNode.GetComponentByType<Health>();
                            var team = entity.GetComponentByType<Allegiance>();
                            var tarTeam = targetNode.GetComponentByType<Allegiance>();

                            if (tarTeam != null && team != null && targetHP != null)
                            {
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
                                    targetHP.Current = 0;
                                }
                                if (targetHP.Current > targetHP.Max)
                                {
                                    targetHP.Current = targetHP.Max;
                                }
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
    }
}