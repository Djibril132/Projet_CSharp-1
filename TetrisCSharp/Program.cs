


namespace Raylib_cs
{
    class TetrisGame
    {
        static void Main()
        {
            // Screen variables
            const int screenWidth = 1500;
            const int screenHeight = 800;
            int currentFrame = 0;

            // Main variables 
            string currentScene = "menu";
            int[ , ] grid = new int[10, 20];
            int gridInitialX = screenWidth/2 - 30*5;
            int gridInitialY = 100;

            // Tetris pieces
            int[ , ] shapeI = {{0, 0, 0, 0}, {1, 1, 1, 1}, {0, 0, 0, 0}, {0, 0, 0, 0}};
            int[ , ] shapeJ = {{1, 0, 0}, {1, 1, 1}, {0, 0, 0}};
            int[ , ] shapeL = {{0, 0, 1}, {1, 1, 1}, {0, 0, 0}};
            int[ , ] shapeO = {{1, 1}, {1, 1}};
            int[ , ] shapeS = {{0, 1, 1}, {1, 1, 0}, {0, 0, 0}};
            int[ , ] shapeT = {{0, 1, 0}, {1, 1, 1}, {0, 0, 0}};
            int[ , ] shapeZ = {{1, 1, 0}, {0, 1, 1}, {0, 0, 0}};

            // Array of pieces to randomize them
            int[][ , ] shapes = { shapeO, shapeI, shapeT, shapeL, shapeJ, shapeZ, shapeS };
            string[] shapesNames = { "O", "I", "T", "L", "J", "Z", "S" };

            // Randomizer
            int randomSelector = new Random().Next(0, shapes.Length);

            // Randomized piece
            int[ , ] currentPiece = (int[ , ])shapes[randomSelector].Clone();
            string currentPieceName = shapesNames[randomSelector].ToString();
        
            // Base rotation copy of current piece to display in "next piece" and "held piece" frames
            int[ , ] baseRotationCurrentPiece = (int[ , ])currentPiece.Clone();

            // Next piece randomizer
            int randomSelectorNext = new Random().Next(0, shapes.Length);

            // Randomized next piece
            int[ , ] nextPiece = (int[ , ])shapes[randomSelectorNext].Clone();
            string nextPieceName = shapesNames[randomSelectorNext].ToString();

            // Dictionaries for rotations
            int[ ,, ] rotationClockWiseDictionaryI = {{{-2, 0}, {1, 0}, {-2, -1}, {1, 2}}, {{-1, 0}, {2, 0}, {-1, 2}, {2, -1}}, {{2, 0}, {-1, 0}, {2, 1}, {-1, -2}}, {{1, 0}, {-2, 0}, {1, -2}, {-2, 1}}};
            int[ ,, ] rotationCounterClockWiseDictionaryI = {{{2, 0}, {-1, 0}, {2, 1}, {-1, -2}}, {{1, 0}, {-2, 0}, {1, -2}, {-2, 1}}, {{-2, 0}, {1, 0}, {-2, -1}, {1, 2}}, {{-1, 0} , {2, 0}, {-1, 2}, {2, -1}}};

            int[ ,, ] rotationClockWiseDictionaryRest = {{{-1, 0}, {-1, 1}, {0, -2}, {-1, -2}}, {{1, 0}, {1, -1}, {0, 2}, {1, 2}}, {{1, 0}, {1, 1}, {0, -2}, {1, -2}}, {{-1, 0}, {-1, -1}, {0, 2}, {-1, 2}}};
            int[ ,, ] rotationCounterClockWiseDictionaryRest = {{{1, 0}, {1, -1}, {0, 2}, {1, 2}}, {{-1, 0}, {-1, 1}, {0, -2}, {-1, -2}}, {{-1, 0}, {-1, -1}, {0, 2}, {-1, 2}}, {{1, 0}, {1, 1}, {0, -2}, {1, -2}}};

            // Held piece variables
            int[ , ] heldPiece = new int[2, 2];
            bool nothingHeld = true;
            bool heldPieceAvailable = true;

            // Initial piece position
            int pieceX = gridInitialX + 30*4;
            int pieceY = gridInitialY;

            // Rotation variable
            int currentRotation = 0;

            // Game variables
            bool gameStarted = false;
            int score = 0;
            int gameSpeed = 50;
            int timeUntilLocked = gameSpeed;
            int moveSpeed = 4;

            // Window setup
            Raylib.InitWindow(screenWidth, screenHeight, "Tetris");
            Raylib.SetTargetFPS(60);
            

            while (!Raylib.WindowShouldClose())
            {
                // Scene management
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

                    case "gameover":
                        if (Raylib.IsKeyPressed(KeyboardKey.Space))
                        {
                            currentScene = "game";
                            grid = new int[10, 20];
                            currentPiece = (int[ , ])shapes[new Random().Next(0, shapes.Length)].Clone();
                            baseRotationCurrentPiece = (int[ , ])currentPiece.Clone();
                            nextPiece = (int[ , ])shapes[new Random().Next(0, shapes.Length)].Clone();
                            pieceX = gridInitialX + 30*4;
                            pieceY = gridInitialY;
                            currentRotation = 0;
                            score = 0;
                            gameSpeed = 50;
                            timeUntilLocked = gameSpeed;
                            gameStarted = false;
                        }
                        break;
                }



                Raylib.BeginDrawing();
                
                switch (currentScene)
                {
                    case "menu":
                        // Main menu text
                        Raylib.ClearBackground(Color.Purple);
                        Raylib.DrawText("Tetris", screenWidth/2 - (Raylib.MeasureText("Tetris", 60)/2), 250, 60, Color.Black);
                        Raylib.DrawText("Press Space to start", screenWidth/2 - (Raylib.MeasureText("Press Space to start", 30)/2), 500, 30, Color.Black);
                        break;

                    case "game":
                        // Game display
                        Raylib.ClearBackground(Color.Purple);
                        Draw.DrawGrid(grid, gridInitialX, gridInitialY);
                        Draw.DrawFrame(gridInitialX, gridInitialY);
                        Draw.DrawPiece(currentPiece, pieceX, pieceY);

                        Draw.DrawNextPieceFrame(gridInitialX, gridInitialY);
                        Draw.DrawNextPiece(nextPiece, gridInitialX, gridInitialY);

                        Draw.DrawHeldPieceFrame(gridInitialX, gridInitialY);
                        Draw.DrawHeldPiece(heldPiece, gridInitialX, gridInitialY);

                        Draw.DrawScore(score, gridInitialX, gridInitialY);
                        
                        currentFrame++;

                        // Countdown before game starts
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

                            // Lower the piece every 'gameSpeed' frames
                            if (currentFrame%gameSpeed == 0)
                            {
                                if (!Calculate.CollisionDown(currentPiece, pieceY, gridInitialY) && !Calculate.CollisionGridDown(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceY += 30;
                                }
                            }

                            // Decrease the time until the piece locks
                            if (!(!Calculate.CollisionDown(currentPiece, pieceY, gridInitialY) && !Calculate.CollisionGridDown(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY)))
                            {
                                timeUntilLocked--;
                            }

                            // Lock the piece instantly if space is pressed
                            if (Raylib.IsKeyPressed(KeyboardKey.Space))
                            {
                                while (!Calculate.CollisionDown(currentPiece, pieceY, gridInitialY) && !Calculate.CollisionGridDown(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceY += 30;
                                }
                                timeUntilLocked = 0;
                            }

                            // Lock the piece, draw it and initialize all variables for a new one
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

                                currentPiece = nextPiece;
                                currentPieceName = nextPieceName;
                                baseRotationCurrentPiece = (int[ , ])currentPiece.Clone();
                                randomSelectorNext = new Random().Next(0, shapes.Length);
                                nextPiece = (int[ , ])shapes[randomSelectorNext].Clone();
                                nextPieceName = shapesNames[randomSelectorNext].ToString();
                                pieceX = gridInitialX + 30*4;
                                pieceY = gridInitialY;
                                timeUntilLocked = gameSpeed;
                                heldPieceAvailable = true;
                                currentRotation = 0;

                                if (Calculate.IsGameFinished(grid))
                                {
                                    currentScene = "gameover";
                                }
                            }

                            // Remove lines and update score accordingly
                            Calculate.CheckLines(grid, ref score, ref gameSpeed);
                            Draw.DrawScore(score, gridInitialX, gridInitialY);

                            // Moves the piece to the right
                            if (Raylib.IsKeyDown(KeyboardKey.Right) && currentFrame%moveSpeed == 0)
                            {
                                if (!Calculate.CollisionRight(currentPiece, pieceX, gridInitialX) && !Calculate.CollisionGridRight(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceX += 30;
                                }
                            }

                            // Moves the piece to the left
                            if (Raylib.IsKeyDown(KeyboardKey.Left) && currentFrame%moveSpeed == 0)
                            {
                                if (!Calculate.CollisionLeft(currentPiece, pieceX, gridInitialX) && !Calculate.CollisionGridLeft(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceX -= 30;
                                }
                            }

                            // Moves the piece down
                            if (Raylib.IsKeyDown(KeyboardKey.Down) && currentFrame%4 == 0)
                            {
                                if (!Calculate.CollisionDown(currentPiece, pieceY, gridInitialY) && !Calculate.CollisionGridDown(currentPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                                {
                                    pieceY += 30;
                                }
                            }

                            // Rotates the piece right
                            if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Up))
                            {
                                Calculate.RotatePieceRight(currentPieceName, ref currentPiece, ref pieceX, ref pieceY, grid, gridInitialX, gridInitialY, ref currentRotation, rotationClockWiseDictionaryI, rotationClockWiseDictionaryRest);
                            }

                            // Rotates the piece left
                            if (Raylib.IsKeyPressed(KeyboardKey.S))
                            {
                                Calculate.RotatePieceLeft(currentPieceName, ref currentPiece, ref pieceX, ref pieceY, grid, gridInitialX, gridInitialY, ref currentRotation, rotationCounterClockWiseDictionaryI, rotationCounterClockWiseDictionaryRest);
                            }

                            // Holds the piece
                            if (Raylib.IsKeyPressed(KeyboardKey.C))
                            {
                                if (nothingHeld)
                                {
                                    heldPiece = baseRotationCurrentPiece;
                                    currentPiece = nextPiece;
                                    baseRotationCurrentPiece = (int[ , ])currentPiece.Clone();
                                    nextPiece = shapes[new Random().Next(0, shapes.Length)];
                                    pieceX = gridInitialX + 30*3;
                                    pieceY = gridInitialY;
                                    timeUntilLocked = gameSpeed;
                                    heldPieceAvailable = true;
                                    nothingHeld = false;
                                    currentRotation = 0;
                                }
                                else
                                {
                                    if (heldPieceAvailable)
                                    {
                                        int[ , ] temp = heldPiece;
                                        heldPiece = baseRotationCurrentPiece;
                                        currentPiece = temp;
                                        baseRotationCurrentPiece = (int[ , ])currentPiece.Clone();
                                        pieceX = gridInitialX + 30*3;
                                        pieceY = gridInitialY;
                                        timeUntilLocked = gameSpeed;
                                        heldPieceAvailable = false;
                                        currentRotation = 0;
                                    }
                                }
                            }
                        }
                        break;
                    case "gameover":
                        // Game over text
                        Raylib.ClearBackground(Color.Purple);
                        Raylib.DrawText("Game Over", screenWidth/2 - (Raylib.MeasureText("Game Over", 60)/2), 250, 60, Color.Black);
                        Raylib.DrawText("Score: " + score, screenWidth/2 - (Raylib.MeasureText("Score: " + score, 30)/2), 400, 30, Color.Black);
                        Raylib.DrawText("Press Space to restart", screenWidth/2 - (Raylib.MeasureText("Press Space to restart", 30)/2), 500, 30, Color.Black);
                        break;
                    }
                
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}