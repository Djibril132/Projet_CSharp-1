

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
                if (gameSpeed > 20)
                {
                    gameSpeed -= 1;
                }
            }
        }
    }

    public static bool AllCollisions(int[ , ] piece, int pieceX, int pieceY, int[ , ] grid, int gridInitialX, int gridInitialY)
    {
        if (CollisionRight(piece, pieceX, gridInitialX) || CollisionLeft(piece, pieceX, gridInitialX) || CollisionDown(piece, pieceY, gridInitialY) || CollisionGridRight(piece, pieceX, pieceY, grid, gridInitialX, gridInitialY) || CollisionGridLeft(piece, pieceX, pieceY, grid, gridInitialX, gridInitialY) || CollisionGridDown(piece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
        {
            return true;
        }
        return false;
    }

    public static bool RotationVerification(int[,] piece, int pieceX, int pieceY, int[,] grid, int gridInitialX, int gridInitialY)
    {
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                if (piece[j, i] == 1)
                {
                    if (pieceX + i * 30 < gridInitialX || pieceX + i * 30 >= gridInitialX + 30 * 11 || pieceY + j * 30 < gridInitialY || pieceY + j * 30 >= gridInitialY + 30 * 20)
                    {
                        return false;
                    }
                }
            }
        }
        return !AllCollisions(piece, pieceX, pieceY, grid, gridInitialX, gridInitialY);
    }

    public static void RotatePieceRight(string pieceName, ref int[ , ] piece,ref int pieceX,ref int pieceY, int[ , ] grid, int gridInitialX, int gridInitialY, ref int rotation, int[ ,, ] dictionaryI, int[ ,, ] dictionaryRest)
    {
        int[] nextTry = new int[2];
        int[ , ] tempPiece = new int[piece.GetLength(1), piece.GetLength(0)];
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                tempPiece[j, piece.GetLength(0) - 1 - i] = piece[i, j];
            }
        }
        if (pieceX + tempPiece.GetLength(0)*30 <= gridInitialX + 30*10 && pieceY + tempPiece.GetLength(1)*30 <= gridInitialY + 30*20)
        {
            if (pieceName == "O")
            {
                if (RotationVerification(tempPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                {
                    piece = tempPiece;
                    rotation = (rotation + 90) % 360;
                }
            }
            else if (pieceName == "I")
            {
                if (RotationVerification(tempPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                {
                    piece = tempPiece;
                    rotation = (rotation + 90) % 360;
                }
                else {
                    for (int i = 0; i < dictionaryI.GetLength(1); i++)
                    {
                        nextTry[0] = dictionaryI[rotation/90, i, 0];
                        nextTry[1] = dictionaryI[rotation/90, i, 1];
                        if (RotationVerification(tempPiece, pieceX + nextTry[0]*30, pieceY + nextTry[1]*30, grid, gridInitialX, gridInitialY))
                        {
                            piece = tempPiece;
                            pieceX += nextTry[0]*30;
                            pieceY += nextTry[1]*30;
                            rotation = (rotation + 90) % 360;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (RotationVerification(tempPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                {
                    piece = tempPiece;
                    rotation = (rotation + 90) % 360;
                }
                else {
                    for (int i = 0; i < dictionaryRest.GetLength(1); i++)
                    {
                        nextTry[0] = dictionaryRest[rotation/90, i, 0];
                        nextTry[1] = dictionaryRest[rotation/90, i, 1];
                        if (RotationVerification(tempPiece, pieceX + nextTry[0]*30, pieceY + nextTry[1]*30, grid, gridInitialX, gridInitialY))
                        {
                            piece = tempPiece;
                            pieceX += nextTry[0]*30;
                            pieceY += nextTry[1]*30;
                            rotation = (rotation + 90) % 360;
                            break;
                        }
                    }
                }
            }
        }
    }

    public static void RotatePieceLeft(string pieceName, ref int[ , ] piece,ref int pieceX,ref int pieceY, int[ , ] grid, int gridInitialX, int gridInitialY, ref int rotation, int[ ,, ] dictionaryI, int[ ,, ] dictionaryRest)
    {
        int[] nextTry = new int[2];
        int[ , ] tempPiece = new int[piece.GetLength(1), piece.GetLength(0)];
        for (int i = 0; i < piece.GetLength(0); i++)
        {
            for (int j = 0; j < piece.GetLength(1); j++)
            {
                tempPiece[piece.GetLength(1) - 1 - j, i] = piece[i, j];
            }
        }
        if (pieceX + tempPiece.GetLength(0)*30 <= gridInitialX + 30*10 && pieceY + tempPiece.GetLength(1)*30 <= gridInitialY + 30*20)
        {
            if (pieceName == "O")
            {
                if (RotationVerification(tempPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                {
                    piece = tempPiece;
                    if (rotation == 0)
                    {
                        rotation = 270;
                    }
                    else
                    {
                        rotation = (rotation - 90) % 360;
                    }
                }
            }
            else if (pieceName == "I")
            {
                if (RotationVerification(tempPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                {
                    piece = tempPiece;
                    if (rotation == 0)
                    {
                        rotation = 270;
                    }
                    else
                    {
                        rotation = (rotation - 90) % 360;
                    }
                }
                else {
                    for (int i = 0; i < dictionaryI.GetLength(1); i++)
                    {
                        nextTry[0] = dictionaryI[rotation/90, i, 0];
                        nextTry[1] = dictionaryI[rotation/90, i, 1];
                        if (RotationVerification(tempPiece, pieceX + nextTry[0]*30, pieceY + nextTry[1]*30, grid, gridInitialX, gridInitialY))
                        {
                            piece = tempPiece;
                            pieceX += nextTry[0]*30;
                            pieceY += nextTry[1]*30;
                            if (rotation == 0)
                            {
                                rotation = 270;
                            }
                            else
                            {
                                rotation = (rotation - 90) % 360;
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                if (RotationVerification(tempPiece, pieceX, pieceY, grid, gridInitialX, gridInitialY))
                {
                    piece = tempPiece;
                    if (rotation == 0)
                    {
                        rotation = 270;
                    }
                    else
                    {
                        rotation = (rotation - 90) % 360;
                    }
                }
                else {
                    for (int i = 0; i < dictionaryRest.GetLength(1); i++)
                    {
                        nextTry[0] = dictionaryRest[rotation/90, i, 0];
                        nextTry[1] = dictionaryRest[rotation/90, i, 1];
                        if (RotationVerification(tempPiece, pieceX + nextTry[0]*30, pieceY + nextTry[1]*30, grid, gridInitialX, gridInitialY))
                        {
                            piece = tempPiece;
                            pieceX += nextTry[0]*30;
                            pieceY += nextTry[1]*30;
                            if (rotation == 0)
                            {
                                rotation = 270;
                            }
                            else
                            {
                                rotation = (rotation - 90) % 360;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    }
}