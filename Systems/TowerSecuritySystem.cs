using System.Collections.Generic;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class TowerSecuritySystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            var singletonData = singleton.GetComponentByType<Singleton>();
            switch (singletonData.State)
            {
                case Enums.GameStates.InProgress:
                case Enums.GameStates.Paused:
                default:
                    InProgress(entities, engine);
                    break;

            }
        }
        private void InProgress(List<Entity> entities, GameEngine engine)
        {
            foreach (var entity in entities)
            {
                var mySecurity = entity.GetComponentByType<TowerSecurity>();
                if (mySecurity == null)
                    return;

                    

            }
        }
    }
}