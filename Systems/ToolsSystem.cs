using System.Collections.Generic;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class ToolsSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singletonEntity = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            var singleton = singletonEntity.GetComponentByType<Singleton>();

            switch (singleton.State)
            {
                case Enums.GameStates.InProgress:
                    InProgress(entities, engine);
                    break;
                default:
                    return;

            }


        }

        private void InProgress(List<Entity> entities, GameEngine engine)
        {   

        }
    }
}