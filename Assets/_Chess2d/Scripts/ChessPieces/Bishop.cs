using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
     Vector2Int[] directions = {
            new Vector2Int(1, 1),   // Top-right
            new Vector2Int(-1, 1),  // Top-left
            new Vector2Int(1, -1),  // Bottom-right
            new Vector2Int(-1, -1)  // Bottom-left
        };

    public override List<(int, int)> GetWays()
    {
        List <(int, int)> validMoves = new List<(int,int)>();
        foreach (var direction in directions)
        {
            Vector2Int newPosition = CurrentPosition;

            while (true)
            {
                newPosition += direction;

                if (!IsInsideBoard(newPosition.x, newPosition.y))
                    break;

                var cell = BoardManager.GetCell(newPosition.x, newPosition.y);

                if (cell.HasEnemy(Skin))
                {
                    validMoves.Add((newPosition.x, newPosition.y));
                    break;
                }
                else if (cell.IsEmpty())
                {
                    validMoves.Add((newPosition.x, newPosition.y));
                }
                else
                    break;
            }
        }

        return validMoves;
    }

    public override void Move(Vector2Int newPos)
    {
        this.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        SetPosition(newPos);
    }
}
