using Raylib_cs;
using System.Numerics;
using FlappyBird.Klasy;

namespace FlappyBird
{
    public class Game
    {
        private Bird bird;
        private GameStatus gameStatus;
        private const int screenWidth = 800;
        private const int screenHeight = 600;
        private float gravity = 0.5f;
        private int countdown = 3;
        private float countdownTimer = 0;
        private int score = 0;

        public void Initialize()
        {
            Raylib.InitWindow(screenWidth, screenHeight, "Flappy Bird Game");
            Raylib.SetTargetFPS(60);
            bird = new Bird(new Vector2(screenWidth / 4, screenHeight / 2), gravity);

            gameStatus = GameStatus.Ready;
            countdown = 3; 
            countdownTimer = 0;
            score = 0;
        }

        public void Update()
        {
            if (gameStatus == GameStatus.Ready)
            {
                countdownTimer += Raylib.GetFrameTime();
                if (countdownTimer >= 1)
                {
                    countdown--;
                    countdownTimer = 0;
                }

                if (countdown <= 0)
                {
                    gameStatus = GameStatus.Playing;
                }
            }
            else if (gameStatus == GameStatus.Playing)
            {
                bird.Update();

                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    bird.Flap();
                }


                if (bird.CheckCollisionWithGround(screenHeight))
                {
                    gameStatus = GameStatus.GameOver;
                }
            }

        }

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.SkyBlue);

            if (gameStatus == GameStatus.Ready)
            {
                Raylib.DrawText(countdown.ToString(), screenWidth / 2 - 20, screenHeight / 2 - 20, 60, Raylib_cs.Color.Black);
            }
            else
            {
                bird.Draw();
                if (gameStatus == GameStatus.GameOver)
                {
                    Raylib.DrawText("KONIEC GRY", screenWidth / 2 - 200, screenHeight / 2 - 50, 70, Raylib_cs.Color.Red);
                    Raylib.DrawText("Nacisnij Enter aby zrestartowac", screenWidth / 2 - 230, screenHeight / 2 + 30, 30, Raylib_cs.Color.White);
                }
                Raylib.DrawText("Punkty: " + score, 10, 10, 30, Raylib_cs.Color.Black);
            }

            Raylib.EndDrawing();
        }

        public void Run()
        {
            Initialize();

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
            }

            Raylib.CloseWindow();
        }
    }
}