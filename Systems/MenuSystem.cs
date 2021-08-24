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
            else if (data.State == Enums.GameStates.InProgress)
            {
                var pause = DrawButton("Pause", 0, MenuPositions.TopRight);
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    var mousePos = Raylib.GetMousePosition();
                    if (Raylib.CheckCollisionPointRec(mousePos, pause))
                    {
                        data.State = Enums.GameStates.Paused;
                    }
                }

            }
            else if (data.State == Enums.GameStates.Paused)
            {
                var pause = DrawButton("Resume", 0);
                var newGame = DrawButton("New Game", 1);
                var exitRec = DrawButton("Exit Game", 3);

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    var mousePos = Raylib.GetMousePosition();
                    if (Raylib.CheckCollisionPointRec(mousePos, pause))
                    {
                        data.State = Enums.GameStates.InProgress;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, newGame))
                    {
                        engine.Entities.RemoveAll(x => x.GetComponentByType<Singleton>() == null);
                        data.WorldGenerated = false;
                        data.State = Enums.GameStates.InProgress;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, exitRec))
                    {
                        Raylib.EndDrawing();
                        Raylib.CloseWindow();
                        Environment.Exit(0);
                    }
                }
            }
            else if (data.State == Enums.GameStates.PlayerWin || data.State == Enums.GameStates.PlayerLose)
            {
                var text = data.State == Enums.GameStates.PlayerWin ? "You win" : "You Lose";
                Raylib.DrawText(text, Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 24, Color.BLACK);

                var newGame = DrawButton("New Game", 0);
                var exitRec = DrawButton("Exit Game", 2);

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    var mousePos = Raylib.GetMousePosition();

                    if (Raylib.CheckCollisionPointRec(mousePos, newGame))
                    {
                        engine.Entities.RemoveAll(x => x.GetComponentByType<Singleton>() == null);
                        data.WorldGenerated = false;
                        data.State = Enums.GameStates.InProgress;
                    }
                    if (Raylib.CheckCollisionPointRec(mousePos, exitRec))
                    {
                        Raylib.EndDrawing();
                        Raylib.CloseWindow();
                        Environment.Exit(0);
                    }
                }

            }

        }
        private Rectangle DrawButton(string text, int index = 1, MenuPositions pos = MenuPositions.Center)
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
            return box;

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