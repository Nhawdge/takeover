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

            var allTeams = entities.Where(x => x.GetComponentByType<Allegiance>() != null)
                .Select(x => x.GetComponentByType<Allegiance>().Team)
                .Distinct();

            if (singleton.GetComponentByType<Singleton>().State == Enums.GameStates.InProgress)
            {
                Raylib.DrawText("Teams: " + allTeams.Count(), 10, 10, 12, Color.BLACK);

                if (allTeams.Count() == 1)
                {
                    var team = allTeams.FirstOrDefault();
                    if (team == Enums.Factions.Player)
                    {
                        singleton.GetComponentByType<Singleton>().State = Enums.GameStates.PlayerWin;
                    }
                    else {
                        singleton.GetComponentByType<Singleton>().State = Enums.GameStates.PlayerLose;
                    }
                    singleton.GetComponentByType<Singleton>().WorldGenerated = false;
                }

            }
        }
    }
}