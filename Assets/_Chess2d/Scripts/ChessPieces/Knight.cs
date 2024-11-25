using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    Vector2Int[] directions = {
            new Vector2Int(2, 1),  new Vector2Int(2, -1),  // Two right, one up/down
            new Vector2Int(-2, 1), new Vector2Int(-2, -1), // Two left, one up/down
            new Vector2Int(1, 2),  new Vector2Int(1, -2),  // One right, two up/down
            new Vector2Int(-1, 2), new Vector2Int(-1, -2)  // One left, two up/down
        };

    public override List<(int, int)> GetWays()
    {
        List<(int, int)> validMoves = new List<(int, int)>();
        foreach (var move in directions)
        {
            Vector2Int newPosition = CurrentPosition + move;

            if (IsInsideBoard(newPosition.x, newPosition.y))
            {
                var cell = BoardManager.GetCell(newPosition.x, newPosition.y);

                if(cell.HasEnemy(Skin) || cell.IsEmpty())
                {
                    validMoves.Add((newPosition.x, newPosition.y));
                }
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
