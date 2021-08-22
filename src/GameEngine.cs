using System.Collections.Generic;
using Takeover.Components;
using Takeover.Entities;
using Takeover.Systems;

namespace Takeover {
    public class GameEngine {

        private List<Systems.System> Systems { get; set; } = new List<Systems.System>();
        private List<Entity> Entities { get; set; } = new List<Entity>();

        public GameEngine() {
            this.Systems.Add(new RenderSystem());

            var node = new Entity();
            node.Components.Add(new Render());

            this.Entities.Add(node);

        }

        public void GameLoop() {
            foreach (var system in this.Systems) {
                system.UpdateAll(this.Entities);
            }
        }
    }
}