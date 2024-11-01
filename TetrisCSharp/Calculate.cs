

namespace Raylib_cs
{
    class Calculate
    {
        
    public static bool CollisionRight(int[ , ] piece, int pieceX, int gridInitialX)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (pieceX + i*30 >= gridInitialX + 30*9)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool CollisionLeft(int[ , ] piece, int pieceX, int gridInitialX)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (pieceX + i*30 <= gridInitialX)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool CollisionDown(int[ , ] piece, int pieceY, int gridInitialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (pieceY + j*30 >= gridInitialY + 30*19)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool CollisionGridRight(int[ , ] piece, int pieceX, int pieceY, int[ , ] grid, int gridInitialX, int gridInitialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (grid[(pieceX - gridInitialX)/30 + i + 1, (pieceY - gridInitialY)/30 + j] == 1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool CollisionGridLeft(int[ , ] piece, int pieceX, int pieceY, int[ , ] grid, int gridInitialX, int gridInitialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (grid[(pieceX - gridInitialX)/30 + i - 1, (pieceY - gridInitialY)/30 + j] == 1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool CollisionGridDown(int[ , ] piece, int pieceX, int pieceY, int[ , ] grid, int gridInitialX, int gridInitialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (grid[(pieceX - gridInitialX)/30 + i, (pieceY - gridInitialY)/30 + j + 1] == 1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static void CheckLines(int[ , ] grid, ref int score, ref int gameSpeed)
    {
        for (int i = 0; i < 20; i++)
        {
            bool fullLine = true;
            for (int j = 0; j < 10; j++)
            {
                if (grid[j, i] == 0)
                {
                    fullLine = false;
                }
            }
            if (fullLine)
            {
                for (int j = 0; j < 10; j++)
                {
                    grid[j, i] = 0;
                }
                for (int k = i; k > 0; k--)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        grid[j, k] = grid[j, k - 1];
                    }
                }
                score += 100;
                gameSpeed -= 1;
            }
        }
    }

    }
}