using System;
using Raylib_cs;

namespace Takeover
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(1024, 800, "Take Over");

            Raylib.SetTargetFPS(30);
            var game = new GameEngine();
            Camera2D camera = new Camera2D();
            camera.zoom = 1;
            
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.BeginMode2D(camera);
                Raylib.ClearBackground(Color.WHITE);

                game.GameLoop();

                Raylib.EndMode2D();
                Raylib.EndDrawing();

            }
            Raylib.CloseWindow();

        }
    }
}