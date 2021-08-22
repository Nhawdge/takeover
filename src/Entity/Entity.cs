using System;

namespace Takeover.Entity {
    public class Entity {

        public Guid Id { get; private set; }
        public Entity(Guid id) {
            this.Id = id;
        }
    }
}