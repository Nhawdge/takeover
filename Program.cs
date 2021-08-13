using System;
using SDL2;

namespace Takeover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started!");
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            var window = IntPtr.Zero;

            window = SDL.SDL_CreateWindow("Test",
            SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                1024,
                800,
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );
            var renderer = SDL.SDL_CreateRenderer(window, 0, 0);

            // var img = SDL.SDL_LoadFile("200.png",);

            SDL.SDL_Event evt;
            bool quit = false;

            while (!quit)
            {
                while (SDL.SDL_PollEvent(out evt) != 0)
                {
                    switch (evt.type)
                    {
                        case (SDL.SDL_EventType.SDL_QUIT):
                            quit = true;
                            break;
                        case (SDL.SDL_EventType.SDL_KEYDOWN):
                            switch (evt.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_q:
                                    quit = true;
                                    break;
                                case SDL.SDL_Keycode.SDLK_f:
                                    SDL.SDL_SetRenderDrawColor(renderer, 155, 155, 150, 0);
                                    SDL.SDL_RenderDrawLine(renderer, 110, 210, 310, 410);

                                    SDL.SDL_RenderPresent(renderer);
                                    break;
                                case SDL.SDL_Keycode.SDLK_g:
                                    SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 1);
                                    SDL.SDL_RenderDrawLine(renderer, 100, 400, 200, 300);

                                    SDL.SDL_RenderPresent(renderer);
                                    break;
                            }
                            break;
                    }
                }
            }

            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }
    }
}
