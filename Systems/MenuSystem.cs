using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;

namespace Takeover.Systems
{
    public class MenuSystem : Systems.System
    {
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            if (singleton.GetComponentByType<Singleton>().State != Enums.GameStates.Menu)
            {
                return;
            }

            var startRec = DrawButton("Start Game", 1);
            var exitRec = DrawButton("Exit Game", 2);
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                var mousePos = Raylib.GetMousePosition();
                if (Raylib.CheckCollisionPointRec(mousePos, startRec))
                {
                    singleton.GetComponentByType<Singleton>().State = Enums.GameStates.InProgress;
                }
                if (Raylib.CheckCollisionPointRec(mousePos, exitRec))
                {
                    Raylib.EndDrawing();
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                }
            }
        }
        private Rectangle DrawButton(string text, int index = 1)
        {
            const int spacing = 30;
            var startingCoods = new Vector2(Raylib.GetScreenWidth() / 4, (Raylib.GetScreenHeight() / 2) + (spacing * index) + 5);
            var startLength = Raylib.MeasureText(text, spacing);
            Rectangle rect = new Rectangle();

            rect.x = startingCoods.X;
            rect.y = startingCoods.Y;
            rect.width = startLength;
            rect.height = spacing;

            Font font = Raylib.GetFontDefault();

            Raylib.DrawRectangle((int)rect.x - 4, (int)rect.y - 4, (int)rect.width + 8, (int)rect.height + 8, Color.GRAY);
            Raylib.DrawTextRec(font, text, rect, 30, 1, false, Color.BEIGE);
            return rect;

        }
    }

}