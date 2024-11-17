namespace Raylib_cs
{
    class Draw
    {

    public static void DrawGrid(int[ , ] array, int initialX, int initialY)
        {
            Color trueColor;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 1)
                    {
                        trueColor = Color.Purple;
                    }
                    else
                    {
                        trueColor = Color.White;
                    }
                    Raylib.DrawRectangle(initialX + i * 30, initialY + j * 30, 30, 30, trueColor);
                }
            }  
        }

    public static void DrawFrame(int initialX, int initialY)
    {
        Raylib.DrawRectangle(initialX - 30, initialY - 30, 30, 30*22, Color.LightGray);
        Raylib.DrawRectangle(initialX + 30*10, initialY - 30, 30, 30*22, Color.LightGray);
        Raylib.DrawRectangle(initialX - 30, initialY - 30, 30*12, 30, Color.LightGray);
        Raylib.DrawRectangle(initialX - 30, initialY + 30*20, 30*12, 30, Color.LightGray);
    }   

    public static void DrawNextPieceFrame(int initialX, int initialY)
    {
        Raylib.DrawRectangle(initialX + 30*12 + 30, initialY + 15, 30*7, 30*7, Color.LightGray);
        Raylib.DrawRectangle(initialX + 30*14 - 15, initialY + 30, 30*6, 30*6, Color.White);
        Raylib.DrawText("Next piece", initialX + 30*12 + 60, initialY - 30, 30, Color.Black);
    }

    public static void DrawNextPiece(int[ , ] piece, int initialX, int initialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (piece.GetLength(0) == 2)
                    {
                        Raylib.DrawRectangle(initialX + 30*15 + i * 30 + 15, initialY + 30*3 + j * 30, 30, 30, Color.Purple);
                    }
                    else if (piece.GetLength(0) == 4)
                    {
                        Raylib.DrawRectangle(initialX + 30*15 + i * 30 - 15, initialY + 30*3 + j * 30 - 15, 30, 30, Color.Purple);
                    }
                    else
                    {
                        Raylib.DrawRectangle(initialX + 30*15 + i * 30, initialY + 30*3 + j * 30, 30, 30, Color.Purple);
                    }
                }
            }
        }
    }

    public static void DrawHeldPieceFrame(int initialX, int initialY)
    {
        Raylib.DrawRectangle(initialX - 30*10, initialY + 15, 30*7, 30*7, Color.LightGray);
        Raylib.DrawRectangle(initialX - 30*9 - 15, initialY + 30, 30*6, 30*6, Color.White);
        Raylib.DrawText("Held piece", initialX - 30*9, initialY - 30, 30, Color.Black);
    }

    public static void DrawHeldPiece(int[ , ] piece, int initialX, int initialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (piece.GetLength(0) == 2)
                    {
                        Raylib.DrawRectangle(initialX - 30*8 + i * 30 + 15, initialY + 30*3 + j * 30, 30, 30, Color.Purple);
                    }
                    else if (piece.GetLength(0) == 4)
                    {
                        Raylib.DrawRectangle(initialX - 30*8 + i * 30 - 15, initialY + 30*3 + j * 30 - 15, 30, 30, Color.Purple);
                    }
                    else
                    {
                        Raylib.DrawRectangle(initialX - 30*8 + i * 30, initialY + 30*3 + j * 30, 30, 30, Color.Purple);
                    }
                }
            }
        }
    }

    public static void DrawScore(int score, int initialX, int initialY)
    {
        Raylib.DrawRectangle(initialX + 30*13, initialY + 30*10 + 15, 30*7, 30*3, Color.LightGray);
        Raylib.DrawRectangle(initialX + 30*13 + 15, initialY + 30*10 + 30, 30*6, 60, Color.White);
        Raylib.DrawText("Score", initialX + 30*13 + 30, initialY + 30*9, 30, Color.Black);
        Raylib.DrawText("" + score, initialX + 30*13 + 35, initialY + 30*10 + 45, 30, Color.Black);
    }

    public static void DrawCountdown(int countdown, int initialX, int initialY)
    {
        Raylib.DrawText("" + countdown, initialX, initialY, 70, Color.Black);
    }

    public static void DrawPiece(int[ , ] piece, int initialX, int initialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    Raylib.DrawRectangle(initialX + i * 30, initialY + j * 30, 30, 30, Color.Purple);
                }
            }
        }
    }

    }
}