using System;
using Raylib_cs;

namespace Takeover {
    class Program {
        static void Main(string[] args) {
            Raylib.InitWindow(1024, 800, "Take Over");

            Raylib.SetTargetFPS(30);
            var game = new GameEngine();

            while (!Raylib.WindowShouldClose()) {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                game.GameLoop();

                Raylib.EndDrawing();

            }
            Raylib.CloseWindow();

        }
    }
}