using System.Collections.Generic;
using Takeover.Entities;

namespace Takeover.Systems
{
    public abstract class System
    {
        public abstract void UpdateAll(List<Entity> entities, GameEngine engine);

    }
}