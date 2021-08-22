using System;
using System.Collections.Generic;
using Takeover.Components;

namespace Takeover.Entities {
    public class Entity {

        public Guid Id { get; private set; }
        public List<Component> Components { get; set; } = new List<Component>();

        public Entity() {
            this.Id = Guid.NewGuid();
        }

        public Entity(Guid id) {

            this.Id = id;
        }

    }
}