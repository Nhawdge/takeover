using System.Collections.Generic;

namespace Takeover.Systems {
    public abstract class System {
        public abstract void UpdateAll(List<Entities.Entity> entities);

    }
}