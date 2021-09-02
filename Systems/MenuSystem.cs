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
            var data = singleton.GetComponentByType<Singleton>();
            if (data.State == Enums.GameStates.Menu)
            {
                DrawButton("Campaign", () =>
                {
                    singleton.GetComponentByType<Singleton>().State = Enums.GameStates.CampaignLevelSelect;
                });
                DrawButton("Random Map", () =>
                {
                    singleton.GetComponentByType<Singleton>().State = Enums.GameStates.InProgress;

                }, 1);
                DrawButton("Exit Game", () =>
                {
                    Raylib.EndDrawing();
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                }, 3);
            }
            else if (data.State == Enums.GameStates.InProgress)
            {
                DrawButton("Pause", () =>
               {
                   data.State = Enums.GameStates.Paused;
               }, 0, MenuPositions.TopRight);
            }
            else if (data.State == Enums.GameStates.Paused)
            {
                DrawButton("Resume", () =>
                {
                    data.State = Enums.GameStates.InProgress;
                });
                DrawButton("New Game", () =>
                {
                    engine.Entities.RemoveAll(x => x.GetComponentByType<Singleton>() == null);
                    data.WorldGenerated = false;
                    data.State = Enums.GameStates.InProgress;
                }, 1);
                DrawButton("Exit Game", () =>
                {
                    Raylib.EndDrawing();
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                }, 3);
            }
            else if (data.State == Enums.GameStates.PlayerWin || data.State == Enums.GameStates.PlayerLose)
            {
                var text = data.State == Enums.GameStates.PlayerWin ? "You win" : "You Lose";
                Raylib.DrawText(text, Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 24, Color.BLACK);

                DrawButton("New Game", () =>
                {
                    engine.Entities.RemoveAll(x => x.GetComponentByType<Singleton>() == null);
                    data.WorldGenerated = false;
                    data.State = Enums.GameStates.InProgress;
                });
                DrawButton("Exit Game", () =>
                {
                    Raylib.EndDrawing();
                    Raylib.CloseWindow();
                    Environment.Exit(0);
                }, 2);
            }
            else if (data.State == Enums.GameStates.CampaignLevelSelect)
            {
                DrawButton("Level 1", () =>
                {
                    data.State = Enums.GameStates.InProgress;
                    data.CampaignLevel = "1";
                });
                DrawButton("Level 2", () =>
                {
                    data.State = Enums.GameStates.InProgress;
                    data.CampaignLevel = "2";
                }, 1);
            }
        }

        private void DrawButton(string text, Action action, int index = 0, MenuPositions pos = MenuPositions.Center)
        {
            const int spacing = 30;
            Vector2 startingCoords;

            var startLength = Raylib.MeasureText(text, spacing);

            switch (pos)
            {
                default:
                case MenuPositions.Center:
                    startingCoords = new Vector2(Raylib.GetScreenWidth() / 4, (Raylib.GetScreenHeight() / 2) + (spacing * index) + 5);
                    break;
                case MenuPositions.TopRight:
                    startingCoords = new Vector2(Raylib.GetScreenWidth() - (startLength + 10), 10 + (spacing * index) + 5);
                    break;
            }

            Rectangle rect = new Rectangle();
            rect.x = startingCoords.X;
            rect.y = startingCoords.Y;
            rect.width = startLength;
            rect.height = spacing;

            var box = new Rectangle((int)rect.x - 4, (int)rect.y - 4, (int)rect.width + 8, (int)rect.height + 8);
            Font font = Raylib.GetFontDefault();

            Raylib.DrawRectangle((int)box.x, (int)box.y, (int)box.width, (int)box.height, Color.GRAY);
            Raylib.DrawTextRec(font, text, rect, 30, 1, false, Color.BEIGE);

            var mousepos = Raylib.GetMousePosition();
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && Raylib.CheckCollisionPointRec(mousepos, box))
            {
                action();
            }
            return;
        }
        public enum MenuPositions
        {
            TopLeft,
            TopCenter,
            TopRight,
            Center
        }
    }

}