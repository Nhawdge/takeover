using System.Collections.Generic;
using System.Linq;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class WinSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }
            var allTeams = entities.Where(x => x.GetComponentByType<Allegiance>() != null)
            .Select(x => x.GetComponentByType<Allegiance>().Team)
            .Distinct();
            Raylib.DrawText("Teams: " + allTeams.Count(), 10, 10, 12, Color.BLACK);

            if (allTeams.Count() == 1)
            {
                var team = allTeams.FirstOrDefault();
                if (team == Enums.Factions.Player)
                {
                    Raylib.DrawText("YOU WIN", 20, 20, 24, Color.BLACK);
                }
                singleton.GetComponentByType<Singleton>().WorldGenerated = false;
            }

        }
    }
}