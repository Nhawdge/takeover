using System;
using System.Collections.Generic;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;
using Takeover.Systems;

namespace Takeover
{
    public class GameEngine
    {

        private List<Systems.System> Systems { get; set; } = new List<Systems.System>();
        public List<Entity> Entities { get; set; } = new List<Entity>();
        public Camera2D Camera { get; set; }

        public GameEngine(Camera2D camera)
        {
            this.Camera = camera;

            this.Systems.Add(new LevelGeneratorSystem());
            this.Systems.Add(new RenderSystem());
            this.Systems.Add(new MovementSystem());
            this.Systems.Add(new AttackSystem());
            this.Systems.Add(new RegenerationSystem());
            this.Systems.Add(new WinSystem());
            this.Systems.Add(new AISystem());
            this.Systems.Add(new MenuSystem());

            var singleton = new Entity();
            singleton.Components.Add(new Singleton());

            this.Entities.Add(singleton);
        }

        public void GameLoop()
        {
            foreach (var system in this.Systems)
            {
                system.UpdateAll(this.Entities, this);
            }
        }
    }
}