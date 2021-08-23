using System.Collections.Generic;
using Raylib_cs;
using Takeover.Entities;
using Takeover.Components;
using Takeover.Enums;

namespace Takeover.Systems
{
    public class MovementSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.InProgress)
            {
                return;
            }
            foreach (var entity in entities)
            {
                var render = entity.GetComponentByType<Render>();
                var selectable = entity.GetComponentByType<Selectable>();
                if (render == null || selectable == null)
                {
                    continue;
                }
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    //render.Position = Raylib.GetMousePosition();
                    var rect = new Rectangle(render.Position.X, render.Position.Y, render.width, render.height);
                    var isClicked = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect);
                    var team = entity.GetComponentByType<Allegiance>();

                    selectable.IsSelected = false;

                    if (team != null && team.Team == Factions.Player)
                    {

                        if (isClicked)
                        {
                            selectable.IsSelected = true;
                        }

                    }


                }
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
                {
                    var currentSelection = entities.Find(x => x.GetComponentByType<Selectable>()?.IsSelected == true);
                    if (currentSelection != null)
                    {
                        var selectedRender = currentSelection.GetComponentByType<Render>();
                        var pos = Raylib.GetMousePosition();

                        Raylib.DrawLine((int)selectedRender.Position.X + (selectedRender.width / 2), (int)selectedRender.Position.Y + (selectedRender.height / 2), (int)pos.X, (int)pos.Y, Color.ORANGE);

                        var rect = new Rectangle(render.Position.X, render.Position.Y, render.width, render.height);
                        var isTarget = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect);
                        if (isTarget)
                        {
                            var curTarget = currentSelection.GetComponentByType<Target>();
                            if (curTarget != null)
                            {
                                if (currentSelection.Id != entity.Id)
                                {

                                    curTarget.TargetId = entity.Id;
                                }
                            }
                        }

                    }
                }

            }
        }
    }
}