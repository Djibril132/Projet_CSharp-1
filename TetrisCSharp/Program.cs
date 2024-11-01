


namespace Raylib_cs
{
    class TetrisGame
    {
        static void Main()
        {
            /* Screen variables */
            const int screenWidth = 1500;
            const int screenHeight = 800;
            int currentFrame = 0;

            /* Game variables */
            string currentScene = "menu";
            int[ , ] grid = new int[10, 20];
            int gridInitialX = screenWidth/2 - 30*5;
            int gridInitialY = 100;
            int[ , ] shapeO = {{0, 1, 1, 0}, {0, 1, 1, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}};
            int[ , ] shapeI = {{0, 0, 1, 0}, {0, 0, 1, 0}, {0, 0, 1, 0}, {0, 0, 1, 0}};
            int[ , ] shapeT = {{0, 0, 1, 0}, {0, 1, 1, 1}, {0, 0, 0, 0}, {0, 0, 0, 0}};
            int[ , ] shapeL = {{0, 1, 0, 0}, {0, 1, 0, 0}, {0, 1, 1, 0}, {0, 0, 0, 0}};
            int[ , ] shapeJ = {{0, 0, 1, 0}, {0, 0, 1, 0}, {0, 1, 1, 0}, {0, 0, 0, 0}};
            int[ , ] shapeZ = {{0, 1, 1, 0}, {0, 0, 1, 1}, {0, 0, 0, 0}, {0, 0, 0, 0}};
            int[ , ] shapeN = {{0, 0, 1, 1}, {0, 1, 1, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}};

            int[ , ] shapeNull = new int[4, 4];

            int[][ , ] shapes = { shapeO, shapeI, shapeT, shapeL, shapeJ, shapeZ, shapeN };
            int[ , ] currentPiece = shapeNull;

            int pieceX = gridInitialX + 30*3;
            int pieceY = gridInitialY;

            bool gameStarted = false;
            int score = 0;

            int gameSpeed = 10;
            int timeUntilLocked = 5;
            int moveSpeed = 4;
            int moveTimer = 0;


            Raylib.InitWindow(screenWidth, screenHeight, "Tetris");
            Raylib.SetTargetFPS(60);
            

            while (!Raylib.WindowShouldClose())
            {
                
                switch (currentScene)
                {
                    case "menu":
                        if (Raylib.IsKeyPressed(KeyboardKey.Space))
                        {
                            currentScene = "game";
                        }
                        break;
                    case "game":
                        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                        {
                            currentScene = "menu";
                        }
                        break;
                }



                Raylib.BeginDrawing();
                
                switch (currentScene)
                {
                    case "menu":
                        Raylib.ClearBackground(Color.Purple);
                        Raylib.DrawText("Tetris", screenWidth/2 - (Raylib.MeasureText("Tetris", 60)/2), 250, 60, Color.Black);
                        Raylib.DrawText("Press Space to start", screenWidth/2 - (Raylib.MeasureText("Press Space to start", 30)/2), 500, 30, Color.Black);
                        break;
                    case "game":
                        Raylib.ClearBackground(Color.Purple);
                        Draw.DrawGrid(grid, gridInitialX, gridInitialY);
                        Draw.DrawFrame(gridInitialX, gridInitialY);
                        Draw.DrawNextPieceFrame(gridInitialX, gridInitialY);
                        Draw.DrawHeldPieceFrame(gridInitialX, gridInitialY);
                        Draw.DrawScore(score, gridInitialX, gridInitialY);
                        
                        currentFrame++;
                        if (!gameStarted)
                        {
                            if (currentFrame < 60)
                            {
                                Draw.DrawCountdown(3, screenWidth/2 - 15, screenHeight/2 - 15);
                            }
                            else if (currentFrame < 120)
                            {
                                Draw.DrawCountdown(2, screenWidth/2 - 15, screenHeight/2 - 15);
                            }
                            else if (currentFrame < 180)
                            {
                                Draw.DrawCountdown(1, screenWidth/2 - 15, screenHeight/2 - 15);
                            }
                            else
                            {
                                gameStarted = true;
                                currentFrame = 0;
                            }
                        }
                        else
                        {
                            // Game logic
                            if (currentFrame%gameSpeed == 0)
                            {
                                if (!Calculate.CollisionDown(currentPiece, pieceY, gridInitialY) && !Calculate.CollisionGridDown(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceY += 30;
                                }
                                else
                                {
                                    timeUntilLocked--;
                                }
                            }

                            if (timeUntilLocked == 0)
                            {
                                for (int i = 0; i < currentPiece.GetLength(0); i++)
                                {
                                    for (int j = 0; j < currentPiece.GetLength(1); j++)
                                    {
                                        if (currentPiece[j, i] == 1)
                                        {
                                            grid[(pieceX - gridInitialX)/30 + i, (pieceY - gridInitialY)/30 + j] = 1;
                                            Draw.DrawGrid(grid, gridInitialX, gridInitialY);
                                        }
                                    }
                                }
                                currentPiece = shapeNull;
                                pieceX = gridInitialX + 30*3;
                                pieceY = gridInitialY;
                                timeUntilLocked = 5;
                            }

                            Calculate.CheckLines(grid, ref score, ref gameSpeed);
                            Draw.DrawScore(score, gridInitialX, gridInitialY);

                            if (moveTimer > 0)
                            {
                                moveTimer--;
                            }

                            if (Raylib.IsKeyDown(KeyboardKey.Right) && moveTimer == 0)
                            {
                                if (!Calculate.CollisionRight(currentPiece, pieceX, gridInitialX) && !Calculate.CollisionGridRight(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceX += 30;
                                    moveTimer = moveSpeed;
                                }
                            }

                            if (Raylib.IsKeyDown(KeyboardKey.Left) && moveTimer == 0)
                            {
                                if (!Calculate.CollisionLeft(currentPiece, pieceX, gridInitialX) && !Calculate.CollisionGridLeft(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceX -= 30;
                                    moveTimer = moveSpeed;
                                }
                            }

                            if (Raylib.IsKeyDown(KeyboardKey.Down) && currentFrame%4 == 0)
                            {
                                if (!Calculate.CollisionDown(currentPiece, pieceY, gridInitialY) && !Calculate.CollisionGridDown(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceY += 30;
                                }
                            }

                            if (currentPiece == shapeNull)
                            {
                                currentPiece = shapes[new Random().Next(0, shapes.Length)];
                            }

                            else
                            {
                                Draw.DrawPiece(currentPiece, pieceX, pieceY);
                            }

                        }
                        break;
                    }
                
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}